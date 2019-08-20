using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XphoneCreateUserData
{
    struct TextObj
    {
        public string Name;
        public int Length;
        public string Unicode;// UTF16 of Variable
        public byte[] ByteArray
        {
            get
            {
                return ToIntArr(Unicode);
            }
        }

        public string CodeString
        {
            get
            {
                string code = string.Format("init_string(text.{0} ,\"{1}\"); /* {2} */", Name, Unicode, ReadableString);
                return code;
            }
        }

        public string ReadableString
        {
            get
            {
                string Readable = Encoding.BigEndianUnicode.GetString(ByteArray);
                return Readable;
            }
        }

        public static byte[] ToIntArr(string unicode)
        {
            List<byte> listByte = new List<byte>();
            int len = unicode.Length;
            byte byte1;
            byte byte2;
            string var;
            int intVar;

            for (int i = 0; i < len; i += 4)
            {
                var = unicode.Substring(i, 4);
                intVar = Convert.ToInt32(var, 16);

                byte1 = (byte)((intVar >> 8) & 0xFF);
                byte2 = (byte)(intVar & 0xFF);

                listByte.Add((byte)(byte1));
                listByte.Add((byte)(byte2));
            }

            return listByte.ToArray();
        }

        public static string ToStringUnicode(byte[] array)
        {
            int i;
            int len = array.Length;
            string s = "";
            string hexStr;
            for (i = 0; i < len; i += 1)
            {
                hexStr = array[i].ToString("X");
                if (hexStr.Length == 1)
                {
                    hexStr = '0' + hexStr;
                }
                s += hexStr;
            }

            return s;
        }

        public static string GetInitCode(List<TextObj> textObjs)
        {
            string initObjsStr = "\r\n";
            string shiftLine = "    ";
            int numObjs = textObjs.Count;
            int i;
            for (i = 0; i < numObjs; i++)
            {
                initObjsStr += shiftLine + textObjs[i].CodeString + "\r\n";
            }
            return initObjsStr + "\r\n";
        }
    }

    class TextObjectParser
    {
        const string DATA_TYPE = "uint16_t";
        /// <summary>
        /// Paser for define text object
        /// Found int file "text.h"
        /// </summary>
        /// <param name="fileContent"></param>
        /// <returns></returns>
        public static List<TextObj> ParseDefineFile(string fileContent)
        {
            List<TextObj> textObjs = new List<TextObj>();

            string[] lines = fileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int numLines = lines.Length;
            int lineStart = 0;
            int lineEnd = 0;
            for(int i = 0; i < numLines; i++)
            {
                string lineContent = lines[i].Trim();
                if (lineContent.IndexOf("typedef struct") == 0)
                {
                    if(lines[i + 1].Trim().IndexOf("{") == 0)
                    {
                        lineStart = i + 1;
                        for(int j = lineStart; j < numLines; j++)
                        {
                            string endLine = lines[j].Trim();
                            if(endLine.IndexOf("}") == 0 && endLine.Contains(LanguageParser.TEXT_DATA_TYPE))
                            {
                                lineEnd = j;
                                goto found_line_start_end;
                            }
                        }
                    }
                }
            }

found_line_start_end:
            for(int i = lineStart; i < lineEnd; i++)
            {
                string lineContent = lines[i].Trim();
                if(lineContent.IndexOf(DATA_TYPE) == 0)
                {
                    lineContent = lineContent.Substring(DATA_TYPE.Length).Trim();
                    int idxOpen = lineContent.IndexOf("[");
                    int idxClose = lineContent.IndexOf("]");
                    string name = lineContent.Substring(0, idxOpen).Trim();
                    string lenStr = lineContent.Substring(idxOpen + 1, idxClose - idxOpen - 1);
                    int len = 0;
                    int.TryParse(lenStr, out len);
                    TextObj textObj = new TextObj();
                    textObj.Name = name;
                    textObj.Length = len;

                    textObjs.Add(textObj);
                }
            }
            return textObjs;
        }

        public static int LengthListTextObject(List<TextObj> textObjs)
        {
            int len = 0;
            int numObjs = textObjs.Count;
            for(int i = 0; i < numObjs; i++)
            {
                len += textObjs[i].Length;
            }
            return len;
        }
    }
}
