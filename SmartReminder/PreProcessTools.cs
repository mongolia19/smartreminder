using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace SmartReminder
{
    class PreProcessTools
    {



        static String[] PunctuationList = { "?", "。", "，", "：", "“", "“", "”", "？", "," ,"\r","\n"," ","、","/"};


        static String[] CNStopWords = { "我", "你", "的", "得", "这", "那", "他", "是", "为", "在", "了", "有", "就", "到", "个", "不", "否", "也", "还", "以", "一", "人", "但", "要", "把", "用", "靠", "中", "出现", "来", "它", "们", "最", "可", "于", "和", "等" };

        public static string cleanTaggedData(string TaggedData)
        { 
            Regex tags=new Regex(@"-([A-Za-z]+)-([0-9]+) ");

            string cleaned= tags.Replace(TaggedData, " ");
            return cleaned;
        }

        public static void get_string_array_into_arraylist(String[] string_array, ArrayList array)
        {

            for (int i = 0; i < string_array.GetLength(0); i++)
            {
                if (string_array[i].Length > 0)
                {
                    Q_n_A tempq = new Q_n_A(string_array[i].ToString(), "");

                    array.Add(tempq);
                }


            }

        }

        public static String[] read_page_from_web(String PageContent)
        {
            Regex regex = new Regex("(\r\n)+");
            PageContent = regex.Replace(PageContent, ".");

            String[] asm_file = PageContent.Split('.', '!', '?', ':', '-', '。', '！', '？', ';');
            // = new String[tempArray.Count];




            return asm_file;

        }

        public static Hashtable Get_idf_table(String article) 
        {
            Regex pattern = new Regex(@"(-(([a-zA-Z]+)|@)-)");

            Hashtable ht = new Hashtable();

            String [] ArticalArray= (article).Split(' ');

            
            for (int i = 0; i < ArticalArray.Length; i++)
            {
                ArticalArray[i] = pattern.Replace(ArticalArray[i], ",");
            }

            for (int i = 0; i < ArticalArray.Length; i++)
            {
                String[] splitted = ArticalArray[i].Split(',');
                if ((!ht.ContainsKey(splitted[0].ToString()))&&(splitted[0].Length>0))
	            {
                    ht.Add(splitted[0].ToString(), splitted[1].ToString());
	            } 
            }
            //ArrayList ArticleAL=new ArrayList();
           // get_string_array_into_arraylist(ArticalArray, ArticleAL);
            return ht;
        }
      public static Dictionary<String,int> Stats(String article)
        {
            MatchCollection mc = Regex.Matches(article, @"\b\w+\b");
           // Response.Write("总数：" + mc.Count + "<br/>");
            Dictionary<String, int> dct = new Dictionary<String, int>(mc.Count);
            foreach (Match mt in mc)
            {
                if (dct.ContainsKey(mt.Value))
                {
                    dct[mt.Value]++;
                }
                else
                {
                    dct.Add(mt.Value, 1);
                }
            }
            //dct = dct.OrderBy(i => i.Key).ToDictionary(c => c.Key, c => c.Value);

            dct = (from entry in dct
                        orderby entry.Value descending
                        select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            return dct;

            ////输出结果
            //foreach (KeyValuePair<String, int> de in dct)
            //{
            //    Response.Write(de.Key + ":" + de.Value + "<br/>");
            //    //换成Writeline输出，如果是WinForm
            //}
        }

        public static int[] MarkIntArray(int[] marker,int [] marked) //Mark the selected sentences
        {

            for (int i = 0; i < marker.GetLength(0); i++)
            {
                if (marker[i]<=marked.GetLength(0)-1)
                {
                    marked[marker[i]]=1;
                }
            }

            return marked;
        
        } 


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
                    RawText = RawText.Replace(PunctuationList[i]," ");

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
