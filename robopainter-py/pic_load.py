import requests
import json

def load_pic():
    for i in range(36,41):
        url = f'https://cdn.robovinci.xyz/imageframes/{i}.png'
        img_data = requests.get(url).content
        with open(f'images/{i}.png', 'wb') as handler:
            handler.write(img_data)

def load_init_config():
    for i in range(36,41):
        url = f'https://cdn.robovinci.xyz/imageframes/{i}.initial.json'
        data = requests.get(url).json()
        with open(f'initial_config/{i}.json', 'w') as f:
           json.dump(data,f)

def load_sourcepngs():
    for i in range(36,41):
        url = f"https://cdn.robovinci.xyz/sourcepngs/{i}.source.json"
        data = requests.get(url).json()
        with open(f'sourcepng/{i}.json', 'w') as f:
            json.dump(data, f)

load_sourcepngs()
