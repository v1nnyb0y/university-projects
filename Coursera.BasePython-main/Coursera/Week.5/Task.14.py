'''
Четные элементы
'''


def evenNum(a):
    i = 0
    for i in a:
        if (i % 2 == 0):
            print(i, end=' ')
    print('')

a = list(map(int, input().split()))
evenNum(a)
