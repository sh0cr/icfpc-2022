from PIL import Image

img = Image.open('images/40.png')
pix = img.load()
print(pix[180, 399 - 140])