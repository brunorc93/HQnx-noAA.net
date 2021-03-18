using System;
using System.Drawing;
using static Loader;
using static System.Math;
using System.IO;

namespace my_app
{
    class Program
    {
        // D:\Google Drive\__Projects\_02_X_unorganized - Bitmap_Upscale\Scaling example\SG\_png\SavedBG_12.png
        static void Main(string[] args)
        {
            Bitmap bmp = Load();
            int count = ScaleNumber();

            Console.WriteLine("Save results in multiple files? (Y/N) - Default is a single file");
            string result = Console.ReadLine();
            Bitmap[] bitmaps;
            if (result.ToUpper() == "Y") 
            {
              bitmaps = ScaleChunked(bmp, count);
            }
            else 
            { 
              bitmaps = ScaleSingle(bmp, count);
            }

            string path = Directory.GetCurrentDirectory()+"/Saved/";
            Directory.CreateDirectory(@path);
            int k = Directory.GetDirectories(@path,"*",SearchOption.TopDirectoryOnly).Length;
            path += k.ToString("D3");
            bmp.Save(path+"-original.png", System.Drawing.Imaging.ImageFormat.Png);
            path += "-Scale-x"+((int)Pow(2,count)).ToString()+"/";
            Directory.CreateDirectory(@path);
            for (int a=0; a<bitmaps.Length; a++)
            {
              bitmaps[a].Save(path+a.ToString("D3")+".png",System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        static Bitmap[] ScaleChunked(Bitmap bitmap, int count)
        {
            Bitmap[] bitmaps = new Bitmap[1]{bitmap};
            for (int i=0; i<count; i++)
            {
              Bitmap[] new_bitmaps = new Bitmap[4*bitmaps.Length];
              for (int j=0; j<bitmaps.Length; j++)
              {
                int n = (int)Pow(2,i);
                int A = j%n;
                int B = j/n;
                bitmaps[j] = bitmaps[j].Scale2x();
                new_bitmaps[2*A + 4*n*B] = bitmaps[j].Chunk(0,2);
                new_bitmaps[2*A + 4*n*B + 1] = bitmaps[j].Chunk(1,2);
                new_bitmaps[2*A + 4*n*B + 2*n] = bitmaps[j].Chunk(2,2);
                new_bitmaps[2*A + 4*n*B + 2*n + 1] = bitmaps[j].Chunk(3,2);
              }
              bitmaps = new_bitmaps;
            }

            return bitmaps;
        }

        static Bitmap[] ScaleSingle(Bitmap bitmap, int count)
        {
            Bitmap[] bitmaps = new Bitmap[1]{bitmap};
            for (int i=0; i<count; i++)
            {
              bitmaps[0] = bitmaps[0].Scale2x();
            }
            return bitmaps;
        }

        static int ScaleNumber()
        {
            int count = 0;
            Console.WriteLine("How many times do you want to scale (x2) the Square Image?"+" Maximum of 4 multiplications (x16).");
            string mult = Console.ReadLine();
            string[] options = new string[4]{"1","2","3","4"};
            if (Array.Exists(options,s => { return s==mult;}))
            {
                count = Int32.Parse(mult);
            }
            else
            {
                Console.WriteLine("Please write a valid number.");
                count = ScaleNumber();
            }
            
            return count;
        }
    }
}