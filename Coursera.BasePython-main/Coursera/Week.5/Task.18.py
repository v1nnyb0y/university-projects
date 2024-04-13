'''
Возрастает ли список?
'''


def isHigh(a):
    i = 0
    size = len(a)
    while(i < size - 1):
        if (a[i] >= a[i + 1]):
            print('NO')
            exit()
        i += 1
    print('YES')

a = list(map(int, input().split()))
isHigh(a)
