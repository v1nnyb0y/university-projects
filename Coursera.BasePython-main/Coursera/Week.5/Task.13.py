'''
Четные индексы
'''


def evenNum(a):
    i = 0
    while(len(a) > i):
        print(a[i], end=' ')
        i += 2
    print('')

a = list(map(int, input().split()))
evenNum(a)
