using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EzIna
{
    public class DBConfigManager
    {
        private static string configFileName; //= @"Config.xml";
        public static void InitDBConfig()
        {
            configFileName = string.Format("{0}MES\\Config.xml", FA.DIR.CFG); // 원본 코드
            // configFileName = string.Format("{0}MES\\Config_TestDB3.xml", FA.DIR.CFG); // 2026.03.12 KKW DB 프로시저 TEST 추가

        }

        public static string GetValue(params string[] args)
        {
            string result = string.Empty;

            try
            {
                XDocument xDoc = XDocument.Load(configFileName);
                result = GetNodeValue(xDoc.FirstNode as XElement, 0, args);
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private static string GetNodeValue(XElement node, int idx, params string[] args)
        {
            string result = string.Empty;

            if (args.Length > idx + 1)
                result = GetNodeValue(node.Element(args[idx]), ++idx, args);
            else
                result = node.Element(args[idx]).Value.ToString();

            return result;
        }
    }
}
