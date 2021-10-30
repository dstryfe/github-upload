using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Match_Game
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        //creating a list of symbols
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "j", "j", "v", "v", "w", "w", "z", "z",
        "Y", "Y", "q", "q", "d", "d", "Z", "Z",
        "~", "~", "@", "@", "$", "$", "l", "l",
        "-", "-", "m", "m"
    };
        //label pointers
        Label first = null, second = null;


        private void assignIcons()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = rand.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            assignIcons();
        }

        private void timerTick(object sender, EventArgs e)
        {
            timer1.Stop();

            //hide the labels
            first.ForeColor = first.BackColor;
            second.ForeColor = second.BackColor;

            //reset pointers
            first = null;
            second = null;
        }
        private void winCheck()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            //code getting this far == win
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }

        private void label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            //disable click if timer is active
            if (timer1.Enabled == true)
                return;

            if (clickedLabel != null)
            {
                //checks if it was already clicked; text color set to black if not
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                //set pointers to labels chosen
                if (first == null)
                {
                    first = clickedLabel;
                    first.ForeColor = Color.Black;
                    return;
                }

                second = clickedLabel;
                second.ForeColor = Color.Black;
                winCheck();

                //reset pointers if labels match to allow the next selections
                if (first.Text == second.Text)
                {
                    first = null;
                    second = null;
                    return;
                }
                timer1.Start();
            }
        }
    }
}
