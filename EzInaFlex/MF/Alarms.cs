using System;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace EzIna.MF
{
    public class AlarmItem
    {
        private int _Code;

        public string Caption, Category, Description, Module, ImgFile;

        public int Severity;
        public int Count = 0;    // 알람 발생 횟수를 저장
        public int MaxCount = 0; // 최대 발생 횟수, 도달시 별도 처리

        public int Code { get { return _Code; } }

        public AlarmItem(int code, string caption = "", string category = "", string txt = "", string module = "", int severity = 0)
        {
            _Code = code;
            Caption = caption;
            Category = category;
            Description = txt;
            Module = module;
            Severity = severity;
            ImgFile = code.ToString() + ".jpg";
        }
    }

    public static class ALM
    {
        private const int ALARM_COUNT = 1000;

        public static string ImgDir = "";

        public static AlarmItem[] Items = new AlarmItem[ALARM_COUNT];



        public static string GetImgPath(int code) { return ImgDir + Items[code].ImgFile; }

        public static void Init(string alarmList, string imgPath)
        {
            ImgDir = imgPath;

            for (int i = 0; i < ALARM_COUNT; i++)
                Items[i] = new AlarmItem(i);

            if (File.Exists(alarmList))
                LoadFromFile(alarmList);
        }

        public static void LoadFromFile(string filename, char seperator = '|')
        {
            if (!File.Exists(filename))
            {
                if (MsgBox.Confirm(filename + " file not found!\n" + "created base schema file?"))
                    SaveToFile(filename);
            }

            using (StreamReader reader = new StreamReader(filename, Encoding.Unicode))
            {
                string sLine;
                while ((sLine = reader.ReadLine()) != null)
                {
                    sLine.Trim();

                    // ignore comment
                    if (sLine.IndexOf("//") == 0) continue;
                    if (sLine.IndexOf(';') == 0) continue;

                    string[] words = sLine.Split(seperator);
                    if (words.Length < 1) continue;

                    int code = -1;
                    if (!int.TryParse(words[0], out code) || code >= ALARM_COUNT || code < 0) continue;

                    for (int i = 1; i < words.Length; i++)
                    {
                        words[i] = words[i].Trim();
                        if (words[i] == "") continue;

                        switch (i)
                        {
                            case 1: Items[code].Category = words[1]; break;
                            case 2: Items[code].Caption = words[2]; break;
                            case 3: Items[code].Description = words[3].Replace("\\n", "\n"); break;
                            case 4: Items[code].Module = words[4]; break;
                            case 5: int.TryParse(words[5], out Items[code].Severity); break;
                            case 6: Items[code].ImgFile = words[6]; break;
                            case 7: int.TryParse(words[7], out Items[code].MaxCount); break;
                        }
                    }
                }
            }
        }

        public static void SaveToFile(string filename, char seperator = '|')
        {
            using (StreamWriter w = new System.IO.StreamWriter(filename, false, Encoding.Unicode))
            {
                w.WriteLine("//------------------------------------------------------------------------------");
                w.WriteLine("// Code | Category | Caption | Text | Module | Severity | ImageFile | MaxCount");
                w.WriteLine("//");

                string fmt = "{0,4:D4} " + seperator + " {1} " + seperator + " {2} " + seperator + " {3} " + seperator + " {4} " + seperator + " {5} " + seperator + " {6}" + seperator + " {7}";

                foreach (AlarmItem item in Items)
                {
                    string sLine = string.Format(fmt,
                        item.Code,
                        item.Category,
                        item.Caption,
                        item.Description.Replace("\n", "\\n"),
                        item.Module,
                        item.Severity,
                        item.ImgFile,
                        item.MaxCount);

                    w.WriteLine(sLine);
                }
            }
        }
    }
}