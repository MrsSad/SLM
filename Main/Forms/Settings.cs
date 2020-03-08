using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using WeAreDevs_API;

namespace Huj_Cheat.Main.Forms
{
    public partial class Settings : Form
    {
        ExploitAPI api = new ExploitAPI();
        Point lastPoint;
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            Main m = new Main();
            tm.Checked = SLM_Cheat.Properties.Settings.Default.topmost;
            label3.Text = m.version;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Settings_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Settings_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void hide_Tick(object sender, EventArgs e)
        {
        }

        private void tm_CheckedChanged(object sender, EventArgs e)
        {
            SLM_Cheat.Properties.Settings.Default.topmost = tm.Checked;
            SLM_Cheat.Properties.Settings.Default.Save();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            WebClient wc = new WebClient();
            if (!wc.DownloadString("https://pastebin.com/raw/CYhBznJr").Contains(m.version))
            {
                if (MessageBox.Show("Wait, it's look like we have an update! Would you like to update '" + m.name + "'", "SLM Cheat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TopMost = false;
                    m.TopMost = false;
                    Process.Start("https://slmcheats.weebly.com/download.html");
                }
            } else
            {
                MessageBox.Show("You have latest version!", "SLM Cheat", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
