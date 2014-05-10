using System;
using System.Collections.Generic;

using System.Text;
using System.Collections;

namespace SmartReminder
{
    class sentence_matcher
    {
        String name;
       Double MatchPrecent=0.5;
       public sentence_matcher() 
        {
            SearchError = "I don't know!";
        }
        public String  Match(String sentence,ArrayList sentence_list) 
        {
            
            String [] words=WordFilter(sentence);
            int wordNum= words.GetLength(0);
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
                        if ((float)((float)hitNum / (float)SentenceListWordCnt) >= MatchPrecent && (hitNum!=SentenceListWordCnt))
                        {
                            return q.question;
                        }
                        
                    }
                   
                }   
            }
            return SearchError;
        }
        String[] WordFilter(String sentence) 
        {
              return  sentence.Split(' ');
        
        }

        public string SearchError { get; set; }
    }
}
