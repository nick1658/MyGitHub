﻿using System;
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

        System.Timers.Timer _t;
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
                //启动.Text = "停止";
            }
            else
            {
                parentFrm.send_cmd("0003");//停止
                //启动.Text = "启动";
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
            yz_dao1jiao.Text = "0";
            yz_5fen.Text = "0";
            yz_2fen.Text = "0";
            yz_1fen.Text = "0";
            yz_jnb_10yuan.Text = "0";
            yz_jnb_5yuan.Text = "0";
            dq_1yuan.Text = "0";
            dq_1jiao.Text = "0";
            dq_dao1jiao.Text = "0";
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
            system_boot_delay.Text = "0";
            dq_speed.Text = "9999";

            cb_coinType.Items.Add("1元");
            cb_coinType.Items.Add("5角铜");
            cb_coinType.Items.Add("5角钢");
            cb_coinType.Items.Add("大1角铝");
            cb_coinType.Items.Add("1角钢");
            cb_coinType.Items.Add("1角铝");
            cb_coinType.Items.Add("5分");
            cb_coinType.Items.Add("2分");
            cb_coinType.Items.Add("1分");
            cb_coinType.Items.Add("纪念币10元");
            cb_coinType.Items.Add("纪念币5元");
            //parentFrm.send_cmd("0001");//同步数据
        }
        
        private void setValue (int index, string str)
        {
            #region -=[ switch ]=-
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
                    yz_jnb_10yuan.Text = str;
                    break;
                case 12:
                    yz_jnb_5yuan.Text = str;
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
                case 25:
                    system_boot_delay.Text = str;
                    break;
                case 30:
                    dq_dao1jiao.Text = str;
                    break;
                case 50:
                    if (str == "0")
                        启动.Text = "启动";//停机信号
                    else
                        启动.Text = "停止";//停机信号
                    break;
                case 51:
                    if (Int32.Parse(str) < 11)
                    {
                        cb_coinType.SelectedIndex = Int32.Parse(str);
                    }
                    break;
                case 52:
                    tz_H_Max.Text = str;
                    break;
                case 53:
                    tz_H_Min.Text = str;
                    break;
                case 54:
                    tz_M_Max.Text = str;
                    break;
                case 55:
                    tz_M_Min.Text = str;
                    break;
                case 56:
                    tz_L_Max.Text = str;
                    break;
                case 57:
                    tz_L_Min.Text = str;
                    break;
                case 58:
                    dq_speed.Text = str;
                    break;
                default:
                    break;
            }
            #endregion
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
                        index = 0;
                        newstring = "";
                        //MessageBox.Show(e.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // continue;
                    }
                }
                else if (c == ';')
                {
                    setValue(index, newstring);
                    newstring = "";
                }
                else if (c == ':')
                {
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
            stop_poll();
            poll_data_ckeck.Checked = false;
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
        private void yz_dao1jiao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("30", yz_dao1jiao.Text);
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
                    parentFrm.send_value("11", yz_jnb_10yuan.Text);
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
                    parentFrm.send_value("12", yz_jnb_5yuan.Text);
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
        private void system_boot_delay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("25", system_boot_delay.Text);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            parentFrm.send_value("51", "" + cb_coinType.SelectedIndex);
        }

        private void tz_H_Max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("52", tz_H_Max.Text);
                }
            }
        }

        private void tz_H_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("53", tz_H_Min.Text);
                }
            }
        }

        private void tz_M_Max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("54", tz_M_Max.Text);
                }
            }
        }

        private void tz_M_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("55", tz_M_Min.Text);
                }
            }
        }

        private void tz_L_Max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("56", tz_L_Max.Text);
                }
            }
        }

        private void tz_L_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("57", tz_L_Min.Text);
                }
            }
        }

        private void poll_data(object sender, EventArgs e)
        {
            parentFrm.send_cmd("0006");//同步数据
        }
        private void poll_data_ckeck_CheckedChanged(object sender, EventArgs e)
        {
            if (poll_data_ckeck.Checked == true)
            {
                start_poll();
            }
            else
            {
                stop_poll();
            }
        }

        private void start_poll ()
        {
            _t = new System.Timers.Timer(1000);
            _t.Elapsed += poll_data;//到达时间的时候执行事件；
            _t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            _t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
        private void stop_poll()
        {
            if (_t == null) return;
            _t.Elapsed -= poll_data;
            _t.Stop();
            _t.Dispose();
            _t = null;
        }


    }
}
