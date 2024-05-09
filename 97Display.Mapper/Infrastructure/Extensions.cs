using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SwapIt.Mapper.Infrastructure
{
    public static class Extensions
    {
         

        public static string GetCSV<T>(this List<T> list)
        {
            StringBuilder sb = new StringBuilder();

            PropertyInfo[] propInfos = typeof(T).GetProperties();



            for (var i = 0; i <= propInfos.Length - 1; i++)
            {
                sb.Append(propInfos[i].Name);

                if ((i < propInfos.Length - 1))
                    sb.Append(",");
            }


            sb.AppendLine();


            for (var i = 0; i <= list.Count - 1; i++)
            {
                T item = list[i];
                for (var j = 0; j <= propInfos.Length - 1; j++)
                {
                    var o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if ((o != null))
                    {
                        var value = o.ToString();


                        if ((value.Contains(",")))
                        {

                            // first replace the " with ' in the html 
                            value = value.Replace("\"", "'");

                            value = string.Concat("\"", value, "\"");
                        }



                        if ((value.Contains(@"\r")))
                            value = value.Replace(@"\r", " ");

                        if ((value.Contains(@"\n")))
                            value = value.Replace(@"\n", " ");


                        sb.Append(value);
                    }

                    if ((j < propInfos.Length - 1))
                        sb.Append(",");
                }


                sb.AppendLine();
            }



            return sb.ToString();
        }

         public static string ExtractDomainFromURL(this string url)
        {
            Regex rg = new Regex(@"://(?<host>([a-z\d][-a-z\d]*[a-z\d]\.*[a-z][-a-z\d]+[a-z]))");



            if (rg.IsMatch(url))
                return rg.Match(url).Result("${host}");
            else
            {
                Regex rg2 = new Regex(@"://(?<host>([a-z\d][-a-z\d]*[a-z\d]\.*[a-z][-a-z\d]))");
                if (rg.IsMatch(url))
                    return rg.Match(url).Result("${host}");
            }
            return string.Empty;
        }


        
    }
}
