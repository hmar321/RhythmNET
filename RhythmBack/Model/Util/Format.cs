using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RhythmBack.Model.Util
{
    public class Format
    {
        public static string FormatearTexto(string texto)
        {
            var resultado = Regex.Replace(texto, @"[^\w\s]", "");
            resultado = Regex.Replace(resultado, @"\s+", "");
            resultado = resultado.ToLower();
            return resultado;
        }
    }
}
