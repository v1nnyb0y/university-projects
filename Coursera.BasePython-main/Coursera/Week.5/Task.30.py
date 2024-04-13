'''
Количество различных элементов
'''


def countNum(a):
    count = 0
    last = float('-inf')
    for i in a:
        if (i != last):
            last = i
            count += 1
    return count

a = list(map(int, input().split()))
print(countNum(a))
