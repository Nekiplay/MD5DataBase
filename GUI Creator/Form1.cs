using MD5DataBase;
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

namespace GUI_Creator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MD5Info current_md5_info;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                guna2TextBox1.Text = "";
                guna2TextBox2.Text = "";
                guna2TextBox3.Text = "";
                guna2TextBox4.Text = "";
                guna2TextBox5.Text = "";

                string[] names = Enum.GetNames(typeof(MD5Info.Type));
                int index = 0;
                foreach (string n in names)
                {
                    checkedListBox1.SetItemChecked(index, false);
                    index++;
                }

                FileInfo file = new FileInfo(openFileDialog.FileName);
                string md5 = HashUtils.GetMD5Hash(file);
                string sha256 = HashUtils.GetSHA2565Hash(file);
                label1.Text = "MD5: " + md5;
                label2.Text = "SHA256: " + sha256;

                MD5Extractor extractor = new MD5Extractor();
                try
                {
                    MD5Info info = extractor.GetInfoFromCloud(md5);
                    if (info != null)
                    {
                        current_md5_info = info;
                        guna2TextBox1.Text = info.Name;
                        guna2TextBox2.Text = info.Version;
                        guna2TextBox3.Text = info.Description;
                        guna2TextBox4.Text = info.Game;
                        guna2TextBox5.Text = info.Site;

                        foreach (var type in info.Types)
                        {
                            string[] names2 = Enum.GetNames(typeof(MD5Info.Type));
                            int index2 = 0;
                            foreach (string n in names2)
                            {
                                if (n == type.ToString())
                                {
                                    checkedListBox1.SetItemChecked(index2, true);
                                }
                                index2++;
                            }
                        }
                    }
                }
                catch
                {
                    current_md5_info = new MD5Info();
                    MD5Info.Hash hash = new MD5Info.Hash();
                    hash.MD5 = md5;
                    hash.SHA256 = sha256;
                    current_md5_info.hash = hash;
                    current_md5_info.Extension = file.Extension;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] names = Enum.GetNames(typeof(MD5Info.Type));
            checkedListBox1.Items.AddRange(names);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (current_md5_info != null)
            {
                current_md5_info.Name = guna2TextBox1.Text;
                current_md5_info.Version = guna2TextBox2.Text;
                current_md5_info.Description = guna2TextBox3.Text;
                current_md5_info.Game = guna2TextBox4.Text;
                current_md5_info.Site = guna2TextBox5.Text;
                foreach (object o in checkedListBox1.CheckedItems)
                {
                    string type = o.ToString();
                    MD5Info.Type type1 = MD5Info.Type.Cheat;
                    Enum.TryParse(type, out type1);
                    current_md5_info.Types.Add(type1);
                }
                Directory.CreateDirectory("output");
                File.Create("output\\" + current_md5_info.hash.MD5 + ".json").Close();
                string json = current_md5_info.ToJson();
                File.WriteAllText("output\\" + current_md5_info.hash.MD5 + ".json", json);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (current_md5_info != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] files = openFileDialog.FileNames;
                    foreach (string f in files)
                    {
                        FileInfo file = new FileInfo(f);
                        string md5 = HashUtils.GetMD5Hash(file);
                        string sha256 = HashUtils.GetSHA2565Hash(file);
                        MD5Info.Hash hash = new MD5Info.Hash();
                        hash.MD5 = md5;
                        hash.SHA256 = sha256;
                        bool find = false;
                        foreach (MD5Info.Hash hash1 in current_md5_info.Libs)
                        {
                            if (hash1.MD5 == hash.MD5 && hash1.SHA256 == hash.SHA256)
                            {
                                find = true;
                            }
                        }
                        if (find == false)
                        {
                            current_md5_info.Libs.Add(hash);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (current_md5_info != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string[] files = openFileDialog.FileNames;
                    foreach (string f in files)
                    {
                        FileInfo file = new FileInfo(f);
                        string md5 = HashUtils.GetMD5Hash(file);
                        string sha256 = HashUtils.GetSHA2565Hash(file);
                        MD5Info.Hash hash = new MD5Info.Hash();
                        hash.MD5 = md5;
                        hash.SHA256 = sha256;
                        bool find = false;
                        foreach (MD5Info.Hash hash1 in current_md5_info.Used)
                        {
                            if (hash1.MD5 == hash.MD5 && hash1.SHA256 == hash.SHA256)
                            {
                                find = true;
                            }
                        }
                        if (find == false)
                        {
                            current_md5_info.Used.Add(hash);
                        }
                    }
                }
            }
        }
    }
}
