using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Image_Converter_sharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> s = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Multiselect = true;
            if (o.ShowDialog() == DialogResult.OK)
            {
                foreach (string x in o.FileNames)
                {
                    if (s.Contains(x) == false)
                    {
                        s.Add(x);
                        string[] Split = x.Split(new Char[] { '\\' });
                        listBox1.Items.Add(Split[Split.Length - 1]);
                    }
                }
                listBox1.SelectedIndex = 0;
            }

        }

        public void conv(string path, string ext)
        {

            System.Drawing.Image image1;
            string y;
            System.Drawing.Imaging.ImageFormat z;
            if (ext == "tiff")
            {
                z = System.Drawing.Imaging.ImageFormat.Tiff;

            }
            else if (ext == "gif")
            {
                z = System.Drawing.Imaging.ImageFormat.Gif;

            }
            else if (ext == "jpeg")
            {
                z = System.Drawing.Imaging.ImageFormat.Jpeg;

            }
            else if (ext == "png")
            {
                z = System.Drawing.Imaging.ImageFormat.Png;

            }
            else if (ext == "wmf")
            {
                z = System.Drawing.Imaging.ImageFormat.Wmf;

            }
            else
            {
                z = System.Drawing.Imaging.ImageFormat.Bmp;

            }
            double f = (100f/(s.Count));
            double k=0;
            foreach (string x in s)
            {
                image1 = System.Drawing.Image.FromFile(x);
                string[] Split = x.Split(new Char[] { '\\', '.' });
                y = Split[Split.Length - 2];
                //listBox1.SelectedIndex = (listBox1.SelectedIndex + 1) % listBox1.Items.Count;
                image1.Save(path + "\\" + y + "." + ext, z);
                if ((progressBar1.Value + f) <= 100)
                {
                    k += f;
                    if (k > 1)
                    {
                        progressBar1.Value += Convert.ToInt32(k);
                        k = k - Convert.ToInt32(k);
                    }
                }
                image1.Dispose();

            }
            progressBar1.Value=100;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (s.Count > 0)
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    progressBar1.Show();
                    label1.Show();

                    //this.Cursor = Cursors.WaitCursor;
                    conv(fb.SelectedPath, comboBox1.SelectedItem.ToString());
                    //this.Cursor = Cursors.Default;
                    MessageBox.Show("Done!!");
                    progressBar1.Hide();
                    label1.Hide();

                }
            }
            else
            {
                MessageBox.Show("Add Some Pics First..!!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }
                pictureBox1.Image = System.Drawing.Image.FromFile(s[listBox1.SelectedIndex]);
            }
            else
            {
                pictureBox1.Image = null;
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }



        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                listBox1.SelectedIndex = (listBox1.SelectedIndex+1)%listBox1.Items.Count;
                if (listBox1.SelectedIndex != 0)
                {
                    s.RemoveAt(listBox1.SelectedIndex - 1);
                    listBox1.Items.RemoveAt((listBox1.SelectedIndex - 1) % listBox1.Items.Count);
                }
                else
                {
                    s.RemoveAt(listBox1.Items.Count - 1);
                    listBox1.Items.RemoveAt(listBox1.Items.Count-1);
                }

            }
        }
    }
}