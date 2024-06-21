using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMathQuiz
{
    public partial class Form1 : Form
    {
        // Create a Random object called randomizer to generate random numbers.
        Random randomizer = new Random();

        // These integer variables store the numbers for the problems.
        int addend1, addend2, minuend, subtrahend, 
            multiplicand, multiplier, dividend, divisor;

        private void answer_Correct(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            bool numericUpDownCorrect = false;
            
            
            if (numericUpDown.Name == "sum")
            {
                numericUpDownCorrect = (addend1 + addend2 == numericUpDown.Value);
            }
            else if (numericUpDown.Name == "difference")
            {
                numericUpDownCorrect = (minuend - subtrahend == numericUpDown.Value);
            }
            else if (numericUpDown.Name == "product")
            {
                numericUpDownCorrect = (multiplicand * multiplier == numericUpDown.Value);
            }
            else if (numericUpDown.Name == "quotent")
            {
                numericUpDownCorrect = (dividend / divisor == numericUpDown.Value);
            }

            if (numericUpDownCorrect)
            {
                SystemSounds.Hand.Play();
            }
            else
            {
                SystemSounds.Exclamation.Play();
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        int timeLeft;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                    "Congratulations!");
                startButton.Enabled = true;
                if (timeLeft <= 5)
                    timeLabel.BackColor = SystemColors.Control;
            }
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";

                if (timeLeft <= 5)
                    timeLabel.BackColor = Color.Red;
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotent.Value = dividend / divisor;
                startButton.Enabled = true;
                
                if (timeLeft <= 5)
                    timeLabel.BackColor = SystemColors.Control;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        public Form1()
        {
            InitializeComponent();
        }

        public void StartTheQuiz()
        {
            // Fill in the addition problem.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            // Fill in the subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            dividend = randomizer.Next(2, 11);
            divisor = randomizer.Next(2, 11);
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotent.Value = 0;

            // Start the timer.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        /// <summary>
        /// Check the answers to see if the user got everything right.
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotent.Value))
                return true;
            else
                return false;
        }
    }
}
