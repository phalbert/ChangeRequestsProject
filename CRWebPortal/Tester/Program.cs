using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string txt = "Update Students set Column=1";

            string re1 = ".*?"; // Non-greedy match on filler
            string re2 = "(?:[a-z][a-z0-9_]*)"; // Uninteresting: var
            string re3 = ".*?"; // Non-greedy match on filler
            string re4 = "((?:[a-z][a-z0-9_]*))";   // Variable Name 1
            string re5 = ".*?"; // Non-greedy match on filler
            string re6 = "(?:[a-z][a-z0-9_]*)"; // Uninteresting: var
            string re7 = ".*?"; // Non-greedy match on filler
            string re8 = "(?:[a-z][a-z0-9_]*)"; // Uninteresting: var
            string re9 = ".*?"; // Non-greedy match on filler
            string re10 = "((?:[a-z][a-z0-9_]*))";  // Variable Name 2
            string re11 = ".*?";    // Non-greedy match on filler
            string re12 = "((?:[a-z][a-z0-9_]*))";  // Variable Name 3
            string re13 = "(=)";    // Any Single Character 1
            string re14 = "(1)";    // Any Single Character 2

            Regex r = new Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9 + re10 + re11 + re12 + re13 + re14, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String var1 = m.Groups[1].ToString();
                String var2 = m.Groups[2].ToString();
                String var3 = m.Groups[3].ToString();
                String c1 = m.Groups[4].ToString();
                String c2 = m.Groups[5].ToString();
                Console.Write("(" + var1.ToString() + ")" + "(" + var2.ToString() + ")" + "(" + var3.ToString() + ")" + "(" + c1.ToString() + ")" + "(" + c2.ToString() + ")" + "\n");
            }
            Console.ReadLine();
        }
    }
}
