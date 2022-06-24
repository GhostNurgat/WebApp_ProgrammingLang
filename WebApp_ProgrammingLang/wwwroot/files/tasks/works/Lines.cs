using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Подготовка_1_Блок_
{
    public static class Lines
    {
        //время выполнения ~ 60 мин
        public static int LettersCount(string Input) => Input.Count(x => Char.IsLetter(x));
        public static string Censore(string Text) => Regex.Replace(Text, "редиска", "***", (RegexOptions.IgnoreCase | RegexOptions.CultureInvariant));
        public static List<char> GetAllDigits(string Text) => new HashSet<char>(Regex.Replace(Text, @"\D", "").ToCharArray()).ToArray().OrderBy(x => x).ToList();
        public static string InverseBinary(string BinValue) => BinValue.Replace("0", "!").Replace("1", "0").Replace("!", "1");
        public static bool CanUseInGame(string PreviousWord, string CurrentWord) => PreviousWord.Length == 0 ? true: (PreviousWord.EndsWith("ь") ? (CurrentWord.StartsWith(PreviousWord.Remove(0, PreviousWord.Length - 2).Remove(1,1))) : (CurrentWord.StartsWith(PreviousWord.Remove(0, PreviousWord.Length - 1))));
        public static string[] ListOfWords(string Text) => new HashSet<string>(Text.Split(new char[] { ' ', ',', '.', '!', '?' },StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLowerInvariant()).OrderBy(x => x)).ToArray();
    }
}
