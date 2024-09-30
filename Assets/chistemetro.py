# import json

# jokes = []

# with open("chistes.txt", 'r' , encoding='utf-8') as file:
#     for linea in file:
#         if (linea[0] == "-"):
#             linea2 = linea[2:]

#             jokes.append(linea2.strip())

# print(jokes)

# with open('jokes.json', 'w') as file:
#     json.dump(jokes, file, indent=1)

import json

dark_jokes = {
    "jokes": []
}
with open('darkjokes.json', 'r') as file:
    json_obj = json.loads(file.read())



    for joke in json_obj['jokes']:
        dark_jokes['jokes'].append(joke['buildup'] + ". " + joke['punchline'])

    print(dark_jokes)

with open('darkJokes2.json', 'w') as file:
    json.dump(dark_jokes, file, indent=1)