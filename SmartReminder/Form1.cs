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
            return "...";
        
        
        }

        private void StartTimer(String reminder)
        {
            MatchCollection vMatchs = Regex.Matches(reminder, @"(\d+)");
            int[] vInts = new int[vMatchs.Count];
            for (int i = 0; i < vMatchs.Count; i++)
            {
                vInts[i] = int.Parse(vMatchs[i].Value);
            }
            int time_int = vInts[0];
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
      

        private void Form1_Load(object sender, EventArgs e)
        {
            
            matcher = new sentence_matcher();
            sentenceList = new ArrayList();
            sentenceList.Add("remind me wake");

        }

        private void talk_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void talk_button_Click(object sender, EventArgs e)
        {

            String Humanwords = talk_textBox.Text.Trim().ToLower();
            current_cmd = Humanwords;
            String cmd=matcher.Match(Humanwords, sentenceList);
            cmd_Handler(current_cmd);
        }
    }
}
