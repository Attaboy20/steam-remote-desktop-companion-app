using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MonitorSwitcher
{
    public class MainForm : Form
    {
        private Button switchMonitorButton;

        public MainForm()
        {
            // Set up the form
            this.Text = "Monitor Switcher";
            this.Size = new Size(300, 200);

            // Create the button
            switchMonitorButton = new Button();
            switchMonitorButton.Text = "Switch Monitor";
            switchMonitorButton.Size = new Size(150, 50);
            switchMonitorButton.Location = new Point((this.ClientSize.Width - switchMonitorButton.Width) / 2,
                                                       (this.ClientSize.Height - switchMonitorButton.Height) / 2);
            switchMonitorButton.Anchor = AnchorStyles.None;
            switchMonitorButton.Click += SwitchMonitorButton_Click;

            // Add the button to the form
            this.Controls.Add(switchMonitorButton);
        }

        private void SwitchMonitorButton_Click(object sender, EventArgs e)
        {
            // Get all screens
            var screens = Screen.AllScreens;

            if (screens.Length > 1)
            {
                // Find the current screen
                var currentScreen = Screen.FromControl(this);

                // Find the next screen
                var currentIndex = Array.IndexOf(screens, currentScreen);
                var nextIndex = (currentIndex + 1) % screens.Length;
                var nextScreen = screens[nextIndex];

                // Move the form to the next screen
                this.Location = new Point(nextScreen.WorkingArea.Left + 100, nextScreen.WorkingArea.Top + 100);
            }
            else
            {
                MessageBox.Show("Only one monitor detected.", "Monitor Switcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
