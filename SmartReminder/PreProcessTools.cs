﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartReminder
{
    class PreProcessTools
    {

        static String[] PunctuationList = { "?", "。", "，", "：", "“", "“", "”", "？", "," ,"\r","\n"," ","、","/"};


        static String[] CNStopWords = { "我", "你", "的", "得", "这", "那", "他", "是", "为","在","了" ,"有","就","到","个","不","否","也","还","以","一","人","但"};

        public static ArrayList CombineArrayLists(ArrayList a, ArrayList b) 
        {
            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < b.Count; j++)
                {
                    if (!b[j].ToString().Contains(a[i].ToString()))
                    {
                        b.Add(a[i]);

                    }
                }

            }
            return b;
        
        }
        
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

        public static ArrayList AddArray2ArrayList(String[] StrArr, ArrayList list)
        {

            for (int i = 0; i < StrArr.GetLength(0); i++)
            {
                list.Add(StrArr[i]);
            }
            return list;

        }

        public static ArrayList RemoveSameObj(ArrayList former) 
        {
            ArrayList al_New=new ArrayList();
             
            foreach (object a in former)
            {
                if (!al_New.Contains(a))
                {
                    al_New.Add(a);
                }
            }
            return al_New;
        
        }


       
          public static ArrayList removeDuplicate(ArrayList list) {
            for (int i = 0; i < list.Count - 1; i++) 
            {
                for (int j = list.Count - 1; j > i; j--) 
                {
                    if (((Q_n_A)list[j]).question.Equals(((Q_n_A)list[i]).question))
                    {
                         list.RemoveAt(j);
                    }
                 }
            }
              return list;

//            System.out.println(list);
            }

    }
}
