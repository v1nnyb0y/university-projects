'''
Ряд - 3
'''


def printRangeOdd(n):
    start = int('1' + '0' * n) - 1
    if (n == 1):
        end = 0
    else:
        end = int('9' + '9' * (n - 2))
    for i in range(start, end, -2):
        print(i, end=' ')
    print('')

n = int(input())
printRangeOdd(n)
