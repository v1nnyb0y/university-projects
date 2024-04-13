'''
Количество положительных
'''


def countPositive(a):
    i = 0
    count = 0
    for i in a:
        if (i > 0):
            count += 1
    return count

a = list(map(int, input().split()))
print(countPositive(a))
