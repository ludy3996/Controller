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

        private void CloseBedroomLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Bedroom.Load("Bedroom_LightOff.jpg");
            });
        }

        private void OpenBedroomLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Bedroom.Load("Bedroom_LightOn.jpg");
            });
        }

        private void CloseKitchenLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Kitchen.Load("Kitchen_LightOff.jpg");
            });
        }

        private void OpenKitchenLight()
        {
            MakesureRunInUI(() =>
            {
                picture_Kitchen.Load("Kitchen_LightOff.jpg");
            });
        }
    }
}
