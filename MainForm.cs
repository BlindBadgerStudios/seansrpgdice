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
        /* Creat global vars */
        private int histIndex;          //track the last formula used
        private Dice dice;              //dice class to handle rolling and getting results
        private Color highlight;        //what color to highlight a row

        /* Startup stuff */
        public MainForm()
        {
            InitializeComponent();

            //initialize our global variables
            histIndex = 0;
            dice = new Dice();
            highlight = Color.White;
        }

        /* STUB: check online for updates */
        private void checkUpdates()
        {
            //check github site for version number
            //compare version number with existing version number
            //if server version is newer than existing version
            //prompt user to update
            //if user clicks yes
            //open site to github release page using System.Diagnostics.Process.Start("URL"); 
        }

        /* Parse and replace the symbols in the string  with strings using regex */
        public static double Evaluate(string expression)
        {
            return (double)new System.Xml.XPath.XPathDocument
            (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
            (string.Format("number({0})", new
            System.Text.RegularExpressions.Regex(@"([\+\-\*])").Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")));
        }

        /* main parsing/calculating function */
        private string Calculate(string input)
        {
            // Initialize variables
            string label = "";
            char[] operators = { '+', '*', '/', '(', ')', '-', '%' };
            int start, end, temp;

            // Remove spaces and change to lowercase to make it easier/more predictable to work with
            string str = input.ToLower().Replace(" ", "");
            


            // Skip processing if the input is empty
            if (str.Length == 0)
            {
                return "";
            }

            // Store labels
            if (input.Contains('='))
            {
                //parse out the label into it's own variable
                label = str.Substring(0, str.IndexOf('='));
                //remove the label from the string so that the roller functions don't get confused
                str = str.Substring(str.IndexOf('=') + 1);

                //error check for invalid
                if (str.Contains('='))
                {
                    throw new DiceException("Formula can only contain one '=' character", "Invalid Formula");
                }

                if (str.Contains(label))
                {
                    throw new DiceException("Formula can not reference itself", "Invalid Formula");
                }
                //store formula in gridFormulas
                gridFormulas.Rows.Add(label, str);
                return "Stored: " + input;
            }

            // Reset dice for fresh rolls
            dice.Clear();

            // Parse variables called in the formula
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

            string rollme = "";

            // Parse through the input string character by character for dice rolls and process them
            for (start = 0; start < str.Length; start++)
            {
                //set the start variable to the 'd' chracter
                start = str.IndexOf('d', start);
                //if there's no dice, move on
                if (start == -1)
                {
                    //Exit Loop
                    break;
                }

                // To find the beginning of the XdY, move the start variable back until it reaches a non-number
                for (start--; start >= 0 && !operators.Contains(str[start]); start--)
                { }
                // Move start variable forward one to identify the first number
                start++;

                // Set the end variable to the next operator to define the end of the dice string
                end = str.IndexOfAny(operators, start);

                // If there isn't any more operators, set it to the end of the string
                if (end == -1)
                {
                    end = str.Length;
                }

                // Use the end variable to calculate and store the length of the detected dice string
                end -= start;

                // Store the detected dice string into rollme
                rollme = str.Substring(start, end);

                // If the Open roll feature is enabled and the dice is a d100, automatically add the open roll operator
                if (chkOpenRoll.Checked && rollme.Contains("d100"))
                {
                    //append the open roll operator
                    rollme = rollme.Insert(rollme.IndexOf("d100") + 4, "o");
                }

                // Roll the dice and store the result in temp
                temp = dice.Roll(rollme);

                // Replace the dice string (XdY) with the result (a number)
                str = str.Remove(start, end).Insert(start, temp.ToString());
            }

            // Copy the input string (which should just be a math string now) into a temporary variable
            string tempstr = str;

            // Convert subtraction operations into additions of negative numbers for ease of processing
            for (int i = 1; i < tempstr.Length; i++)
            {
                if (tempstr[i] == '-' && tempstr[i - 1] != '*' &&
                    tempstr[i - 1] != '/' && tempstr[i - 1] != '+' && tempstr[i - 1] != '(')
                {
                    tempstr = tempstr.Insert(i++, "+");
                }
            }

            // Check every character of the string for forbidden characters
            for (int i = 0; i < tempstr.Length; i++)
            {
                if (!char.IsDigit(tempstr[i]) && !operators.Contains(tempstr[i]) && !(tempstr[i] == '.'))
                {
                    throw new Exception("Invalid formula");
                }
            }

            // Process n(a) formatted multiplication by inserting an explicit '*'
            for (int i = 1; i < tempstr.Length - 1; i++)
            {
                // Detect if there is a number preceding the '(' character
                if (tempstr[i] == '(' && char.IsDigit(tempstr[i - 1]))
                {
                    tempstr = tempstr.Insert(i, "*");
                }
                // Detect if there is a number succeeding the ')' character
                if (tempstr[i] == ')' && char.IsDigit(tempstr[i + 1]))
                {
                    tempstr = tempstr.Insert(i + 1, "*");
                }
            }

            // Do math and show your work (run the calculations and get the total, showing the formula and the result)
            str += " = " + Evaluate(tempstr);

            // If the highlighting option is enabled, highlight critical successes/failures
            if (chkHighlight.Checked)
            {
                // Split out the result of multiple rolls
                string[] results = dice.RollResults.Split(new char[] { ',', '[', ']' });

                // Highlight critical successes in green
                if ((rollme.Contains('o') && dice.RollCount > 1)
                    || (results.Contains("20") && input.Contains("d20")))
                {
                    highlight = Color.Green;
                }
                // Highlight critical failures in red if there wasn't already a success in the list of rolls
                else if ((rollme.Contains("d20") && results.Contains("1"))
                    || (rollme.Contains("d100o") && (results.Contains("1") || results.Contains("2") || results.Contains("3"))))
                {
                    highlight = Color.Red;
                }
            }

            // Assemble the final output string to be displayed
            input += ": " + dice.RollResults + "->" + System.Environment.NewLine + str;
            return input;
        }

        //add data to roll history datagridview and highlight if an open roll occured
        private void addHistory(string input, string orig)
        {
            //add string to the main grid
            gridHistory.Rows.Add(input, orig);
            //set the background color, changes only if set to something different by another function
            gridHistory.Rows[gridHistory.Rows.Count - 1].Cells[0].Style.BackColor = highlight;
            //set the highlight color back to default
            highlight = Color.White;
        }

        // Handle enter key to submit the input
        private void txtInput_OnEnter(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:      //enter key
                    try
                    {
                        string str = Calculate(txtInput.Text);
                        //do nothing if there is nothing to input
                        if (str.Length == 0)
                            break;
                        //run calculation and add it to display
                        addHistory(str, txtInput.Text);
                        //scroll to newly added value
                        gridHistory.FirstDisplayedScrollingRowIndex = gridHistory.RowCount - 1;
                        //save this formula for rerolling
                        histIndex = gridHistory.RowCount - 1;
                        //clear input
                        txtInput.Clear();
                        histIndex = gridHistory.RowCount;
                        //remove selection highlight to ensure crit successes show through
                        gridHistory.ClearSelection();
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
            //TODO: Add confirmation dialog to prevent accidental closure
            this.Close();
        }

        // Show "about" info
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Convert to another form so we can make the URL clickable with a LinkLabel
            MessageBox.Show("This program created by Sean Wells and Thomas Wild." + System.Environment.NewLine
                + "Send feedback to the github repository" + System.Environment.NewLine
                + "https://github.com/BlindBadgerStudios/seansrpgdice"
                + System.Environment.NewLine
                + "Extra Thanks to..." + System.Environment.NewLine
                + "Aaron Biegalski" + System.Environment.NewLine
                + "Trevor Hoagland" + System.Environment.NewLine
                , "About...");
        }

        // Display instructions on how to use the app (needs improvement)
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Calculations are performed in the order it is typed (left to right), each modifier being performed on the whole before it." +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "Input similar to the following examples:" + System.Environment.NewLine +
                "2d6" + System.Environment.NewLine +
                "6d6+24-10/2 (modifiers are supported)" + System.Environment.NewLine +
                "10d4+2d6-5+3/2 (multiple dice sets are supported)" + System.Environment.NewLine +
                "1d100*1.1 (decimals are supported)" + System.Environment.NewLine +
                "10d4+(2d6-5)+3/2 (parenthesis sets are supported)" + System.Environment.NewLine +
                "cha = 18 (labelled saving of formulas are supported)" + System.Environment.NewLine +
                "1d20+cha (using labels to reference saved formulas are supported)" + System.Environment.NewLine +
                System.Environment.NewLine +
                "You may add as many modifiers and dice sets as you wish.", "Instructions");
        }

        // Clear the roll history
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: Add a confirmation prompt
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
                saveme += gridHistory.Rows[i].Cells[0].Value.ToString().Replace(System.Environment.NewLine, "|") + System.Environment.NewLine;
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

                for (int i = 0; i < input.Count; i++)
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
                            gridFormulas.Rows.Add(s.Substring(0, s.IndexOf(',')), s.Substring(s.IndexOf(',') + 1));
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

        // Import roll history file
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
                MessageBox.Show(ex.Message);
            }
        }

        // Import formula file
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

        // Run this on every keystroke because keypress events don't parse arrow keys -.-
        private void txtTest(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (histIndex > 0)
                {
                    histIndex--;
                }
                txtInput.Text = gridHistory.Rows[histIndex].Cells[1].Value.ToString();
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (histIndex < gridHistory.RowCount - 1)
                {
                    histIndex++;
                }
                txtInput.Text = gridHistory.Rows[histIndex].Cells[1].Value.ToString();
            }
        }
    }
}
