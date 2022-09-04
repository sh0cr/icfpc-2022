using System.Collections.Generic;
using System.Drawing;

namespace RoboPainter;

class Block
{
    public string id;
    public IBlock blockType;
}

interface IBlock
{
    //string id { get; set; }
    (Point, Point) crd { get; set; }
    (int, int) shape => (crd.Item2.X - crd.Item1.X, crd.Item2.Y - crd.Item1.X);
}

public class SimpleBlock
{
    public string blockId { get; set; }
    public  List<int> BottomLeft { get; set; }
    public  List<int> TopRight { get; set; }
    public List<byte> Color { get; set; }

    internal (int, int) Shape => (TopRight[0] - BottomLeft[0], TopRight[1] - BottomLeft[0]);
    internal bool IsFinal = false;
}

class ComplexBlock : IBlock
{
    public string id { get; set; }
    public List<SimpleBlock> childBlocks = new List<SimpleBlock>();
    public (Point, Point) crd { get; set; }
}