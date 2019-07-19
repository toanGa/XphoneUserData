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
        UInt16[] DataUint16;
        UInt16[] DataSaved;

        string HeaderFile;
        List<String> SourceFiles;


        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string fileName = @"E:\git-server\3.COMPARE_CODE\2\os\source\libs\text\text.h";
            string sourceFileName = @"E:\git-server\3.COMPARE_CODE\2\os\source\libs\text\text-eng.c";
            string content = File.ReadAllText(fileName);
            string srcContent = File.ReadAllText(sourceFileName);
            List<TextObj> textObjs = TextObjectParser.ParseDefineFile(content);
            List<UInt16[]> textDatas = new List<UInt16[]>();

            int numTextObjs = textObjs.Count;

            DataSaved = new UInt16[0];

            for (int i = 0; i < numTextObjs; i++)
            {
                UInt16[] contentParser = LanguageParser.ParseTextObjectContent(textObjs[i], srcContent);
                textDatas.Add(contentParser);

                Console.WriteLine(contentParser.Length);
                DataSaved = CombineData(DataSaved, contentParser);
            }

            byte[] result = new byte[DataSaved.Length * sizeof(UInt16)];
            Buffer.BlockCopy(DataSaved, 0, result, 0, result.Length);

            File.WriteAllBytes("text_en.bin", result);
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

        private void Button2_Click(object sender, EventArgs e)
        {
            byte[] data = File.ReadAllBytes("text_en.bin");
            DataUint16 = new UInt16[data.Length / 2];
            Buffer.BlockCopy(data, 0, DataUint16, 0, data.Length);
            bool readConfirm = true;
            for(int i = 0; i < DataUint16.Length; i++)
            {
                if(DataUint16[i] != DataSaved[i])
                {
                    readConfirm = false;
                    break;
                }
            }

            if(readConfirm)
            {
                MessageBox.Show("Confirm OK");
            }
            else
            {
                MessageBox.Show("Confirm wrong data");
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

                byte[] result = new byte[dataSinggleLang.Length * sizeof(UInt16)];
                Buffer.BlockCopy(dataSinggleLang, 0, result, 0, result.Length);

                string fileOnly = Path.GetFileNameWithoutExtension(sourceFileName);
                byte[] nameFileByArr = new byte[100];
                byte[] nameTmp = Encoding.ASCII.GetBytes(fileOnly);
                int[] orderInt = new int[1] { orderFake };
                byte[] orderByte = new byte[sizeof(int)];
                Buffer.BlockCopy(dataSinggleLang, 0, orderByte, 0, orderByte.Length);

                Buffer.BlockCopy(orderInt, 0, nameFileByArr, 0, nameTmp.Length);

                result = CombineData(result, nameFileByArr);
                result = CombineData(result, orderByte);

                File.WriteAllBytes(fileOnly + ".bin", result);

                compressedData = CombineData(compressedData, result);
            }

            File.WriteAllBytes("sys_text.bin", compressedData);
        }
    }
}
