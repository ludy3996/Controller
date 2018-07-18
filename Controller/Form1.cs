using Microsoft.CognitiveServices.Speech;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTS;

namespace Controller
{
    public partial class Controller : Form
    {
        #region 全局变量

        private SpeechRecognizer recognizer;
        // 变量为记录是否在录音
        private bool isRecording = false;
        // 保存每一行输入的意图
        private List<string> strList = new List<string>();
        // 数字表示每个颜色
        private int color = 0;
        // true 表示灯在开着
        private bool lightBedroom = false;
        private bool lightKitchen = false;
        // true 表示用TTS输出
        private bool sound = true;
        private bool sound_Female = true;
        private bool sound_Mandarin = true;

        #endregion

        #region Controller构造函数

        /// <summary>
        /// 通过构造函数进行变量的初始化
        /// </summary>
        public Controller()
        {
            InitializeComponent();
            button.Text = "开始";
            textBox_Error.Visible = false;
            CloseBedroomLight();
            CloseKitchenLight();
            checkBox_Sound.Checked = true;
            checkBox_Female.Checked = true;
            checkBox_Male.Checked = false;
            checkBox_Cn.Checked = true;
            checkBox_Hk.Checked = false;
        }

        #endregion

        #region 语言转换文本处理

        /// <summary>
        /// 通过微软的语言服务，而且进行初始化
        /// 然后通过获得的密钥可以执行操作
        /// </summary>
        private void Controller_Load(object sender, EventArgs e)
        {
            try
            {
                // 初始化语音服务SDK并启动识别器，进行语音转文本
                // 密钥和区域可在 https://azure.microsoft.com/zh-cn/try/cognitive-services/my-apis/?api=speech-services 中找到
                // 密钥示例: 5ee7ba6869f44321a40751967accf7a9
                // 区域示例: westus
                SpeechFactory speechFactory = SpeechFactory.FromSubscription("056cdb2e8c34477fb059ede9b1c8490a", "westus");
                // 识别中文
                recognizer = speechFactory.CreateSpeechRecognizer("zh-CN");
                // 识别过程中的中间结果
                recognizer.IntermediateResultReceived += Recognizer_IntermediateResultReceived;
                // 识别的最终结果
                recognizer.FinalResultReceived += Recognizer_FinalResultReceived;
                // 出错时的处理
                recognizer.RecognitionErrorRaised += Recognizer_RecognitionErrorRaised;
            }
            catch (Exception ex)
            {
                if (ex is System.TypeInitializationException)
                {
                    MakesureRunInUI(() =>
                    {
                        textBox_Error.AppendText("语音SDK不支持Any CPU, 请更改为x64" + "\r\n");
                    });
                }
                else
                {
                    MakesureRunInUI(() =>
                    {
                        textBox_Error.AppendText("初始化出错，请确认麦克风工作正常" + "\r\n" + "已降级到文本语言理解模式" + "\r\n");
                    });
                }
                textBox_Error.Visible = true;
                button.Visible = false;
            }
        }

        /// <summary>
        /// 识别过程中的中间结果
        /// </summary>
        private void Recognizer_IntermediateResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("中间结果: " + e.Result.Text);
            }
        }

        /// <summary>
        /// 识别的最终结果
        /// </summary>
        private void Recognizer_FinalResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("最终结果: " + e.Result.Text);
                ProcessSttResult(e.Result.Text);
            }
        }

        /// <summary>
        /// 出错时的处理
        /// </summary>
        private void Recognizer_RecognitionErrorRaised(object sender, RecognitionErrorEventArgs e)
        {
            Log("错误: " + e.FailureReason);
        }

        #endregion
        
        #region 意图处理（利用LUIS）

        /// <summary>
        /// 通过Luis获得意图，并且处理操作
        /// 即可以打开关闭对应的灯
        /// 也可以处理换颜色灯的操作
        /// 还有上下文和文本转换语言的处理
        /// </summary>
        /// <param name="text">输入的文本</param>
        private async void ProcessSttResult(string text)
        {
            if (strList.Count > 3)
                strList.RemoveAt(3);
            // 调用语言理解服务取得用户意图
            string intent = await GetLuisResult(text);
            // 按照意图控制灯
            // 考虑可读性和速度方面，改成switch case
            if (!string.IsNullOrEmpty(intent))
            {
                switch(intent)
                {
                    case "Bedroom_On":  //打开卧室的灯
                        OpenBedroomLight();
                        TTS("打开了卧室的灯");
                        strList.Clear();
                        break;
                    case "Kitchen_On":  //打开厨房的灯
                        OpenKitchenLight();
                        TTS("打开了厨房的灯");
                        strList.Clear();
                        break;
                    case "Bedroom_Off": //关闭卧室的灯
                        CloseBedroomLight();
                        TTS("关闭了卧室的灯");
                        strList.Clear();
                        break;
                    case "Kitchen_Off": //关闭厨房的灯
                        TTS("关闭了厨房的灯");
                        CloseKitchenLight();
                        strList.Clear();
                        break;
                    case "All_On":  //打开所有的灯
                        OpenBedroomLight();
                        OpenKitchenLight();
                        TTS("打开了所有的灯");
                        strList.Clear();
                        break;
                    case "All_Off": //关闭所有的灯
                        CloseBedroomLight();
                        CloseKitchenLight();
                        TTS("关闭了所有的灯");
                        strList.Clear();
                        break;
                    case "BedroomOff_KitchenOn":    //打开厨房的灯，关闭卧室的灯
                        CloseBedroomLight();
                        OpenKitchenLight();
                        TTS("关闭了卧室的灯，打开了厨房的灯");
                        strList.Clear();
                        break;
                    case "BedroomOn_KitchenOff":    //打开卧室的灯，关闭厨房的灯
                        OpenBedroomLight();
                        CloseKitchenLight();
                        TTS("打开了卧室的灯，关闭了厨房的灯");
                        strList.Clear();
                        break;
                    case "OpenWhat":    //缺信息通过识别下文的过程才能进行操作
                        Log("打开哪里的灯？");
                        TTS("打开哪里的灯？");
                        strList.Clear();
                        break;
                    case "CloseWhat":   //缺信息通过识别下文的过程才能进行操作
                        Log("关闭哪里的灯？");
                        TTS("关闭哪里的灯？");
                        strList.Clear();
                        break;
                    case "BedroomAlso":
                        if(strList.Contains("Kitchen_On"))
                        {
                            OpenBedroomLight();
                            TTS("卧室的灯也打开了");
                        }
                        else if (strList.Contains("Kitchen_Off"))
                        {
                            CloseBedroomLight();
                            TTS("卧室的灯也关闭了");
                        }
                        strList.Clear();
                        break;
                    case "KitchenAlso":
                        if (strList.Contains("Bedroom_On"))
                        {
                            OpenKitchenLight();
                            TTS("厨房的灯也打开了");
                        }
                        else if (strList.Contains("Bedroom_Off"))
                        {
                            CloseKitchenLight();
                            TTS("厨房的灯也关闭了");
                        }
                        strList.Clear();
                        break;
                    case "All": ////缺信息通过识别上文的过程才能进行操作
                        foreach (string str in strList)
                        {
                            if (str.Equals("OpenWhat"))
                            {
                                OpenBedroomLight();
                                OpenKitchenLight();
                                TTS("打开了所有的灯");
                                strList.Clear();
                                break;
                            }
                            else if (str.Equals("CloseWhat"))
                            {
                                CloseBedroomLight();
                                CloseKitchenLight();
                                TTS("关闭了所有的灯");
                                strList.Clear();
                                break;
                            }
                            else if (!str.Equals("None"))
                                break;
                        }
                        break;
                    case "Bedroom": //缺信息通过识别上文的过程才能进行操作
                        foreach (string str in strList)
                        {
                            if (str.Equals("OpenWhat"))
                            {
                                OpenBedroomLight();
                                TTS("打开了卧室的灯");
                                strList.Clear();
                                break;
                            }
                            else if (str.Equals("CloseWhat"))
                            {
                                CloseBedroomLight();
                                TTS("关闭了卧室的灯");
                                strList.Clear();
                                break;
                            }
                            else if (!str.Equals("None"))
                                break;
                        }
                        break;
                    case "Kitchen": ////缺信息通过识别上文的过程才能进行操作
                        foreach (string str in strList)
                        {
                            if (str.Equals("OpenWhat"))
                            {
                                OpenKitchenLight();
                                TTS("打开了厨房的灯");
                                strList.Clear();
                                break;
                            }
                            else if (str.Equals("CloseWhat"))
                            {
                                CloseKitchenLight();
                                TTS("关闭了厨房的灯");
                                strList.Clear();
                                break;
                            }
                            else if (!str.Equals("None"))
                                break;
                        }
                        break;
                    case "ChangeColor": //随机换灯的颜色
                        Random rand = new Random();
                        int temp;
                        do
                        {
                            temp = rand.Next(0,4);
                        } while (color == temp);
                        ChangeColor(temp);
                        break;
                    case "Yellow":  //把灯的颜色换成黄色
                        ChangeColor(0);
                        break;
                    case "Red": //把灯的颜色换成红色
                        ChangeColor(1);
                        break;
                    case "Blue":    //把灯的颜色换成蓝色
                        ChangeColor(2);
                        break;
                    case "Green":   //把灯的颜色换成绿色
                        ChangeColor(3);
                        break;
                    case "Hello":   //跟机器打招呼
                        TTS("你好，我叫Anna。");
                        break;
                    case "None":    //不能识别
                        TTS("请再说一遍");
                        break;
                }
                strList.Insert(0,intent);
            }
        }
        
        /// <summary>
        /// 调用语言理解服务取得用户意图
        /// </summary>
        /// <param name="text">输入的文本</param>
        /// <returns>通过LUIS里的功能得到的意图</returns>
        private async Task<string> GetLuisResult(string text)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // LUIS 终结点地址, 示例: https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/102f6255-0c32-4f36-9c79-fe12fea4d6c4?subscription-key=9004421650254a74876cf3c888b1d11f&verbose=true&timezoneOffset=0&q=
                // 可在 https://www.luis.ai 中进入app右上角publish中找到
                string luisEndpoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/fda16211-ae4d-4d68-993d-38c0e610f326?subscription-key=09995e6c1ca14d788aed63cac9ef0612&verbose=true&timezoneOffset=480&q=";
                string luisJson = await httpClient.GetStringAsync(luisEndpoint + text);
                try
                {
                    dynamic result = JsonConvert.DeserializeObject<dynamic>(luisJson);
                    string intent = (string)result.topScoringIntent.intent;
                    double score = (double)result.topScoringIntent.score;
                    Log("意图: " + intent + "\r\n得分: " + score + "\r\n");
                    return intent;
                }
                catch (Exception ex)
                {
                    Log(ex.Message);
                    return null;
                }
            }
        }

        #endregion

        #region TTS处理

        /// <summary> 
        /// 此方法在从服务返回的音频后调用
        /// 然后, 它将尝试播放该音频文件
        /// 请注意, 如果输出音频格式不是 pcm 编码的, 则回放将失败
        /// </summary> 
        private static void PlayAudio(object sender, GenericEventArgs<Stream> args)
        {
            Console.WriteLine(args.EventData);
            // 为了使 SoundPlayer 能够播放 wav 文件, 它必须在 PCM 中进行编码
            // 使用输出音频格式的音频输出格式. Riff16Khz16BitMonoPcm 就是
            SoundPlayer player = new SoundPlayer(args.EventData);
            player.PlaySync();
            args.EventData.Dispose();
        }

        /// <summary> 
        /// 在 TTS 请求失败时处理错误
        /// </summary> 
        private static void ErrorHandler(object sender, GenericEventArgs<Exception> e)
        {
            Console.WriteLine("Unable to complete the TTS request: [{0}]", e.ToString());
        }

        /// <summary>
        /// 利用微软的TTS功能实例，
        /// 通过TTS.cs里的代码，用语言输出text字符串er
        /// </summary>
        /// <param name="text">要语言输出的字符串儿</param>
        private void TTS(string text)
        {
            if (sound)
            {
                var cortana = new Synthesize();
                cortana.OnAudioAvailable += PlayAudio;
                cortana.OnError += ErrorHandler;
                cortana.Speak(CancellationToken.None, new Synthesize.InputOptions(text,sound_Female,sound_Mandarin)).Wait();
            }
        }

        #endregion
        
        #region 界面操作

        /// <summary>
        /// 在textBox上输出 message 并换行
        /// </summary>
        private void Log(string message)
        {
            MakesureRunInUI(() =>
            {
                textBox.AppendText(message + "\r\n");
            });
        }

        /// <summary>
        /// 更换灯的颜色
        /// 查看每个灯的状态，进行操作
        /// 并且利用TTS函数输出结果
        /// </summary>
        /// <param name="color">0-3的数字表示每个颜色</param>
        private void ChangeColor(int color)
        {
            this.color = color;
            if (lightBedroom)
                OpenBedroomLight();
            if (lightKitchen)
                OpenKitchenLight();
            TTS("更换了灯的颜色");
        }
        
        /// <summary>
        /// 把卧室的灯关闭
        /// </summary>
        private void CloseBedroomLight()
        {
            lightBedroom = false;
            MakesureRunInUI(() =>
            {
                picture_Bedroom.Load("Bedroom_Off.jpg");
            });
        }

        /// <summary>
        /// 把卧室的灯打开
        /// lightBedroom改成true标记打开了灯
        /// 按照color，设置灯的颜色
        /// </summary>
        private void OpenBedroomLight()
        {
            lightBedroom = true;
            MakesureRunInUI(() =>
            {
                switch (color)
                {
                    case 0:
                        picture_Bedroom.Load("Bedroom_YellowOn.jpg");
                        break;
                    case 1:
                        picture_Bedroom.Load("Bedroom_RedOn.jpg");
                        break;
                    case 2:
                        picture_Bedroom.Load("Bedroom_BlueOn.jpg");
                        break;
                    case 3:
                        picture_Bedroom.Load("Bedroom_GreenOn.jpg");
                        break;
                }
            });
        }

        /// <summary>
        /// 把厨房的灯关闭
        /// </summary>
        private void CloseKitchenLight()
        {
            lightKitchen = false;
            MakesureRunInUI(() =>
            {
                picture_Kitchen.Load("Kitchen_Off.jpg");
            });
        }

        /// <summary>
        /// 把厨房的灯打开。
        /// lightBedroom改成true标记打开了灯。
        /// 按照color，设置灯的颜色。
        /// </summary>
        private void OpenKitchenLight()
        {
            lightKitchen = true;
            MakesureRunInUI(() =>
            {
                switch (color)
                {
                    case 0:
                        picture_Kitchen.Load("Kitchen_YellowOn.jpg");
                        break;
                    case 1:
                        picture_Kitchen.Load("Kitchen_RedOn.jpg");
                        break;
                    case 2:
                        picture_Kitchen.Load("Kitchen_BlueOn.jpg");
                        break;
                    case 3:
                        picture_Kitchen.Load("Kitchen_GreenOn.jpg");
                        break;
                }
            });
        }
        
        /// <summary>
        /// 利用InvokeRequired保证安全
        /// </summary>
        /// <param name="action">Gui上的动作</param>
        private void MakesureRunInUI(Action action)
        {
            if (InvokeRequired)
            {
                MethodInvoker method = new MethodInvoker(action);
                Invoke(action, null);
            }
            else
            {
                action();
            }
        }

        #endregion

        #region GUI Event处理

        /// <summary>
        /// 鼠标点击了这个按钮之后他会进行录音
        /// 再点击一次就停止录音
        /// </summary>
        private async void button_ClickAsync(object sender, EventArgs e)
        {
            button.Enabled = false;

            isRecording = !isRecording;
            if (isRecording)
            {
                // 启动识别器
                await recognizer.StartContinuousRecognitionAsync();
                button.Text = "停止";
            }
            else
            {
                // 停止识别器
                await recognizer.StopContinuousRecognitionAsync();
                button.Text = "开始";
            }

            button.Enabled = true;
        }

        /// <summary>
        /// 如果勾上了这个，sound为true
        /// 并且可以用语言输出，即可以TTS
        /// 不然Female和Malebox为不可操作的，
        /// 而且没有语言输出，即不可以TTS
        /// </summary>
        private void checkBox_Sound_CheckedChanged(object sender, EventArgs e)
        {
            sound = !sound;
            if (sound)
            {
                checkBox_Female.Enabled = true;
                checkBox_Male.Enabled = true;
                checkBox_Hk.Enabled = true;
                checkBox_Cn.Enabled = true;
            }
            else
            {
                checkBox_Female.Enabled = false;
                checkBox_Male.Enabled = false;
                checkBox_Hk.Enabled = false;
                checkBox_Cn.Enabled = false;
            }
        }

        /// <summary>
        /// 点击这个，语言输出为女生的声音
        /// </summary>
        private void checkBox_Female_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox_Female.Checked = true;
            checkBox_Male.Checked = false;
            sound_Female = true;
        }

        /// <summary>
        /// 点击这个，语言输出为男生的声音
        /// </summary>
        private void checkBox_Male_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox_Female.Checked = false;
            checkBox_Male.Checked = true;
            sound_Female = false;
        }

        /// <summary>
        /// 点击这个，语言输出为中文普通话
        /// </summary>
        private void checkBox_Cn_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox_Cn.Checked = true;
            checkBox_Hk.Checked = false;
            sound_Mandarin = true;
        }

        /// <summary>
        /// 点击这个，语言输出为中文粤语
        /// </summary>
        private void checkBox_Hk_MouseClick(object sender, MouseEventArgs e)
        {
            checkBox_Cn.Checked = false;
            checkBox_Hk.Checked = true;
            sound_Mandarin = false;
        }

        #endregion
    }
}
