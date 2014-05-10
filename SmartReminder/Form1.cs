using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Timers;
using System.Text.RegularExpressions;
using System.IO;

namespace SmartReminder
{
    public partial class Form1 : Form
    {
        public int AlertTime = 0;
        sentence_matcher matcher;
        ArrayList sentenceList;
        String current_cmd;
        private System.Timers.Timer timerClock = new System.Timers.Timer();
        int alert_time;
        String[] Memery;
        public Form1()
        {
            InitializeComponent();
        }
        String cmd_Handler(String cmd)
        {
            if (cmd.Contains("remind me"))//////remind issues
            {
                StartTimer(cmd);
                return "finish";
            }
            else
            {
                respond_textBox.Text += cmd+" \r\n";
                return cmd;
            }
            
        
        
        }

        private void StartTimer(String reminder)
        {
            MatchCollection vMatchs = Regex.Matches(reminder, @"(\d+)");
            int[] vInts = new int[vMatchs.Count];
            int time_int;
            if (vInts.GetLength(0) > 0)
            {


                for (int i = 0; i < vMatchs.Count; i++)
                {
                    vInts[i] = int.Parse(vMatchs[i].Value);
                }
                 time_int = vInts[0];
            }
            else
            {
                time_int = 5;
            }
            
            String[] splittedwords = reminder.ToLower().Split(' ');
            String[] at_pattern = { "at"};
            String[] min_later_pattern = { "minute","later" };
            String[] hour_later_pattern = { "hour", "later" };
            String[] evening_pattern = { "evening" };
            if (time_pattern_match(reminder, at_pattern, 1)!=-1)
            {
                this.timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
                this.timerClock.Interval = 1000;
                
                int hour_now = DateTime.Now.Minute;
                if (hour_now>=time_int)
                {
                    respond_textBox.Text += "wrong time,sorry!\n\r";
                }
                else
                {
                    alert_time = (time_int-hour_now)*60;
                }
            }
            if (time_pattern_match(reminder, hour_later_pattern, 2) != -1)
            {
                this.timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
                this.timerClock.Interval = 1000;
              
                int hour_now = DateTime.Now.Hour;
              
                alert_time = time_int*60*60;
                this.timerClock.Enabled = true;
            }

            if (time_pattern_match(reminder, min_later_pattern, 3) != -1)
            {
                this.timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
                this.timerClock.Interval = 1000;
                int hour_now = DateTime.Now.Minute;
               
                    alert_time = time_int*60;
                    this.timerClock.Enabled = true;
                
            }


            this.timerClock.Enabled = true;

            
            respond_textBox.Text += "CPU:ok,alerm set!"+"\r\n";
        }
        private int time_pattern_match(String reminder,String [] pattern,int pattern_id) 
        {   
            int cnt= pattern.GetLength(0);
            for (int i = 0; i < pattern.GetLength(0); i++)
			{
			 if(reminder.Contains  (pattern[i]))
             {
                cnt--;
             }

			}
            if (cnt>0)
            {
                return -1;
            }
            else
            {
                return pattern_id;
            }
        
        }
        public void OnTimer(Object source, ElapsedEventArgs e)
        {
            try
            {
                if (alert_time > 0)
                {
                    alert_time--;
                }
                else
                {
                    timerClock.Enabled = false;
                   
                    MessageBox.Show("Play Sound");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("OnTimer(): " + ex.Message);
            }
        }

        public String[] read_file(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);//"d:\\enstopword.txt"d:\\books\\37913.txt

            StreamReader m_streamReader = new StreamReader(fs);

            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            //string arry = "";
            string strLine;
            ArrayList tempArray = new ArrayList();
            do
            {
                
                strLine = m_streamReader.ReadLine();
                if (strLine != null&&!strLine.Equals(""))
                {
                    tempArray.Add(strLine.Trim());
                }


            } while (strLine != null);

            String[] asm_file = new String[tempArray.Count];

            for (int i = 0; i < tempArray.Count; i++)
            {
                if (tempArray[i].ToString() == "")
                {
                    tempArray.RemoveAt(i);
                }
            }

            for (int i = 0; i < tempArray.Count; i++)
            {
                asm_file[i] = Convert.ToString(tempArray[i]);
            }

            m_streamReader.Close();
            m_streamReader.Dispose();
            fs.Close();
            fs.Dispose();

            return asm_file;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Memery= read_file("g:\\enstopword.txt");
            
            matcher = new sentence_matcher();
            sentenceList = new ArrayList();
           

            for (int i = 0; i < Memery.GetLength(0); i++)
            {
                Q_n_A tempq = new Q_n_A(Memery[i].ToString(), "");

                sentenceList.Add(tempq);

  
            }
            sentenceList.Add(new Q_n_A("remind me wake",""));
            sentenceList.Add( new Q_n_A("the weather is sunny",""));
        }

        private void talk_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void talk_button_Click(object sender, EventArgs e)
        {

            String Humanwords = talk_textBox.Text.Trim().ToLower();
            current_cmd = Humanwords;
            String cmd=matcher.Match(Humanwords, sentenceList);
            cmd_Handler(cmd);

            cmd = matcher.Match(cmd, sentenceList);
            cmd_Handler(cmd);
        }

        private void respond_textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
