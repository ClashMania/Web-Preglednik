using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Web
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void BtnUrl_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                browser.Navigate(TxtUrl.Text);
                await Task.Delay(600);
                TxtUrl.Text = browser.Url.ToString();
            }
        }

        private async void BtnForw_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                browser.GoForward();
                await Task.Delay(100);
                TxtUrl.Text = browser.Url.ToString();
            }            
        }

        private async void BtnBack_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                browser.GoBack();
                await Task.Delay(100);
                TxtUrl.Text = browser.Url.ToString();
            }
        }

        private void BtnRefr_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                browser.Refresh();
                TxtUrl.Text = browser.Url.ToString();
            }
        }

        private async void BtnHome_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                browser.Navigate("Google.com");
                await Task.Delay(600);
                TxtUrl.Text = browser.Url.ToString();
            }            
        }

        private async void TxtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    browser.Navigate(TxtUrl.Text);
                    await Task.Delay(600);
                    TxtUrl.Text = browser.Url.ToString();
                }
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TxtUrl.Text == "")
            {
                MessageBox.Show("Niste na stranici!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                toolCombo1.Items.Add(TxtUrl.Text);
                toolCombo1.SelectedItem = TxtUrl.Text;
                toolCombo1.Focus();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void toolCombo1_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                if (toolCombo1.SelectedItem == null)
                {
                    MessageBox.Show("Niste dodali niti jedan bookmark!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    browser.Navigate(toolCombo1.SelectedItem.ToString());
                    await Task.Delay(600);
                    TxtUrl.Text = browser.Url.ToString();
                }
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolCombo1.Items.Clear();
        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Html File (*.html)|*.html| Htm File (*.htm)|*.htm";
                openFileDialog.FilterIndex = 1;

                StreamReader reader;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    TxtHtml.Clear();
                    reader = new System.IO.StreamReader(openFileDialog.FileName);
                    TxtHtml.Text = reader.ReadToEnd();
                    browser.Navigate(openFileDialog.FileName);
                    await Task.Delay(100);
                    TxtUrl.Text = browser.Url.ToString();
                }
            }            
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Html File (*.html)|*.html| Htm File (*.htm)|*.htm";
            saveFileDialog.DefaultExt = "*.html";
            saveFileDialog.FilterIndex = 1;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine(TxtHtml.Text);
                }
            }
        }

        private void TabtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewTab();
        }

        private void NewTab()
        {
            TabPage tab = new TabPage();
            tabControl1.Controls.Add(tab);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
            WebBrowser browser = new WebBrowser() { ScriptErrorsSuppressed = true };
            browser.Parent = tab;
            browser.Dock = DockStyle.Fill;
            browser.Navigate("Google.com");
            TxtUrl.Text = "Google.com";
            browser.DocumentCompleted += Browser_DocumentCompleted;
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (browser != null)
            {
                tabControl1.SelectedTab.Text = browser.DocumentTitle;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            NewTab();
        }

        private void ClosetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
