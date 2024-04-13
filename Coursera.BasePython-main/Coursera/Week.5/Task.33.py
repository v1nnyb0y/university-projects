'''
Переставить min и max
'''


def exchangeMinMax(a):
    minEl = float('inf')
    minId = -1
    maxEl = float('-inf')
    maxId = -1
    for i in range(len(a)):
        if (a[i] > maxEl):
            maxEl = a[i]
            maxId = i
        if (a[i] < minEl):
            minEl = a[i]
            minId = i
    (a[minId], a[maxId]) = (a[maxId], a[minId])

a = list(map(int, input().split()))
exchangeMinMax(a)
print(*a)
