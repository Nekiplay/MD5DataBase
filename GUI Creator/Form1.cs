using MD5DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        SHA3FileInfo current_md5_info;
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

                string[] names = Enum.GetNames(typeof(SHA3FileInfo.Type));
                int index = 0;
                foreach (string n in names)
                {
                    checkedListBox1.SetItemChecked(index, false);
                    index++;
                }

                string[] names3 = Enum.GetNames(typeof(SHA3FileInfo.PornographyType));
                int index3 = 0;
                foreach (string n in names3)
                {
                    checkedListBox2.SetItemChecked(index3, false);
                    index3++;
                }

                FileInfo file = new FileInfo(openFileDialog.FileName);
                string md5 = HashUtils.GetMD5Hash(file);
                string sha256 = HashUtils.GetSHA2565Hash(file);
                string sha3 = HashUtils.GetSHA3Hash(file);
                label1.Text = "MD5: " + md5;
                label2.Text = "SHA3: " + sha3;

                SHA3Extractor extractor = new SHA3Extractor();
                try
                {
                    SHA3FileInfo info = extractor.GetFileInfoFromCloud(sha3);
                    if (info != null)
                    {
                        current_md5_info = info;
                        guna2TextBox1.Text = info.Name;
                        guna2TextBox2.Text = info.Version;
                        guna2TextBox3.Text = info.Description;
                        guna2TextBox4.Text = info.Game;
                        guna2TextBox5.Text = info.Site;
                        info.OriginalName = file.Name;

                        foreach (var type in info.Types)
                        {
                            string[] names2 = Enum.GetNames(typeof(SHA3FileInfo.Type));
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

                        foreach (var type in info.PornographyTypes)
                        {
                            string[] names2 = Enum.GetNames(typeof(SHA3FileInfo.PornographyType));
                            int index2 = 0;
                            foreach (string n in names2)
                            {
                                if (n == type.ToString())
                                {
                                    checkedListBox2.SetItemChecked(index2, true);
                                }
                                index2++;
                            }
                        }
                    }
                }
                catch
                {
                    current_md5_info = new SHA3FileInfo();
                    HashInfo hash = new HashInfo(md5, sha256, sha3);
                    current_md5_info.hash = hash;
                    current_md5_info.OriginalName = file.Name;
                    current_md5_info.Extension = file.Extension;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.AddRange(Enum.GetNames(typeof(SHA3FileInfo.Type)));
            checkedListBox2.Items.AddRange(Enum.GetNames(typeof(SHA3FileInfo.PornographyType)));
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
                current_md5_info.Types.Clear();
                current_md5_info.PornographyTypes.Clear();
                foreach (object o in checkedListBox1.CheckedItems)
                {
                    string type = o.ToString();
                    SHA3FileInfo.Type type1 = SHA3FileInfo.Type.Cheat;
                    Enum.TryParse(type, out type1);
                    current_md5_info.Types.Add(type1);
                }
                foreach (object o in checkedListBox2.CheckedItems)
                {
                    string type = o.ToString();
                    SHA3FileInfo.PornographyType type1 = SHA3FileInfo.PornographyType.Other;
                    Enum.TryParse(type, out type1);
                    current_md5_info.PornographyTypes.Add(type1);
                }
                Directory.CreateDirectory("output");
                Directory.CreateDirectory("output\\files");
                string path = "output\\files\\" + current_md5_info.hash.SHA3256 + ".json";
                File.Create(path).Close();
                string json = current_md5_info.ToJson();
                File.WriteAllText(path, json);
                File.SetCreationTime(path, new DateTime(2023, 1, 1));
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
                        string sha3256 = HashUtils.GetSHA3Hash(file);
                        HashInfo hash = new HashInfo(md5, sha256, sha3256);
                        bool find = false;
                        foreach (HashInfo hash1 in current_md5_info.Libs)
                        {
                            if (hash1.MD5 == hash.MD5 && hash1.SHA3256 == hash.SHA3256)
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
                        string sha3256 = HashUtils.GetSHA3Hash(file);
                        HashInfo hash = new HashInfo(md5, sha256, sha3256);
                        bool find = false;
                        foreach (HashInfo hash1 in current_md5_info.Used)
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

        private void button5_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                string text = guna2TextBox6.Text;
                Directory.CreateDirectory("output");
                Directory.CreateDirectory("output\\text");
                try
                {
                    string md5_utf8 = HashUtils.GetMD5Hash(text, Encoding.BigEndianUnicode);
                    string sha256_utf8 = HashUtils.GetSHA2565Hash(text, Encoding.BigEndianUnicode);
                    string sha3256 = HashUtils.GetSHA3Hash(text, Encoding.BigEndianUnicode);
                    HashInfo hashInfo = new HashInfo(md5_utf8, sha256_utf8, sha3256);
                    SHA3TextInfo textInfo = new SHA3TextInfo();
                    textInfo.text = text;
                    textInfo.hash = hashInfo;
                    string path = "output\\text\\" + sha3256 + ".json";
                    File.Create(path).Close();
                    string json = textInfo.ToJson();
                    File.WriteAllText(path, json);
                } 
                catch { }
            });
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
