'''
Последний максимум
'''


def lastMax(a):
    maxEl = -1
    maxI = -1
    currId = 0
    for i in a:
        if (i >= maxEl):
            maxEl = i
            maxI = currId
        currId += 1
    return (maxEl, maxI)

a = list(map(int, input().split()))
print(*lastMax(a))
