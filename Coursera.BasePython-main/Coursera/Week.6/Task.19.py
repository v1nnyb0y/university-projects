'''
Упорядочить список партий по числу голосов
'''


class Party:
    votes = 0
    name = ''


def partyKey(p):
    return (-p.votes, p.name)


parties = []
with open('input.txt', 'r', encoding='utf8') as file:
    text = file.read().split('\n')
    id = text.index('VOTES:')
    textV = text[id + 1:]
    text = text[1:id]
    for i in text:
        p = Party()
        p.name = i
        p.votes = textV.count(i)
        parties.append(p)
parties.sort(key=lambda p: (-p.votes, p.name))
for i in parties:
    print(i.name)
