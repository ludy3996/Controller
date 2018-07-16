using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controller
{
    public partial class Controller : Form
    {
        private SpeechRecognizer recognizer;
        private bool isRecording = false;

        public Controller()
        {
            InitializeComponent();
            button.Text = "开始";
            textBox_Error.Visible = false;
            CloseBedroomLight();
            CloseKitchenLight();
        }

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

        // 把卧室的灯关闭
        private void CloseBedroomLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Bedroom.Load("Bedroom_LightOff.jpg");
            });
        }

        // 把卧室的·灯打开
        private void OpenBedroomLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Bedroom.Load("Bedroom_LightOn.jpg");
            });
        }

        // 把厨房的灯关闭
        private void CloseKitchenLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Kitchen.Load("Kitchen_LightOff.jpg");
            });
        }

        // 把厨房的灯关闭
        private void OpenKitchenLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Kitchen.Load("Kitchen_LightOff.jpg");
            });
        }

        private void Controller_Load(object sender, EventArgs e)
        {
            try
            {
                // 第一步
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

        // 识别过程中的中间结果
        private void Recognizer_IntermediateResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("中间结果: " + e.Result.Text);
            }
        }

        // 识别的最终结果
        private void Recognizer_FinalResultReceived(object sender, SpeechRecognitionResultEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Result.Text))
            {
                Log("最终结果: " + e.Result.Text);
                //ProcessSttResult(e.Result.Text);
            }
        }

        // 出错时的处理
        private void Recognizer_RecognitionErrorRaised(object sender, RecognitionErrorEventArgs e)
        {
            Log("错误: " + e.FailureReason);
        }

        #region 界面操作

        // 在textBox上输出 message 并换行
        private void Log(string message)
        {
            MakesureRunInUI(() =>
            {
                textBox.AppendText(message + "\r\n");
            });
        }

        #endregion

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
    }
}
