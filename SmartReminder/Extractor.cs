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
        public static String[] indecatingWords = { "首先", "其次", "最重要", "但是", "然而", "总之" }; 

        public static ArrayList getImportSentenceByIndecatingWords(String[] dic,ArrayList SentenceList) 
        {
            ArrayList resultList = new ArrayList();

            for (int i = 0; i < SentenceList.Count; i++)
            {
                for (int j = 0; j < dic.GetLength(0); j++)
                {
                    if (SentenceList[i].ToString().Contains(dic[j]))
                    {
                        resultList.Add(SentenceList[i]);

                    }
                }

            }

            return resultList;

        
        }
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


        private static int getWordNum(String str)////figure out how many english words in it
        {

            String pattern = @"(?<Character>[a-zA-Z])";
            Regex r = new Regex(pattern);
            MatchCollection mc = r.Matches(str);
            int strNum = 0;
            foreach (Match m in mc)
            {
                strNum++;
            }
            return strNum;

        }

        private static int getDigtNum(String str)////figure out how many digits in it
        {
            Regex reg = new Regex(@"[0-9][0-9,.]*");
            MatchCollection mc = reg.Matches(str);
            return mc.Count;

        
        }

        public static void RemoveDetails(ArrayList titleList,String [] pattern)
        {
            for (int i = 0; i < titleList.Count; i++)
			{
			    if ( getWordNum( titleList[i].ToString())>1||getDigtNum( titleList[i].ToString())>1)
	            {
		            titleList.RemoveAt(i);
                    if (i>0)
                    {
                        i--;
                    }
             
	            }

			}



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
