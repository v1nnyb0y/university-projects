'''
Ряд - 2
'''


def printRangeDown(a, b):
    for i in range(a, b - 1, -1):
        print(i, end=' ')
    print('')


def printRangeUp(a, b):
    for i in range(a, b + 1, 1):
        print(i, end=' ')
    print('')

a = int(input()), int(input())
if (a[0] > a[1]):
    printRangeDown(a[0], a[1])
else:
    printRangeUp(a[0], a[1])
