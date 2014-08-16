using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

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


        public static ArrayList GetTitles(ArrayList sections) 
        {


            return null;
        
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
