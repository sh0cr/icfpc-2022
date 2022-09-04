from enum import Enum
from skfda import FDataGrid
from PIL import Image
from skfda.exploratory.stats import geometric_median

max_depth = 4


class Block:
    def __init__(self, l, a, b, c, d, id,color = (0,0,0,0)):
        self.x1 = a
        self.y1 = b
        self.x2 = c
        self.y2 = d
        self.length = l
        self.id = id
        self.color = color

    def get_size(self):
        return (self.x2 - self.x1) * (self.y2 - self.y1)

class BlockType(Enum):
    SimpleBlock = 1
    ComplexBlock = 2


def mean_pixel(b, pix):
    matrix = []
    for x in range(b.x1, b.x2):
        for y in range(b.y1, b.y2):
            matrix.append(pix[x, 399 - y])

    X = FDataGrid(matrix)
    median = geometric_median(X)
    return (round(median.data_matrix[0][0][0]),round(median.data_matrix[0][1][0]),round(median.data_matrix[0][2][0]),round(median.data_matrix[0][3][0]))

    # mean = (0, 0, 0, 0)
    # for x in range(b.x1, b.x2):
    #     for y in range(b.y1, b.y2):
    #         mean = tuple(sum(x) for x in zip(mean, pix[x, 399 - y]))
    # size = b.get_size()
    # return (round(mean[0] / size), round(mean[1] / size), round(mean[2] / size), round(mean[3] / size))


def pcut(block, depth, pix, img_num):
    if depth == max_depth:
        c = mean_pixel(block, pix)
        if block.color != c:
            block.color = c
            with open(f'isl_codes/{img_num}.isl', 'a') as file:
                file.write(f'color [{block.id}] [{c[0]}, {c[1]}, {c[2]}, {c[3]}]\n')
    else:
        offsest = block.length // 2
        with open(f'isl_codes/{img_num}.isl', 'a') as file:
            file.write(f'cut [{block.id}] [{block.x1 + offsest}, {block.y1 + offsest}]\n')
        pcut(Block(offsest, block.x1, block.y1, block.x1 + offsest, block.y1 + offsest, block.id + '.0'), depth + 1, pix, img_num)
        pcut(Block(offsest, block.x1 + offsest, block.y1, block.x2, block.y1 + offsest, block.id + '.1'), depth + 1, pix, img_num)
        pcut(Block(offsest, block.x1 + offsest, block.y1 + offsest, block.x2, block.y2, block.id + '.2'), depth + 1, pix, img_num)
        pcut(Block(offsest, block.x1, block.y1 + offsest, block.x1 + offsest, block.y2, block.id + '.3'), depth + 1, pix, img_num)



num = 36
path = f'images/{num}.png'
img = Image.open(path)
pix = img.load()
initial = Block(400, 0, 0, 400, 400, '0')
pcut(initial, 0, pix, num)
