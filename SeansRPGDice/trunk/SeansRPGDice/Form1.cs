using System;
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
         * More error checking
         * ensure there is only one '=' character
         * Fix Highlighting text for open rolls, etc.
         * Debug new code
         */
        private string lastinput;       //track the last formula used
        private Dice dice;

        public MainForm()
        {
            InitializeComponent();
            lastinput = "";
            dice = new Dice();
        }

        public static double Evaluate(string expression)
        {
            return (double)new System.Xml.XPath.XPathDocument
            (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
            (string.Format("number({0})", new
            System.Text.RegularExpressions.Regex(@"([\+\-\*])").Replace(expression, " ${1} ")
.Replace("/", " div ").Replace("%", " mod ")));
        }

        //if the string has parenthesis, calculate the totals and output the resulting string
        /*private string Calc(string str)
        {
            string temp;
            int begin, end, first, second, index;

            //handle parenthesis
            if (str.Contains('('))
            {
                //put the marker at the beginning of the parenthesis
                index = str.IndexOf('(');

                if (char.IsDigit(str[index - 1]))
                {
                    str = str.Insert(index++, "*");
                }

                //recurse to calculate what is within the parenthesis first
                str = str.Substring(0, index) + Calc(str.Substring(index + 1));
            }

            //ensure the end parenthesis exists
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

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin - 1] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin + 1] == '-'); end++)
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

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin - 1] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin + 1] == '-'); end++)
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

                for (begin = index - 1; begin > 0 && (char.IsDigit(temp[begin - 1]) || temp[begin - 1] == '-'); begin--)
                { }
                for (end = index + 1; end < temp.Length - 1 && (char.IsDigit(temp[end + 1]) || temp[begin + 1] == '-'); end++)
                { }

                first = int.Parse(temp.Substring(begin, index - begin));
                second = int.Parse(temp.Substring(index + 1, end - index));
                temp = temp.Remove(begin, end - begin + 1);
                temp = temp.Insert(begin, (first + second).ToString());
            }

            return temp + str;
        }*/

        //main parsing/calculating function
        private string Calculate(string input)
        {
            //remove spaces and change to lowercase to make it easier/more predictable to work with
            string str = input.ToLower().Replace(" ", "");
            string label = "";
            char[] operators = {'+','*','/','(',')','-','%'};
            int start, end, temp;

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

            dice.Clear();

            //handle variables called in the formula
            for (int i = 0; i < gridFormulas.Rows.Count; i++)
            {
                //if a variable name exists in the formula and the label is not the same as the value
                if (str.Contains(gridFormulas.Rows[i].Cells[0].Value.ToString().ToLower()) &&
                    gridFormulas.Rows[i].Cells[0].Value.ToString().ToLower() != gridFormulas.Rows[i].Cells[1].Value.ToString().ToLower())
                {
                    //replace it with the string stored for that label
                    str = str.Replace(gridFormulas.Rows[i].Cells[0].Value.ToString(), gridFormulas.Rows[i].Cells[1].Value.ToString());
                }
            }

            for (start = 0; start < str.Length; start++)
            {
                start = str.IndexOf('d', start);
                if (start == -1)
                {
                    break;
                }
                for (start--; start >= 0 && !operators.Contains(str[start]); start--)
                { }
                //start = tempstr.LastIndexOfAny(operators, 0, start);
                start++;
                end = str.IndexOfAny(operators, start);
                if (end == -1)
                {
                    end = str.Length;
                }
                end -= start;
                temp = dice.Roll(str.Substring(start, end));
                str = str.Remove(start, end).Insert(start, temp.ToString());
            }

            string tempstr = str;
            //convert subtraction operations into additions of negative numbers
            for (int i = 1; i < tempstr.Length; i++)
            {
                if (tempstr[i] == '-' && tempstr[i - 1] != '*' &&
                    tempstr[i - 1] != '/' && tempstr[i - 1] != '+' && tempstr[i - 1] != '(')
                {
                    tempstr = tempstr.Insert(i++, "+");
                }
            }

            for (int i = 0; i < tempstr.Length; i++)
            {
                if ( !char.IsDigit(tempstr[i]) && !operators.Contains(tempstr[i]) )
                {
                    throw new Exception("Invalid formula");
                }
            }

            //run the calculations and assemble the string for output
            //str += " = " + Calc(tempstr + ")");
            
            //handle n(formula)...multiply
            for (int i = 1 ; i < tempstr.Length - 1 ; i++ )
            {
                if (tempstr[i] == '(' && char.IsDigit(tempstr[i - 1]))
                {
                    tempstr = tempstr.Insert(i, "*");
                }
                if (tempstr[i] == ')' && char.IsDigit(tempstr[i + 1]))
                {
                    tempstr = tempstr.Insert(i + 1, "*");
                }
            }
            str += " = " + Evaluate(tempstr);
            input += ": " + dice.RollResults + "->" + System.Environment.NewLine + str;
            return input;
        }

        //add data to roll history datagridview and highlight if an open roll occured
        private void addHistory(string input)
        {
            gridHistory.Rows.Add(input);

            //check for open rolls
            /*if (openRollLimit > 90)
            {
                //highlight the row
                gridHistory.Rows[gridHistory.Rows.Count - 1].Cells[0].Style.BackColor = Color.Green;
            }*/
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
