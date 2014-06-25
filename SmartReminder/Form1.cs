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
        public String answer;
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
            if (cmd != null)
            {
                if (cmd.Contains("remind me"))//////remind issues
                {
                    StartTimer(cmd);
                    return "finish";
                }
                else
                {
                    respond_textBox.Text += cmd + " \r\n";
                    return cmd;
                }
            }
            else 
            {
                return "I have no idea!";
            }
        
        
        }
        private void read_all_file_in_dir( String dir ,ArrayList memory)
        {
            string[] fn = Directory.GetFiles(dir);
            foreach (string s in fn)
            {
                String [] tempFileName = read_file( s);
                get_string_array_into_arraylist(tempFileName, memory);
                
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
            else if (time_pattern_match(reminder, hour_later_pattern, 2) != -1)
            {
                this.timerClock.Elapsed += new ElapsedEventHandler(OnTimer);
                this.timerClock.Interval = 1000;
              
                int hour_now = DateTime.Now.Hour;
              
                alert_time = time_int*60*60;
                this.timerClock.Enabled = true;
            }
            else if (time_pattern_match(reminder, min_later_pattern, 3) != -1)
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
            //string strLine;
            ArrayList tempArray = new ArrayList();
            String strAll= m_streamReader.ReadToEnd();

            String[] asm_file = strAll.Split('.', '!', '?');
               // = new String[tempArray.Count];

            m_streamReader.Close();
            m_streamReader.Dispose();
            fs.Close();
            fs.Dispose();

            return asm_file;

        }
         void get_string_array_into_arraylist(String [] string_array,ArrayList array)
        {

            for (int i = 0; i < string_array.GetLength(0); i++)
            {
                Q_n_A tempq = new Q_n_A(string_array[i].ToString(), "");

                array.Add(tempq);

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Memery= read_file("g:\\enstopword.txt");
            
            matcher = new sentence_matcher();
            sentenceList = new ArrayList();
            read_all_file_in_dir("D:\\TrainTxt\\",sentenceList);

            /*            for (int i = 0; i < Memery.GetLength(0); i++)
            {
                Q_n_A tempq = new Q_n_A(Memery[i].ToString(), "");
                sentenceList.Add(tempq);
            } */

            //sentenceList.Add(new Q_n_A("remind me wake",""));
           // sentenceList.Add( new Q_n_A("the weather is sunny",""));
        }

        private void talk_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void talk_button_Click(object sender, EventArgs e)
        {

            String Humanwords = talk_textBox.Text.Trim().ToLower();
            current_cmd = Humanwords;
            answer=matcher.Match(Humanwords, sentenceList);
            LatestAnswertextBox.Text = answer;

            cmd_Handler(answer);

           
        }

        private void respond_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            SetAnswerForQuestion(current_cmd,LatestAnswertextBox.Text,sentenceList);

        }

        private void SetAnswerForQuestion(String q,String a,ArrayList list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Q_n_A tqa=(Q_n_A)list[i];
                if (tqa.question.Equals(q))
                {
                    tqa.anser = a;
                    list[i] = tqa;
                    return;
                }

            }
        }
        String[] read_page_from_web(String PageContent) 
        {
            Regex regex = new Regex("(\r\n)+");
            PageContent = regex.Replace(PageContent, ".");

            String[] asm_file = PageContent.Split('.', '!', '?', ':', '-', '。', '！', '？', ';');
            // = new String[tempArray.Count];
            

          

            return asm_file;
        
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList webList = new ArrayList();////store sentences search from the Web Page
            ArrayList webHyperLinks = new ArrayList();
            ArrayList abstractedList = new ArrayList();
            String extracted = null;
            String Humanwords = talk_textBox.Text.Trim().ToLower();
            current_cmd = Humanwords;
            String WebPageRaw=GetMainContentHelper.getDataFromUrl("http://www.baidu.com/s?wd=" + current_cmd);
            webHyperLinks = GetMainContentHelper.GetHyperLinks(WebPageRaw);
            extracted = GetMainContentHelper.GetMainContent(WebPageRaw);
            String[] firstPageContent = read_page_from_web(extracted);
            get_string_array_into_arraylist(firstPageContent, webList);

            for (int i = 0; i < webHyperLinks.Count/2; i++)
           
            {
                WebPageRaw = GetMainContentHelper.getDataFromUrl(webHyperLinks[i].ToString());
                 extracted= GetMainContentHelper.GetMainContent(WebPageRaw);
             
                 String[] tempPageContent = read_page_from_web(extracted);
                  
                 get_string_array_into_arraylist(tempPageContent, webList);
                ArrayList extendedList= abstractor.GetAllSentenceContainingWordsInKeySentence(webList, current_cmd);
                 webList = abstractor.GetRelatedSentencesFromExtendedSentenceList(webList,extendedList );
                 //webList = abstractor.GetAbstractedFromSentenceList(webList, current_cmd, 0.1);

            }
           

            answer = matcher.Match(Humanwords, webList);

            answer = matcher.getRelatedSentences(answer, webList, 0.02);
            
            LatestAnswertextBox.Text = answer;

            cmd_Handler(answer);

        }
    }
}
