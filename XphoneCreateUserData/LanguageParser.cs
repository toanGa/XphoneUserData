using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XphoneCreateUserData
{
    class LanguageParser
    {
        public const string TEXT_VARIABLE = "text";
        public const string TEXT_DATA_TYPE = "system_text_t";
        public static UInt16[] ParseTextObjectContent(TextObj textObj, string srcFileContent)
        {
            UInt16[] objContent = new UInt16[textObj.Length];
            string[] lines = srcFileContent.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            int numLines = lines.Length;
            for(int i = 0; i < numLines; i++)
            {
                string lineContent = lines[i].Trim();
                if(lineContent.IndexOf("init_string") == 0)
                {
                    // Parse this string
                    string variableName;
                    string stringIn;
                    // Function and assign content is same line
                    if(lineContent.Contains("\""))
                    {

                    }
                    else
                    {
                        string nextLine = lines[i + 1];
                        if(nextLine.Contains("\""))
                        {

                        }
                        else
                        {
                            Console.WriteLine("Find content error");
                        }
                        lineContent += nextLine;
                        i++;
                    }

                    int idxOpen = lineContent.IndexOf("(");
                    int idxSeperate = lineContent.IndexOf(",");
                    int idxOpenContent = lineContent.IndexOf("\"");
                    int idxCloseContent = idxOpenContent + 1 + lineContent.Substring(idxOpenContent + 1).IndexOf("\"");

                    string textDefine = lineContent.Substring(idxOpen + 1, idxSeperate - idxOpen - 1);
                    if(textDefine.IndexOf(TEXT_VARIABLE + ".") == 0)
                    {
                        string preStr = TEXT_VARIABLE + ".";
                        textDefine = textDefine.Substring(preStr.Length);
                    }

                    if (textDefine.Trim() == textObj.Name)
                    {
                        string contentDefine = lineContent.Substring(idxOpenContent + 1, idxCloseContent - idxOpenContent - 1);
                        copy_text(objContent, contentDefine);
                        break;
                    }
                }
            }

            return objContent;
        }

        static void copy_text(UInt16[] arrayPtr, string string_in)
        {
            int i = 0;
            int array_length = arrayPtr.Length;

            // convert
            while (i < string_in.Length - 3)
            {
                Debug.Assert((i / 4) < array_length);

                arrayPtr[i / 4] = (UInt16)(char2num(string_in[i]) * 4096 
                                            + char2num(string_in[i + 1]) * 256
                                            +  char2num(string_in[i + 2]) * 16 + char2num(string_in[i + 3]));
                i += 4;
            }
            for (i = (i / 4); i<array_length; i++)
            {
                arrayPtr[i] = 0;
            }
        }

        static SByte char2num(char c)
        {
            if (('0' <= c) && (c <= '9'))
            {
                return (SByte)(c - 48);
            }
            else if (('a' <= c) && (c <= 'f'))
            {
                return (SByte)(c - 97 + 10);
            }
            else if (('A' <= c) && (c <= 'F'))
            {
                return (SByte)(c - 65 + 10);
            }
            return 0;
        }
    }
}
