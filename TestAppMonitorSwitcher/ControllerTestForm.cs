using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;

namespace MonitorSwitcher
{
    public partial class ControllerTestForm : Form
    {
        Button button;
        public ControllerTestForm()
        {

            this.Text = "Monitor Switcher";
            this.Size = new Size(500, 300);
            button = new Button();
            button.Text = "Switch Monitor";
            button.Size = new Size(150, 50);
            button.Location = new Point((this.ClientSize.Width - button.Width) / 2,
                                                       (this.ClientSize.Height - button.Height) / 2);
            button.Anchor = AnchorStyles.None;

            this.Controls.Add(button);

            Console.WriteLine("Start XGamepadApp");
            // Initialize XInput
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };

            // Get 1st controller available
            Controller controller = null;
            foreach (var selectControler in controllers)
            {
                if (selectControler.IsConnected)
                {
                    controller = selectControler;
                    break;
                }
            }

            if (controller == null)
            {
                Console.WriteLine("No XInput controller installed");
               
            }
            else
            {
                Console.WriteLine("Press buttons on the controller to display events or escape key to exit... ");
                // Poll events from joystick
                var previousState = controller.GetState();
                InitializeComponent();
                while (controller.IsConnected)
                {
                    if (IsKeyPressed(ConsoleKey.Escape))
                    {
                        break;
                    }
                    var state = controller.GetState();
                    if (previousState.PacketNumber != state.PacketNumber)
                        Console.WriteLine(state.Gamepad);
                    Thread.Sleep(10);
                    previousState = state;
                }
            }
            Console.WriteLine("End XGamepadApp");
            
        }

        private bool IsKeyPressed(ConsoleKey key)
        {
            return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }

        

            }
    
}

    
