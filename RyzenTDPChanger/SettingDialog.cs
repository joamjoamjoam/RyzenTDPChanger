using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RyzenTDPChanger
{
    public partial class SettingDialog : Form
    {
        String settingFilePath = "";
        Dictionary<String, byte> platformDict = new Dictionary<string, byte>();
        public SettingDialog(String settingFilePath, String [] platforms)
        {
            InitializeComponent();
            this.settingFilePath = settingFilePath;

            if (platforms != null && platforms.Length > 0)
            {
                foreach (string platform in platforms)
                {
                    if (!platformDict.ContainsKey(platform))
                    {
                        platformDict.Add(platform, 0);
                    }
                }
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            JObject saveObj = new JObject();

            saveObj.Add("launchBoxTDP", (uint)launchBoxNumUD.Value);
            saveObj.Add("defaultGameTDP", (uint)defaultGameNumUD.Value);
            saveObj.Add("bigBoxTDP", (uint)bigBoxNumUD.Value);
            saveObj.Add("ryzenAdjPath", (String) ryzenAdjPathTB.Text);
            saveObj.Add("ryzenAdjEnabled", (bool) enabledCB.Checked);


            JObject platforms = new JObject();
            List<String> keys = platformDict.Keys.ToList();
            for (int i = 0; i < keys.Count; i++)
            {
                String key = keys[i];

                platforms.Add(key, Byte.Parse((String)platformGridView.Rows[i].Cells[1].Value));
            }

            if (platformDict.Keys.Count > 0)
            {
                saveObj.Add("platforms", platforms);
            }

            try
            {
                using (StreamWriter file = File.CreateText(settingFilePath))
                using (JsonTextWriter writer = new JsonTextWriter(file))
                {
                    saveObj.WriteTo(writer);
                }
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error Saving Settings: {ex.Message}");
            }
        }

        private void SettingDialog_Load(object sender, EventArgs e)
        {
            if (File.Exists(settingFilePath))
            {
                try
                {
                    JObject saveObj = JObject.Parse(File.ReadAllText(settingFilePath));

                    try
                    {
                        ulong tmp = saveObj["launchBoxTDP"].Value<ulong>();

                        if (tmp > 0 && tmp <= 100)
                        {
                            launchBoxNumUD.Value = tmp;
                        }
                        else
                        {
                            launchBoxNumUD.Value = 10;
                        }
                    }
                    catch
                    {
                        launchBoxNumUD.Value = 10;
                    }


                    try
                    {
                        ulong tmp = saveObj["defaultGameTDP"].Value<ulong>();

                        if (tmp > 0 && tmp <= 100)
                        {
                            defaultGameNumUD.Value = tmp;
                        }
                        else
                        {
                            defaultGameNumUD.Value = 10;
                        }
                    }
                    catch
                    {
                        defaultGameNumUD.Value = 10;
                    }

                    try
                    {
                        bool tmp = saveObj["ryzenAdjEnabled"].Value<bool>();

                        enabledCB.Checked = tmp;
                    }
                    catch
                    {
                        enabledCB.Checked = false;
                    }

                    try
                    {
                        ulong tmp = saveObj["bigBoxTDP"].Value<ulong>();

                        if (tmp > 0 && tmp <= 100)
                        {
                            bigBoxNumUD.Value = tmp;
                        }
                        else
                        {
                            bigBoxNumUD.Value = 10;
                        }
                    }
                    catch
                    {
                        bigBoxNumUD.Value = 10;
                    }

                    try
                    {
                        String tmp = saveObj["ryzenAdjPath"].Value<String>();
                        ryzenAdjPathTB.Text = tmp;
                    }
                    catch
                    {
                        ryzenAdjPathTB.Text = "";
                    }


                    try
                    {
                        JObject platformObj = saveObj["platforms"].Value<JObject>();


                        foreach (KeyValuePair<String, Byte> kp in platformDict)
                        {
                            try
                            {
                                byte tmp = platformObj[kp.Key].Value<Byte>();
                                if (tmp > 0 && tmp <= 100)
                                {
                                    platformDict[kp.Key] = tmp;
                                }
                                else
                                {
                                    platformDict[kp.Key] = 0;
                                }
                            }
                            catch
                            {
                                // use 0 as default
                            }
                        }
                    }
                    catch
                    {
                        // Defaults setup in constructor
                    }

                }
                catch
                {
                    // Use Defaults
                    launchBoxNumUD.Value = 10;
                    bigBoxNumUD.Value = 10;
                    defaultGameNumUD.Value = 15;

                }
            }
            else
            {
                // Use Defaults
                launchBoxNumUD.Value = 10;
                bigBoxNumUD.Value = 10;
                defaultGameNumUD.Value = 15;
            }

            // Add Platform Defaults to View
            foreach (String key in platformDict.Keys)
            {
                byte value = platformDict[key];

                try
                {
                    platformGridView.Rows.Add(new string[] { new CultureInfo("en-US", false).TextInfo.ToTitleCase(key), value.ToString() });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void platformGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex];
            String newValue = e.FormattedValue.ToString().Replace(" ", "");

            if (e.ColumnIndex == 1)
            {
                if (!Regex.IsMatch(newValue, "[0-9]+") || int.Parse(newValue) < 0 || int.Parse(newValue) > 100)
                {
                    MessageBox.Show("Given TDP was Invalid. 0 < TDP (W) <= 100");
                    cell.Value = "0";
                    e.Cancel = true;
                }
                else
                {
                    cell.Value = int.Parse(newValue).ToString();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process webProcess = new Process();
                // true is the default, but it is important not to set it to false
                webProcess.StartInfo.UseShellExecute = true;
                webProcess.StartInfo.FileName = "https://github.com/FlyGoat/RyzenAdj";
                webProcess.Start();
            }
            catch
            {

            }
            
        }

        private void ryzenAdjPathBrowseBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = Application.ExecutablePath,
                Title = "Select RyzenAdj Executable ...",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "exe",
                Filter = "exe files (*.exe)|*.exe",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ryzenAdjPathTB.Text = openFileDialog1.FileName;
            }
        }
    }
}
