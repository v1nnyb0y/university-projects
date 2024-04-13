using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Lab._1.TreeIDs.IDs;
using Lab._1.TreeIDs.IDs.Enum;
using Lab._1.TreeIDs.IDs.Types;

namespace Lab._1.TreeIDs
{
    public class FileParser : IEnumerable
    {
        public static readonly Regex SpaceRegex = new Regex("[ ]*");
        public static readonly Regex EndWord = new Regex("\\b" + SpaceRegex);


        public static readonly Regex VarRegex = new Regex("(bool|char|float|int|string)" + EndWord);
        public static readonly Regex ConstRegex = new Regex("const" + EndWord);
        public static readonly Regex ClassRegex = new Regex("class" + EndWord);
        public static readonly Regex TypesRegex = new Regex($"({ClassRegex}|{ConstRegex}|{VarRegex})" + EndWord);
        public static readonly Regex FindName = new Regex($"(?<={TypesRegex})[\\d\\w]+" + EndWord);

        public static readonly Regex SpecialSymbols =
            new Regex($"\\b({VarRegex}|{ConstRegex}|{ClassRegex}|(true|false)|(out|ref))" + EndWord);

        public static readonly Regex BoolSymbols = new Regex("(true|false)");
        public static readonly Regex DigitsSymbols = new Regex("(\\d+(.|,)\\d+|//d+)");
        public static readonly Regex CharSymbols = new Regex("'[\\s\\S]'");
        public static readonly Regex StringSymbols = new Regex("\"[\\s\\S]{1,}\"");

        public static readonly Regex AllSymbols =
            new Regex($"({BoolSymbols}|{DigitsSymbols}|{CharSymbols}|{StringSymbols})");

        public static readonly Regex MethodRegex = new Regex($"{VarRegex}{FindName}[(]([\\s\\S])*[)]");

        public static readonly Regex VariableRegex =
            new Regex($"(?<=[(]{SpaceRegex})([\\s\\S])*(?={SpaceRegex}[)])" + EndWord);

        public static readonly Regex VarElementRegex =
            new Regex($"(ref|out|{SpaceRegex})({SpaceRegex}){VarRegex}{FindName}({EndWord})");

        public static readonly Regex PrefixRegex = new Regex($"(out|ref|{SpaceRegex})" + EndWord);

        public FileParser(string _text) {
            SetText = _text;
        }

        private string SetText {
            get
            {
                return Text;
            }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(value.GetType().Name, "The file should be felt!");
                var can_use = new Regex($"[^\\d|\\w|{SpaceRegex}|,|;|\"]");
                if (!can_use.IsMatch(value)) throw new ArgumentException("The text includes non-allowed symbols!");

                Text = value.Replace("\n", " ").Replace("\t", " ").Trim();
                Lines = Text.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
                    .ToArray();
                Index = 0;
                CurrEl = default(ID);
            }
        }

        public IEnumerator GetEnumerator() {
            Index = 0;

            do {
                if (Parse()) yield return CurrEl;
                Index++;
            } while (Index + 1 < Lines.Length);

            Index = 0;
        }


        public bool Parse() {
            if (string.IsNullOrEmpty(CurrRow)) return false;

            if (ClassRegex.IsMatch(CurrRow)) {
                CurrEl = new Class(GetName(CurrRow));
                return true;
            }

            if (ConstRegex.IsMatch(CurrRow)) {
                CurrEl = new Constant(GetName(CurrRow), GetType(CurrRow), GetValue(CurrRow));
                return true;
            }

            if (MethodRegex.IsMatch(CurrRow)) {
                CurrEl = new Method(GetName(CurrRow), GetType(CurrRow), GetVariables(CurrRow));
                return true;
            }

            if (VarRegex.IsMatch(CurrRow)) {
                CurrEl = new Vars(GetName(CurrRow), GetType(CurrRow));
                return true;
            }

            return false;
        }

        public string GetName(string Row) {
            if (string.IsNullOrEmpty(Row)) return null;

            foreach (Match currMatch in FindName.Matches(Row))
                if (!SpecialSymbols.IsMatch(currMatch.Value))
                    return currMatch.Value.Trim();

            return "nothing";
        }

        public VarType GetType(string Row) {
            if (string.IsNullOrEmpty(Row)) return VarType.Default;

            var temp = VarRegex.Match(Row).Value.Trim().ToLower();

            switch (temp) {
                case "bool":
                    return VarType.bool_type;
                case "char":
                    return VarType.char_type;
                case "float":
                    return VarType.float_type;
                case "int":
                    return VarType.int_type;
                case "string":
                    return VarType.string_type;
            }

            return VarType.Default;
        }

        public string GetValue(string Row) {
            if (string.IsNullOrEmpty(Row)) return "nothing";

            //Row.Replace("\"", "").Replace("'", "").Trim();
            return AllSymbols.Match(Row).Value.Replace("\"", "").Replace("'", "").Trim();
        }

        public VarPrefixes GetPrefixes(string row) {
            if (string.IsNullOrEmpty(row)) return VarPrefixes.none;
            ;

            var temp = PrefixRegex.Match(row).Value.Trim().ToLower();
            switch (temp) {
                case "ref":
                    return VarPrefixes.param_ref;
                case "out":
                    return VarPrefixes.param_out;
                case "":
                    return VarPrefixes.param_val;
            }

            return VarPrefixes.none;
        }

        public List<Vars> GetVariables(string Row) {
            if (string.IsNullOrEmpty(Row)) return null;

            var list = new List<Vars>();
            Row = VariableRegex.Match(Row).Value.Trim();
            foreach (Match currMatch in VarElementRegex.Matches(Row))
                list.Add(new Vars(GetName(currMatch.Value), GetType(currMatch.Value), GetPrefixes(currMatch.Value)));

            return list;
        }

        #region Landscape

        private string Text;
        private string[] Lines { get; set; }
        private int Index { get; set; }
        private ID CurrEl { get; set; }

        private string CurrRow => Lines[Index];

        #endregion
    }
}