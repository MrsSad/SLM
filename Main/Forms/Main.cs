using Huj_Cheat.Main.Scripts;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Huj_Cheat.Main.Forms
{
    public partial class Main : Form
    {
        //
        public string version = "v1.0"; // Your version here
        public string name = "SLM Cheat"; // Your name here
        Point lastPoint;
        //



        public Main()
        {
            InitializeComponent();
        }
        ExploitAPI api = new ExploitAPI();
        WebClient wc = new WebClient();
        private string defPath = Application.StartupPath + "//Monaco//";

        private void addIntel(string label, string kind, string detail, string insertText)
        {
            string text = "\"" + label + "\"";
            string text2 = "\"" + kind + "\"";
            string text3 = "\"" + detail + "\"";
            string text4 = "\"" + insertText + "\"";
            webBrowser1.Document.InvokeScript("AddIntellisense", new object[]
            {
                label,
                kind,
                detail,
                insertText
            });
        }

        private void addGlobalF()
        {
            string[] array = File.ReadAllLines(this.defPath + "//globalf.txt");
            foreach (string text in array)
            {
                bool flag = text.Contains(':');
                if (flag)
                {
                    this.addIntel(text, "Function", text, text.Substring(1));
                }
                else
                {
                    this.addIntel(text, "Function", text, text);
                }
            }
        }

        private void addGlobalV()
        {
            foreach (string text in File.ReadLines(this.defPath + "//globalv.txt"))
            {
                this.addIntel(text, "Variable", text, text);
            }
        }

        private void addGlobalNS()
        {
            foreach (string text in File.ReadLines(this.defPath + "//globalns.txt"))
            {
                this.addIntel(text, "Class", text, text);
            }
        }

        private void addMath()
        {
            foreach (string text in File.ReadLines(this.defPath + "//classfunc.txt"))
            {
                this.addIntel(text, "Method", text, text);
            }
        }

        private void addBase()
        {
            foreach (string text in File.ReadLines(this.defPath + "//base.txt"))
            {
                this.addIntel(text, "Keyword", text, text);
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            timer1.Start();
            label1.Show();
            label2.Text = name;
            label3.Text = version;
            WebClient wc = new WebClient();
            wc.Proxy = null;
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                bool flag2 = registryKey.GetValue(friendlyName) == null;
                if (flag2)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                registryKey = null;
                friendlyName = null;
            }
            catch (Exception)
            {
            }
            webBrowser1.Url = new Uri(string.Format("file:///{0}/Monaco/Monaco.html", Directory.GetCurrentDirectory()));
            await Task.Delay(500);
            webBrowser1.Document.InvokeScript("SetTheme", new string[]
            {
                   "Dark" 
                   /*
                    There are 2 Themes Dark and Light
                   */
            });
            addBase();
            addMath();
            addGlobalNS();
            addGlobalV();
            addGlobalF();
            webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                 "-- SLMCheat v1.0 --\n"+
                 "-- Author: SLM Team | slayer --\n"+
                 "-- Credits: --\n"+
                 "-- WeAreDevs: API --\n"+
                 "-- PareX: Monaco --"
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TopMost = false;
            timer1.Stop();
            if (api.isAPIAttached() == true)
            {
                HtmlDocument document = webBrowser1.Document;
                string scriptName = "GetText";
                object[] args = new string[0];
                object obj = document.InvokeScript(scriptName, args);
                string script = obj.ToString();
                api.SendLuaScript(script);
            } else
            {
                MessageBox.Show("Please attach SLM Cheats", "SLM Cheats", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TopMost = false;
            timer1.Stop();
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    webBrowser1.Document.InvokeScript("SetText", new object[]
                    {
                          MainText
                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            webBrowser1.Document.InvokeScript("SetText", new object[]
            {
                ""
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TopMost = false;
            timer1.Stop();
            new Settings().ShowDialog();
            timer1.Start();
        }


        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Main_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (api.isAPIAttached() == false)
            {
                label1.Text = "Not attached!";
                label1.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                label1.Text = "Attached!";
                label1.ForeColor = System.Drawing.Color.Green;
            }
            if (SLM_Cheat.Properties.Settings.Default.topmost == true)
            {
                TopMost = true;
            } else
            {
                TopMost = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            TopMost = false;
            if (api.isAPIAttached() == false)
            {
                Main m = new Main();
                Process[] p = Process.GetProcessesByName("robloxplayerbeta");
                if (p.Length != 0)
                {
                    api.LaunchExploit();
                }
                else
                {
                    MessageBox.Show("Please run roblox first!", "SLM Cheat", MessageBoxButtons.OK, MessageBoxIcon.None);
                }
            }
            else
            {
                MessageBox.Show("Already injected! No problem!", "SLM Cheat", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            timer1.Start();
        }
    }
}
