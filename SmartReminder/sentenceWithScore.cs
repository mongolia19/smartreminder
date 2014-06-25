using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartReminder
{
    class sentenceWithScore
    {
        public Double score;
        public String sentence;
        public sentenceWithScore(String sen, Double sco) 
        {
            sentence = sen;
            score = sco;
        
        }
    }
}
