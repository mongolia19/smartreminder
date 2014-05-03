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
        public String  Match(String sentence,ArrayList sentence_list) 
        {
            
            String [] words=WordFilter(sentence);
            int wordNum = words.GetLength(0);
            for (int i = 0; i < sentence_list.Count; i++)
            {
                int hitNum=0;////record the num that hits the words in the sentence
                for (int j = 0; j < wordNum; j++)
                {
                    if (sentence_list[i].ToString().Contains(words[j]))
                    {
                        hitNum++;
                    }
                    if ((float)((float)hitNum/(float)wordNum)>=MatchPrecent)
                    {
                        return sentence_list[i].ToString();
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
