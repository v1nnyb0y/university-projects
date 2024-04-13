'''
Отсортировать список участников по алфавиту
'''


class Person:
    fi = ' '
    score = -1


def midScore():
    classes = []
    with open('input.txt', 'r', encoding='utf-8') as file:
        for line in file:
            arr = line.split()
            p = Person()
            p.fi = arr[0] + ' ' + arr[1]
            p.score = int(arr[3])
            classes.append(p)
    classes.sort(key=lambda person: person.fi)
    with open('output.txt', 'w', encoding='utf-8') as file:
        for p in classes:
            print(p.fi, p.score, sep=' ', end='\n', file=file)

midScore()
