using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAK_1._0
{
    public partial class FortuneWheelEditObject : Form
    {
        public string textTB { get; set; }

        public FortuneWheelEditObject()
        {
            InitializeComponent();
        }

        private void FortuneWheelEditObject_Load(object sender, EventArgs e)
        {
            label1.Text = "Editing: "+textTB;
            textBox1.Text = textTB;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            f1.deleteData(textTB);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();

            //User leaves textbox in blank
            if (textBox1.Text != "")
            {
                f1.editData(textBox1.Text, textTB);
                this.Close();
            }
            else 
            {
                //Set strings
                string title = "Delete object";
                string message = "Leaving the object in blank will delete it from the list." +
                    "\nDo you wish to proceed?";

                //Messagebox buttons
                MessageBoxManager.Unregister();
                MessageBoxManager.Yes = "Yeah";
                MessageBoxManager.No = "Nah";
                MessageBoxManager.Register();
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                //Display messagebox
                DialogResult dr = MessageBox.Show(message,title,buttons);

                if (dr == DialogResult.Yes) 
                {
                    f1.deleteData(textTB);
                    this.Close();
                }
            }
        }
    }
}
