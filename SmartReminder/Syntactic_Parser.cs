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

        //[a-zA-Z0-9_\u4e00-\u9fa5]+
        static String NP_Pattern = "(([0-9_\u4e00-\u9fa5]+)(,an|,ag|,a|,q|,m))*" + "([0-9_\u4e00-\u9fa5]+(" + j + "|" + r + "|" + i + "|" + s + "|" + nr + "|" + ns + "|" + nt + "|" + nz + "|" + an + "|" + SingleN + "|,vn|,l|,f|,uj))+";
        public static Regex NP = new Regex(@"(([0-9_\u4e00-\u9fa5]+)(,an|,ag|,a|,q|,m))*" + "([0-9_\u4e00-\u9fa5]+(" + j + "|" + r + "|" + i + "|" + s + "|" + nr + "|" + ns + "|" + nt + "|" + nz + "|" + an + "|" + SingleN + "|,vn|,l|,f|,uj))+");
        const String VP_Pattern = "([0-9_\u4e00-\u9fa5]+(,ad|,dg|,vg|,v|,vd|,d))*([0-9_\u4e00-\u9fa5]+(,c))*([0-9_\u4e00-\u9fa5]+(,ad|,dg|,vg|,v|,vd|,d))+([0-9_\u4e00-\u9fa5]+(,bei|,ba|,p|,z|,vd))*";
        public static Regex VP = new Regex(@"([0-9_\u4e00-\u9fa5]+(,ad|,dg|,vg|,v|,vd|,d))*([0-9_\u4e00-\u9fa5]+(,c))*([0-9_\u4e00-\u9fa5]+(,ad|,dg|,vg|,v|,vd|,d))+([0-9_\u4e00-\u9fa5]+(,bei|,ba|,p|,z|,vd))*");

        public static Regex NP_VP_NP = new Regex(@NP_Pattern + VP_Pattern + NP_Pattern);

    }
}
