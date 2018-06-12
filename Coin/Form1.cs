using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.Net;
using Comzept.Genesis.NetworkTools;


namespace Coin
{
    public partial class Form1 : Form
    {
        public const int FRAME_SIZE = 512;

        System.Timers.Timer send_timer, save_timer;
        int send_state = 0;
        int respond_msg = 0xA0000;
        int send_lock = 0;
        int delay_ms = 0;
        CoinOP coinOp = new CoinOP();
        string fileName = null;
        List<byte> buffer = new List<byte>(4096);
        List<byte> record_buffer = new List<byte>(516*1024);
        List<string> send_buff = new List<string>(32);
        SerialPort s = new SerialPort();    //实例化一个串口对象，在前端控件中可以直接拖过来，但最好是在后端代码中写代码，这样复制到其他地方不会出错。s是一个串口的句柄  

        public void set_send_state(int state)
        {
            send_state = state;
        }
        
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;   //防止跨线程访问出错，好多地方会用到  

            打开串口.Text = "打开串口";
            int[] item = { 9600, 115200 };    //定义一个Item数组，遍历item中每一个变量a，增加到comboBox2的列表中  
            foreach (int a in item)
            {
                comboBox2.Items.Add(a.ToString());
            }

            comboBox2.SelectedItem = comboBox2.Items[1];    //默认为列表第二个变量  
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        private void Form1_Load(object sender, EventArgs e)   //窗体事件要先配置端口信息。  
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
            }
            coinOp.parentFrm = this;
            txtIP.Text = "192.168.1.250";

            send_timer = new System.Timers.Timer(500);
            send_timer.Elapsed += send_handler;//到达时间的时候执行事件；
            send_timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            send_timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件;
            save_timer = new System.Timers.Timer(3000);
            save_timer.Elapsed += save_handler;//到达时间的时候执行事件；
            save_timer.AutoReset = false;//设置是执行一次（false）还是一直执行(true)；
            if (MyApp.Default.download_mode == 0)
            {
                radioSerial.Checked = true;
            }
            else
            {
                radioNet.Checked = true;
            }
        }

        private void send_handler(object sender, EventArgs e)
        {
            if (send_lock == 1)
            {
                return;
            }     
            Interlocked.Exchange(ref send_lock, 1);
            if (send_buff.Count > 0)
            {
                byte[] sendData = Encoding.ASCII.GetBytes(send_buff[0]);
                send_buff.RemoveAt(0);
                this.SendData(sendData);
            }
            Interlocked.Exchange(ref send_lock, 0);
        }
        private void save_handler(object sender, EventArgs e)
        {
            byte[] recordBytes = new byte[record_buffer.Count];
            string filePath = "";
            record_buffer.CopyTo(0, recordBytes, 0, record_buffer.Count);
            if (!Directory.Exists(@"ExportData"))
            {
                Directory.CreateDirectory(@"ExportData");
            }
            if (coinOp.isHistoryRecord == 1)
            {
                filePath = "ExportData\\HistoryData " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".txt";
            }
            else
            {
                filePath = "ExportData\\DetectData " + DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ".txt";
            }
            System.IO.File.WriteAllBytes(filePath, recordBytes);
            exportRecord.Enabled = true;
            coinOp.Save_finished();
            record_buffer.RemoveRange(0, record_buffer.Count);
            buffer.RemoveRange(0, buffer.Count);
            set_send_state(0);
        }

        private bool open_com ()
        {
            try
            {
                if (!s.IsOpen)
                {
                    if (comboBox1.Items.Count > 0)
                    {
                        s.PortName = comboBox1.SelectedItem.ToString();
                        s.BaudRate = Convert.ToInt32(comboBox2.SelectedItem.ToString());
                        s.Open();
                        s.DataReceived += s_DataReceived;
                        打开串口.Text = "关闭串口";
                        //MessageBox.Show("串口已打开");  
                    }
                    else
                    {
                        MessageBox.Show("没有可用串口", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    s.Close();
                    s.DataReceived -= s_DataReceived;
                    打开串口.Text = "打开串口";
                }
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
                return false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)  
        {
            open_com ();
        }
        void s_DataReceived(object sender, SerialDataReceivedEventArgs e)   //数据接收事件，
        {
            int count = s.BytesToRead;
            byte[] buff = new byte[count];
            string respond_str = null;
            s.Read(buff, 0, count);
            //1.缓存数据
            if (send_state == 3)//导出记录
            {
                //this.AddData(Encoding.Default.GetBytes("."));//输出数据
                save_timer.Stop();
                record_buffer.AddRange(buff);
                save_timer.Start();
            }
            else
            {
                buffer.AddRange(buff);
            }
            if (buffer.Count > 0) //至少包含帧头（2字节）、长度（1字节）、校验位（1字节）；根据设计不同而不同
            {
                byte[] ReceiveBytes = new byte[buffer.Count];
                //得到完整的数据，复制到ReceiveBytes中进行校验
                buffer.CopyTo(0, ReceiveBytes, 0, buffer.Count);
                 
                respond_str = new ASCIIEncoding().GetString(ReceiveBytes);
                if (respond_str == "\b")
                    return;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                if (send_state == 0)
                {
                    if (respond_str.Length > 0)
                        respond_str = respond_str.Remove(respond_str.Length - 1);
                    if (respond_str == "\b \b\b:")
                    {
                    }
                    else
                    {
                        if (respond_str == "\b \b\b")
                        {
                            respond_str = richTextBox1.Text.Remove(richTextBox1.Text.Length - 1);
                            richTextBox1.Clear();
                            richTextBox1.AppendText(respond_str);
                        }
                        else
                        {
                            this.AddData(ReceiveBytes);//输出数据
                        }
                        buffer.RemoveRange(0, buffer.Count);
                    }
                }
                else if(send_state == 1)//发送代码状态
                {
                    if (respond_str == "OK\r\n")
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        Interlocked.Exchange(ref respond_msg, 0xA001);
                    }
                    else if (respond_str == "write start\r\n\r\nNick-Cmd:")/////执行其他代码，对数据进行处理。
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        Interlocked.Exchange(ref respond_msg, 0xA001);
                    }
                    else if(respond_str == "ERROR\r\n")/////执行其他代码，对数据进行处理。
                    {
                        buffer.RemoveRange(0, buffer.Count);
                        Interlocked.Exchange(ref respond_msg, 0xE001);
                    }
                }
                else
                {
                    if (respond_str.Length > 1)
                    {
                        if ((respond_str[respond_str.Length - 2] == '\r') && (respond_str[respond_str.Length - 1] == '\n'))
                        {
                            coinOp.CoinOP_SetText(respond_str);
                            buffer.RemoveRange(0, buffer.Count);
                        }
                    }    
                }
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data">字节数组</param>
        public void AddData(byte[] data){
            AddContent(new ASCIIEncoding().GetString(data));
        }

        /// <summary>
         /// 输入到显示区域
         /// </summary>
         /// <param name="content"></param>
         private void AddContent(string content)
         {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                /*
                string str_temp;

                str_temp = richTextBox1.Text;
                while (str_temp.Length + content.Length > 2000)
                {
                    int len = str_temp.IndexOf('\n');
                    str_temp = str_temp.Remove(0, len + 1);
                }
                richTextBox1.Clear();
                richTextBox1.AppendText(str_temp+content);*/
                richTextBox1.AppendText(content);

                richTextBox1.Focus();
                richTextBox1.Select(richTextBox1.TextLength, 0);
                richTextBox1.ScrollToCaret();
             }));
         }
        /// 发送数据
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="data"></param>
         public bool SendData(byte[] data)
         {
             if (!s.IsOpen)
             {
                 if (!open_com())
                     return false;
             }
             try
             {
                 s.Write(data, 0, data.Length);//发送数据
                 return true;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return false;
             }
         }
        private void 下载_Click(object sender, EventArgs e)
        {
            if (下载.Text == "下载")
            {

                Thread thread_send = null;
                //下载.Text = "停止";
                if (radioNet.Checked == true)
                {
                    thread_send = new Thread(this.DoNetSend);
                    thread_send.Start();
                }
                else if (radioSerial.Checked == true)
                {
                    thread_send = new Thread(this.DoSerialSend);
                    thread_send.Start();
                }
                else
                {
                    MessageBox.Show("请选择下载方式");
                }
            }
            else
            {
                Interlocked.Exchange(ref send_state, 0);
                下载.Text = "下载";
            }
        }
       #region -=[ CRC Array ]=-
       byte [] auchCRCHi=
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01,
            0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01,
            0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81,
            0x40

        };

        byte [] auchCRCLo =
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7, 0x05, 0xC5, 0xC4,
            0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD,
            0x1D, 0x1C, 0xDC, 0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32, 0x36, 0xF6, 0xF7,
            0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE,
            0x2E, 0x2F, 0xEF, 0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1, 0x63, 0xA3, 0xA2,
            0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB,
            0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0, 0x50, 0x90, 0x91,
            0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88,
            0x48, 0x49, 0x89, 0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83, 0x41, 0x81, 0x80,
            0x40
        };
       #endregion

        int CRC16(byte [] _data_buf, int len)
        {
            byte uchCRCHi = 0xff;
            byte uchCRCLo = 0xff;
            int uindex = 0;
            int data_index = 0;
	        while(len > 0){
                len--;
		        uindex = uchCRCHi ^ _data_buf[data_index++];
		        uchCRCHi = (byte)(uchCRCLo ^ auchCRCHi[uindex]);
		        uchCRCLo = auchCRCLo[uindex];
	        }
	        return (uchCRCHi<<8|uchCRCLo);
        }
        //! \brief method for check whecher string only consist of hex value
        static public Boolean IsHexNumber(String tHexString)
        {
            if (null == tHexString)
            {
                return false;
            }

            tHexString = tHexString.Trim().ToUpper();
            if ("" == tHexString)
            {
                return false;
            }

            foreach (Char tSymble in tHexString.ToCharArray())
            {
                if (!Char.IsNumber(tSymble))
                {
                    if ((tSymble < 'A') || (tSymble > 'F'))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public static string RecordParse(String tHexRecord)
        {
            //! check input
            if (null == tHexRecord)
            {
                return null;
            }

            tHexRecord = tHexRecord.Trim().ToUpper();
            if ("" == tHexRecord)
            {
                return null;
            }

            //! \brief check record head
            if (':' != tHexRecord[0])
            {
                return null;
            }
            if (!IsHexNumber(tHexRecord.Substring(1)))
            {
                return null;
            }

            Int32 tRecordLength = 0;
            Int32 tCheckSUM = 0;
            Int32 tLoadOffSet = 0;
            Byte tRecordType = 0;
            Byte[] tData = null;

            //! try to get record length
            try
            {
                tRecordLength = Int32.Parse(tHexRecord.Substring(1, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch (Exception Err)
            {
                Err.ToString();
                return null;
            }
            tCheckSUM += tRecordLength;

            //! check record string length
            if (tHexRecord.Length < (11 + tRecordLength * 2))
            {
                //! incomplete record
                return null;
            }

            //! try to get LoadOffset
            try
            {
                tLoadOffSet = UInt16.Parse(tHexRecord.Substring(3, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch (Exception Err)
            {
                Err.ToString();
                return null;
            }
            tCheckSUM += tLoadOffSet >> 8;
            tCheckSUM += tLoadOffSet & 0xFF;

            //! try to get record type
            try
            {
                tRecordType = Byte.Parse(tHexRecord.Substring(7, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            catch (Exception Err)
            {
                Err.ToString();
                return null;
            }

            tCheckSUM += tRecordType;

            //! get data byte
            tData = new Byte[tRecordLength];
            if (null == tData)
            {
                //! failed to allocated memory
                return null;
            }

            //! read all data bytes in a record
            for (Int32 n = 0; n < tData.Length; n++)
            {
                try
                {
                    tData[n] = Byte.Parse(tHexRecord.Substring(9 + n * 2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                catch (Exception Err)
                {
                    Err.ToString();
                    return null;
                }
                tCheckSUM += tData[n];
            }
            tCheckSUM %= 256;
            tCheckSUM = 0x100 - tCheckSUM;
            tCheckSUM &= 0xFF;
            return string.Format("{0:X2}", tCheckSUM);
        }
        public void send_value(string addr_str, string value_str)
        {
            int addr = int.Parse(addr_str) % 10000;
            string a_str = string.Format("{0:X4}", addr);
            int value = int.Parse(value_str) % 10000;
            string v_str = string.Format("{0:X4}", value);
            while (send_lock == 1) ;
            Interlocked.Exchange(ref send_lock, 1);
            send_buff.Add(":02" + a_str + "09" + v_str + RecordParse(":02" + a_str + "09" + v_str + "FF"));
            Interlocked.Exchange(ref send_lock, 0);
        }
        public void send_hopper_value(string addr_str, string value_str, string value_str1, string value_str2)
        {
            int addr = int.Parse(addr_str) % 10000;
            string a_str = string.Format("{0:X4}", addr);
            int value = int.Parse(value_str) % 256;
            int value1 = int.Parse(value_str1) % 256;
            int value2 = int.Parse(value_str2) % 256;
            string v_str = string.Format("{0:X2}", value);
            string v_str1 = string.Format("{0:X2}", value1);
            string v_str2 = string.Format("{0:X2}", value2);
            while (send_lock == 1) ;
            Interlocked.Exchange(ref send_lock, 1);
            send_buff.Add(":03" + a_str + "0A" + v_str + v_str1 + v_str2 + RecordParse(":03" + a_str + "0A" + v_str + v_str1 + v_str2 + "FF"));
            Interlocked.Exchange(ref send_lock, 0);
        }
        public void send_cmd_code(string str)
        {
            while (send_lock == 1);
            Interlocked.Exchange(ref send_lock, 1);
            send_buff.Add(":02000008" + str + RecordParse(":02000008" + str + "FF"));
            Interlocked.Exchange(ref send_lock, 0);
        }
        public void send_str(string str)
        {
            byte[] sendData = null;
            sendData = Encoding.ASCII.GetBytes(str);
            if (this.SendData(sendData))//如果发送数据成功
            {
            }
        }
        private void DoNetSend()
        {

            if (fileName == null)
            {
                MessageBox.Show("请先选择要下载的文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IPAddress ip;
            try
            {
                ip = IPAddress.Parse(txtIP.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("IP地址设置有误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtIP.Focus();
                return;
            }

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            下载.Enabled = false;
            try
            {
                TFTPClient tftp = new TFTPClient(txtIP.Text);
                tftp.Put("Coin.bin", fileName);
                //MessageBox.Show("PUT finished");
            }
            catch (TFTPClient.TFTPException tx)
            {
                MessageBox.Show("Exception in PUT: " + tx.ErrorMessage);
                Cursor.Current = Cursors.Default;
            }
            下载.Enabled = true;
            Cursor.Current = Cursors.Default;
            Application.DoEvents();
        }
        private void DoSerialSend()
        {
            if (fileName != null)
            {
                FileStream readStream = new FileStream(fileName, FileMode.Open);
                FileInfo fileInfo = new FileInfo(fileName);
                byte[] data = new byte[fileInfo.Length + 2];
                int length;
                int crc;

                下载.Enabled = false;
                send_str("version\r");
                Thread.Sleep(100);
                send_str("write reset\r");
                Thread.Sleep(100);
                Application.DoEvents();//重点，必须加上，否则父子窗体都假死
                //////////////////////////////////////////////////////////////
                if (fileInfo.Length <= 0)
                {
                    send_state = 0;
                    MessageBox.Show("文件大小为零", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    length = readStream.Read(data, 0, data.Length - 2);
                    crc = CRC16(data, data.Length - 2);//FRAME_SIZE字节为一帧数据
                    data[data.Length - 2] = (byte)((crc >> 8) & 0xff);//最后两字节为CRC校验码
                    data[data.Length - 1] = (byte)((crc) & 0xff);

                    send_state = 1;
                    Interlocked.Exchange(ref respond_msg, 0xA000);
                    send_str("write start\r");
                    Thread.Sleep(200);
                    while (respond_msg == 0xA000)//等待下位机响应
                    {
                        Thread.Sleep(5);
                        delay_ms++;
                        if (delay_ms > 400)
                        {
                            MessageBox.Show("清分机未响应", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }
                    }
                    send_state = 0;
                    if (delay_ms <= 400 )
                    {
                        if (this.SendData(data))//如果发送数据成功
                        {
                        }
                    }
                }
                delay_ms = 0;
                readStream.Close();
            }
            else
            {
                MessageBox.Show("请先选择要下载的文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                send_state = 0;
                下载.Text = "下载";
            }
            下载.Enabled = true;
        }

        private void 打开文件_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "bin文件|*.bin|所有文件|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog.FileName;
                filePath.Text = fileName;
            }
        }

        public void exit ()
        {
            Thread.Sleep(100);
            Application.Exit();
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (send_timer != null)
            {
                send_timer.Elapsed -= send_handler;
                send_timer.Stop();
                send_timer.Dispose();
                send_timer = null;
            }
            if (save_timer != null)
            {
                save_timer.Elapsed -= save_handler;
                save_timer.Stop();
                save_timer.Dispose();
                save_timer = null;
            }
            if (radioNet.Checked == true)
            {
                MyApp.Default.download_mode = 1;
            }
            else
            {
                MyApp.Default.download_mode = 0;
            }
            MyApp.Default.Save();
            
            Thread.Sleep(100);
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioSerial.Checked == true)
            {
                send_str("reset\r");
            }
            else if(radioNet.Checked == true)
            {
                IPAddress ip;
                try
                {
                    ip = IPAddress.Parse(txtIP.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("IP地址设置有误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIP.Focus();
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                Application.DoEvents();
                try
                {
                    TFTPClient tftp = new TFTPClient(txtIP.Text);
                    tftp.sendNetCmd("reset");
                    //MessageBox.Show("PUT finished");
                }
                catch (TFTPClient.TFTPException tx)
                {
                    MessageBox.Show("Exception in PUT: " + tx.ErrorMessage);
                    Cursor.Current = Cursors.Default;
                }
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            set_send_state(2);
            send_str("\r");
            this.Hide();
            coinOp.Show();
            send_cmd_code("0001");//同步数据

        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            byte[] sendData = new byte[1]; ;
            string str = null;
            if (send_state == 1)//如果正在发送代码则忽略
            {
                return;
            }
            if (e.KeyChar == '\b')
            {
                sendData[0] = 0x08;//退格键值
            }
            else
            { 
                str = "" + e.KeyChar;
                sendData = Encoding.ASCII.GetBytes(str);
            }
            if (this.SendData(sendData))//如果发送数据成功
            {
                //MessageBox.Show("发送指令完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void exportRecord_Click(object sender, EventArgs e)
        {
            send_str("version\r");
            Thread.Sleep(100);
            exportRecord.Enabled = false;
            set_send_state(3);
            send_cmd_code("0007");//导出数据
        }
    }
}