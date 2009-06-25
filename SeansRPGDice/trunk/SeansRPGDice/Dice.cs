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
        private string roll_results;
        private static RNGCryptoServiceProvider gen;
        private static Regex dice_parse;

        //initialize variables
        public Dice()
        {
            roll_results = "";
            gen = new RNGCryptoServiceProvider();
            dice_parse = new Regex("^(?<dice>\\d+)d(?<sides>\\d+)(r(?<reroll>\\d+)|d(?<drop>\\d+)|(?<open>o))*$",RegexOptions.Compiled);
        }

        //Rolls a single die of n sides, while allowing for rerolls for a result less than or equal to m;
        private int Roll(int n, int m)
        {
            byte[] temp = new byte[4];
            gen.GetBytes(temp);
            return m + 1 + (int)(BitConverter.ToUInt32(temp,0) % (n - m));
        }

        private void Parse(string str, ref int dice, ref int sides, ref int reroll, ref int drop)
        {
        }

        //check if the variable input fits the format of dice
        public bool IsDice(string dice)
        {
            return dice_parse.IsMatch(dice);
        }

        //return the results of the last rolls
        public string RollResults
        {
            get { return roll_results; }
        }

        //clear the results of the last rolls
        public void Clear()
        {
            roll_results = "";
        }


        public int Roll(string str)
        {
            int dice, sides, reroll, index, drop, total = 0;
            bool OpenRoll = false;
            List<int> rolls = new List<int>();

            if (!dice_parse.IsMatch(str))
            {
                throw new DiceException("Dice expression is unrecognized", "Dice Format Error");
            }
            Match m = dice_parse.Match(str);
            dice = int.Parse(m.Groups["dice"].Value);
            sides = int.Parse(m.Groups["sides"].Value);
            drop = int.Parse("0" + m.Groups["drop"].Value);
            reroll = int.Parse("0" + m.Groups["reroll"].Value);
            OpenRoll = (m.Groups["open"].Length == 1);
            if (drop >= dice)
            {
                throw new DiceException("The number of dice to drop cannot be greater than the number of dice rolled", "Dice Format Error");
            }
            if ( OpenRoll && (dice != 1 || sides != 100))
            {
                throw new DiceException("Only a single d100 can be used with open roll option", "Dice Format Error");
            }
            if (reroll >= sides)
            {
                throw new DiceException("Reroll value must be less than the number of sides on the die", "Dice Format Error");
            }

            /*index = str.IndexOf('d');
            dice = int.Parse(str.Substring(0, index));
            str = str.Substring(index + 1);
            for (index = 0 ; index < str.Length && char.IsDigit(str[index]); index++)
            { }
            sides = int.Parse(str.Substring(0, index));
            str = str.Substring(index);

            while (str.Length > 0)
            {
                switch (str[0])
                {
                    case 'r':
                        for (index = 1; index < str.Length && char.IsDigit(str[index]); index++)
                        { }
                        reroll = int.Parse(str.Substring(1, index - 1));
                        //ensure that we aren't rerolling infinitely
                        if (reroll >= sides)
                        {
                            throw new DiceException("Reroll value must be less than the number of sides on the die", "Dice Format Error");
                        }
                        str = str.Substring(index);
                        break;
                    case 'o':
                        if (dice != 1 || sides != 100)
                        {
                            throw new DiceException("Only a single d100 can be used with open roll option", "Dice Format Error");
                        }
                        str = str.Substring(1);
                        OpenRoll = true;
                        break;
                    case 'd':
                        for (index = 1; index < str.Length && char.IsDigit(str[index]); index++)
                        { }
                        drop = int.Parse(str.Substring(1, index - 1));
                        if (drop >= dice)
                        {
                            throw new DiceException("", "Dice Format Error");
                        }
                        str = str.Substring(index);
                        break;
                    default:
                        throw new DiceException("Invalid modifier on dice roll: " + str[0] + " is unrecognized", "Dice Format Error");
                }
            }*/
            for (index = 0; index < dice ; index++)
            {
                rolls.Add( Roll(sides, reroll) );
                if (OpenRoll && rolls[rolls.Count - 1] > 88 + rolls.Count)
                {
                    index--;
                }
            }
            for (index = 0; index < drop ; index++)
            {
                rolls.Remove(rolls.Min());
            }
            roll_results += "[";
            for (index = 0; index < rolls.Count - 1 ; index++)
            {
                roll_results += rolls[index] + ",";
                total += rolls[index];
            }
            roll_results += rolls[index] + "]";
            total += rolls[index];
            return total;
        }
    }
}
