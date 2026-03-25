using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EzIna.MF
{
    public enum Authority { Operator, Engineer, Supervisor, Developer };

    public static class USER
    {
        private static Authority _Authority = Authority.Operator;

        public static int USER_COUNT = 4;

        public static UserInfo[] Items = new UserInfo[USER_COUNT];

        public static Authority Authority
        {
            get { return _Authority; }
            set
            {
                if (value != _Authority)
                {
										FA.LOG.LogInOccurMsg("LOGIN", value.ToString());
                    _Authority = value;
                }
            }
        }

        public static void Init(string iniFile)
        {
            Items[0] = new UserInfo("DEVELOPER", Authority.Developer, "1234"); // 개발자는 저장하지 않음

            Items[1] = new UserInfo("OPERATOR", Authority.Operator		, "1");
            Items[2] = new UserInfo("ENGINEER", Authority.Engineer		, "1");
            Items[3] = new UserInfo("MASTER"  , Authority.Supervisor	, "1");

            LoadFromFile(iniFile);
        }

        public static void LoadFromFile(string iniFileName)
        {
            IniFile ini = new IniFile(iniFileName);

            for (int i = 0; i < USER_COUNT; i++)
            {
                if (Items[i].Authority == Authority.Developer) continue;
                
                // Name | Authority | Password
                string s = string.Format("{0}|{1}|{2}", Items[i].Name, Items[i].Authority.ToString(), Items[i].Password);
                    
                s = ini.Read("USERS", i.ToString("D2"), s);
                
                string[] words = s.Split('|');
                if (words.Length > 2)
                {
                    Authority a;
                    if (Enum.TryParse(words[1], out a))
                        Items[i] = new UserInfo(words[0], a, words[2]);
                }
            }
        }

        public static void SaveToFile(string iniFileName)
        {
            IniFile ini = new IniFile(iniFileName);

            for (int i = 0; i < USER_COUNT; i++)
            {
                if (Items[i].Authority == Authority.Developer) continue;

                // Name | Authority | Password
                ini.Write("USERS", i.ToString("D2"), Items[i].Name + "|" + Items[i].Authority.ToString() + "|" + Items[i].Password);
            }
        }

        public static void WriteTo(ComboBox cb)
        {
            cb.Items.Clear();

            for (int i = 0; i < Items.Length; i++)
                if (Items[i].Authority != Authority.Developer)
                cb.Items.Add(Items[i].Name);
        }
    }

    public struct UserInfo
    {
        public string Name;
        public Authority Authority;
        public string Password;

        public UserInfo(string name, Authority authority, string password)
        {
            Name = name;
            Authority = authority;
            Password = password;
        }
    }
}
