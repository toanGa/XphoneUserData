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
