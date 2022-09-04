using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Drawing;

//using SkiaSharp;

namespace RoboPainter;

public class Canvas
{
    public List<SimpleBlock> Blocks { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    internal Bitmap image; 
    internal void LoadImage(int num)
    {
        // string resId = $"RoboPainter.Images.{num}.png";
        // Assembly assembly = GetType().GetTypeInfo().Assembly;
        // using (Stream stream = assembly.GetManifestResourceStream(resId))
        // {
        //     painting = SKBitmap.Decode(stream);
        // }

        image = new Bitmap(@$"D:\repos\icfpc-2022\RoboPainter\RoboPainter\Images\{num}.png");
    }
}