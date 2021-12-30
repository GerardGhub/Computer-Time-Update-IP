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
    public partial class Form1 : Form
    {
        int tick;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             DialogResult dialogResult12 = MessageBox.Show("Update time?", "TimeKeeping One", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
             if (dialogResult12 == DialogResult.Yes)
             {

                 var dt = DateTime.Now.AddSeconds(22);
                 string remoteMachine = "192.168.2.136";
                 string appName = "PsExec.exe";
               //  string args = string.Format("\\192.168.2.126 cmd && time && " + textBox1.Text + "");
                 string args = string.Format("\\\\{0} cmd /c time " + dt.ToString("HH:mm:ss") + "", remoteMachine); //  Pacific Standard Time         // Hawaiian
                 Process.Start(appName, args);

                 // 22 SECONDS

                 //Process p = new Process();
                 //p.ex
                 //Process.Start(pStartInfo);


                 //Process.Start("PsExec.exe");

                 ActivitiesLogs("Updated time of TimeKeeping One ");
             }
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
             DialogResult dialogResult12 = MessageBox.Show("Update time?", "TimeKeeping Two", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
             if (dialogResult12 == DialogResult.Yes)
             {
                 var dt = DateTime.Now.AddSeconds(22);
                 string remoteMachine = "192.168.2.171";
                 string appName = "PsExec.exe";
                 //  string args = string.Format("\\192.168.2.126 cmd && time && " + textBox1.Text + "");
                 string args = string.Format("\\\\{0} cmd /c time " + dt.ToString("HH:mm:ss") + "", remoteMachine); //  Pacific Standard Time         // Hawaiian
                 Process.Start(appName, args);


                 ActivitiesLogs("Updated time of TimeKeeping Two ");
             }
          
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Form.CheckForIllegalCrossThreadCalls = false;

            try
            {

                // Task.Factory.StartNew(get_time(machineName));

                //string machineName = "192.168.2.136";
                //lblTkOne.Text = get_time(machineName);
                Task.Factory.StartNew(tkone);

                Task.Factory.StartNew(tktwo);

              //  Task.Factory.StartNew(tksample);
                //Task.Factory.StartNew(() => tkone();

                //string machineName2 = "192.168.2.171";
                //lblTkTwo.Text = get_time(machineName2);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ActivitiesLogs("Checked timekeeping Time ");
        }
        void tksample()
        {
            //string machineName = "192.168.2.126";
            //Process proc = new Process();
            //proc.StartInfo.UseShellExecute = false;
            //proc.StartInfo.RedirectStandardOutput = true;
            //proc.StartInfo.FileName = "net";
            //proc.StartInfo.Arguments = @"time \\" + machineName;
            //proc.Start();
            //proc.WaitForExit();

            //List<string> results = new List<string>();

            //while (!proc.StandardOutput.EndOfStream)
            //{
            //    string currentline = proc.StandardOutput.ReadLine();

            //    if (!string.IsNullOrEmpty(currentline))
            //    {
            //        results.Add(currentline);
            //    }
            //}

            //string currentTime = string.Empty;

            //if (results.Count > 0 && results[0].ToLower().StartsWith(@"current time at \\" + machineName.ToLower() + " is "))
            //{
            //    currentTime = results[0].Substring((@"current time at \\" +
            //                  machineName.ToLower() + " is ").Length);
            //}

            //label3.Text = currentTime;
        }
        void tkone()
        {
             string machineName = "192.168.2.136";
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

            lblTkOne.Text = currentTime;
        }
        void tktwo()
        {
            string machineName = "192.168.2.171";
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

            lblTkTwo.Text = currentTime;
        }
        private string get_time(string ip)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.FileName = "net";
            proc.StartInfo.Arguments = @"time \\" + ip;
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

            if (results.Count > 0 && results[0].ToLower().StartsWith(@"current time at \\" + ip.ToLower() + " is "))
            {
                currentTime = results[0].Substring((@"current time at \\" +
                              ip.ToLower() + " is ").Length);
            }

            return currentTime;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button3_Click(sender, e);
        }
        public void ActivitiesLogs(string logs)
        {

            try
            {

                //@"c:\a\UserName.txt"
                const string location = @"UpdateTimeLogs";

                if (!File.Exists(location))
                {
                    var createText = "New Activities Logs" + Environment.NewLine;
                    File.WriteAllText(location, createText);

                }
                var appendLogs = "Activities Logs: " + logs + " " + DateTime.Now + Environment.NewLine;
                File.AppendAllText(location, appendLogs);
            }
            catch (Exception ex)
            {
                const string location = @"UpdateTimeLogs";
                if (!File.Exists(location))
                {
                    TextWriter file = File.CreateText(@"C:\UpdateTimeLogs");
                    var createText = "New Activities Logs" + Environment.NewLine;

                    File.WriteAllText(location, createText);

                }

                var appendLogs = ex.Message + logs + DateTime.Now + Environment.NewLine;
                File.AppendAllText(location, appendLogs);


            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tick = 0;

            timer2.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //timer1.Stop();

            string remoteMachine = "192.168.2.8";
            string appName = "PsExec.exe";
            string args = string.Format("\\\\{0} w32tm /resync", remoteMachine); //  Pacific Standard Time
            Process.Start(appName, args);

        }

        private void button5_Click(object sender, EventArgs e)
        {
           // var one = 
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (tick == 10)
            {
                button3_Click(sender, e);
            }
            textBox1.Text = DateTime.Now.ToString("hh:mm:ss ttt");
            tick++;


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                Form2 ac = new Form2();
                ac.Show();
            }
        }
    }
}
