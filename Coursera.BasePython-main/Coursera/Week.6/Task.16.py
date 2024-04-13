'''
Максимальный балл не-победителя
'''
nine = []
max_n = float('-inf')
ten = []
max_t = float('-inf')
el = []
max_el = float('-inf')
with open('input.txt', 'r', encoding='utf8') as file:
    for line in file:
        arr = line.split()
        score = int(arr[3])
        sClass = int(arr[2])
        if (sClass == 9):
            if (max_n < score):
                max_n = score
            nine.append(score)
        elif (sClass == 10):
            if (max_t < score):
                max_t = score
            ten.append(score)
        elif (sClass == 11):
            if (max_el < score):
                max_el = score
            el.append(score)


def findMaxL(a, max):
    a.sort(reverse=True)
    i = 1
    while(i < len(a)):
        if (a[i] != max):
            return a[i]
        i += 1
    return None

print(findMaxL(nine, max_n), end=' ')
print(findMaxL(ten, max_t), end=' ')
print(findMaxL(el, max_el))
