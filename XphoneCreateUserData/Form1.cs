﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace XphoneCreateUserData
{
    public partial class Form1 : Form
    {
        class LanguageConfig
        {
            public string FileName;
            public int Order;
        }

        string FILE_LANG_CFG = "sys_lang.cfg";

        // Need synth with OMAP define
        const string FILE_LANG_0 = "text-eng";
        const string FILE_LANG_1 = "text-vietnamese";

        List<LanguageConfig> mLanguages = new List<LanguageConfig>();

        string HeaderFile;
        List<String> SourceFiles;


        public Form1()
        {
            InitializeComponent();
            textBoxTextFoder.Text = "C://CTY//Xphone//os//source//libs//text";
            SetDefaultLanguage();

            if (File.Exists(FILE_LANG_CFG))
            {
                string s = File.ReadAllText(FILE_LANG_CFG);
                ReadLanguages(s);

            }
        }

        void SetDefaultLanguage()
        {
            LanguageConfig language;
            // Clear all data old
            mLanguages.Clear();

            language = new LanguageConfig();
            language.FileName = FILE_LANG_0;
            language.Order = 0;
            mLanguages.Add(language);

            language = new LanguageConfig();
            language.FileName = FILE_LANG_1;
            language.Order = 1;
            mLanguages.Add(language);
        }

        void ReadLanguages(string fileContent)
        {
            string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int numLines = lines.Length;
            string line;

            List<LanguageConfig> Languages = new List<LanguageConfig>();

            // text_eng.c:1
            for (int i = 0; i < numLines; i++)
            {
                line = lines[i].Trim().Replace("*.c", "").Replace("*.cpp", "");
                string[] s = line.Split(new[] { ":" }, StringSplitOptions.None);
                if (s != null)
                {
                    if (s.Length > 1)
                    {
                        string fileName = s[0].Trim();
                        int order = -1;
                        int.TryParse(s[1].Trim(), out order);
                        if (order >= 0)
                        {
                            LanguageConfig language = new LanguageConfig();
                            language.FileName = fileName;
                            language.Order = order;
                            Languages.Add(language);
                        }
                    }
                }
            }

            if (Languages.Count > 0)
            {
                mLanguages = Languages;
            }
            else
            {
                MessageBox.Show("Cannot parse language list");
            }
        }

        void WriteLanguages(string FileName)
        {
            int numLangs = mLanguages.Count;
            string s = "The source foder in project OS: os/source/libs/text\r\n";
            for (int i = 0; i < numLangs; i++)
            {
                s += mLanguages[i].FileName + ":" + mLanguages[i].Order;
            }
            File.WriteAllText(FileName, s);
        }
        void setDefault_text(List<TextObj> textObjs)
        {
            string t = null;
            for (int i=0;i<textObjs.Count;i++)
            {
                string text = textObjs[i].Name;
                t += "text." + text + "[sizeof(text." + text + ") / 2 - 1] = NULL;" +"\n";
            }
            if(File.Exists("setDefault_text.txt"))
            {
                File.Delete("setDefault_text.txt");
            }
            File.WriteAllText("setDefault_text.txt", t);
        }

        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.Arguments = folderPath;
                startInfo.FileName = "explorer.exe";
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            TextObj FindObject(string initCode)
            {
                TextObj obj = new TextObj();
                string TextObjName = "text";
                string preVar = TextObjName;

                int idxStartVar = initCode.IndexOf('.') + 1;
                int idxEndVar = initCode.IndexOf(',');
                string var = initCode.Substring(idxStartVar, idxEndVar - idxStartVar);
                var = var.Replace(" ", "").Replace("\t", "");

                int idxStartUnicode = initCode.IndexOf("\"") + 1;
                string strUnicode = initCode.Substring(idxStartUnicode);
                int idxEndUnicode = strUnicode.IndexOf("\"");
                string unicode = strUnicode.Substring(0, idxEndUnicode);

                obj.Name = var;
                obj.Unicode = unicode;

                return obj;
            }

            List<TextObj> ParseBaseObject(string content)
            {
                List<TextObj> ListBaseTextObj = new List<TextObj>();
                // Get ListBaseTextObj from content
                string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                int count = lines.Length;
                for (int i = 0; i < count; i++)
                {
                    string line = lines[i].Trim();
                    if (line.StartsWith("init_string"))
                    {
                        if (!line.Contains(";"))
                        {
                            line += lines[i + 1].Trim();
                        }
                        TextObj obj = FindObject(line);

                        ListBaseTextObj.Add(obj);
                    }
                }
                return ListBaseTextObj;
            }

            FindResource(textBoxTextFoder.Text);

            // Hardcode for test
            string headerFileName = this.HeaderFile;
            string headerContent = File.ReadAllText(headerFileName);
            List<TextObj> textObjs0 = ParseBaseObject(File.ReadAllText(SourceFiles[0]));
            List<TextObj> textObjs1 = ParseBaseObject(File.ReadAllText(SourceFiles[1]));
            List<TextObj> textObjs2 = ParseBaseObject(File.ReadAllText(SourceFiles[2]));

            List<TextObj> headerObjs = TextObjectParser.ParseDefineFile(headerContent);

            dataGridViewVariable.Rows.Clear();
            int numBaseObjs0 = textObjs0.Count;
            int numBaseObjs1 = textObjs1.Count;
            int numBaseObjs2 = textObjs2.Count;

            int numHeaderObjs = headerObjs.Count;

            TextObj cuurobj;
            string varReadable;

            for (int h = 0; h < numHeaderObjs; h++)
            {
                string s0 = "", s1 = "", s2 = "";
                for (int i = 0; i < numBaseObjs0; i++)
                {
                    if (headerObjs[h].Name == textObjs0[i].Name)
                    {
                        cuurobj = textObjs0[i];
                        varReadable = Encoding.BigEndianUnicode.GetString(cuurobj.ByteArray);
                        s0 = varReadable;

                        break;
                    }
                }

                for (int i = 0; i < numBaseObjs1; i++)
                {
                    if (headerObjs[h].Name == textObjs1[i].Name)
                    {
                        cuurobj = textObjs1[i];
                        varReadable = Encoding.BigEndianUnicode.GetString(cuurobj.ByteArray);
                        s1 = varReadable;

                        break;
                    }
                }

                for (int i = 0; i < numBaseObjs2; i++)
                {
                    if (headerObjs[h].Name == textObjs2[i].Name)
                    {
                        cuurobj = textObjs2[i];
                        varReadable = Encoding.BigEndianUnicode.GetString(cuurobj.ByteArray);
                        s2 = varReadable;

                        break;
                    }
                }

                dataGridViewVariable.Rows.Add(headerObjs[h].Name, s0, s1, s2);
            }
        }

        public static UInt16[] CombineData(UInt16[] first, UInt16[] second)
        {
            UInt16[] ret = new UInt16[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length * sizeof(UInt16));
            Buffer.BlockCopy(second, 0, ret, first.Length * sizeof(UInt16), second.Length * sizeof(UInt16));
            return ret;
        }

        public static byte[] CombineData(byte[] first, byte[] second)
        {
            byte[] ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length * sizeof(byte));
            Buffer.BlockCopy(second, 0, ret, first.Length * sizeof(byte), second.Length * sizeof(byte));
            return ret;
        }

        /// <summary>
        /// get order language by name with out full path
        /// </summary>
        /// <param name="fileNameOnly"></param>
        /// <returns></returns>
        int GetOrderByNameFile(string fileNameOnly)
        {
            int order = -1;
            for (int i = 0; i < mLanguages.Count; i++)
            {
                if (fileNameOnly == mLanguages[i].FileName)
                {
                    order = mLanguages[i].Order;
                    break;
                }
            }
            return order;
        }

        private void FindResource(string foderName)
        {
            string[] AllFiles = Directory.GetFiles(foderName, "*", SearchOption.AllDirectories);
            int numFiles = AllFiles.Length;
            List<string> srcFiles = new List<string>();
            List<string> headerFiles = new List<string>();
            for (int i = 0; i < numFiles; i++)
            {
                if (AllFiles[i].EndsWith(".c") || AllFiles[i].EndsWith(".cpp"))
                {
                    srcFiles.Add(AllFiles[i]);
                }
                else if (AllFiles[i].EndsWith(".h") || AllFiles[i].EndsWith(".h"))
                {
                    headerFiles.Add(AllFiles[i]);
                }
            }

            if (headerFiles.Count > 0)
            {
                if (headerFiles.Count > 1)
                {
                    MessageBox.Show("More than 1 header file. You need config in setting option");
                }

                HeaderFile = headerFiles[0];
                SourceFiles = srcFiles;
            }
            else
            {
                MessageBox.Show("Cannot found the header file. Select another foder");
            }
        }




        private void Button2_Click(object sender, EventArgs e)
        {
            string currDir = Directory.GetCurrentDirectory();
            OpenFolder(currDir);
        }



        private void ButtonOpenTextFoder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder contain system text source file";
            dlg.ShowNewFolderButton = true;

            DialogResult result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxTextFoder.Text = dlg.SelectedPath;
            }
        }



        /*
         * NUM_LANG - 4 Byte
         * LANG_SIZE - 4 Byte
         * NAME - 100 Byte
         * {ORDER - 4Byte, Content - LANG_SIZE}
         * {ORDER - 4Byte, Content - LANG_SIZE}
         * {ORDER - 4Byte, Content - LANG_SIZE}
         * ...
         */
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxTextFoder.Text))
            {
                MessageBox.Show("Foder not existed");
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            // Find resource from foder
            FindResource(textBoxTextFoder.Text);

            string headerFileName = this.HeaderFile;
            string headerContent = File.ReadAllText(headerFileName);

            List<TextObj> textObjs = TextObjectParser.ParseDefineFile(headerContent);
            setDefault_text(textObjs);
            int numTextObjs = textObjs.Count;
            int listObjLenByByte = TextObjectParser.LengthListTextObject(textObjs) * sizeof(UInt16);

            byte[] compressedData = new byte[8];
            int[] byteCount = new int[] { SourceFiles.Count };
            int[] byteLen = new int[1] { listObjLenByByte };

            Buffer.BlockCopy(byteCount, 0, compressedData, 0, sizeof(int));
            Buffer.BlockCopy(byteLen, 0, compressedData, 4, sizeof(int));

            for (int srcCnt = 0; srcCnt < SourceFiles.Count; srcCnt++)
            {
                
                string sourceFileName = SourceFiles[srcCnt];
                string srcContent;
                //List<UInt16[]> textDatas;
                UInt16[] dataSinggleLang;

                if (!File.Exists(sourceFileName))
                {
                    // Return when file not existed
                    MessageBox.Show("File error: " + sourceFileName);
                    continue;
                }

                srcContent = File.ReadAllText(sourceFileName);
                //textDatas = new List<UInt16[]>();
                dataSinggleLang = new UInt16[0];

                for (int i = 0; i < numTextObjs; i++)
                {
                    UInt16[] contentParser = LanguageParser.ParseTextObjectContent(textObjs[i], srcContent);
                    //textDatas.Add(contentParser);

                    //Console.WriteLine(contentParser.Length);
                    dataSinggleLang = CombineData(dataSinggleLang, contentParser);
                }

                byte[] dataSinggleLangU8 = new byte[dataSinggleLang.Length * sizeof(UInt16)];
                Buffer.BlockCopy(dataSinggleLang, 0, dataSinggleLangU8, 0, dataSinggleLangU8.Length);
                byte[] langPkg = new byte[0];

                string fileOnly = Path.GetFileNameWithoutExtension(sourceFileName);
                byte[] nameFileByArr = new byte[100];
                byte[] nameTmp = Encoding.ASCII.GetBytes(fileOnly);
                int[] orderInt = new int[1] { GetOrderByNameFile(Path.GetFileName(fileOnly)) };
                byte[] orderByte = new byte[sizeof(int)];
                Buffer.BlockCopy(nameTmp, 0, nameFileByArr, 0, nameTmp.Length);

                Buffer.BlockCopy(orderInt, 0, orderByte, 0, orderByte.Length);

                langPkg = CombineData(langPkg, orderByte); // Order first
                langPkg = CombineData(langPkg, nameFileByArr); // then name
                langPkg = CombineData(langPkg, dataSinggleLangU8); // then content
                //File.WriteAllBytes(fileOnly + ".bin", result);
                TraceLog(string.Format("Lang: {0} : {1} Bytes , Language order: {2}", fileOnly, dataSinggleLangU8.Length, orderInt[0]));
                compressedData = CombineData(compressedData, langPkg);
            }

            File.WriteAllBytes("sys_text.bin", compressedData);
            Cursor.Current = Cursors.Default;
            MessageBox.Show("Genenate success: " + Path.GetFullPath("sys_text.bin"));
        }

        private void EditConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void TraceLog(string s)
        {
            Console.WriteLine(s);
        }

        private void textBoxTextFoder_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
