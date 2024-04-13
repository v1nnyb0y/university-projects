'''
Соседи одного знака
'''


def oneMark(a):
    size = len(a)
    i = 0
    while(i < size - 1):
        if (a[i] >= 0 and a[i + 1] >= 0):
            print(a[i], a[i + 1], sep=' ')
            exit()
        elif (a[i] < 0 and a[i + 1] < 0):
            print(a[i], a[i + 1], sep=' ')
            exit()
        i += 1

a = list(map(int, input().split()))
oneMark(a)
