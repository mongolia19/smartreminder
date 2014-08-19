using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartReminder
{
    class PreProcessTools
    {

        static String[] PunctuationList = { "?", "。", "，", "：", "“", "“", "”", "？", "," ,"\r","\n"," ","、","/"};


        static String[] CNStopWords = { "我", "你", "的", "得", "这", "那", "他", "是", "为","在","了" ,"有","就","到","个","不","否"};
        
        public static String RemovePunctuation(String RawText)//Remove charactors such as , . ? 
        {
            for (int i = 0; i < PunctuationList.GetLength(0); i++)
            {
                if (RawText.Contains(PunctuationList[i]))
                {
                    RawText = RawText.Replace(PunctuationList[i],"");

                }
            }


            return RawText;
        
        
        }


        public static String RemoveCNStopWords(String RawText)//Remove charactors such as , . ? 
        {
            for (int i = 0; i < CNStopWords.GetLength(0); i++)
            {
                if (RawText.Contains(CNStopWords[i]))
                {
                    RawText = RawText.Replace(CNStopWords[i], "");

                }
            }


            return RawText;


        }




    }
}
