using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace _97Display.Mapper.Models.Infrastructure
{
    public static class StringExtensions
    {
        public static string ToUSAPhoneNumber(this string number)
        {
            if (number.StartsWith("+1"))
            {
                number = number.Substring(2);
            }

            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace(" ", "");
            number = number.Replace("-", "");
            return "+1" + number;
        }

        public static string ToUKPhoneNumber(this string number)
        {
            if (number.StartsWith("CC"))
            {
                number = number.Substring(3);
            }

            if (Conversions.ToString(number[0]) == "0")
            {
                number = number.Substring(1);
            }

            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace(" ", "");
            number = number.Replace("-", "");
            return "+44" + number;
        }

        public static string ToAustralianPhoneNumber(this string number)
        {
            if (number.StartsWith("+61"))
            {
                number = number.Substring(3);
            }

            if (Conversions.ToString(number[0]) == "0")
            {
                number = number.Substring(1);
            }

            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace(" ", "");
            number = number.Replace("-", "");
            return "+61" + number;
        }

        public static string GetCSV<T>(this List<T> list)
        {
            var sb = new StringBuilder();
            var propInfos = typeof(T).GetProperties();
            for (int i = 0, loopTo = propInfos.Length - 1; i <= loopTo; i++)
            {
                sb.Append(propInfos[i].Name);
                if (i < propInfos.Length - 1)
                {
                    sb.Append(",");
                }
            }

            sb.AppendLine();
            for (int i = 0, loopTo1 = list.Count - 1; i <= loopTo1; i++)
            {
                var item = list[i];
                for (int j = 0, loopTo2 = propInfos.Length - 1; j <= loopTo2; j++)
                {
                    var o = item.GetType().GetProperty(propInfos[j].Name).GetValue(item, null);
                    if (o is object)
                    {
                        string value = o.ToString();
                        if (value.Contains(","))
                        {

                            // first replace the " with ' in the html 
                            value = value.Replace("\"", "'");
                            value = string.Concat("\"", value, "\"");
                        }

                        if (value.Contains(@"\r"))
                        {
                            value = value.Replace(@"\r", " ");
                        }

                        if (value.Contains(@"\n"))
                        {
                            value = value.Replace(@"\n", " ");
                        }

                        sb.Append(value);
                    }

                    if (j < propInfos.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }

        public static string ToCallPhoneNumber(this string number, int countryId)
        {
            if (number.StartsWith("+61") | number.StartsWith("+44") | number.StartsWith("+27") | number.StartsWith("+60"))
            {
                number = number.Substring(3);
            }

            if (number.StartsWith("+1"))
            {
                number = number.Substring(2);
            }

            if (number.StartsWith("+353"))
            {
                number = number.Substring(4);
            }

            if (Conversions.ToString(number[0]) == "0")
            {
                number = number.Substring(1);
            }

            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace(" ", "");
            number = number.Replace("-", "");
            if (countryId == 1)
            {
                return "+1" + number;
            }
            else if (countryId == 2)
            {
                return "+1" + number;
            }
            else if (countryId == 3)
            {
                return "+44" + number;
            }
            else if (countryId == 4)
            {
                return "+61" + number;
            }
            else if (countryId == 5)
            {
                return "+353" + number;
            }
            else if (countryId == 6)
            {
                // Malaysia 
                return "+60" + number;
            }
            else if (countryId == 7)
            {
                // South Africa
                return "+27" + number;
            }

            return number;
        }
    }
}