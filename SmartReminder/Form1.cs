﻿using System;
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
    /// <summary>
    /// To Do :
    /// 1.First use tf--idf to extract sentences 
    /// 2.Tagging the extracted sentences ,store them to file
    /// 3.Extract words that are shared by different articles on the same subject
    /// </summary>
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
        static ArrayList DefinationPattern;
        
        static String[] DefinPattern = { "是的", "为的", "定义是", "所谓是" };
        static String[] MethodPattern = { "首先", "其次", "最后", "第", "然后" };
        static String[] ReasonPattern = { "因为", "所以", "因此", "原因" };
        static String[] LocationPattern = { "位于", "在旁", "坐落", "对过", "对面" };
        static String[] TimePattern = { "早", "晚", "在时候", "当时候" };
        static String[] Conclusion = { "总之", "综上", "概", "但是", "一句", "总结", "归根", "最后", "无论" };
        private float ExtractRatio=0.002f;
        
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
                PreProcessTools. get_string_array_into_arraylist(tempFileName, memory);
                
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
      
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //Memery= read_file("g:\\enstopword.txt");
            String input = "这些-r 数据-n 文件-n 貌似-v 是-v 有-v 一定-d 的-uj 格式-n 的-uj 我-r 刚-d 开始-v 也-d 试图-v 把-p 他们-r 当做-v 标准-n 的-uj xml-en 文档-n 来-v 处理-vn 因为-p 下载-v 包里还-nr 像模像样-l 的-uj 包含-v 了-v 一个-m SGMLDTD-en 的-uj 文件-n 但-c 老是-n 报-n 错-n 最终-d 发现-v 很多-m 的-uj 记录-v 格式-n 是-v 错误-n 的-uj 而且-c 错误-n 千奇百怪-i 所以-c 干脆-d 放弃-v 直接-ad 把-p 它们-r 全部-m 看做-v 文本-n 文件-n 来-v 处理-vn 得了-n";
            input = input.Replace(" ", "");
            input = input.Replace("-", ",");
            MatchCollection MC =Syntactic_Parser.NP_VP_NP.Matches(input);
            
            string s1 = MC[0].Value;
            string s2 = MC[1].Value;
            string left=Syntactic_Parser.NP.Replace(input, "");

            if (Syntactic_Parser.NP.IsMatch(input))
            {
                input = "success!";
            
            
            }

            left = Syntactic_Parser.VP.Replace(left, "");
            //Regex.IsMatch(input, Syntactic_Parser.NP);
            matcher = new sentence_matcher();
            sentenceList = new ArrayList();
            read_all_file_in_dir("D:\\TrainTxt\\",sentenceList);

            DefinationPattern = new ArrayList();


            DefinationPattern= PreProcessTools.AddArray2ArrayList(DefinPattern, DefinationPattern);
            DefinationPattern= PreProcessTools.AddArray2ArrayList(MethodPattern, DefinationPattern);
            DefinationPattern = PreProcessTools.AddArray2ArrayList(ReasonPattern, DefinationPattern);
            //DefinationPattern = PreProcessTools.AddArray2ArrayList(LocationPattern, DefinationPattern);
            //DefinationPattern = PreProcessTools.AddArray2ArrayList(TimePattern, DefinationPattern);
            DefinationPattern = PreProcessTools.AddArray2ArrayList(Conclusion, DefinationPattern);
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

            //String Humanwords = talk_textBox.Text.Trim().ToLower();
            //current_cmd = Humanwords;
            //answer=matcher.Match(Humanwords, sentenceList);

            String MultiDocResult = ExtractMulityDocs(talk_textBox.Text);

            LatestAnswertextBox.Text = MultiDocResult;

            //cmd_Handler(answer);

           
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
     

        public String ExtractMulityDocs(String question) 
        {

            ArrayList webList = new ArrayList();////store sentences search from the Web Page
            ArrayList webHyperLinks = new ArrayList();
            ArrayList abstractedList = new ArrayList();
            String extracted = "";
            String Humanwords = talk_textBox.Text.Trim().ToLower();

            String WebPageRaw = GetMainContentHelper.getDataFromUrl("http://www.sogou.com/web?query=" + question);
            webHyperLinks = GetMainContentHelper.GetHyperLinks(WebPageRaw);
            //extracted = GetMainContentHelper.GetMainContent(WebPageRaw);
            //String[] firstPageContent = read_page_from_web(extracted);
            //get_string_array_into_arraylist(firstPageContent, webList);
            int docNum=0;
            if (webHyperLinks.Count<10)
            {
                docNum = 8;
            }
            else
            {
                docNum = 11;
            }
            for (int i = 0; i < docNum; i++)
            {

                extracted+=ExtractOneUrl(webHyperLinks[i].ToString())+"\r\n";
                //WebPageRaw = GetMainContentHelper.getDataFromUrl(webHyperLinks[i].ToString());
                //extracted = GetMainContentHelper.GetMainContent(WebPageRaw);

                //String[] tempPageContent = read_page_from_web(extracted);

                //get_string_array_into_arraylist(tempPageContent, webList);
                //ArrayList extendedList = abstractor.GetAllSentenceContainingWordsInKeySentence(webList, current_cmd);
                //webList = abstractor.GetRelatedSentencesFromExtendedSentenceList(webList, extendedList);
                //webList = abstractor.GetAbstractedFromSentenceList(webList, current_cmd, 0.1);

            }


            return extracted;
           



        
        
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
            String[] firstPageContent =PreProcessTools. read_page_from_web(extracted);
            PreProcessTools.get_string_array_into_arraylist(firstPageContent, webList);

            for (int i = 0; i < webHyperLinks.Count/2; i++)
           
            {
                WebPageRaw = GetMainContentHelper.getDataFromUrl(webHyperLinks[i].ToString());
                 extracted= GetMainContentHelper.GetMainContent(WebPageRaw);

                 String[] tempPageContent = PreProcessTools.read_page_from_web(extracted);

                 PreProcessTools.get_string_array_into_arraylist(tempPageContent, webList);
                ArrayList extendedList= abstractor.GetAllSentenceContainingWordsInKeySentence(webList, current_cmd);
                 webList = abstractor.GetRelatedSentencesFromExtendedSentenceList(webList,extendedList );
                 //webList = abstractor.GetAbstractedFromSentenceList(webList, current_cmd, 0.1);

            }
           

            answer = matcher.Match(Humanwords, webList);
            answer = matcher.getRelatedSentences(answer, webList, 0.02);
            if (abstractor.QuestionClassifierCN(Humanwords)==0) //decide which kind of question this qeustion is
	        {
               answer= abstractor.whatExtractor(webList, Humanwords);
	        }  
            LatestAnswertextBox.Text = answer;

            cmd_Handler(answer);

        }

        public static String ExtractOneUrl(String url)
        {

            String WebPageRaw = GetMainContentHelper.getDataFromUrl(url);
            if (WebPageRaw.Length<=0)
            {
                return "";
            }
            Match TitleMatch = Regex.Match(WebPageRaw, "<title>([^<]*)</title>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            String title = TitleMatch.Groups[1].Value;

            title = PreProcessTools.RemovePunctuation(title);
            title = PreProcessTools.RemoveCNStopWords(title);

            //webHyperLinks = GetMainContentHelper.GetHyperLinks(WebPageRaw);
            String extracted = GetMainContentHelper.GetMainContent(WebPageRaw);

            if ( extracted.Length <= 0)
            {
                return "";
            }
            String SplitedWords = abstractor.Segment(extracted);

            // ArrayList sortedList = CharCollector.WordFreqStatistic(SplitedWords);
            Hashtable ht= PreProcessTools.Get_idf_table(SplitedWords);
            String TextForWordFreq = SplitedWords;
            String CleanedData = TextForWordFreq.Replace(".", "");
            CleanedData = PreProcessTools.cleanTaggedData(CleanedData);

            TextForWordFreq = PreProcessTools.RemovePunctuation(TextForWordFreq);
            TextForWordFreq = PreProcessTools.RemoveCNStopWords(TextForWordFreq);

            // Dictionary<Char, int> Testdic = CharCollector.FnCountWord(TextForWordFreq);
            Dictionary<String, int> Testdic = PreProcessTools.Stats(TextForWordFreq);

            ArrayList ArticleSentences = new ArrayList();
            String[] firstPageContent = PreProcessTools.read_page_from_web(extracted);
            PreProcessTools.get_string_array_into_arraylist(firstPageContent, ArticleSentences);
            string TaggedSentence=abstractor.Segment(((Q_n_A)ArticleSentences[11]).question);
            TaggedSentence = TaggedSentence.Replace(" ", "").Replace("-", ",");

            MatchCollection MC = Syntactic_Parser.NP_VP_NP.Matches(TaggedSentence);
            ArrayList keys = new ArrayList();
            /////////////////
            //////Remove english words and numbers in key words
            foreach (String k in Testdic.Keys)
            {

                keys.Add(k.ToString());
            }
          
            int MaxBase = Testdic[keys[0].ToString()];//the count of most frequect word
            Double[] weightArray = abstractor.weight(Testdic, ArticleSentences);
            Double r = 0.05;
            int[] selected = abstractor.GetHighestScoreIndex(weightArray, Convert.ToInt32(r * weightArray.GetLength(0)));


            ArrayList afterExtract = new ArrayList();

            for (int i = 0; i < r * ArticleSentences.Count; i++)
            {
                afterExtract = abstractor.DefinationExtractorReturnIndex(keys[i].ToString(), DefinationPattern, ArticleSentences, afterExtract);
                //afterExtract = abstractor.DefinationExtractor(keys[i].ToString(), DefinationPattern, ArticleSentences, afterExtract);

            }

            for (int i = 0; i < title.Length; i++)
            {
                afterExtract = abstractor.DefinationExtractorReturnIndex(title[i].ToString(), DefinationPattern, ArticleSentences, afterExtract);
            }

            int[] selectedMoreDetail = new int[afterExtract.Count];

            for (int i = 0; i < afterExtract.Count; i++)
            {
                selectedMoreDetail[i] = Convert.ToInt32(afterExtract[i].ToString());

            }




            int[] TotalArticle = new int[ArticleSentences.Count];

            TotalArticle = PreProcessTools.MarkIntArray(selected, TotalArticle);

            String HeadLineText = AddSentencesTogether(TotalArticle, ArticleSentences);

            TotalArticle = PreProcessTools.MarkIntArray(selectedMoreDetail, TotalArticle);

            ArrayList secs = Extractor.GetSections(extracted);
            secs = Extractor.GetTitles(secs);
            Extractor.RemoveDetails(secs, null);
           

            String DetailText = AddSentencesTogether(TotalArticle, ArticleSentences);


            //CreateHtml(title, LatestAnswertextBox.Text, "");
            return HeadLineText +"\r\n Detail:"+ DetailText;
        
        }


        private void ExtractButton_Click(object sender, EventArgs e)
        {

            LatestAnswertextBox.Text = ExtractOneUrl(webLinkTextBox.Text);
        


            CreateHtml("Extracted", LatestAnswertextBox.Text, "");


        }
        static void CreateHtml(String title,String body,String file_path)
        {
            FileStream fs = new FileStream("Extracted.html", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);

            String FirstPart = "<HTML><HEAD><meta http-equiv=Content-Type content=\"text/html;charset=gb2312\"><TITLE>";
            String MidPart = "</TITLE></HEAD><BODY bgcolor='green' style='color:black'>";
            String LastPart = "</BODY></HTML> ";
            String Whole = FirstPart + title + MidPart + body + LastPart;
                sw.Write(Whole);  //这里是写入的内容
                sw.Flush();
                sw.Close();
                fs.Close();

        }
        static String AddSentencesTogether(int[] IndexArray,ArrayList article) 
        {
            String tempStr="";
            for (int i = 0; i < IndexArray.GetLength(0); i++)
            {
                if (IndexArray[i] == 1)
                {
                    tempStr += ((Q_n_A)article[i]).question + ".\r\n";
                }

            }
            return tempStr;


        
        }
    }
}
