using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            try
            {
                return (double)new System.Xml.XPath.XPathDocument
                (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
                (string.Format("number({0})", new Regex(@"([\+\-\*])").Replace(expression, " ${1} ")
                .Replace("/", " div ").Replace("%", " mod ")));
            }
            catch
            {
                throw new DiceException("Unable to evaluate equation.","Format error in equation");
            }
        }

        //main parsing/calculating function
        private string Calculate(string input)
        {
            //remove spaces and change to lowercase to make it easier/more predictable to work with
            string str = input.ToLower().Replace(" ", "");
            string label = "";

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
                if (label.Length < 1)
                {
                    throw new DiceException("Missing name in formula declaration", "Invalid Formula");
                }
                //remove the label from the string so that the roller functions don't get confused
                str = str.Substring(str.IndexOf('=') + 1);
                if (str.Contains(label))
                {
                    throw new DiceException("Formula can not reference itself", "Invalid Formula");
                }
                //store formula in gridFormulas
                //gridFormulas.Rows.Add(label,str);
                try
                {
                    Calculate(str);
                }
                catch (Exception ex)
                {
                    throw new DiceException(ex.Message,"Invalid Formula");
                }
                //store formula in gridFormulas
                int index = 0;
                for(  ; index < gridFormulas.Rows.Count ; index++ )
                {
                    if ((string)gridFormulas.Rows[index].Cells[0].Value == label)
                    {
                        gridFormulas.Rows[index].Cells[1].Value = str;
                        break;
                    }
                }
                if (index == gridFormulas.Rows.Count)
                {
                    gridFormulas.Rows.Add(label, str);
                }
                return "Stored: " + input;
            }

            dice.Clear();

            for (int i = gridFormulas.Rows.Count - 1 ; i >= 0 ; i-- )
            {
                str = Regex.Replace(str, "(\\b|(?<=\\d))" + gridFormulas.Rows[i].Cells[0].Value 
                    + "\\b","(" + gridFormulas.Rows[i].Cells[1].Value + ")");
            }
            str = dice.RollAll(str);

            foreach (char c in str)
            {
                if (char.IsLetter(c))
                {
                    throw new Exception("Unrecognized variable in equation");
                }
            }
            string tempstr = str;
            
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

            //compare the number of dice rolled to the number of dice requested to tell open rolls

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

        private void gridFormulas_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            List<int> Dependencies = new List<int>();
            Dependencies.Add(e.Row.Index);

            for (int d = 0 ; d < Dependencies.Count; d++ )
            {
                //Regex reg = new Regex("(\\b|(?<=\\d))" + gridFormulas.Rows[Dependencies[d]].Cells[0].Value + "\\b");
                for (int i = 0; i < gridFormulas.Rows.Count; i++)
                {
                    if (Regex.IsMatch((string)gridFormulas.Rows[i].Cells[1].Value,
                        "(\\b|(?<=\\d))" + gridFormulas.Rows[Dependencies[d]].Cells[0].Value + "\\b"))
                    {
                        Dependencies.Add(i);
                    }
                }
            }
            if( Dependencies.Count == 1 )
            {
                return;
            }
            e.Cancel = true;
            string str = "There are other formulas dependent upon this variable.\nDeleting "
                + (string)e.Row.Cells[0].Value +
                " will remove the other formulas as well.\nDo you wish to continue with this action?";
            if (MessageBox.Show(str, "Error deleting variable", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            Dependencies.Sort();
            Dependencies.Reverse();
            foreach (int i in Dependencies)
            {
                gridFormulas.Rows.RemoveAt(i);
            }
        }
    }
}
