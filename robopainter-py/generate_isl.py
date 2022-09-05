import requests
from PIL import Image
import rp

initial = rp.Block(400, 0, 0, 400, 400, '0')

for i in range(2,24):
    path = f'images/{i}.png'
    img = Image.open(path)
    pix = img.load()
    rp.pcut(initial, 0, pix, i)