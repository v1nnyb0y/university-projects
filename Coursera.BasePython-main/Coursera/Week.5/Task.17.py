'''
Больше предыдущего
'''


def moreThanPrev(a):
    size = len(a)
    i = 1
    while(i < size):
        if (a[i] > a[i - 1]):
            print(a[i], end=' ')
        i += 1
    print('')

a = list(map(int, input().split()))
moreThanPrev(a)
