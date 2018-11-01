using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coin
{
    public partial class CoinOP : Form
    {

        public Form1 parentFrm;
        public int isHistoryRecord = 0;

        System.Timers.Timer _t;

        private int coin_mode = 0;
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
            parentFrm.send_cmd_code("0005");//清零
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (启动.Text == "启动")
            {
                parentFrm.send_cmd_code("0002");//启动
                start_poll();
                //启动.Text = "停止";
            }
            else
            {
                parentFrm.send_cmd_code("0003");//停止
                //启动.Text = "启动";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0004");//打印
        }

        private void button4_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("E001");//清除报警
        }

        private void 特征学习启动_Click(object sender, EventArgs e)
        {
            if (特征学习启动.Text == "启动")
            {
                parentFrm.send_cmd_code("0009");//特征学习启动
                start_poll();
                特征学习启动.Text = "停止";
                取消保存.Enabled = false;
            }
            else if(特征学习启动.Text == "停止")
            {
                stop_poll();
                parentFrm.send_cmd_code("000A");//特征学习停止
                特征学习启动.Text = "保存";
                取消保存.Enabled = true;
            }
            else if(特征学习启动.Text == "保存")
            {
                DialogResult dr= MessageBox.Show("确定保存此次学习的特征值？","保存特征值", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {//点确定的代码
                    parentFrm.send_cmd_code("000B");//特征学习保存
                }
                else
                { //点取消的代码 
                    parentFrm.send_cmd_code("000C");//特征学习取消保存
                } 
                取消保存.Enabled = false;
                特征学习启动.Text = "启动";
            }
        }

        private void 取消保存_Click(object sender, EventArgs e)
        {
            取消保存.Enabled = false;
            特征学习启动.Text = "启动";
            parentFrm.send_cmd_code("000C");//特征学习取消保存
        }

        private void CoinOP_Load(object sender, EventArgs e)
        {
            kick0_delay.Text = "0";
            kick0_keep.Text = "0";
            kick1_keep.Text = "0";
            kick1_delay.Text = "0";
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
            hopper_1yuan.Text = "1";
            hopper_5jiao.Text = "1";
            hopper_1jiao.Text = "1";
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
            //parentFrm.send_cmd_code("0001");//同步数据
        }
        
        private void setValue (int index, string str)
        {
            #region -=[ switch ]=-
            switch (index)
            {
                case 1:
                    kick0_delay.Text = str;
                    break;
                case 2:
                    kick0_keep.Text = str;
                    break;
                case 3:
                    kick1_delay.Text = str;
                    break;
                case 4:
                    kick1_keep.Text = str;
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
                case 26:
                    if (str == "1")
                    {
                        coin_mode = 1;
                        coin_mode_button.Image = global::Coin.Properties.Resources._1_switch_on;
                    }
                    else if (str == "0")
                    {
                        coin_mode = 0;
                        coin_mode_button.Image = global::Coin.Properties.Resources._0_switch_off;
                    }
                    break;
                case 27:
                    yz_dao1jiao.Text = str;
                    break;
                case 28:
                    cpuUsage.Text = str;
                    break;
                case 30:
                    dq_dao1jiao.Text = str;
                    break;
                case 50:
                    if (str == "0")
                    {
                        //poll_data_ckeck.Checked = false;
                        if (poll_data_ckeck.Checked == false)
                        {
                            stop_poll();
                        }
                        
                        启动.Text = "启动";//停机信号
                    }
                    else
                    {
                        //poll_data_ckeck.Checked = true;
                        启动.Text = "停止";//停机信号
                    }
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
                case 60:
                    kick2_delay.Text = str;
                    break;
                case 61:
                    kick2_keep.Text = str;
                    break;
                case 62:
                    kick3_delay.Text = str;
                    break;
                case 63:
                    kick3_keep.Text = str;
                    break;
                case 64:
                    if (str == "1")
                    {
                        inhibitCoin_check.Checked = true;
                    }
                    else
                    {
                        inhibitCoin_check.Checked = false;
                    }
                    break;
                case 65:
                    textBox_Tips.Text = str;
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
                if (c == '$')
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
                    try
                    {
                        setValue(index, newstring);
                        newstring = "";
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
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
        public void Save_finished ()
        {
            exportRecord.Enabled = true;
            saveRecord.Enabled = true;
            isHistoryRecord = 0;
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
            parentFrm.send_cmd_code("0001");//同步数据
        }

        private void kick1_delay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("1", kick0_delay.Text);
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
                    parentFrm.send_value("2", kick0_keep.Text);
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
                    parentFrm.send_value("3", kick1_delay.Text);
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
                    parentFrm.send_value("4", kick1_keep.Text);
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
                    parentFrm.send_value("27", yz_dao1jiao.Text);
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
            parentFrm.send_cmd_code("0006");//同步数据
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
            if (_t == null)
            {
                _t = new System.Timers.Timer(1000);
                _t.Elapsed += poll_data;//到达时间的时候执行事件；
                _t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
                _t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
            }
        }
        private void stop_poll()
        {
            if (_t != null)
            {
                _t.Elapsed -= poll_data;
                _t.Stop();
                _t.Dispose();
                _t = null;
            }
        }

        private void coin_mode_Click(object sender, EventArgs e)
        {
            if (coin_mode == 1)
            {
                coin_mode = 0;
                coin_mode_button.Image = global::Coin.Properties.Resources._0_switch_off;
            }
            else
            {
                coin_mode = 1;
                coin_mode_button.Image = global::Coin.Properties.Resources._1_switch_on;
            }
            parentFrm.send_value("26", "" + coin_mode);
        }

        private void en_1yuan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_5jiao_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_1jiao_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_dao1jiao_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_5fen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_2fen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_1fen_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_jnb10yuan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void en_jnb5yuan_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void exportRecord_Click(object sender, EventArgs e)
        {
            isHistoryRecord = 0;
            parentFrm.send_str("version\r");
            Thread.Sleep(200);
            saveRecord.Enabled = false;
            exportRecord.Enabled = false;
            parentFrm.set_send_state(3);
            parentFrm.send_cmd_code("0007");//导出数据
        }

        private void saveRecord_Click(object sender, EventArgs e)
        {
            isHistoryRecord = 1;
            parentFrm.send_str("version\r");
            Thread.Sleep(200);
            saveRecord.Enabled = false;
            exportRecord.Enabled = false;
            parentFrm.set_send_state(3);
            parentFrm.send_cmd_code("0008");//导出数据
        }

        private void hopper_test_Click(object sender, EventArgs e)
        {
            if (hopper_1yuan.Text == "")
            {
                hopper_1yuan.Text = "1";
            }
            if (hopper_5jiao.Text == "")
            {
                hopper_5jiao.Text = "1";
            }
            if (hopper_1jiao.Text == "")
            {
                hopper_1jiao.Text = "1";
            }

            textBox_Tips.Text = "";
            parentFrm.send_hopper_value("80", hopper_1yuan.Text, hopper_5jiao.Text, hopper_1jiao.Text);
        }

        private void hopper_1yuan_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void hopper_5jiao_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void hopper_1jiao_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void kick2_delay_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("60", kick2_delay.Text);
                }
            }

        }

        private void kick2_keep_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("61", kick2_keep.Text);
                }
            }

        }

        private void kick3_delay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("62", kick3_delay.Text);
                }
            }

        }

        private void kick3_keep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b' && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (e.KeyChar == '\r')
                {
                    parentFrm.send_value("63", kick3_keep.Text);
                }
            }

        }

        private void inhibitCoin_check_CheckedChanged(object sender, EventArgs e)
        {
            if (inhibitCoin_check.Checked == true)
            {
                parentFrm.send_value("64", "1");//拒收
            }
            else
            {
                parentFrm.send_value("64", "0");
            }
        }

        private void button_Kick1_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0010");//
        }

        private void button_kick2_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0011");//
        }

        private void button_kick3_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0012");//
        }

        private void button_kick4_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0013");//
        }

        private void pan_motor_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0014");//
        }

        private void belt_motor_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0015");//
        }

        private void hopper_status_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0016");//
        }

        private void empty_hopper_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0017");//
        }

        private void reset_hopper_Click(object sender, EventArgs e)
        {
            parentFrm.send_cmd_code("0018");//
        }



    }
}
