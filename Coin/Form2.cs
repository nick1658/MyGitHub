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
    public partial class CoinOP : Form
    {

        public Form1 parentFrm;
        public CoinOP()
        {
            InitializeComponent();
        }
        private void CoinOP_FormClosing(object sender, FormClosingEventArgs e)
        {
            parentFrm.exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd("0005");//启动
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (启动.Text == "启动")
            {
                parentFrm.send_cmd("0002");//启动
                启动.Text = "停止";
            }
            else
            {
                parentFrm.send_cmd("0003");//停止
                启动.Text = "启动";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd("0004");//打印
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd("E001");//清除报警
        }

        private void CoinOP_Load(object sender, EventArgs e)
        {
            kick1_delay.Text = "0";
            kick1_keep.Text = "0";
            kick2_keep.Text = "0";
            kick2_delay.Text = "0";
            yz_1yuan.Text = "0";
            yz_5jiao.Text = "0";
            yz_1jiao.Text = "0";
            yz_5fen.Text = "0";
            yz_2fen.Text = "0";
            yz_1fen.Text = "0";
            jnb_10yuan.Text = "0";
            jnb_5yuan.Text = "0";
            dq_1yuan.Text = "0";
            dq_1jiao.Text = "0";
            dq_5jiao.Text = "0";
            dq_5fen.Text = "0";
            dq_2fen.Text = "0";
            dq_1fen.Text = "0";
            dq_zms.Text = "0";
            dq_zje.Text = "0";
            dq_yb.Text = "0";
            idle_time.Text = "0";
            full_rej_pos.Text = "0";
            full_stop_num.Text = "0";
        }
        
        private void setValue (int index, string str)
        {
            switch (index)
            {
                case 1:
                    kick1_delay.Text = str;
                    break;
                case 2:
                    kick1_keep.Text = str;
                    break;
                case 3:
                    kick2_delay.Text = str;
                    break;
                case 4:
                    kick2_keep.Text = str;
                    break;
                case 5:
                    yz_1yuan.Text = str;
                    break;
                case 6:
                    yz_5jiao.Text = str;
                    break;
                case 7:
                    yz_1jiao.Text = str;
                    break;
                case 8:
                    yz_5fen.Text = str;
                    break;
                case 9:
                    yz_2fen.Text = str;
                    break;
                case 10:
                    yz_1fen.Text = str;
                    break;
                case 11:
                    jnb_10yuan.Text = str;
                    break;
                case 12:
                    jnb_5yuan.Text = str;
                    break;
                case 13:
                    dq_1yuan.Text = str;
                    break;
                case 14:
                    dq_5jiao.Text = str;
                    break;
                case 15:
                    dq_1jiao.Text = str;
                    break;
                case 16:
                    dq_5fen.Text = str;
                    break;
                case 17:
                    dq_2fen.Text = str;
                    break;
                case 18:
                    dq_1fen.Text = str;
                    break;
                case 19:
                    dq_zms.Text = str;
                    break;
                case 20:
                    dq_zje.Text = str;
                    break;
                case 21:
                    dq_yb.Text = str;
                    break;
                case 22:
                    idle_time.Text = str;
                    break;
                case 23:
                    full_stop_num.Text = str;
                    break;
                case 24:
                    full_rej_pos.Text = str;
                    break;
                case 50:
                    启动.Text = "启动";//停机信号
                    break;
                default:
                    break;
            }
        }
        public void CoinOP_SetText(string str)
        {
            int index = 0;
            char c;
            string newstring = "";
            for (int i = 0; i < str.Length; i++)
            {
                c = str[i];
                if (c == ',')
                {
                    try
                    {
                        index = int.Parse(newstring);
                        newstring = "";
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
                else if (c == ';')
                {
                    setValue(index, newstring);
                    newstring = "";
                }
                else if (c == '\r')
                {
                    continue;
                }
                else if (c == '\n')
                {
                    continue;
                }
                else
                {
                    newstring += c;
                }
            }
           
        }
        private void button5_Click(object sender, EventArgs e)
        {
            parentFrm.set_send_state(0);
            this.Hide();
            parentFrm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            parentFrm.set_send_state(2);
            parentFrm.send_cmd("0001");//同步数据
        }

        private void kick1_delay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("1", kick1_delay.Text);
                }
                
            }  
        }
        private void kick1_keep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("2", kick1_keep.Text);
                }
            }  
        }
        private void kick2_delay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("3", kick2_delay.Text);
                }
            }
        }

        private void kick2_keep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("4", kick2_keep.Text);
                }
            }
        }
        private void yz_1yuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("5", yz_1yuan.Text);
                }
            }
        }

        private void yz_5jiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("6", yz_5jiao.Text);
                }
            }
        }

        private void yz_1jiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("7", yz_1jiao.Text);
                }
            }
        }

        private void yz_5fen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("8", yz_5fen.Text);
                }
            }
        }

        private void yz_2fen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("9", yz_2fen.Text);
                }
            }
        }

        private void yz_1fen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("10", yz_1fen.Text);
                }
            }
        }

        private void jnb_10yuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("11", jnb_10yuan.Text);
                }
            }
        }

        private void jnb_5yuan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("12", jnb_5yuan.Text);
                }
            }
        }
        private void idle_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("22", idle_time.Text);
                }
            }
        }

        private void full_stop_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("23", full_stop_num.Text);
                }
            }
        }

        private void full_rej_pos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("24", full_rej_pos.Text);
                }
            }
        }
    }
}
