using System.Drawing;
using System;
using System.IO;

public static class Loader
{
    public static Bitmap Load()
    {
        Console.WriteLine("Please write the path to the bitmap file you want to scale:");
        string path = Console.ReadLine();
        path = CheckPath(path);
        Bitmap bmp = Load(path);
        return bmp;
    }
    public static Bitmap Load(string path){
        Bitmap bmp = null;
        if (File.Exists(path))
        { 
            bmp = new Bitmap(new MemoryStream(File.ReadAllBytes(path)));
            Console.WriteLine("File "+'"'+path+'"'+" successfully loaded");
        }
        else
        {
            Console.WriteLine("No File in desired path "+'"'+path+'"');

            Console.WriteLine("Retry? (Y/N)");
            string answer = Console.ReadLine();
            if (answer.ToUpper() == "Y")
            {
                bmp = Load();
            }
            else 
            {
                Environment.Exit(0);
            }
        }
        return bmp;
    }
    private static string CheckPath(string path){
        if (path.Length > 0)
        {
            if (path.Length > 1 && path[1] == ':'){
                return path;
            }
            else 
            {
                if (path[0] != '/' && path[0] != '\\')
                {
                    path = "/" + path;
                }
                path = path.Replace('/','\\');
            }
            path = Directory.GetCurrentDirectory() + path;
            return path;
        }
        else
        {
            return "";
        }
    }
}