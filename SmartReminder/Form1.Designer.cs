namespace SmartReminder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.talk_textBox = new System.Windows.Forms.TextBox();
            this.talk_button = new System.Windows.Forms.Button();
            this.respond_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // talk_textBox
            // 
            this.talk_textBox.Location = new System.Drawing.Point(12, 386);
            this.talk_textBox.Multiline = true;
            this.talk_textBox.Name = "talk_textBox";
            this.talk_textBox.Size = new System.Drawing.Size(435, 107);
            this.talk_textBox.TabIndex = 0;
            this.talk_textBox.TextChanged += new System.EventHandler(this.talk_textBox_TextChanged);
            // 
            // talk_button
            // 
            this.talk_button.Location = new System.Drawing.Point(453, 386);
            this.talk_button.Name = "talk_button";
            this.talk_button.Size = new System.Drawing.Size(105, 107);
            this.talk_button.TabIndex = 1;
            this.talk_button.Text = "talk";
            this.talk_button.UseVisualStyleBackColor = true;
            this.talk_button.Click += new System.EventHandler(this.talk_button_Click);
            // 
            // respond_textBox
            // 
            this.respond_textBox.Location = new System.Drawing.Point(12, 85);
            this.respond_textBox.Multiline = true;
            this.respond_textBox.Name = "respond_textBox";
            this.respond_textBox.Size = new System.Drawing.Size(435, 282);
            this.respond_textBox.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 505);
            this.Controls.Add(this.respond_textBox);
            this.Controls.Add(this.talk_button);
            this.Controls.Add(this.talk_textBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox talk_textBox;
        private System.Windows.Forms.Button talk_button;
        private System.Windows.Forms.TextBox respond_textBox;
    }
}

