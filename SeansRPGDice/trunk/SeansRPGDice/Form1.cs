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
         * Automatically check for updates
         * Create a better about box
         * Create a better set of instructions
         * Save settings to settings file
         */
        private int histIndex;          //track the last formula used
        private Dice dice;              //dice class to handle rolling and getting results
        private Color highlight;        //what color to highlight a row
        private string settingsFile;    //variable to store the settings file

        //startup stuff
        public MainForm()
        {
            InitializeComponent();

            //initialize our global variables
            histIndex = 0;
            dice = new Dice();
            highlight = Color.White;
            settingsFile = "SRDSettings.txt";
            
            //load settings from settings file if it exists
            loadSettings();
        }

        //load settings into the program
        private void loadSettings()
        {
            try
            {
                //open settings file
                FileStream fs = new FileStream(settingsFile, FileMode.Open);
                StreamReader sr = new StreamReader(fs);

                //load settings (window height x width, openroll checkbox, highlight checkbox)
            }
            catch (FileNotFoundException ex)
            {
                writeSettings();
            }
        }

        //write settings to a file
        private void writeSettings()
        {
            try
            {
                //create a new file to write settings to, overwriting anything existing
                FileStream fs = new FileStream(settingsFile, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                
                //window height x width, openroll checkbox, highlight checkbox
                string setString = "";

                //store window size
                setString += this.Height.ToString() + "x" + this.Width.ToString() + ",";
                
                //openroll checkbox
                setString += this.chkOpenRoll.Checked.ToString() + ",";

                //highlight checkbox
                setString += this.chkHighlight.Checked.ToString() + ",";

                //write the settings to the file and close the stream
                setString[setString.Length - 1] = ';';
                sw.WriteLine(setString);
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error saving settings");
            }
        }

        //check online for updates
        private void checkUpdates()
        {
            //check site for version number
            //compare version number with existing version number
            //if server version is newer than existing version
            //prompt user to update
            //if user clicks yes
            //download and update application
        }

        //parse and replace the symbols in the string  with strings using regex
        public static double Evaluate(string expression)
        {
            return (double)new System.Xml.XPath.XPathDocument
            (new System.IO.StringReader("<r/>")).CreateNavigator().Evaluate
            (string.Format("number({0})", new
            System.Text.RegularExpressions.Regex(@"([\+\-\*])").Replace(expression, " ${1} ").Replace("/", " div ").Replace("%", " mod ")));
        }

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

            string rollme = "";

            //parse through the string character by character for dice rolls and process them
            for (start = 0; start < str.Length; start++)
            {
                //set the start variable to the 'd' chracter
                start = str.IndexOf('d', start);
                //if there's no dice, move on
                if (start == -1)
                {
                    break;
                }

                //move the start variable back until it reaches a non-number
                for (start--; start >= 0 && !operators.Contains(str[start]); start--)
                { }
                //start = tempstr.LastIndexOfAny(operators, 0, start);
                start++;

                //set the end variable to the next operator
                end = str.IndexOfAny(operators, start);

                //if there isn't any more operators, set it to the end of the string
                if (end == -1)
                {
                    end = str.Length;
                }

                //make the end variable the length of the dice variable
                end -= start;

                rollme = str.Substring(start, end);

                //open roll checkbox check (only on d100)
                if (chkOpenRoll.Checked && rollme.Contains("d100"))
                {
                    //append the open roll operator
                    rollme = rollme.Insert(rollme.IndexOf("d100")+4,"o");
                }

                //roll the dice and get a resulting number
                temp = dice.Roll(rollme);

                //replace the dice variable with the result
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

            //check every character of the string for forbidden characters
            for (int i = 0; i < tempstr.Length; i++)
            {
                if ( !char.IsDigit(tempstr[i]) && !operators.Contains(tempstr[i]) && !(tempstr[i] == '.') )
                {
                    throw new Exception("Invalid formula");
                }
            }
            
            //handle n(formula) multiplication...insert a '*'
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

            //run the calculations and get the total
            str += " = " + Evaluate(tempstr);

            //if the highlighting option is enabled
            if (chkHighlight.Checked)
            {
                //split out the results
                string[] results = dice.RollResults.Split(new char[] {',','[',']'});

                //highlight critical successes in green
                if ((rollme.Contains('o') && dice.RollCount > 1) 
                    || (results.Contains("20") && input.Contains("d20")))
                {
                    highlight = Color.Green;
                } 
                //highlight critical failures in red if there wasn't already a success in the list of rolls
                else if ((rollme.Contains("d20") && results.Contains("1"))
                    || (rollme.Contains("d100o") && (results.Contains("1") || results.Contains("2") || results.Contains("3"))))
                {
                    highlight = Color.Red;
                }
            }

            //assemble the string to output
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

        //handle enter key to submit the input
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
            MessageBox.Show("This program created by Sean Wells and Thomas Wild." + System.Environment.NewLine 
                + "Send feedback to <codemonk84@gmail.com>" + System.Environment.NewLine
                + System.Environment.NewLine 
                + "Extra Thanks to..." + System.Environment.NewLine 
                + "Aaron Biegalski" + System.Environment.NewLine
                + "Trevor Hoagland" + System.Environment.NewLine
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
                "10d4+(2d6-5)+3/2 (parenthesis sets are supported)" + System.Environment.NewLine +
                "cha = 18 (labelled saving of formulas are supported)" + System.Environment.NewLine +
                "1d20+cha (using labels to reference saved formulas are supported)" + System.Environment.NewLine +
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
