using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoboPainter;

enum Orientation
{
    X,
    Y
}

static class Move
{
    static int counter;
/*
    static (Block, Block, Block, Block) PointCut(Block block, Point offset)
    {
        var blocks = LineCut(block, Orientation.Y, offset.Y);
        var bottomBlocks = LineCut(blocks.Item1, Orientation.X, offset.X);
        var topBlocks = LineCut(blocks.Item2, Orientation.X, offset.X);
        return (bottomBlocks.Item1, bottomBlocks.Item2, topBlocks.Item2, topBlocks.Item1);
    }
    */

    static (SimpleBlock, SimpleBlock) LineCut(SimpleBlock block, Orientation o, int offset)
    {
        return o switch
        {
            Orientation.X =>
                LineCutX(block.blockId, block, offset),
            Orientation.Y =>
                LineCutY(block.blockId, block, offset)
        };
        //case ComplexBlock c:
        // var childs = new List<SimpleBlock>();
        // var lookup = c.childBlocks.ToLookup(sb => IsCutNeeded(sb, c.crd.Item1, o, offset));
        // lookup[true].Select(sb => LineCut(sb, o, of));
        // c.childBlocks = lookup[false];
        //TODO:
    }

    // static bool IsCutNeeded(SimpleBlock sb, Point crd, Orientation o, int offset) =>
    //     o switch
    //     {
    //         Orientation.X => sb.crd.Item1.X < crd.X + offset && sb.crd.Item2.X > crd.X+ offset,
    //         Orientation.Y => sb.crd.Item1.Y < crd.Y + offset && sb.crd.Item2.Y > crd.Y + offset
    //     };

    static (SimpleBlock, SimpleBlock) LineCutX(string oldId, SimpleBlock sblock, int offset)
    {
        var b1 =  new SimpleBlock()
            {
                blockId = oldId + ".0",
                Color = sblock.Color,
                BottomLeft = sblock.BottomLeft,
                TopRight = new List<int>(){sblock.BottomLeft[0] + offset, sblock.TopRight[1]}
            };
        var b2 = new SimpleBlock()
        {
            blockId = oldId + ".1",
            Color = sblock.Color,
            BottomLeft = new List<int>() { sblock.BottomLeft[0] + offset, sblock.BottomLeft[1] },
            TopRight = sblock.TopRight
        };
        return (b1, b2);
    }

    static (SimpleBlock, SimpleBlock) LineCutY(string oldId, SimpleBlock sblock, int offset)
    {
        var b1 = new SimpleBlock()
        {
            blockId = oldId + ".0",
            Color = sblock.Color,
            BottomLeft = sblock.BottomLeft,
            TopRight = new List<int>() { sblock.TopRight[0], sblock.TopRight[1] + offset }
        };
        var b2 = new SimpleBlock()
        {
            blockId = oldId + ".1",
            Color = sblock.Color,
            BottomLeft = new List<int>() { sblock.BottomLeft[0], sblock.BottomLeft[1] + offset },
            TopRight = sblock.TopRight
        };
        return (b1, b2);
    }

    static void Color(Block b, List<byte> color)
    {
        if (b.blockType is SimpleBlock sb)
        {
            sb.Color = color;
        }
        else if (b.blockType is ComplexBlock cb)
        {
            foreach (var simpleBlock in cb.childBlocks)
            {
                simpleBlock.Color = color;
            }
        }
    }

    static bool IsSameShape(Block b1, Block b2)
    {
        return b1.blockType.shape == b2.blockType.shape;
    }

    public static void Swap(SimpleBlock b1, SimpleBlock b2)
    {
        var b1bl = b1.BottomLeft;
        var b1tr = b1.TopRight;

        b1.BottomLeft = b2.BottomLeft;
        b1.TopRight = b2.TopRight;

        b2.BottomLeft = b1bl;
        b2.TopRight = b1tr;
    }

    // static Block Merge(Block b1, Block b2)
    // {
    //     var innerBlocks = new List<SimpleBlock>();
    //     if (b1.blockType is SimpleBlock s)
    //     {
    //         innerBlocks.Add(s);
    //     }
    //     else
    //     {
    //         innerBlocks.AddRange((b1.blockType as ComplexBlock).childBlocks);
    //     }
    //     if (b2.blockType is SimpleBlock s1)
    //     {
    //         innerBlocks.Add(s1);
    //     }
    //     else
    //     {
    //         innerBlocks.AddRange((b1.blockType as ComplexBlock).childBlocks);
    //     }
    //
    //     counter++;
    //     return new Block()
    //     {
    //         id = counter.ToString(),
    //         blockType = new ComplexBlock()
    //         {
    //             childBlocks = innerBlocks,
    //             crd = (innerBlocks.MinBy(sb => sb.BottomLeft).BottomLeft,
    //                 innerBlocks.MaxBy(sb => sb.crd.Item2).crd.Item2)
    //         }
    //     };
    //     //TODO: remove b1, b2 from canvas
    // }
}