using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ScreenDimmer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.notifyIcon.Icon = new Icon("app.ico");
            this.notifyIcon.ContextMenu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Close", OnCloseClicked)
            });
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] thisAppProcesses = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (var proc in thisAppProcesses)
            {
                if (proc.Id == currentProcess.Id)
                    continue;
                proc.Kill();
            }

            bool overrideConfigFile = false;
            string[] args = Environment.GetCommandLineArgs();
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] == "-off" || args[i] == "-close" || args[i] == "-kill")
                    {
                        currentProcess.Kill();
                        return;
                    }
                    if (args[i] == "-opacity")
                    {
                        Opacity = double.Parse(args[++i]);
                        overrideConfigFile = true;
                    }
                    if (args[i] == "-color")
                    {
                        overrideConfigFile = true;
                        string colorCode = args[++i];
                        switch (colorCode)
                        {
                            case "black":
                            case "1":
                                BackColor = Color.Black;
                                break;
                            case "white":
                            case "0":
                                BackColor = Color.White;
                                break;
                            case "orange":
                                BackColor = Color.Orange;
                                break;
                            case "blue":
                                BackColor = Color.Blue;
                                break;
                            case "red":
                                BackColor = Color.Red;
                                break;
                            case "yellow":
                                BackColor = Color.Yellow;
                                break;
                            case "green":
                                BackColor = Color.Green;
                                break;
                            default:
                                string[] colors = colorCode.Split(',');
                                if (colors.Length > 0)
                                {
                                    int r, g = 0, b = 0;
                                    r = int.Parse(colors[0]);
                                    if (colors.Length > 1)
                                    {
                                        g = int.Parse(colors[1]);
                                        if (colors.Length > 2)
                                            b = int.Parse(colors[2]);
                                    }
                                    BackColor = Color.FromArgb(r, g, b);
                                }
                                break;
                        }
                    }
                }
            }
            if (!overrideConfigFile)
            {
                using (StreamReader reader = new StreamReader("settings.config"))
                {
                    string line;
                    while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                    {
                        line = line.ToLower();
                        if (line.StartsWith("color"))
                        {
                            string[] rgbStr = line.Split('=')[1].Split(',');
                            int.TryParse(rgbStr[0], out int red);
                            int.TryParse(rgbStr[1], out int green);
                            int.TryParse(rgbStr[2], out int blue);
                            BackColor = Color.FromArgb(red, green, blue);
                        }
                        else if (line.StartsWith("opacity"))
                            Opacity = double.Parse(line.Split('=')[1]);
                    }
                }
            }

            // Tell the OS that this form is an overlay and won't capture any inputs
            WindowWrapper.SetWindowLong(this.Handle, WindowWrapper.GWL.ExStyle, WindowWrapper.WS_EX.Transparent | WindowWrapper.WS_EX.Layered);
        }
    }
}
