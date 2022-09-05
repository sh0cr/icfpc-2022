from PIL import Image
from rp import mean_pixel, Block

img = Image.open('images/36.png')
pix = img.load()
a = 12

b0 = Block(a, 0, 0, a, a, "0")
b011 = Block(a, img.width - 1 - a, 0, img.width - 1, a, "0.1.1")
b022 = Block(a, img.width - 1 - a, img.height - 1 - a, img.width - 1, img.height - 1, '0.2.2')
b031 = Block(a, 0, img.height - 1 - a, a, img.height - 1, "0.3.1")
corners = [b0, b011, b022, b031]

for b in corners:
    c = mean_pixel(b, pix)
    with open('isl_codes/manual/36.isl', 'a') as file:
        file.write(f'color [{b.id}] [{c[0]}, {c[1]}, {c[2]}, {c[3]}]\n')
