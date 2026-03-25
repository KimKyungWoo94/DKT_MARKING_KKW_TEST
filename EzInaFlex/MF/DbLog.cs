using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace EzIna.MF
{
    public static class DbLog
    {
        private static Queue<string> m_queLotInfor   = new Queue<string>();
        private static string        m_strFileDbLot  ;


        public static void Init(string a_strFileName)
        {
            m_strFileDbLot   = a_strFileName;
        }

        public static void ClearAll()
        {
            m_queLotInfor.Clear();
        }

        public static void ClearLot()
        {
            File.Delete(m_strFileDbLot);
            m_queLotInfor.Clear();
        }

        public static void LotEnqueue(string data)
        {
            m_queLotInfor.Enqueue(data);
        }

        public static void LotSaveToFile()
        {
            if (m_queLotInfor.Count <= 0)
                return;
            using (StreamWriter sw = new StreamWriter(m_strFileDbLot, true))
                sw.WriteLine(m_queLotInfor.Dequeue());
        }
    }
}
