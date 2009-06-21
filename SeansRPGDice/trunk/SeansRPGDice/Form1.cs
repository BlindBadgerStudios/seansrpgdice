﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DiceRoller
{
    public partial class MainForm : Form
    {
        /* TODO:
         * More error checking (reroll+open roll looping)
         * ensure there is only one '=' character
         */
        private Random myRand;          //random generator
        private string lastinput;       //track the last formula used
        private string rolls;           //string listing all the rolls
        private int openRollLimit;      //the sliding limit for open rolls

        private class DiceException : ApplicationException
        {
            private string errorType;

            public DiceException(string message)
                : base(message)
            { }
            public DiceException(string message, string error_type) : base(message)
            {
                errorType = error_type;
            }

            public string ErrorType
            {
                get { return this.errorType; }
                set { this.errorType = value; }
            }
        }

        public MainForm()
        {
            InitializeComponent();
            myRand = new Random();
            lastinput = "";
            rolls = "";
            openRollLimit = 90;
        }

        //roll dice as specified and output the result, replacing reroll values and below with higher numbers
        private int Roll(int dice, int sides, int reroll)
        {
            int temp, total = 0;

            //starting character for roll display
            rolls += '[';

            //ensure that we aren't rerolling infinitely
            if (reroll > sides)
            {
                throw new DiceException("Reroll value must be less than the number of sides on the die","Dice Format Error");
            }

            //ensure we have a valid dice size
            if (sides < 2)
            {
                throw new DiceException("Dice must have atleast 2 sides", "Dice Format Error");
            }

            //for each dice, get a value
            for (int i = 0; i < dice; i++)
            {
                //randomly generate a number between the reroll value and the max value
                temp = myRand.Next(reroll + 1, sides + 1);
                rolls += temp.ToString();

                if (i == dice - 1)
                {
                    rolls += ']';
                }
                else
                {
                    rolls += ',';
                }

                total += temp;

                //open roll check (supports only 1d100 rolls)
                if (dice == 1 && sides == 100 && chkOpenRoll.Checked)
                {
                    //continue open rolling until the result no longer qualifies
                    if (temp >= openRollLimit)
                    {
                        //slide the open roll lower limit up until we reach 100
                        if (openRollLimit < 100)
                        {
                            openRollLimit++;
                        }

                        //replace the end of the string with a comma instead of a bracket
                        rolls.Insert(rolls.Length - 1, ",");

                        //roll again and add the result
                        total += Roll(1, 100);
                    }
                }
            }

            return total;
        }

        //handle dice rolling without rerolls enabled
        private int Roll(int dice, int sides)
        {
            return Roll(dice, sides, 0);
        }

        //handle open rolls for anima system
        private int OpenRoll(int dice, int sides)
        {
            return dice;
        }

        //if the string has parinthesis, calculate the totals and output the resulting string
        private string Calc(string str)
        {
            string temp;
            int begin, end, first, second, index;

            //handle parinthesis
            if (str.Contains('('))
            {
                //put the marker at the beginning of the parinthesis
                index = str.IndexOf('(');

                if (char.IsDigit(str[index - 1]))
                {
                    str = str.Insert(index++, "*");
                }

                //recurse to calculate what is within the parinthesis first
                str = str.Substring(0, index) + Calc(str.Substring(index + 1));
            }

            //ensure the end parinthesis exists
            if (!str.Contains(')'))
            {
                throw new DiceException("Parenthesis do not match", "Invalid Formula");
            }

            //set marker at the end parenthesis
            index = str.IndexOf(')');

            //get just what is within the parenthesis
            temp = str.Substring(0, index);

            //handle n(formula)...multiply
            if (index == str.Length - 1)
            {
                str = "";
            }
            else
            {
                if (char.IsDigit(str[index + 1]))
                    str = str.Insert(index + 1, "*");

                str = str.Substring(index + 1);
            }

            //multiply
            while (temp.Contains('*'))
            {
                index = temp.IndexOf('*');

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin] == '-'); end++)
                { }

                first = int.Parse(temp.Substring(begin, index - begin));
                second = int.Parse(temp.Substring(index + 1, end - index));
                temp = temp.Remove(begin, end - begin + 1);
                temp = temp.Insert(begin, (first * second).ToString());
            }

            //divide
            while (temp.Contains('/'))
            {
                index = temp.IndexOf('/');

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin] == '-'); end++)
                { }

                first = int.Parse(temp.Substring(begin, index - begin));
                second = int.Parse(temp.Substring(index + 1, end - index));
                temp = temp.Remove(begin, end - begin + 1);
                temp = temp.Insert(begin, (first / second).ToString());
            }

            //add (which also handles subtract)
            while (temp.Contains('+'))
            {
                index = temp.IndexOf('+');

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin] == '-'); end++)
                { }

                first = int.Parse(temp.Substring(begin, index - begin));
                second = int.Parse(temp.Substring(index + 1, end - index));
                temp = temp.Remove(begin, end - begin + 1);
                temp = temp.Insert(begin, (first + second).ToString());
            }

            return temp + str;
        }

        //main parsing/calculating function
        private string Calculate(string input)
        {
            //remove spaces and change to lowercase to make it easier/more predictable to work with
            string str = input.ToLowerInvariant().Replace(" ", "");
            string label = "";
            int sides, dice;
            //hard coded starting value the lower limit of open rolls
            openRollLimit = 90;

            //don't process if there's nothing in the formula
            if (str.Length == 0)
            {
                return "";
            }

            //handle labels
            if (input.Contains('='))
            {
                //parse out the label into it's own variable
                label = str.Substring(0, str.IndexOf('='));
                //remove the label from the string so that the roller functions don't get confused
                str = str.Substring(str.IndexOf('=') + 1);
                if (str.Contains(label))
                {
                    throw new DiceException("Formula can not reference itself", "Invalid Formula");
                }
                //store formula in gridFormulas
                gridFormulas.Rows.Add(label,str);
                return "Stored: " + input;
            }

            //clear the rolls variable
            rolls = "";

            //parse through the string, character by character
            for (int i = 0; i < str.Length; i++)
            {
                int begin, end;
                if (str[i] != 'd' || !char.IsDigit(str, i + 1) || !char.IsDigit(str, i - 1))
                {
                    continue;
                }
                //get to the beginning of the dice variable
                for (begin = i - 1; begin > 0 && char.IsDigit(str[begin - 1]); begin--)
                { }

                //get to the end of the dice variable
                for (end = i + 1; end < str.Length - 1 && char.IsDigit(str[end + 1]); end++)
                { }

                //set variable to how many dice we want to roll
                dice = int.Parse(str.Substring(begin, i - begin));

                //set variable to the size of the dice we want to roll
                sides = int.Parse(str.Substring(i + 1, end - i));

                if (end < str.Length - 1 && char.IsLetter(str[end + 1]))
                {
                    i = end + 1;
                    switch (str[i])
                    {
                        case 'r':
                            for (end = i + 1; end < str.Length && char.IsDigit(str[end]); end++)
                            { }
                            --end;
                            if (end == i)
                            {
                                //remove the string so that it won't get parsed again
                                str = str.Remove(begin, end - begin + 1);
                                str = str.Insert(begin, Roll(dice, sides,1).ToString());
                            }
                            else
                            {
                                i = int.Parse(str.Substring(i+1,end-i));
                                //remove the string so that it won't get parsed again
                                str = str.Remove(begin, end - begin + 1);
                                str = str.Insert(begin, Roll(dice, sides, i).ToString());
                            }
                            break;
                        case 'o':

                            break;
                        default:
                            throw new DiceException("Unrecognized modifier on dice roll", "Dice Format Error");
                    }
                }
                else
                {
                    //remove the string so that it won't get parsed again
                    str = str.Remove(begin, end - begin + 1);

                    str = str.Insert(begin, Roll(dice, sides).ToString());
                }
                i = begin;
            }

            //handle variables called in the formula
            for (int i = 0; i < gridFormulas.Rows.Count; i++)
            {
                //if a variable name exists in the formula and the label is not the same as the value
                if (str.ToLower().Contains(gridFormulas.Rows[i].Cells[0].Value.ToString().ToLower()) &&
                    gridFormulas.Rows[i].Cells[0].Value.ToString().ToLower() != gridFormulas.Rows[i].Cells[1].Value.ToString().ToLower())
                {
                    //replace it with the string stored for that label
                    str = str.Replace(gridFormulas.Rows[i].Cells[0].Value.ToString(), gridFormulas.Rows[i].Cells[1].Value.ToString());
                }
            }

            string stupid_string = str;
            //convert subtraction operations into additions of negative numbers
            for (int i = 1; i < stupid_string.Length; i++)
            {
                if (stupid_string[i] == '-' && stupid_string[i - 1] != '*' &&
                    stupid_string[i - 1] != '/' && stupid_string[i - 1] != '+')
                {
                    stupid_string = stupid_string.Insert(i++, "+");
                }
            }

            //run the calculations and assemble the string for output
            str += " = " + Calc(stupid_string + ")");
            input += ": " + rolls + "->" + System.Environment.NewLine + str;

            return input;
        }

        //add data to roll history datagridview and highlight if an open roll occured
        private void addHistory(string input)
        {
            gridHistory.Rows.Add(input);

            //check for open rolls
            if (openRollLimit > 90)
            {
                //highlight the row
                gridHistory.Rows[gridHistory.Rows.Count - 1].Cells[0].Style.BackColor = Color.Green;
            }
        }

        //handle special keys in the input
        private void txtInput_OnEnter(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:      //enter key
                    try
                    {
                        string str = Calculate(txtInput.Text);
                        if (str.Length == 0)
                            break;
                        //run calculation and add it to display
                        addHistory(str);
                        //scroll to newly added value
                        gridHistory.FirstDisplayedScrollingRowIndex = gridHistory.RowCount - 1;
                        //save this formula for rerolling
                        lastinput = txtInput.Text;
                        //clear input
                        txtInput.Clear();
                    }
                    catch (DiceException ex)
                    {
                        MessageBox.Show(ex.Message, ex.ErrorType);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                default:
                    break;
            }
        }

        //exit the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //show about messagebox
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //insert about box here
            MessageBox.Show("This program created by Sean Wells." + System.Environment.NewLine 
                + "Send feedback to <codemonk84@gmail.com>" + System.Environment.NewLine
                + System.Environment.NewLine 
                + "Extra Thanks to..." + System.Environment.NewLine 
                + "Aaron Biegalski" + System.Environment.NewLine
                + "Trevor Hoagland" + System.Environment.NewLine
                + "Thomas Wild" + System.Environment.NewLine 
                , "About...");
        }

        //display instructions on how to use the app
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calculations are performed in the order it is typed, each modifier being performed on the whole before it." + 
                System.Environment.NewLine + 
                System.Environment.NewLine + 
                "Input similar to the following examples:" + System.Environment.NewLine + 
                "2d6" + System.Environment.NewLine +
                "6d6+24-10/2 (modifiers are supported)" + System.Environment.NewLine +
                "10d4+2d6-5+3/2 (multiple dice sets are supported)" + System.Environment.NewLine +
                "1d100*1.1 (decimals are supported)" + System.Environment.NewLine + 
                System.Environment.NewLine + 
                "You may add as many modifiers and dice sets as you wish.", "Instructions");
        }

        //clear the history
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gridHistory.Rows.Clear();
        }

        //show the double clicked saved formula
        private void gridFormulas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridFormulas.Rows.Count == 0)
            {
                return;
            }
            txtInput.Text = gridFormulas.SelectedRows[0].Cells[1].Value.ToString();
            txtInput.Focus();
        }

        //save string to file
        private void saveTxtFile(string saveType, string text2save)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "*.txt";
            sfd.Title = "Save " + saveType + " File";
            sfd.FileName = saveType.Replace(" ", "");
            sfd.Filter = "Txt Files|*.txt|All Files|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = File.CreateText(sfd.FileName);
                sw.Write(text2save);
                sw.Close();
            }
            sfd.Dispose();
        }

        //save the roll history to a file
        private void rollHistoryToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string saveme = "Roll History for: " + System.DateTime.Now + System.Environment.NewLine;
            for (int i = 0; i < gridHistory.Rows.Count; i++)
            {
                //use newline as a delimeter between records
                saveme += gridHistory.Rows[i].Cells[0].Value.ToString().Replace(System.Environment.NewLine,"|") + System.Environment.NewLine;
            }

            saveTxtFile("Roll History", saveme);
        }

        //save the stored formulas to a file
        private void rollHistoryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string saveme = "Formulas: " + System.DateTime.Now + System.Environment.NewLine;
            for (int i = 0; i < gridFormulas.Rows.Count; i++)
            {
                //use newline as delimeter between records
                saveme += gridFormulas.Rows[i].Cells[0].Value.ToString() + "," + gridFormulas.Rows[i].Cells[1].Value.ToString() + System.Environment.NewLine;
            }

            saveTxtFile("Formulas", saveme);
        }

        //load data from textfile
        private void loadTxtFile(string openType)
        {
            List<string> input = new List<string>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.txt";
            ofd.Title = "Open File...";
            ofd.FileName = openType.Replace(" ", "");
            ofd.Filter = "Txt Files|*.txt|All Files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = File.OpenRead(ofd.FileName);
                StreamReader sr = new StreamReader(fs);
                input.Add(sr.ReadLine());
                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }
                sr.Close();

                for(int i=0;i<input.Count;i++)
                {
                    //replace "|" with System.Environment.NewLine
                    input[i] = input[i].Replace("|", System.Environment.NewLine);
                }


            }

            //parse input
            switch (openType)
            {
                case "Formulas":
                    if (!input[0].StartsWith("Formulas:"))
                    {
                        throw new DiceException("This file was not created by this program", "Invalid File format");
                    }
                    else
                    {
                        gridFormulas.Rows.Clear();
                        input.RemoveAt(0);
                        foreach (string s in input)
                        {
                            gridFormulas.Rows.Add(s.Substring(0,s.IndexOf(',')),s.Substring(s.IndexOf(',') +1));
                        }
                    }
                    break;
                case "Roll History":
                    if (!input[0].StartsWith("Roll History for:"))
                    {
                        throw new DiceException("This file was not created by this program", "Invalid File format");
                    }
                    else
                    {
                        gridHistory.Rows.Clear();
                        foreach (string s in input)
                        {
                            gridHistory.Rows.Add(s);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        //load roll history file
        private void rollHistoryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                loadTxtFile("Roll History");
            }
            catch (DiceException ex)
            {
                MessageBox.Show(ex.Message, ex.ErrorType);
            }
            catch (Exception ex)
            {
                MessageBox.Show( ex.Message);
            }
        }

        //load formula file
        private void formulasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                loadTxtFile("Formulas");
            }
            catch (DiceException ex)
            {
                MessageBox.Show(ex.Message, ex.ErrorType);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //run this on every keystroke because keypress events don't parse arrow keys
        private void txtTest(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up && lastinput.Length > 0)
            {
                txtInput.Text = lastinput;
            }
        }
    }
}
