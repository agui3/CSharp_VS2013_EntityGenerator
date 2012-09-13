using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;

namespace EntityGenerator
{
    public class StringHelper
    {
        public static string GetCleanText(string str)
        {
            char[] ch = str.ToCharArray();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ch.Length; i++)
            {
                //如果是\"
                if ((int)ch[i] == 92)
                    continue;

                sb.Append(ch[i]);
            }

            return sb.ToString();
        }
    }
}