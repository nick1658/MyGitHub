namespace Coin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.打开串口 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.Tem = new System.Windows.Forms.RichTextBox();
            this.打开文件 = new System.Windows.Forms.Button();
            this.下载 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.重启 = new System.Windows.Forms.Button();
            this.更多操作 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // 打开串口
            // 
            this.打开串口.Location = new System.Drawing.Point(1, 6);
            this.打开串口.Name = "打开串口";
            this.打开串口.Size = new System.Drawing.Size(75, 23);
            this.打开串口.TabIndex = 0;
            this.打开串口.Text = "打开串口";
            this.打开串口.UseVisualStyleBackColor = true;
            this.打开串口.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(1, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(101, 20);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(1, 62);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(101, 20);
            this.comboBox2.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBox1.Location = new System.Drawing.Point(163, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(776, 525);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // Tem
            // 
            this.Tem.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Tem.Location = new System.Drawing.Point(1, 109);
            this.Tem.Name = "Tem";
            this.Tem.ReadOnly = true;
            this.Tem.Size = new System.Drawing.Size(156, 78);
            this.Tem.TabIndex = 6;
            this.Tem.Text = "";
            // 
            // 打开文件
            // 
            this.打开文件.BackColor = System.Drawing.SystemColors.Control;
            this.打开文件.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打开文件.Location = new System.Drawing.Point(1, 193);
            this.打开文件.Name = "打开文件";
            this.打开文件.Size = new System.Drawing.Size(156, 81);
            this.打开文件.TabIndex = 10;
            this.打开文件.Text = "打开文件";
            this.打开文件.UseVisualStyleBackColor = true;
            this.打开文件.Click += new System.EventHandler(this.打开文件_Click);
            // 
            // 下载
            // 
            this.下载.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下载.ForeColor = System.Drawing.SystemColors.ControlText;
            this.下载.Location = new System.Drawing.Point(1, 277);
            this.下载.Name = "下载";
            this.下载.Size = new System.Drawing.Size(156, 81);
            this.下载.TabIndex = 11;
            this.下载.Text = "下载";
            this.下载.UseVisualStyleBackColor = true;
            this.下载.Click += new System.EventHandler(this.下载_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(-2, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 21);
            this.label1.TabIndex = 12;
            this.label1.Text = "Hex文件路径：";
            // 
            // 重启
            // 
            this.重启.BackColor = System.Drawing.SystemColors.Control;
            this.重启.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.重启.Location = new System.Drawing.Point(1, 361);
            this.重启.Name = "重启";
            this.重启.Size = new System.Drawing.Size(156, 81);
            this.重启.TabIndex = 15;
            this.重启.Text = "重启";
            this.重启.UseVisualStyleBackColor = true;
            this.重启.Click += new System.EventHandler(this.button2_Click);
            // 
            // 更多操作
            // 
            this.更多操作.BackColor = System.Drawing.SystemColors.Control;
            this.更多操作.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.更多操作.Location = new System.Drawing.Point(1, 445);
            this.更多操作.Name = "更多操作";
            this.更多操作.Size = new System.Drawing.Size(156, 81);
            this.更多操作.TabIndex = 16;
            this.更多操作.Text = "更多操作";
            this.更多操作.UseVisualStyleBackColor = true;
            this.更多操作.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(942, 533);
            this.Controls.Add(this.更多操作);
            this.Controls.Add(this.重启);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.下载);
            this.Controls.Add(this.打开文件);
            this.Controls.Add(this.Tem);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.打开串口);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoinUpdate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button 打开串口;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox Tem;
        private System.Windows.Forms.Button 打开文件;
        private System.Windows.Forms.Button 下载;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button 重启;
        private System.Windows.Forms.Button 更多操作;
    }
}

