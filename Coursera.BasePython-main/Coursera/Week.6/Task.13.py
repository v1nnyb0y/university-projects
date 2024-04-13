'''
Количество победителей по классам
'''


def findC(a):
    i = len(a) - 2
    max = a[len(a) - 1]
    c = 1
    while(i >= 0):
        if (a[i] == max):
            c += 1
        i -= 1
    print(c, end=' ')

nine = []
ten = []
el = []
with open('input.txt', 'r', encoding='utf8') as file:
    for e in file:
        arr = e.split()
        sClass = int(arr[2])
        score = int(arr[3])
        if (sClass == 9):
            nine.append(score)
        elif (sClass == 10):
            ten.append(score)
        elif (sClass == 11):
            el.append(score)
nine.sort()
ten.sort()
el.sort()
findC(nine)
findC(ten)
findC(el)
