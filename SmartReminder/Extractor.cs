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


        public static ArrayList GetTitles(String article) 
        {
            


        
        }
    }
}
