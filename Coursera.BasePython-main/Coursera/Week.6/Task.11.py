'''
Максимальный балл по классам
'''


def maxScore():
    max = [0] * 3
    with open('input.txt', 'r', encoding='utf-8') as file:
        for line in file:
            arr = line.split()
            score = int(arr[3])
            sClass = int(arr[2])
            if (max[11 - sClass] < score):
                max[11 - sClass] = score
    max.reverse()
    return max

print(*maxScore())
