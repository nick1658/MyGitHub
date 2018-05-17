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
            this.打开文件 = new System.Windows.Forms.Button();
            this.下载 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.重启 = new System.Windows.Forms.Button();
            this.更多操作 = new System.Windows.Forms.Button();
            this.radioSerial = new System.Windows.Forms.RadioButton();
            this.radioNet = new System.Windows.Forms.RadioButton();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.filePath = new System.Windows.Forms.TextBox();
            this.TFTPServer = new System.Windows.Forms.Label();
            this.exportRecord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // 打开串口
            // 
            this.打开串口.Location = new System.Drawing.Point(48, 4);
            this.打开串口.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.打开串口.Name = "打开串口";
            this.打开串口.Size = new System.Drawing.Size(116, 44);
            this.打开串口.TabIndex = 0;
            this.打开串口.Text = "打开串口";
            this.打开串口.UseVisualStyleBackColor = true;
            this.打开串口.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 160);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(109, 26);
            this.comboBox1.TabIndex = 1;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(124, 160);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(110, 26);
            this.comboBox2.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.richTextBox1.Location = new System.Drawing.Point(249, 57);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(1162, 738);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox1_KeyPress);
            // 
            // 打开文件
            // 
            this.打开文件.BackColor = System.Drawing.SystemColors.Control;
            this.打开文件.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.打开文件.Location = new System.Drawing.Point(2, 290);
            this.打开文件.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.打开文件.Name = "打开文件";
            this.打开文件.Size = new System.Drawing.Size(234, 122);
            this.打开文件.TabIndex = 10;
            this.打开文件.Text = "打开文件";
            this.打开文件.UseVisualStyleBackColor = true;
            this.打开文件.Click += new System.EventHandler(this.打开文件_Click);
            // 
            // 下载
            // 
            this.下载.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.下载.ForeColor = System.Drawing.SystemColors.ControlText;
            this.下载.Location = new System.Drawing.Point(2, 416);
            this.下载.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.下载.Name = "下载";
            this.下载.Size = new System.Drawing.Size(234, 122);
            this.下载.TabIndex = 11;
            this.下载.Text = "下载";
            this.下载.UseVisualStyleBackColor = true;
            this.下载.Click += new System.EventHandler(this.下载_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(243, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 31);
            this.label1.TabIndex = 12;
            this.label1.Text = "Bin文件路径：";
            // 
            // 重启
            // 
            this.重启.BackColor = System.Drawing.SystemColors.Control;
            this.重启.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.重启.Location = new System.Drawing.Point(2, 542);
            this.重启.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.重启.Name = "重启";
            this.重启.Size = new System.Drawing.Size(234, 122);
            this.重启.TabIndex = 15;
            this.重启.Text = "重启";
            this.重启.UseVisualStyleBackColor = true;
            this.重启.Click += new System.EventHandler(this.button2_Click);
            // 
            // 更多操作
            // 
            this.更多操作.BackColor = System.Drawing.SystemColors.Control;
            this.更多操作.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.更多操作.Location = new System.Drawing.Point(2, 668);
            this.更多操作.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.更多操作.Name = "更多操作";
            this.更多操作.Size = new System.Drawing.Size(234, 122);
            this.更多操作.TabIndex = 16;
            this.更多操作.Text = "更多操作";
            this.更多操作.UseVisualStyleBackColor = true;
            this.更多操作.Click += new System.EventHandler(this.button3_Click);
            // 
            // radioSerial
            // 
            this.radioSerial.AutoSize = true;
            this.radioSerial.Location = new System.Drawing.Point(15, 124);
            this.radioSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioSerial.Name = "radioSerial";
            this.radioSerial.Size = new System.Drawing.Size(105, 22);
            this.radioSerial.TabIndex = 17;
            this.radioSerial.TabStop = true;
            this.radioSerial.Text = "串口下载";
            this.radioSerial.UseVisualStyleBackColor = true;
            // 
            // radioNet
            // 
            this.radioNet.AutoSize = true;
            this.radioNet.Location = new System.Drawing.Point(130, 124);
            this.radioNet.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radioNet.Name = "radioNet";
            this.radioNet.Size = new System.Drawing.Size(105, 22);
            this.radioNet.TabIndex = 18;
            this.radioNet.TabStop = true;
            this.radioNet.Text = "网络下载";
            this.radioNet.UseVisualStyleBackColor = true;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(8, 80);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(228, 28);
            this.txtIP.TabIndex = 19;
            // 
            // filePath
            // 
            this.filePath.Location = new System.Drawing.Point(420, 9);
            this.filePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Size = new System.Drawing.Size(810, 28);
            this.filePath.TabIndex = 20;
            // 
            // TFTPServer
            // 
            this.TFTPServer.AutoSize = true;
            this.TFTPServer.Location = new System.Drawing.Point(51, 57);
            this.TFTPServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TFTPServer.Name = "TFTPServer";
            this.TFTPServer.Size = new System.Drawing.Size(143, 18);
            this.TFTPServer.TabIndex = 21;
            this.TFTPServer.Text = "TFTP Server IP:";
            // 
            // exportRecord
            // 
            this.exportRecord.BackColor = System.Drawing.SystemColors.Control;
            this.exportRecord.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exportRecord.Location = new System.Drawing.Point(1238, 4);
            this.exportRecord.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.exportRecord.Name = "exportRecord";
            this.exportRecord.Size = new System.Drawing.Size(162, 52);
            this.exportRecord.TabIndex = 48;
            this.exportRecord.Text = "导出记录";
            this.exportRecord.UseVisualStyleBackColor = true;
            this.exportRecord.Click += new System.EventHandler(this.exportRecord_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1413, 808);
            this.Controls.Add(this.exportRecord);
            this.Controls.Add(this.TFTPServer);
            this.Controls.Add(this.filePath);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.radioNet);
            this.Controls.Add(this.radioSerial);
            this.Controls.Add(this.更多操作);
            this.Controls.Add(this.重启);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.下载);
            this.Controls.Add(this.打开文件);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.打开串口);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
        private System.Windows.Forms.Button 打开文件;
        private System.Windows.Forms.Button 下载;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button 重启;
        private System.Windows.Forms.Button 更多操作;
        private System.Windows.Forms.RadioButton radioSerial;
        private System.Windows.Forms.RadioButton radioNet;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox filePath;
        private System.Windows.Forms.Label TFTPServer;
        private System.Windows.Forms.Button exportRecord;
    }
}

