using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartReminder
{
    class CharCollector
    {

        


        public static Dictionary<Char, int> FnCountWord(String text)
        {

            Dictionary<Char, int> charDict = new Dictionary<char, int>();
            for (int i = 0; i < text.Length; i++)
            {
                if (charDict.ContainsKey(text[i]) == true)
                {
                    charDict[text[i]]++;
                }
                else
                {
                    charDict.Add(text[i], 1);
                }
            }
            //int编号，KeyValuePair<Char, int>中char字符，int统计字符数量
           Dictionary<int, KeyValuePair<Char, int>> charID = new Dictionary<int, KeyValuePair<Char, int>>();
            int k = 0;
            foreach (KeyValuePair<Char, int> charInfo in charDict)
            {
                charID.Add(k,charInfo);
                k++;
            }
            //foreach (KeyValuePair<int, KeyValuePair<Char, int>> charInfo in charID)
            //{
            //    Console.WriteLine("id={0},char={1},amount={2}", charInfo.Key, charInfo.Value.Key,charInfo.Value.Value);
            //}
            charDict = (from entry in charDict
                       orderby entry.Value descending
                       select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            return charDict;
        }


        public static ArrayList getAllChars(ArrayList sentenceList)
        {
            ArrayList charsList = new ArrayList();
            charsList.Add((sentenceList[0].ToString())[0]);

            for (int i = 0; i < sentenceList.Count; i++)
            {

                AddDifferentCharToList(sentenceList[i].ToString(), charsList);
            }

            return charsList;

        }

        private static void AddDifferentCharToList(String CheckedStr, ArrayList char_list)
        {
            for (int i = 0; i < CheckedStr.Length; i++)
            {
                if (char_list.Contains(CheckedStr[i]))
                {

                }
                else
                {
                    char_list.Add(CheckedStr[i]);
                }
            }
        }

    }
}
