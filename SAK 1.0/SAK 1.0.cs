using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace SAK_1._0
{
    public partial class Form1 : Form
    {
        //List for the List Box
        static List<String> options = new List<string>();
        BindingSource optionsBS = new BindingSource();

        //Title for the list
        public string saveTitle { get; set; }

        public Form1()
        {
            InitializeComponent();
            listBox1.HorizontalScrollbar = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load List on ListBox
            optionsBS.DataSource = options;
            listBox1.DataSource = optionsBS;
        }

        internal void deleteData(string text)
        {
            options.Remove(text);
        }

        internal void recieveData(string text)
        {
            options.Add(text);
        }

        internal void editData(string text,string textTB)
        {
            int i = options.FindIndex(a => a.Contains(textTB));
            options[i] = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Buttons
            MessageBoxManager.Unregister();
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            //Message box
            string text = "";
            string title = "Save";
            if (label1.Text == "(List Name)")
            {
                MessageBoxManager.OK = "Gotcha";
                MessageBoxManager.Register();
                text = "Your list is empty! D:";
                MessageBox.Show(text, title, buttons);
            }
            //List doesn´t have default name
            else 
            {
                //Saving list onto the textfile
                if (options.Count != 0)
                {
                    //Finding title already saved on the text file
                    bool alreadySaved = false;
                    string line;
                    int i = 0;
                    StreamReader sr = new StreamReader("Wheel.txt", true);
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        if (i == 0)
                        {
                            if (line == label1.Text) 
                            {
                                alreadySaved = true;
                            }
                        }
                        else if (line == "---")
                        {
                            i = -1;
                        }
                        i++;
                        line = sr.ReadLine();
                    }
                    sr.Close();

                    if (!alreadySaved)
                    {
                        //Writing the content of the list
                        StreamWriter sw = new StreamWriter("Wheel.txt", true);
                        sw.WriteLine(label1.Text);
                        options.ForEach(delegate (string option)
                        {
                            sw.WriteLine(option);
                        });
                        sw.WriteLine("---");
                        sw.Dispose();

                        //MessageBox
                        MessageBoxManager.Unregister();
                        MessageBoxManager.OK = "Epic!";
                        MessageBoxManager.Register();
                        text = "List Saved!";
                        MessageBox.Show(text, title, buttons);
                    }
                    else 
                    {
                        MessageBox.Show("List with this title already exists!");
                    }
                    
                }
                else 
                {
                    MessageBoxManager.Unregister();
                    MessageBoxManager.OK = "Aight";
                    MessageBoxManager.Register();
                    text = "Your list lacks objects";
                    MessageBox.Show(text, title, buttons);
                }
            }
        }

        //Heads or Tails (Beta)
        private void headsOrTailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Gets random number
            PrivateAPI pAPI = new PrivateAPI();
            int answer = pAPI.randomNumber(0,1);
            string output = "";

            //Compares number gotten
            if (answer == 1)
            {
                output = "HEADS!";
            }
            else 
            {
                output = "TAILS!";
            }

            //Message box with the output
            MessageBoxManager.Unregister();
            MessageBoxManager.OK = "Epic!";
            MessageBoxManager.Register();
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(output, "And the result is...", buttons);
        }

        //About
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Main string
            string output = "App for personal use. Done mainly to improve my coding skills" +
                "\nShould you get access to this executable, I´d really appreciate some feedback :D";

            //Message box buttons
            MessageBoxManager.Unregister();
            MessageBoxManager.Yes = "Contacts";
            MessageBoxManager.No = "Close";
            MessageBoxManager.Register();
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;

            //Dialog box
            DialogResult dr = MessageBox.Show(output,"About",buttons);

            //Message box for the contacts
            if (dr == DialogResult.Yes) 
            {
                MessageBoxManager.Unregister();
                string contacts = "atp505.newgrounds.com" +
                    "\n@atp505 on Discord";
                MessageBox.Show(contacts,"Contacts");
            }
        }

        //List Name
        private void label1_Click(object sender, EventArgs e)
        {
            string message, title, defaultValue;
            object myValue;

            //Set strings
            title = "List Name";
            message = "What will the list be named?";
            defaultValue = "(Write Name Here)";

            //Buttons
            MessageBoxManager.Unregister();
            MessageBoxManager.OK = "Contacts";
            MessageBoxManager.Cancel = "Close";
            MessageBoxManager.Register();

            //Display Dialogbox
            myValue = Interaction.InputBox(message,title,defaultValue);

            //if the user clicked cancel, set myValue to defaultValue
            if ((string)myValue != "")
            {
                if ((string)myValue != defaultValue)
                {
                    label1.Text = (string)myValue;
                    label1.Refresh();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button2, 0, button2.Height);
        }

        //Wheel spin
        private void button4_Click(object sender, EventArgs e)
        {
            PrivateAPI pAPI = new PrivateAPI();
            int random = pAPI.randomNumber(0,options.Count-1);
            int a = options.Count;
            if (options.Count > 0)
            {
                MessageBox.Show(options[random]);
                if (checkBox1.Checked) 
                {
                    options.RemoveAt(random);
                }
            }
            else 
            {
                MessageBox.Show("The list has no winners to pick :(");
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            FortuneWheelEditObject FWEO = new FortuneWheelEditObject();
            int i = listBox1.SelectedIndex;
            if (i >= 0) 
            {
                FWEO.textTB = options[i];
                FWEO.ShowDialog();
            }

        }

        //Add Value to List
        private void button3_Click_1(object sender, EventArgs e)
        {
            FortuneWheelAddObject FWAO = new FortuneWheelAddObject();
            FWAO.ShowDialog();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            optionsBS.ResetBindings(false);

            //Load File
            contextMenuStrip1.Items.Clear();
            contextMenuStrip2.Items.Clear();
            string line;
            int i = 0;
            StreamReader sr = new StreamReader("Wheel.txt", true);
            line = sr.ReadLine();
            while (line != null)
            {
                if (i == 0)
                {
                    contextMenuStrip1.Items.Add(line);
                    contextMenuStrip2.Items.Add(line);
                }
                else if (line == "---")
                {
                    i = -1;
                }
                i++;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            options.Sort();
            MessageBox.Show("List Sorted!");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            contextMenuStrip2.Show(button6, 0, button6.Height);
        }

        //Load menu strip
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Clear list and change title
            options.Clear();
            saveTitle = e.ClickedItem.Text;
            label1.Text = saveTitle;

            //Read textfile and upload data
            string line;
            int i = 0;
            StreamReader sr = new StreamReader("Wheel.txt", true);

            line = sr.ReadLine();
            while (line != null)
            {
                if (i == 0)
                {
                    if (line == e.ClickedItem.Text) 
                    {
                        line = sr.ReadLine();
                        while (line != "---") 
                        {
                            options.Add(line);
                            line = sr.ReadLine();
                        }
                    }
                }
                else if (line == "---")
                {
                    i = -1;
                }
                i++;
                line = sr.ReadLine();
            }
            sr.Close();
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //Read texfile and delete data
            string tempFile = Path.GetTempFileName();
            string line = "";
            int i = 0;

            StreamReader sr = new StreamReader("Wheel.txt", true);
            StreamWriter sw = new StreamWriter(tempFile);

            line = sr.ReadLine();
            while (line != null) 
            {
                if (i == 0)
                {
                    if (line != e.ClickedItem.Text)
                    {

                    }
                }
                else if (line == "---")
                {
                    i = -1;
                }
                i++;
                line = sr.ReadLine();
            }
            sr.Close();
        }
    }
}
