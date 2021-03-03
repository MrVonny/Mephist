using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mephist.Extensions
{
    public static class StringExtension
    {
        public static string Transliterate(this string str, bool onlyLettersAndDigits = false)
        {
            Dictionary<string, string> translitLetters = new Dictionary<string, string>();

            #region Letters
            translitLetters.Add("а", "a");
            translitLetters.Add("б", "b");
            translitLetters.Add("в", "v");
            translitLetters.Add("г", "g");
            translitLetters.Add("д", "d");
            translitLetters.Add("е", "e");
            translitLetters.Add("ё", "yo");
            translitLetters.Add("ж", "zh");
            translitLetters.Add("з", "z");
            translitLetters.Add("и", "i");
            translitLetters.Add("й", "j");
            translitLetters.Add("к", "k");
            translitLetters.Add("л", "l");
            translitLetters.Add("м", "m");
            translitLetters.Add("н", "n");
            translitLetters.Add("о", "o");
            translitLetters.Add("п", "p");
            translitLetters.Add("р", "r");
            translitLetters.Add("с", "s");
            translitLetters.Add("т", "t");
            translitLetters.Add("у", "u");
            translitLetters.Add("ф", "f");
            translitLetters.Add("х", "h");
            translitLetters.Add("ц", "c");
            translitLetters.Add("ч", "ch");
            translitLetters.Add("ш", "sh");
            translitLetters.Add("щ", "sch");
            translitLetters.Add("ъ", "j");
            translitLetters.Add("ы", "i");
            translitLetters.Add("ь", "j");
            translitLetters.Add("э", "e");
            translitLetters.Add("ю", "yu");
            translitLetters.Add("я", "ya");
            translitLetters.Add("А", "A");
            translitLetters.Add("Б", "B");
            translitLetters.Add("В", "V");
            translitLetters.Add("Г", "G");
            translitLetters.Add("Д", "D");
            translitLetters.Add("Е", "E");
            translitLetters.Add("Ё", "Yo");
            translitLetters.Add("Ж", "Zh");
            translitLetters.Add("З", "Z");
            translitLetters.Add("И", "I");
            translitLetters.Add("Й", "J");
            translitLetters.Add("К", "K");
            translitLetters.Add("Л", "L");
            translitLetters.Add("М", "M");
            translitLetters.Add("Н", "N");
            translitLetters.Add("О", "O");
            translitLetters.Add("П", "P");
            translitLetters.Add("Р", "R");
            translitLetters.Add("С", "S");
            translitLetters.Add("Т", "T");
            translitLetters.Add("У", "U");
            translitLetters.Add("Ф", "F");
            translitLetters.Add("Х", "H");
            translitLetters.Add("Ц", "C");
            translitLetters.Add("Ч", "Ch");
            translitLetters.Add("Ш", "Sh");
            translitLetters.Add("Щ", "Sch");
            translitLetters.Add("Ъ", "J");
            translitLetters.Add("Ы", "I");
            translitLetters.Add("Ь", "J");
            translitLetters.Add("Э", "E");
            translitLetters.Add("Ю", "Yu");
            translitLetters.Add("Я", "Ya");
            #endregion

            StringBuilder builder = new StringBuilder();
            foreach (var letter in str ?? throw new ArgumentNullException())

                if (translitLetters.ContainsKey(letter.ToString()))
                    builder.Append(translitLetters[letter.ToString()]);
                else
                {
                    if (onlyLettersAndDigits)
                        builder.Append(Char.IsLetter(letter)?letter:'_');
                    else
                        builder.Append(letter);
                }
                    
            return builder.ToString();

        }


    }
}
