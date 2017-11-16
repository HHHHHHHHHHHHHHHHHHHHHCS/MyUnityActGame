using System.IO;

public class FileManager
{

    public static void CheckDirectory(string path)
    {
        if (!Directory.Exists(path))//若文件夹不存在则新建文件夹   
        {
            Directory.CreateDirectory(path); //新建文件夹   
        }
    }

    public static bool CheckFile(string path)
    {
        return File.Exists(path);
    }

    public static void SaveFile(string path,string fileName,string content)
    {
        CheckDirectory(path);
        string _path = string.Format("{0}/{1}", path, fileName);
        FileStream fs = new FileStream(_path, FileMode.Create);
        //获得字节数组
        byte[] data = System.Text.Encoding.Default.GetBytes(content);
        //开始写入
        fs.Write(data, 0, data.Length);
        //清空缓冲区、关闭流
        fs.Flush();
        fs.Close();
        fs.Dispose();
    }

    public static string LoadFile(string path, string fileName)
    {
        string result = null;
        string _path = string.Format("{0}/{1}", path, fileName);
        if(CheckFile(_path))
        {
            StreamReader rs = new StreamReader(_path);
            result = rs.ReadToEnd();
            rs.Close();
            rs.Dispose();
        }
        return result;
    }

}
