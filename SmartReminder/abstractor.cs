using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartReminder
{
   public class abstractor
    {
       public static ArrayList GetAbstractedFromSentenceList(ArrayList sList,String KeySentence,double threshold) 
       {
           ArrayList afterAbs = new ArrayList();//
           for (int i = 0; i < sList.Count; i++)
           {
               Q_n_A temp_q_n_a=(Q_n_A)sList[i];

               if (abstractor.ImportanceValueCal((temp_q_n_a).question.ToString(), KeySentence) >= threshold)
               {
                   afterAbs.Add(sList[i]);
               }
           }
           return afterAbs;
       
       }
       static double ImportanceValueCal(String st,String KeySentence )///return the value standing how familar this sentence is to the key sentence
       {
           char[] keys = KeySentence.ToCharArray();
           double totalcnt = st.ToCharArray().GetLength(0);
           double hitNum=0;
           for (int i = 0; i < keys.GetLength(0); i++)
           {

               if (st.Contains(keys[i]))
               {
                   hitNum++;
               }

           }

           return (hitNum / totalcnt);
       }


    }
}
