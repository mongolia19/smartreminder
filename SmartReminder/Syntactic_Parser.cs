using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartReminder
{
    public class Syntactic_Parser
    {
        static Regex SingleN = new Regex(@",n");
        static Regex j = new Regex(@",j");//short term
        static Regex r = new Regex(@",r");//pronoun
        static Regex i = new Regex(@",i");//idiom
        static Regex s = new Regex(@",s");//space

        static Regex nr = new Regex(@",nr");//name of person

        static Regex ns = new Regex(@",ns");//place
        static Regex nt = new Regex(@",nt");//group org
        static Regex nz = new Regex(@",nz");//special noun
        static Regex an = new Regex(@",an");//adjective noun
        
        public static Regex a = new Regex(@",a");//adjective


        public static Regex NP = new Regex(@"(" + j + "|" + r + "|" + i + "|" + s + "|" + nr + "|" + ns + "|" + nt + "|" + nz + "|" + an + "|" + SingleN+")+");
        
        

    }
}
