using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartReminder
{
    /// <summary>
    /// another thought we sort all the sentences by relating coefficent than get the most related ones 
    /// 
    /// we can use different pattern to deal with differnt kinds of questions:how what where etc.each kind of queston use one function 
    /// 
    /// we must implenment pattern self-extracting and self ranking and self envaluation
    /// </summary>
   public class abstractor
    {

       static void ChoiceSort(sentenceWithScore [] list,String keyS)
       {
           sentenceWithScore temp = null;
           int minIndex = 0;
           for (int i = 0; i < list.GetLength(0); i++)
           {
               minIndex = i;
               for (int j = i; j < list.GetLength(0); j++)
               {
                   //注意这里比较的是list[minIndex]
                   if (ImportanceValueCal( list[j].sentence,keyS) < ImportanceValueCal( list[minIndex].sentence,keyS))
                   {
                       minIndex = j;
                   }
               }
               temp = list[minIndex];
               list[minIndex] = list[i];
               list[i] = temp;
              // PrintList();
           }

       }

       public static int QuestionClassifierCN(String Q_CN)
       {

           if (Q_CN.Contains("什么")&&Q_CN.Contains("是")||Q_CN.Contains("什么")&&Q_CN.Contains("意思")||Q_CN.Contains("定义"))
           {
               return 0;//what
           }
           else if (Q_CN.Contains("如何")  || (Q_CN.Contains("怎么") &&(!Q_CN.Contains("回事")))|| Q_CN.Contains("咋")) 
           {
               return 1;//how
           
           }
           else if (Q_CN.Contains("为什么") || Q_CN.Contains("为啥") || (Q_CN.Contains("回事") && Q_CN.Contains("怎么")))
           {
               return 2;//why
           }
           else 
           {
               return 3;//other 
           }
       
       }
       public static String whatExtractor(ArrayList text,String keySentence)////input text to be extracted output answers to "what" 
       { 
           //////////
             ///first splite the passage
             ///then mark the sentences with scores 
             ///then sort them
             ///get the sentences with the highest scores
             //////
           ArrayList sentenceScore = new ArrayList();

           for (int i = 0; i < text.Count; i++)
           {
               sentenceScore.Add( new sentenceWithScore(((Q_n_A)text[i]).question, ImportanceValueCal(((Q_n_A)text[i]).question,keySentence)));

           }
           for (int j = 0; j < sentenceScore.Count; j++)
           {
               if (((sentenceWithScore)sentenceScore[j]).sentence.Contains('是')||((sentenceWithScore)sentenceScore[j]).sentence.Contains('为')||((sentenceWithScore)sentenceScore[j]).sentence.Contains('：'))
	            {
                    ((sentenceWithScore)sentenceScore[j]).score = 2*((sentenceWithScore)sentenceScore[j]).score;
	            }
                  
           }
           sentenceWithScore [] sArray=new sentenceWithScore[sentenceScore.Count];

           for (int k = 0; k < sentenceScore.Count; k++)
           {
               sArray[k]=(sentenceWithScore)sentenceScore[k];
           }
           ChoiceSort(sArray, keySentence);
           String extracted = null;
           for (int m = sArray.GetLength(0)-1; m> sArray.GetLength(0) / 2; m--)
           {
               if ((!sArray[m].sentence.Contains("什么")) && (!sArray[m].sentence.Contains("?")) && (!sArray[m].sentence.Contains("吗")) && (!sArray[m].sentence.Contains("是不是")) && (!sArray[m].sentence.Contains("是否")))
               {
                   extracted += sArray[m].sentence + ".";
               }
             

           }
           return extracted;
       }
       public static ArrayList GetContainingStrs(String KeyWords,ArrayList sentences)
       {

           ArrayList resultArr = new ArrayList();
           for (int i = 0; i < sentences.Count; i++)
           {
               if (sentences[i].ToString().Contains(KeyWords))
               {
                   resultArr.Add(sentences[i]);
               }
           }
           return resultArr;
       
       }
       public static ArrayList DefinationExtractor(String KeyWords,ArrayList Patterns ,ArrayList sentences)
 
       {
           ArrayList resultArr = new ArrayList();

           resultArr = GetContainingStrs(KeyWords, sentences);
           for (int i = 0; i < Patterns.Count; i++)
           {
               resultArr=GetContainingStrs(Patterns[i].ToString(), resultArr);
           }
           resultArr
      
       }

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
       public static String[] simpleSentenceSplitor(String passage) 
       {
          return passage.Split('.', '!', '?', ':', '-', '。', '！', '？', ';');


                
       }
       public static ArrayList GetRelatedSentencesFromExtendedSentenceList(ArrayList sList,ArrayList KeySList) 
       {
           ArrayList ExtractedsList2Return = new ArrayList();
           for (int i = 0; i < sList.Count; i++)
           {
               for (int j = 0; j < KeySList.Count; j++)
               {
                    Q_n_A OneSPair=(Q_n_A)sList[i];
                    Q_n_A OnekeyPair= (Q_n_A)KeySList[j];

                    if (ImportanceValueCal(OneSPair.question,OnekeyPair.question)>0.1)//we should tryh both key/tested and key/num of keys
                    {
                        ExtractedsList2Return.Add(OneSPair);
                        break;
                    }
               }         
           }

           return ExtractedsList2Return;
       }
       /// <summary>
       /// From the function below ,we get all the sentences(is in Q_n_A.question) in a arraylist 
       /// </summary>
       /// <param name="sList"></param>
       /// <param name="KeySentence"></param>
       /// <returns></returns>
      public static ArrayList GetAllSentenceContainingWordsInKeySentence(ArrayList sList, String KeySentence)
       {
           ArrayList RelatedS=new ArrayList();
           for (int i = 0; i < sList.Count; i++)
           {
               Q_n_A temp_q_n_a = (Q_n_A)sList[i];
               if (IfSentenceContainACharOfAnotherS( temp_q_n_a.question.ToString(),KeySentence))
               {
                   RelatedS.Add(sList[i]);
               }
           }
           return RelatedS;
       
       }
       static Boolean IfSentenceContainACharOfAnotherS(String st, String KeySentence)
       {
           char[] keys = KeySentence.ToCharArray();
           for (int i = 0; i < keys.GetLength(0); i++)
           {

               if (st.Contains(keys[i]))
               {
                   return true;
               }

           }
           return false;
       
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
           //down there we divide the key sentence count rather than the tested sentence length
           //we should see how many key words has been hit rather than the porpotion in the tested sentence
           return (hitNum *hitNum/ ((double)keys.GetLength(0)*(double)st.Length));       
       }


    }
}
