using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SmartReminder
{
    class CharCollector
    {

        public static ArrayList getAllChars(ArrayList sentenceList)
        {
            ArrayList charsList = new ArrayList();
            charsList.Add((sentenceList[0].ToString())[0]);

            for (int i = 0; i < sentenceList.Count; i++)
            {

                AddDifferentCharToList(sentenceList[i].ToString(), charsList);
            }


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
