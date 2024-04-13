'''
Наибольший элемент
'''


def maxEl(a):
    maxId = float('-inf')
    maxE = float('-inf')
    id = 0
    for i in a:
        if (maxE < i):
            maxE = i
            maxId = id
        id += 1
    return (maxE, maxId)

a = list(map(int, input().split()))
print(*maxEl(a))
