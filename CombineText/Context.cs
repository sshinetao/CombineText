using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace CombineText
{
    public class TextFile
    {
        private string currentDir = Directory.GetCurrentDirectory();
        private string newDIRName = "汇总脚本";
        private string newFileName = "汇总脚本";
        private string temp = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
        private List<string> ResultList = new List<string>();
        private string filter = "*.sql";

        /// <summary>
        /// 获取所有.sql
        /// </summary>
        /// <returns></returns>
        private FileInfo[] getFiles()
        {
            FileInfo[] fileNames = null;
            string str = currentDir;
            DirectoryInfo dirInfo = new DirectoryInfo(str);
            fileNames = dirInfo.GetFiles(filter);
            return fileNames;
        }
 
        /// <summary>
        /// 读取文件内容到sb
        /// </summary>
        /// <param name="fileNames"文件名></param>
        /// <param name="sb"></param>
        /// <param name="destFile">目标文件</param>
        /// <returns></returns>
        private StringBuilder getContent(FileInfo[] fileNames, StringBuilder sb)
        {
            ResultList = new List<string>();
            foreach (FileInfo fi in fileNames)
            {
                string text = File.ReadAllText(fi.FullName, Encoding.Default);
                sb.Append("--" + fi.Name);
                sb.Append("\r\n");
                sb.Append(text);
                sb.Append("\r\n");
                sb.Append("\r\n");
            }
            return sb;
        }

        /// <summary>
        /// 写入
        /// </summary>
        public void writeAllText()
        {
            StringBuilder sb = new StringBuilder();

            string newPath = Path.Combine(currentDir, newDIRName);
            string newFile = newPath + "\\" + newFileName + temp + ".sql";
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            sb = getContent(getFiles(), sb);
            sb.Append("\r\n");
            sb.Append("--汇总时间" + string.Format("{0}", DateTime.Now));
            
            FileStream fs = null;
            if (File.Exists(newFile))
            {
                File.Delete(newFile);

            }
            fs = new FileStream(newFile, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(sb.ToString());
            sw.Close();



        }

    }
}
