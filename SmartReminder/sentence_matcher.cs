using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace SmartReminder
{
    class sentence_matcher
    {
        String name;
       Double MatchPrecent=0.01;
       public sentence_matcher() 
        {
            SearchError = "I don't know!";
        }
         double sentence_relation_measure_cn(String sentenceA,String sentenceB) ///return the percentage of words of B showing up in A, that is how many mentioned in A 
        {
            Char[] Achar = sentenceA.Trim().Replace(" ","").ToCharArray();
            Char[] Bchar = sentenceB.Trim().ToCharArray();
            double countA = Achar.GetLength(0);
            double fitCount = 0;
            for (int i = 0; i < Bchar.GetLength(0); i++)
            {
                if (sentenceA.Contains(Bchar[i].ToString()))
                {
                    fitCount++;
                }
            }
            return fitCount / countA;
            
        }
         public String getRelatedSentences(String PointSentence, ArrayList sentence_list, double threshold) 
        {
            String Str2Return = null;
             PointSentence.Replace("\r","");
             PointSentence.Replace("\n", "");
            for (int i = 0; i < sentence_list.Count; i++)
            {
                Q_n_A q = (Q_n_A)sentence_list[i];
                //int hitNum = 0;////record the num that hits the words in the sentence
                String st = ((Q_n_A)sentence_list[i]).question;
                if (sentence_relation_measure_cn(PointSentence, st) >= threshold)
                {
                    Str2Return += st + ". ";
                }
                else 
                {
                
                }
            }
            return Str2Return;

        
        }
        public String  Match(String sentence,ArrayList sentence_list) 
        {
            
            String [] words=WordFilter(sentence);
            int wordNum= words.GetLength(0);
            String sentence2return = SearchError;
            double match_rate = 0;
            for (int i = 0; i < sentence_list.Count; i++)
            {
                Q_n_A q = (Q_n_A)sentence_list[i];
                int hitNum=0;////record the num that hits the words in the sentence
                int SentenceListWordCnt=((Q_n_A)sentence_list[i]).question.Split(' ').GetLength(0);
                for (int j = 0; j < wordNum; j++)
                {
                    if (q.question.Contains(words[j]))
                    {
                        hitNum++;
                    }
                    if (j==wordNum-1)
                    {
                        if ((float)((float)hitNum / (float)SentenceListWordCnt) >= MatchPrecent && (float)((float)hitNum / (float)SentenceListWordCnt) < 0.89 && (float)((float)hitNum / (float)SentenceListWordCnt) >= match_rate)
                        {
                            match_rate = (float)((float)hitNum / (float)SentenceListWordCnt);
                            sentence2return = q.question;
                           // return q.question;
                        }
                        
                    }
                   
                }   
            }
            return sentence2return;
        }
        String[] WordFilter(String sentence) 
        {
              return  sentence.Split(' ');
        
        }

        public string SearchError { get; set; }
    }
}
