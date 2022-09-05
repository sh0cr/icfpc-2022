import requests
from io import StringIO

key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InBpZ3Jhc3RyaWdvQGdtYWlsLmNvbSIsImV4cCI6MTY2MjQwMjcyMiwib3JpZ19pYXQiOjE2NjIzMTYzMjJ9.WwL1wLuXo4rFMiTee5hNAShZUI4taRJZwf06I60RsMk"

for i in range(2,24):
    with open(f'isl_codes/gm/{i}.isl', 'r') as sol:
        content = sol.read()
        response = requests.post(
            f'https://robovinci.xyz/api/submissions/{i}/create',
            headers={'Authorization': f'Bearer {key}'},
            files={'file': ('submission.isl', StringIO(content))})
        response.raise_for_status()



# resp = requests.get('https://robovinci.xyz/api/results/shocr')
# print(resp)

# with open('isl_codes/v24/36.isl', 'r') as sol:
#     content = sol.read()
#     response = requests.post(
#         f'https://robovinci.xyz/api/submissions/36/create',
#         headers={'Authorization': f'Bearer {key}'},
#         files={'file': ('submission.isl', StringIO(content))})
#     response.raise_for_status()