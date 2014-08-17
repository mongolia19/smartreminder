using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace SmartReminder
{
    class Extractor
    {



        public static ArrayList extract(String article)
        {
            ArrayList outputList = new ArrayList();
           String[] sentenceArray =article.Split('.', '!', '?');

           for (int i = 0; i < sentenceArray.GetLength(0); i++)
           {
               outputList.Add(sentenceArray[i]);
           }

           return outputList;
        }
        public static Boolean isASentence(String sec)
        {
            //Regex regex = new Regex(@"(\.)");
            //int count = regex.Matches(sec).Count;
            int num = System.Text.RegularExpressions.Regex.Matches(sec, @"(\.)").Count;
            num += System.Text.RegularExpressions.Regex.Matches(sec, @"(\。)").Count;
            num += System.Text.RegularExpressions.Regex.Matches(sec, @"(\？)").Count;
            num += System.Text.RegularExpressions.Regex.Matches(sec, @"(\；)").Count;
            num += System.Text.RegularExpressions.Regex.Matches(sec, @"(\！)").Count;

            if (num>1) 
            {
                return false;
            }
            else
            {
                return true;
            }
        
        }

        public static ArrayList GetTitles(ArrayList sections) 
        {
            ArrayList resultList = new ArrayList();
            for (int i = 0; i < sections.Count; i++)
            {
                String OneSection=sections[i].ToString();
                if (isASentence( OneSection))
                {
                    resultList.Add(OneSection);
                }


            }
       
            return resultList;
        
        }


        public static ArrayList GetSections(String article) 
        {
            ArrayList resultList = new ArrayList();

            String[] sentenceArray = article.Split('\r','\n');

            for (int i = 0; i < sentenceArray.GetLength(0); i++)
            {
                if (sentenceArray[i].Length != 0)
                {
                    resultList.Add(sentenceArray[i]);
                }
            }

            return resultList;
            
        
        }

    }
}
