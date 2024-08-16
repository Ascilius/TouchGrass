using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TouchGrass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool timerActive = false;
        DateTime start;
        TimeSpan totalTime = new TimeSpan(0, 0, 0);

        TimeSpan goal = new TimeSpan(6, 0, 0);
        bool goalReached = false;
        System.Media.SoundPlayer congrats = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + @"\res\yay.wav"); // congratulatory cheer

        public MainWindow()
        {
            InitializeComponent();

            // initializing text
            updateGoalText();
            timerText.Text = totalTime.ToString();
        }

        private void updateGoalText()
        {
            goalText.Text = "Goal: " + goal.ToString();
        }

        private void timer_Click(object sender, RoutedEventArgs e)
        {
            timerActive = !timerActive; // switch timer state

            // start timer
            if (timerActive)
            {
                timerButton.Content = "Stop touching grass";
                start = DateTime.Now;

                timerText.Text = "Timer active";
            }

            // stop timer
            else
            {
                timerButton.Content = "Start touching grass";

                DateTime finish = DateTime.Now;
                totalTime += finish - start;
                timerText.Text = totalTime.ToString();

                // checking if goal was reached
                if (TimeSpan.Compare(goal, totalTime) == -1)
                {
                    // increase goal
                    while (TimeSpan.Compare(goal, totalTime) == -1)
                        goal = goal.Multiply(2.0);
                    updateGoalText();

                    timerText.Text += " (goal reached!)";
                    congrats.Play(); // yay
                }
            }
        }
    }
}