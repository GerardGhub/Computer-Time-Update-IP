using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using WbemScripting;
using System.Management;
using System.IO;

namespace UpdateTime
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = DateTime.Now.ToString("hh:mm:ss ttt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string machineName = tkOne.Text.Trim();
                Process proc = new Process();
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.FileName = "net";
                proc.StartInfo.Arguments = @"time \\" + machineName;
                proc.Start();
                proc.WaitForExit();

                List<string> results = new List<string>();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string currentline = proc.StandardOutput.ReadLine();

                    if (!string.IsNullOrEmpty(currentline))
                    {
                        results.Add(currentline);
                    }
                }

                string currentTime = string.Empty;

                if (results.Count > 0 && results[0].ToLower().StartsWith(@"current time at \\" + machineName.ToLower() + " is "))
                {
                    currentTime = results[0].Substring((@"current time at \\" +
                                  machineName.ToLower() + " is ").Length);
                }

                textBox2.Text = currentTime;
            }
            catch
            {
                MessageBox.Show("Try Again");
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult12 = MessageBox.Show("Update time?", "SELECTED IP", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult12 == DialogResult.Yes)
            {

                var dt = DateTime.Now.AddSeconds(22);
                string remoteMachine = tkOne.Text;
                string appName = "PsExec.exe";
                //  string args = string.Format("\\192.168.2.126 cmd && time && " + textBox1.Text + "");
                string args = string.Format("\\\\{0} cmd /c time " + dt.ToString("HH:mm:ss") + "", remoteMachine); //  Pacific Standard Time         // Hawaiian
                Process.Start(appName, args);

                // 22 SECONDS

                //Process p = new Process();
                //p.ex
                //Process.Start(pStartInfo);


                //Process.Start("PsExec.exe");

              //  ActivitiesLogs("Updated time of TimeKeeping One ");
            }
          
        }
    }
}
