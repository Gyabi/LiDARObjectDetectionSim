using System.IO;
using System.Collections.Generic;

namespace Gyabi.LiDAR
{
    public static class CSVWriter
    {
        public static void WriteData(List<float[]> datas, string fileName)
        {
            List<string> csvDatas = new List<string>();
            foreach(float[] nums in datas)
            {
                csvDatas.Add(string.Join(",", nums));
            }

            bool isAppend = true; // 上書き or 追記
            using (var fs = new StreamWriter(fileName, isAppend, System.Text.Encoding.GetEncoding("UTF-8")))
            {
                foreach(string data in csvDatas)
                {
                    fs.WriteLine(data);
                }
            }
        } 
    }
}