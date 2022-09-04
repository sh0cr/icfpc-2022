import json

import numpy as np
from PIL import Image


def solve(num):
    blocks = np.genfromtxt(f'sourcepng/{num}.csv', delimiter=',', dtype=None)
    img = Image.open(f'images/{num}.png')
    pix = img.load()
    for y in range(0, img.height):
        for x in range(0, img.width):
            if pix[x, img.height - 1 - y] != tuple(blocks[img.width * y + x]):
                color = pix[x, img.height - 1 - y]
                blocks[img.width * y + x] = pix[x, img.height - 1 - y]

                with open(f'isl_codes/v24/{num}.isl', 'a') as file:
                    file.write(f"color [{img.height * y + x}] [{color[0]}, {color[1]}, {color[2]}, {color[3]}]\n")


solve(36)
