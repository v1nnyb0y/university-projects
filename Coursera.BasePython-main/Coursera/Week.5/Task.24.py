'''
Вывести в обратном порядке
'''


def printReverse(a):
    print(*a[::-1])

a = list(map(int, input().split()))
printReverse(a)
