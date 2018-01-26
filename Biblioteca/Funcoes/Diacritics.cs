using System;
using System.Collections;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text.RegularExpressions;
using Biblioteca.Entidades;
using System.Text;
using System.Globalization;

namespace Biblioteca.Funcoes
{
    public static class Diacritics
    {
        public static string ReplaceDiacritics(this string source)
        {
            string sourceInFormD = source.Normalize(NormalizationForm.FormD);

            var output = new StringBuilder();
            foreach (char c in sourceInFormD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    output.Append(c);
            }

            return (output.ToString().Normalize(NormalizationForm.FormC).Replace(" ","_").Replace("/", "").Replace("\\", "").Replace(":", "").Replace("+", ""));
        }
    }
}
