using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;

namespace DiceRoller
{
    //Custom exception created for this specific application.
    class DiceException : ApplicationException
    {
        private string errorType;

        public DiceException(string message)
            : base(message)
        { }
        public DiceException(string message, string error_type)
            : base(message)
        {
            errorType = error_type;
        }

        public string ErrorType
        {
            get { return this.errorType; }
            set { this.errorType = value; }
        }
    }

    //Dice class, this handles all the math for rolling dice.
    class Dice
    {
        private List<List<int> > roll_results;
        private static RNGCryptoServiceProvider gen;
        private static Regex dice_parse;
        public Dice()
        {
            roll_results = new List<List<int>>();
            gen = new RNGCryptoServiceProvider();
            dice_parse = new Regex("\\b(?<dice>(\\d)+)d(?<sides>\\d+)(r(?<reroll>\\d+)|d(?<drop>\\d+)|(?<open>o))*\\b",
                RegexOptions.Compiled ^ RegexOptions.RightToLeft);
        }
        //Rolls a single die of n sides, while allowing for rerolls for a result less than or equal to m;
        private int Roll(int n, int m)
        {
            byte[] temp = new byte[4];
            gen.GetBytes(temp);
            return m + 1 + (int)(BitConverter.ToUInt32(temp,0) % (n - m));
        }

        public int Roll(int dice, int sides, int reroll, int drop, bool OpenRoll)
        {
            List<int> rolls = new List<int>();
            int index;
            if (drop >= dice)
            {
                throw new DiceException("The number of dice to drop cannot be greater than the number of dice rolled", "Dice Format Error");
            }
            if (OpenRoll && (dice != 1 || sides != 100))
            {
                throw new DiceException("Only a single d100 can be used with open roll option", "Dice Format Error");
            }
            if (reroll >= sides)
            {
                throw new DiceException("Reroll value must be less than the number of sides on the die", "Dice Format Error");
            }

            for (index = 0; index < dice; index++)
            {
                rolls.Add(Roll(sides, reroll));
                if (OpenRoll && rolls[rolls.Count - 1] > 88 + rolls.Count)
                {
                    index--;
                }
            }
            for (index = 0; index < drop; index++)
            {
                rolls.Remove(rolls.Min());
            }

            roll_results.Add(rolls);
            return rolls.Sum();
        }

        public string RollAll( string str )
        {
            foreach (Match m in dice_parse.Matches(str))
            {
                str = str.Remove(m.Index, m.Value.Length).Insert(m.Index, Roll(int.Parse(m.Groups["dice"].Value),
                    int.Parse(m.Groups["sides"].Value),int.Parse("0" + m.Groups["reroll"].Value),
                    int.Parse("0" + m.Groups["drop"].Value),(m.Groups["open"].Length == 1)).ToString());
            }
            roll_results.Reverse();
            return str;
        }

        public bool IsDice(string dice)
        {
            return dice_parse.IsMatch(dice);
        }

        public string RollResults
        {
            get
            {
                string str = "";
                foreach (List<int> result in roll_results)
                {
                    str += "[";
                    for (int i = 0; i < result.Count; i++)
                    {
                        str += result[i].ToString();
                        if (i != result.Count - 1)
                        {
                            str += ",";
                        }
                    }
                    str += "]";
                }
                return str;
            }
        }

        public void Clear()
        {
            roll_results.Clear();
        }

        public int Roll(string str)
        {
            List<int> rolls = new List<int>();

            if (!dice_parse.IsMatch(str))
            {
                throw new DiceException("Dice expression is unrecognized", "Dice Format Error");
            }
            Match m = dice_parse.Match(str);
            return Roll(int.Parse(m.Groups["dice"].Value), int.Parse(m.Groups["sides"].Value),
                int.Parse("0" + m.Groups["reroll"].Value), int.Parse("0" + m.Groups["drop"].Value),
                (m.Groups["open"].Length == 1));
        }
    }
}
