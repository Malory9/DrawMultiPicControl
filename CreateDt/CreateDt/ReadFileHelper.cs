using System.Collections.Generic;
using System.IO;
using System.Text;



namespace CreateDt
{
    /// <summary>
    /// 读取文件的帮助类
    /// 该类已添加到.com组件里
    /// </summary>
    public static class ReadFileHelper
    {
       /// <summary>
       /// foreach(string line in ReadFileHelper.ReadLines(文件名))
       /// 即可每次读取该文件名的一行数据
       /// </summary>
       /// <param name="fileName"></param>
       /// <returns></returns>
        public static IEnumerable<string> ReadLines(string fileName)
        {
            using (TextReader textReader = (TextReader)new StreamReader(fileName, Encoding.Default))
            {
                string line;
                while ((line = textReader.ReadLine()) != null)
                    yield return line;
            }
        }
    }
}
