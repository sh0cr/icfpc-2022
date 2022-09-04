using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
//using System.Text.Json;
using Newtonsoft.Json;
using SkiaSharp;

namespace RoboPainter
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 36; i < 41; i++)
            {
                SolveMean(i);
            }
        }

        static void SolvePuzzle(int num)
        {
            string fileName = $"initial_config/{num}.json";
            var jsonString = File.ReadAllText(fileName);
            var canvas = JsonConvert.DeserializeObject<Canvas>(jsonString);
            canvas.LoadImage(num);

            foreach (var block in canvas.Blocks)
            {
                var bShape = block.Shape;
                var meanColor = GetMean(block.BottomLeft, block.TopRight, canvas.image);
                var mostSimilar = canvas.Blocks
                    .Where(b => b.Shape == bShape && !b.IsFinal)
                    .MinBy(b => ColorDif(b.Color, meanColor));
                if (mostSimilar == default) continue;
                if (mostSimilar.blockId != block.blockId)
                    Move.Swap(block, mostSimilar);
                using (StreamWriter writer = new StreamWriter($"{num}.isl", append: true))
                {
                    writer.WriteLine($"swap [{block.blockId}] [{mostSimilar.blockId}]");
                }

                mostSimilar.IsFinal = true;
            }
        }

        static void SolveMean(int num)
        {
            string fileName = $"initial_config/{num}.json";
            var jsonString = File.ReadAllText(fileName);
            var canvas = JsonConvert.DeserializeObject<Canvas>(jsonString);
            canvas.LoadImage(num);

            foreach (var block in canvas.Blocks)
            {
                var meanColor = GetMean(block.BottomLeft, block.TopRight, canvas.image);
                bool isDif = false;
                for (int i = 0; i < block.Color.Count; i++)
                {
                    if (block.Color[i] != meanColor[i])
                    {
                        isDif = true;
                        break;
                    }
                }

                if (isDif)
                {
                    block.Color = meanColor;
                    using (StreamWriter writer = new StreamWriter($"{num}.isl", append: true))
                    {
                        writer.WriteLine($"color [{block.blockId}] [{meanColor[0]}, {meanColor[1]}, {meanColor[2]}, {meanColor[3]}]");
                    }
                }
            }
        }
//todo: geometric median
        static List<byte> GetMean(List<int> bl, List<int> tr, Bitmap image)
        {
            var mean = new List<int>(4){0,0,0,0};
            for (int x = bl[0]; x < tr[0]; x++)
            {
                for (int y = bl[1]; y < tr[1]; y++)
                {
                    var pixel = image.GetPixel(x, image.Height - 1 - y);
                    mean[0] += pixel.R;
                    mean[1] += pixel.G;
                    mean[2] += pixel.B;
                    mean[3] += pixel.A;
                }
            }

            var size = (tr[0] - bl[0]) * (tr[1] - bl[1]);
            for (int i = 0; i < mean.Count; i++) mean[i] /= size;

            return mean.Select(i => (byte)i).ToList();
        }

        static int ColorDif(List<byte> c1,List<byte> c2)
        {
            int sum = 0;
            for (int i = 0; i < c1.Count; i++) sum += Math.Abs(c1[i] - c2[i]);
            return sum;
        }
    }
}