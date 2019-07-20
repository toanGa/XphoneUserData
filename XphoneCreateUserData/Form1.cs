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

namespace XphoneCreateUserData
{
    public partial class Form1 : Form
    {
        // Need synth with OMAP define
        const string FILE_LANG_0 = "text-eng.c";
        const string FILE_LANG_1 = "text-vietnamese.c";


        string HeaderFile;
        List<String> SourceFiles;


        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        public static UInt16[] CombineData(UInt16[] first, UInt16[] second)
        {
            UInt16[] ret = new UInt16[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length*sizeof(UInt16));
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
            if(fileNameOnly == FILE_LANG_0)
            {
                order = 0;
            }
            else if(fileNameOnly == FILE_LANG_1)
            {
                order = 1;
            }
            return order;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes("sys_text.bin");

            int[] numLang = new int[1];
            int[] LangSize = new int[1];

            Buffer.BlockCopy(data, 0, numLang, 0, 4);
            Buffer.BlockCopy(data, 4, LangSize, 0, 4);
            for(int i = 0; i < numLang[0]; i++)
            {
                byte[] lang_header = new byte[100 + 4];
                byte[] lang_content = new byte[LangSize[0]];
                int[] lang_order = new int[1];
                byte[] lang_name = new byte[100];
               
                Buffer.BlockCopy(data, 4 + 4 + i * (100 + 4 + LangSize[0]), lang_header, 0, lang_header.Length);
                Buffer.BlockCopy(data, 4 + 4 + i * (100 + 4 + LangSize[0]) + lang_header.Length, lang_content, 0, lang_content.Length);

                Buffer.BlockCopy(lang_header, 0, lang_order, 0, 4);
                Buffer.BlockCopy(lang_header, 4, lang_name, 0, lang_name.Length);
                File.WriteAllBytes("test.bin", lang_content);

            }
        }

        private void ButtonOpenTextFoder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.Description = "Select folder contain system text source file";
            dlg.ShowNewFolderButton = true;

            DialogResult result = dlg.ShowDialog();
            if(result == DialogResult.OK)
            {
                textBoxTextFoder.Text = dlg.SelectedPath;

                string[] AllFiles = Directory.GetFiles(dlg.SelectedPath, "*", SearchOption.AllDirectories);
                int numFiles = AllFiles.Length;
                List<string> srcFiles = new List<string>();
                List<string> headerFiles = new List<string>();
                for (int i = 0; i < numFiles; i++)
                {
                    if (AllFiles[i].EndsWith(".c") || AllFiles[i].EndsWith(".cpp"))
                    {
                        srcFiles.Add(AllFiles[i]);
                    }
                    else if(AllFiles[i].EndsWith(".h") || AllFiles[i].EndsWith(".h"))
                    {
                        headerFiles.Add(AllFiles[i]);
                    }
                }

                if(headerFiles.Count > 0)
                {
                    if(headerFiles.Count > 1)
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
            string headerFileName = this.HeaderFile;
            string headerContent = File.ReadAllText(headerFileName);
           
            List<TextObj> textObjs = TextObjectParser.ParseDefineFile(headerContent);
            int numTextObjs = textObjs.Count;
            int listObjLenByByte = TextObjectParser.LengthListTextObject(textObjs) * sizeof(UInt16);

            byte[] compressedData = new byte[8];
            int[] byteCount = new int[] { SourceFiles.Count };
            int[] byteLen = new int[1] { listObjLenByByte };

            Buffer.BlockCopy(byteCount, 0, compressedData, 0, sizeof(int));
            Buffer.BlockCopy(byteLen, 0, compressedData, 4, sizeof(int));

            int orderFake = 1;

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

                    Console.WriteLine(contentParser.Length);
                    dataSinggleLang = CombineData(dataSinggleLang, contentParser);
                }

                byte[] dataSinggleLangU8 = new byte[dataSinggleLang.Length * sizeof(UInt16)];
                Buffer.BlockCopy(dataSinggleLang, 0, dataSinggleLangU8, 0, dataSinggleLangU8.Length);
                byte[] langPkg = new byte[0];

                string fileOnly = Path.GetFileNameWithoutExtension(sourceFileName);
                byte[] nameFileByArr = new byte[100];
                byte[] nameTmp = Encoding.ASCII.GetBytes(fileOnly);
                int[] orderInt = new int[1] { GetOrderByNameFile(Path.GetFileName(sourceFileName))};
                byte[] orderByte = new byte[sizeof(int)];
                Buffer.BlockCopy(nameTmp, 0, nameFileByArr, 0, nameTmp.Length);

                Buffer.BlockCopy(orderInt, 0, orderByte, 0, orderByte.Length);

                langPkg = CombineData(langPkg, orderByte); // Order first
                langPkg = CombineData(langPkg, nameFileByArr); // then name
                langPkg = CombineData(langPkg, dataSinggleLangU8); // then content
                //File.WriteAllBytes(fileOnly + ".bin", result);

                compressedData = CombineData(compressedData, langPkg);
            }

            File.WriteAllBytes("sys_text.bin", compressedData);
        }
    }
}
