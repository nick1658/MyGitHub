using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coin
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();

            //单色填充
            //SolidBrush b1 = new SolidBrush(Color.Blue);//定义单色画刷  
            double x1 = 10;
            double y1 = 0;      
            Pen p = new Pen(Color.Green, 1);

            //转变坐标轴角度
            //for (int i = 0; i < 90; i++)
            //{
            //    g.RotateTransform(0);//每旋转一度就画一条线
            //    g.DrawLine(p, 300, 300, 100, 100);
            //    g.ResetTransform();//恢复坐标轴坐标
            //}
            //{
            //    g.RotateTransform(45);//每旋转一度就画一条线
            //    g.DrawLine(p, 300, 300, 100, 100);
            //    g.ResetTransform();//恢复坐标轴坐标
            //}

            //平移坐标轴
            //g.TranslateTransform(100, 100);
            //g.DrawLine(p, 0, 0, 100, 0);
            g.ResetTransform();

            //先平移到指定坐标,然后进行度旋转
            g.TranslateTransform(0, 400);
            g.ScaleTransform(1.0f, -1.0f);
            for (double x = 0; x < 720; x += 1)
            {
                double y = Math.Sin(x / 180 * Math.PI) * 100;
                g.DrawLine(p, (float)x1, (float)y1 + 110, (float)(x / 1.8) + 10, (float)y + 110);
                x1 = x / 1.8 + 10;
                y1 = y;
            }

            g.Dispose();

        }
    }
}
