using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsMatchingGame
{
    public partial class Form1 : Form
    {
        private SoundPlayer _soundPlayer;

        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        Label firstClicked, secondClicked = null;

        // seconds to complete the game
        int triesLeft = 20;

        int numOfIcons = 16;
        int matchedIcons = 0;

        public Form1()
        {
            InitializeComponent();
            triesLabel.Text = $"{triesLeft} tries left";
            matchedIcons1.Text = $"{matchedIcons}/{numOfIcons} matched icons";
            _soundPlayer = new SoundPlayer();
            AssignIconsToSquares();
        }

        /// <summary>
        ///  Assign each icon from the list of icons to a random square
        /// </summary>
        private void AssignIconsToSquares()
        {
            foreach (Control control in GameBoard.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        /// <summary>
        /// This timer is started when the player clicks two icons that don't match and then stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matchingTicker_Tick(object sender, EventArgs e)
        {
            matchingTicker.Stop();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked.ForeColor = Color.Black;

                matchedIcons += 2;
                matchedIcons1.Text = $"{matchedIcons}/{numOfIcons} matched icons";

                firstClicked = null;
                secondClicked = null;

                _soundPlayer = new SoundPlayer("351566__bertrof__game-sound-correct-organic-violin.wav");
                _soundPlayer.Play();
            }
            else
            {
                triesLeft -= 1;
                triesLabel.Text = $"{triesLeft} tries left";

                _soundPlayer = new SoundPlayer("351565__bertrof__game-sound-incorrect-organic-violin.wav");
                _soundPlayer.Play();

                // Delay for the length of the .wav file
                int delay = 3 * 1000;
                Thread.Sleep(delay);

                _soundPlayer = new SoundPlayer("652918__hidding-icons-sound.wav");
                _soundPlayer.Play();

                // hide the icons
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;

                firstClicked = null;
                secondClicked = null;
            }

            CheckForEndOfGame();

            return;
        }
        private void firstSelectionTicker_Tick(object sender, EventArgs e)
        {
            firstSelectionTicker.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
        }

        /// <summary>
        /// Every label's Clock event is handled by this event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label1_Click(object sender, EventArgs e)
        {
            // ignore any clicks while the two labels are being compared by the timer tick.
            if (matchingTicker.Enabled == true)
                return;

            Label clickedLabel1 = sender as Label;

            if (clickedLabel1 != null)
            {
                // displayed labels cannot be selected again while visible
                if (clickedLabel1.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel1;
                    firstClicked.ForeColor = Color.Black;
                    firstSelectionTicker.Start();
                    return;
                }

                firstSelectionTicker.Stop();
                secondClicked = clickedLabel1;
                secondClicked.ForeColor = Color.Black;

                // evaluate the two selected labels with a timer tick
                matchingTicker.Start();
            }
        }

        private void CheckForEndOfGame()
        {

            if (matchedIcons == numOfIcons)
            {
                MessageBox.Show("You matched all the icons!", "Congratulations");
                Close();
            }
            else
            {
                if (triesLeft <= 0)
                {
                    MessageBox.Show($"You have run out of tries. {matchedIcons}/{numOfIcons} matched icons.", "Sorry!");
                    Close();
                }

                return;
            }
        }
    }
}
