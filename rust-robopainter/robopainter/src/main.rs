use std::{collections::{HashSet}, cmp};

use image::Rgba;

static ISL_CODE : String = String::new();
static mut COUNTER: usize = 0;

trait BlockTrait {
    fn get_shape(&self) -> &'static ((usize, usize),(usize, usize));
}


struct SimpleBlock{
    // (bottom left, top right)
    shape: ((usize, usize), (usize, usize)),
    color: Rgba<u8>,
}

struct ComplexBlock{    
    shape: ((usize, usize), (usize, usize)),
    child_blocks: HashSet<SimpleBlock>,
}

#[derive(Clone)]
enum BlockType {
    Simple(SimpleBlock),
    Complex(ComplexBlock),
}

#[derive(Clone)]
struct Block {
    id: String,
    block_type: BlockType,
}

enum Orientation {
    X, Y
}

fn get_shape(block: Block) -> ((usize, usize), (usize, usize)) {
    match block.block_type {
        BlockType::Simple(s) => s.shape,
        BlockType::Complex(c)=> c.shape
    }
}

fn line_cut_X(id: String, sblock: SimpleBlock, offset: usize) -> [Block;2]{
    let b0 = Block {
        id: format!("{}{}", id, ".0"),
        block_type: BlockType::Simple(
            SimpleBlock {
                shape: (sblock.shape.0, (sblock.shape.0.0 + offset, sblock.shape.1.1)),
                color: sblock.color,
            }),
    };
    let b1 = Block {
        id: format!("{}{}", id, ".1"),
        block_type: BlockType::Simple(
            SimpleBlock {
                shape: ((sblock.shape.0.0 + offset, sblock.shape.0.1), sblock.shape.1),
                color: sblock.color,
            }),
    };
    [b0,b1]
}

fn line_cut_Y(id: String, sblock: SimpleBlock, offset: usize) -> [Block;2]{
    let b0 = Block {
        id: format!("{}{}", id, ".0"),
        block_type: BlockType::Simple(
            SimpleBlock {
                shape: (sblock.shape.0, (sblock.shape.1.0, sblock.shape.0.1 + offset)),
                color: sblock.color,
            }),
    };
    let b1 = Block {
        id: format!("{}{}", id, ".1"),
        block_type: BlockType::Simple(
            SimpleBlock {
                //TODO: check correctness
                shape: ((sblock.shape.0.0, sblock.shape.0.1 + offset), sblock.shape.1),
                color: sblock.color,
            }),
    };
    [b0,b1]
}

fn line_cut(block: Block, o:Orientation, offset: usize)->[Block;2]{
    match block.block_type{
        BlockType::Simple(s) => {
            match o{
                Orientation::X => {
                    line_cut_X(block.id, s, offset)
                },
                Orientation::Y => {
                    line_cut_Y(block.id, s,offset)
                }
            }
        }
        BlockType::Complex(c) =>{
            todo!("cut complex blocks")
        }
    }
}

//(vertical, horizontal) offset
fn point_cut(block: Block, offset:(usize, usize))-> [&'static Block;4]{
    let blocks = line_cut(block, Orientation::Y, offset.1);
    let bottom_blocks = line_cut(blocks[0], Orientation::X, offset.0);
    let top_blocks = line_cut(blocks[1], Orientation::X, offset.0);
    [
        bottom_blocks[0],
        bottom_blocks[1],
        top_blocks[1],
        top_blocks[0]
    ]
}

fn color_move(block: Block, color:Rgba<u8>){
    match block.block_type {
        BlockType::Simple(mut s) => s.color = color,
        BlockType::Complex(c) => {
            for mut simple in c.child_blocks{
                simple.color = color
            }
        }
    }
}

fn swap_move(b0: Block, b1:Block){
    
}


fn pixelize(initial:Block){
    let shape = get_shape(initial);

    for i in 0..cmp::max(shape.1.0 -shape.0.0, shape.1.1 -shape.0.1){
        
    }
}


fn main() {
    println!("Hello, world!");
}
