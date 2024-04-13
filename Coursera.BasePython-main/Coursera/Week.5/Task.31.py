'''
Переставить соседние
'''


def exchange(a):
    i = 0
    while(i < len(a) - 1):
        (a[i], a[i + 1]) = (a[i + 1], a[i])
        i += 2

a = list(map(int, input().split()))
exchange(a)
print(*a)
