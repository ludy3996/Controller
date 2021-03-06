﻿namespace Controller
{
    partial class Controller
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBox_Sound = new System.Windows.Forms.CheckBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.picture_Bedroom = new System.Windows.Forms.PictureBox();
            this.picture_Kitchen = new System.Windows.Forms.PictureBox();
            this.textBox_Error = new System.Windows.Forms.TextBox();
            this.checkBox_Female = new System.Windows.Forms.CheckBox();
            this.checkBox_Male = new System.Windows.Forms.CheckBox();
            this.checkBox_Cn = new System.Windows.Forms.CheckBox();
            this.checkBox_Hk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Bedroom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Kitchen)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_Sound
            // 
            this.checkBox_Sound.AutoSize = true;
            this.checkBox_Sound.Checked = true;
            this.checkBox_Sound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Sound.Font = new System.Drawing.Font("Samsung Sharp Sans Bold", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Sound.Location = new System.Drawing.Point(434, 47);
            this.checkBox_Sound.Name = "checkBox_Sound";
            this.checkBox_Sound.Size = new System.Drawing.Size(113, 35);
            this.checkBox_Sound.TabIndex = 7;
            this.checkBox_Sound.Text = "Sound";
            this.checkBox_Sound.UseVisualStyleBackColor = true;
            this.checkBox_Sound.CheckedChanged += new System.EventHandler(this.checkBox_Sound_CheckedChanged);
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("SimHei", 10F);
            this.textBox.Location = new System.Drawing.Point(408, 118);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ReadOnly = true;
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(360, 380);
            this.textBox.TabIndex = 6;
            // 
            // button
            // 
            this.button.Font = new System.Drawing.Font("Microsoft YaHei", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button.Location = new System.Drawing.Point(109, 16);
            this.button.Margin = new System.Windows.Forms.Padding(4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(200, 90);
            this.button.TabIndex = 5;
            this.button.Text = "开始";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_ClickAsync);
            // 
            // picture_Bedroom
            // 
            this.picture_Bedroom.Location = new System.Drawing.Point(19, 118);
            this.picture_Bedroom.Name = "picture_Bedroom";
            this.picture_Bedroom.Size = new System.Drawing.Size(360, 180);
            this.picture_Bedroom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_Bedroom.TabIndex = 8;
            this.picture_Bedroom.TabStop = false;
            // 
            // picture_Kitchen
            // 
            this.picture_Kitchen.Location = new System.Drawing.Point(19, 318);
            this.picture_Kitchen.Name = "picture_Kitchen";
            this.picture_Kitchen.Size = new System.Drawing.Size(360, 180);
            this.picture_Kitchen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_Kitchen.TabIndex = 9;
            this.picture_Kitchen.TabStop = false;
            // 
            // textBox_Error
            // 
            this.textBox_Error.Location = new System.Drawing.Point(19, 13);
            this.textBox_Error.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Error.Multiline = true;
            this.textBox_Error.Name = "textBox_Error";
            this.textBox_Error.ReadOnly = true;
            this.textBox_Error.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_Error.Size = new System.Drawing.Size(749, 485);
            this.textBox_Error.TabIndex = 10;
            this.textBox_Error.Visible = false;
            // 
            // checkBox_Female
            // 
            this.checkBox_Female.AutoCheck = false;
            this.checkBox_Female.AutoSize = true;
            this.checkBox_Female.Checked = true;
            this.checkBox_Female.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Female.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox_Female.Font = new System.Drawing.Font("Samsung Sharp Sans Bold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Female.Location = new System.Drawing.Point(669, 38);
            this.checkBox_Female.Name = "checkBox_Female";
            this.checkBox_Female.Size = new System.Drawing.Size(81, 21);
            this.checkBox_Female.TabIndex = 11;
            this.checkBox_Female.Text = "Female";
            this.checkBox_Female.UseVisualStyleBackColor = true;
            this.checkBox_Female.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_Female_MouseClick);
            // 
            // checkBox_Male
            // 
            this.checkBox_Male.AutoCheck = false;
            this.checkBox_Male.AutoSize = true;
            this.checkBox_Male.Checked = true;
            this.checkBox_Male.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Male.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox_Male.Font = new System.Drawing.Font("Samsung Sharp Sans Bold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Male.Location = new System.Drawing.Point(669, 71);
            this.checkBox_Male.Name = "checkBox_Male";
            this.checkBox_Male.Size = new System.Drawing.Size(62, 21);
            this.checkBox_Male.TabIndex = 12;
            this.checkBox_Male.Text = "Male";
            this.checkBox_Male.UseVisualStyleBackColor = true;
            this.checkBox_Male.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_Male_MouseClick);
            // 
            // checkBox_Cn
            // 
            this.checkBox_Cn.AutoCheck = false;
            this.checkBox_Cn.AutoSize = true;
            this.checkBox_Cn.Checked = true;
            this.checkBox_Cn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Cn.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox_Cn.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Cn.Location = new System.Drawing.Point(571, 37);
            this.checkBox_Cn.Name = "checkBox_Cn";
            this.checkBox_Cn.Size = new System.Drawing.Size(73, 23);
            this.checkBox_Cn.TabIndex = 13;
            this.checkBox_Cn.Text = "普通话";
            this.checkBox_Cn.UseVisualStyleBackColor = true;
            this.checkBox_Cn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_Cn_MouseClick);
            // 
            // checkBox_Hk
            // 
            this.checkBox_Hk.AutoCheck = false;
            this.checkBox_Hk.AutoSize = true;
            this.checkBox_Hk.Checked = true;
            this.checkBox_Hk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Hk.Cursor = System.Windows.Forms.Cursors.Default;
            this.checkBox_Hk.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_Hk.Location = new System.Drawing.Point(571, 69);
            this.checkBox_Hk.Name = "checkBox_Hk";
            this.checkBox_Hk.Size = new System.Drawing.Size(59, 23);
            this.checkBox_Hk.TabIndex = 14;
            this.checkBox_Hk.Text = "粤语";
            this.checkBox_Hk.UseVisualStyleBackColor = true;
            this.checkBox_Hk.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_Hk_MouseClick);
            // 
            // Controller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 513);
            this.Controls.Add(this.checkBox_Hk);
            this.Controls.Add(this.checkBox_Cn);
            this.Controls.Add(this.checkBox_Male);
            this.Controls.Add(this.checkBox_Female);
            this.Controls.Add(this.picture_Kitchen);
            this.Controls.Add(this.picture_Bedroom);
            this.Controls.Add(this.checkBox_Sound);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox_Error);
            this.Name = "Controller";
            this.Text = "Controller";
            this.Load += new System.EventHandler(this.Controller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture_Bedroom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_Kitchen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_Sound;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.PictureBox picture_Bedroom;
        private System.Windows.Forms.PictureBox picture_Kitchen;
        private System.Windows.Forms.TextBox textBox_Error;
        private System.Windows.Forms.CheckBox checkBox_Female;
        private System.Windows.Forms.CheckBox checkBox_Male;
        private System.Windows.Forms.CheckBox checkBox_Cn;
        private System.Windows.Forms.CheckBox checkBox_Hk;
    }
}

