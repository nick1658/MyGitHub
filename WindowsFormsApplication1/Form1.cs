using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calc_func ()
        {
            Double VALUE_0 = 0.00323;
            Double VALUE_3 = 1.0;
            Double VALUE_4 = 1.0;
            Double VALUE_1 = 1.0;
            Double VALUE_2 = 1.0;
            Double A = 0.0;
            Double HA = 0.0;
            Double HA1 = 0.0;
            Double HB_max = 0.0;
            Double HB1_max = 0.0;
            Double HB_min = 0.0;
            Double HB1_min = 0.0;
            Double Av = 0.0;

            Double B_max = 0.0;
            Double B_min = 0.0;

            Double A1 = 0.0;


            Double 保存范围_v = 0.0;
            Double 使用范围_v = 0.0;

            if (减法系数.Text != "")
            {
                VALUE_3 = Double.Parse(减法系数.Text);
            }
            if (平均值系数.Text != "")
            {
                //VALUE_4 = Double.Parse(平均值系数.Text);
                A = Double.Parse(平均值系数.Text);
            } 
            VALUE_1 = VALUE_3 / VALUE_4;
            VALUE_2 = VALUE_0 / VALUE_4;
//            if (峰峰值.Text != "")
//            {
//                A = Double.Parse(峰峰值.Text);
//            }
            if (保存基准值.Text != "")
            {
                HA = Double.Parse(保存基准值.Text);
            }
            if (实测基准值.Text != "")
            {
                HA1 = Double.Parse(实测基准值.Text);
            }
            if (保存最大值.Text != "")
            {
                HB_max = Double.Parse(保存最大值.Text);
            }
            if (保存最小值.Text != "")
            {
                HB_min = Double.Parse(保存最小值.Text);
            }



            HA *= VALUE_2;
            Av = HA / (A-VALUE_1);
            增益.Text = Av.ToString();


            B_max = ((HB_max * VALUE_2) / Av) + VALUE_1;
            B_min = ((HB_min * VALUE_2) / Av) + VALUE_1;

            A1 = ((HA1 * VALUE_2) / Av) + VALUE_1;

            HB1_max = (((A1 * B_max * VALUE_4) / A) - VALUE_3) * (Av / VALUE_0);
            HB1_min = (((A1 * B_min * VALUE_4) / A) - VALUE_3) * (Av / VALUE_0);

            使用最大值.Text = HB1_max.ToString();
            使用最小值.Text = HB1_min.ToString();

            保存范围_v = HB_max - HB_min;
            使用范围_v = HB1_max - HB1_min;
            保存范围.Text = 保存范围_v.ToString();
            使用范围.Text = 使用范围_v.ToString();

        }

        private void 峰峰值_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    calc_func();
                }
            }
        }

        private void 实测基准值_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    calc_func();
                }
            }
        }

        private void 保存基准值_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    calc_func();
                }
            }
        }

        private void 保存最大值_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    calc_func();
                }
            }
        }

        private void 保存最小值_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    calc_func();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            calc_func();
        }

        private void 平均值系数_KeyPress(object sender, KeyPressEventArgs e)
        {
            calc_func();
        }

        private void 减法系数_KeyPress(object sender, KeyPressEventArgs e)
        {
            calc_func();
        }
    }
}
