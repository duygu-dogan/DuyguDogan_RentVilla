using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Infrastructure.Operations
{
    public static class NameOperation
    {
        public static string CharRegulatory(string name)
        {
           return name.Replace("\"", "")
                .Replace("'", "")
                .Replace(" ", "-")
                .Replace("?", "")
                .Replace("!", "")
                .Replace(".", "-")
                .Replace(",", "")
                .Replace(";", "")
                .Replace(":", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("/", "")
                .Replace("\\", "")
                .Replace("|", "")
                .Replace("*", "")
                .Replace("&", "")
                .Replace("%", "")
                .Replace("#", "")
                .Replace("+", "")
                .Replace("=", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("^", "")
                .Replace("~", "")
                .Replace("_", "")
                .Replace("@", "")
                .Replace("€", "")
                .Replace("¨", "")
                .Replace("$", "")
                .Replace("ç", "c")
                .Replace("Ç", "c")
                .Replace("ğ", "g")
                .Replace("Ğ", "g")
                .Replace("ı", "i")
                .Replace("İ", "i")
                .Replace("ö", "o")
                .Replace("Ö", "o")
                .Replace("ş", "s")
                .Replace("Ş", "s")
                .Replace("ü", "u")
                .Replace("Ü", "u")
                .Replace("ß", "")
                .Replace("â", "a")
                .Replace("î", "i")
                .Replace("û", "u")
                .Replace("ô", "o")
                .Replace("æ", "a");
        }
    }
}
