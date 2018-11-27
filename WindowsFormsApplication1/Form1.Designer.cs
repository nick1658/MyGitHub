namespace WindowsFormsApplication1
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.保存基准值 = new System.Windows.Forms.TextBox();
            this.保存最大值 = new System.Windows.Forms.TextBox();
            this.保存最小值 = new System.Windows.Forms.TextBox();
            this.实测基准值 = new System.Windows.Forms.TextBox();
            this.使用最大值 = new System.Windows.Forms.TextBox();
            this.使用最小值 = new System.Windows.Forms.TextBox();
            this.增益 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.保存范围 = new System.Windows.Forms.TextBox();
            this.使用范围 = new System.Windows.Forms.TextBox();
            this.平均值系数 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.减法系数 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "保存基准值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "保存最大值";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(468, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "使用最大值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(469, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "实测基准值";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(468, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "使用最小值";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(162, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "保存最小值";
            // 
            // 保存基准值
            // 
            this.保存基准值.Location = new System.Drawing.Point(231, 64);
            this.保存基准值.Name = "保存基准值";
            this.保存基准值.Size = new System.Drawing.Size(71, 21);
            this.保存基准值.TabIndex = 9;
            this.保存基准值.Text = "900";
            this.保存基准值.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.保存基准值_KeyPress);
            // 
            // 保存最大值
            // 
            this.保存最大值.Location = new System.Drawing.Point(231, 99);
            this.保存最大值.Name = "保存最大值";
            this.保存最大值.Size = new System.Drawing.Size(71, 21);
            this.保存最大值.TabIndex = 10;
            this.保存最大值.Text = "200";
            this.保存最大值.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.保存最大值_KeyPress);
            // 
            // 保存最小值
            // 
            this.保存最小值.Location = new System.Drawing.Point(231, 134);
            this.保存最小值.Name = "保存最小值";
            this.保存最小值.Size = new System.Drawing.Size(71, 21);
            this.保存最小值.TabIndex = 11;
            this.保存最小值.Text = "150";
            this.保存最小值.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.保存最小值_KeyPress);
            // 
            // 实测基准值
            // 
            this.实测基准值.Location = new System.Drawing.Point(536, 64);
            this.实测基准值.Name = "实测基准值";
            this.实测基准值.Size = new System.Drawing.Size(71, 21);
            this.实测基准值.TabIndex = 12;
            this.实测基准值.Text = "900";
            this.实测基准值.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.实测基准值_KeyPress);
            // 
            // 使用最大值
            // 
            this.使用最大值.Location = new System.Drawing.Point(534, 101);
            this.使用最大值.Name = "使用最大值";
            this.使用最大值.ReadOnly = true;
            this.使用最大值.Size = new System.Drawing.Size(71, 21);
            this.使用最大值.TabIndex = 13;
            this.使用最大值.Text = " 0";
            // 
            // 使用最小值
            // 
            this.使用最小值.Location = new System.Drawing.Point(534, 139);
            this.使用最小值.Name = "使用最小值";
            this.使用最小值.ReadOnly = true;
            this.使用最小值.Size = new System.Drawing.Size(71, 21);
            this.使用最小值.TabIndex = 14;
            this.使用最小值.Text = " 0";
            // 
            // 增益
            // 
            this.增益.Location = new System.Drawing.Point(82, 96);
            this.增益.Name = "增益";
            this.增益.Size = new System.Drawing.Size(71, 21);
            this.增益.TabIndex = 16;
            this.增益.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 15;
            this.label1.Text = "增益";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(305, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "范围";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(611, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "范围";
            // 
            // 保存范围
            // 
            this.保存范围.Location = new System.Drawing.Point(340, 119);
            this.保存范围.Name = "保存范围";
            this.保存范围.Size = new System.Drawing.Size(71, 21);
            this.保存范围.TabIndex = 19;
            this.保存范围.Text = "150";
            // 
            // 使用范围
            // 
            this.使用范围.Location = new System.Drawing.Point(646, 122);
            this.使用范围.Name = "使用范围";
            this.使用范围.Size = new System.Drawing.Size(71, 21);
            this.使用范围.TabIndex = 20;
            this.使用范围.Text = "150";
            // 
            // 平均值系数
            // 
            this.平均值系数.Location = new System.Drawing.Point(82, 63);
            this.平均值系数.Name = "平均值系数";
            this.平均值系数.Size = new System.Drawing.Size(71, 21);
            this.平均值系数.TabIndex = 24;
            this.平均值系数.Text = "3.0";
            this.平均值系数.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.平均值系数_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "平均值";
            // 
            // 减法系数
            // 
            this.减法系数.Location = new System.Drawing.Point(82, 132);
            this.减法系数.Name = "减法系数";
            this.减法系数.Size = new System.Drawing.Size(71, 21);
            this.减法系数.TabIndex = 22;
            this.减法系数.Text = "0.6";
            this.减法系数.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.减法系数_KeyPress);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 21;
            this.label11.Text = "减法系数";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 457);
            this.Controls.Add(this.平均值系数);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.减法系数);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.使用范围);
            this.Controls.Add(this.保存范围);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.增益);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.使用最小值);
            this.Controls.Add(this.使用最大值);
            this.Controls.Add(this.实测基准值);
            this.Controls.Add(this.保存最小值);
            this.Controls.Add(this.保存最大值);
            this.Controls.Add(this.保存基准值);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox 保存基准值;
        private System.Windows.Forms.TextBox 保存最大值;
        private System.Windows.Forms.TextBox 保存最小值;
        private System.Windows.Forms.TextBox 实测基准值;
        private System.Windows.Forms.TextBox 使用最大值;
        private System.Windows.Forms.TextBox 使用最小值;
        private System.Windows.Forms.TextBox 增益;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox 保存范围;
        private System.Windows.Forms.TextBox 使用范围;
        private System.Windows.Forms.TextBox 平均值系数;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox 减法系数;
        private System.Windows.Forms.Label label11;
    }
}

