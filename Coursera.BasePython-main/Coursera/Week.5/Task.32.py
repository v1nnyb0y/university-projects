'''
Циклический сдвиг вправо
'''


def goRight(a):
    temp = a[0]
    i = 1
    while(i < len(a)):
        (a[i], temp) = (temp, a[i])
        i += 1
    a[0] = temp

a = list(map(int, input().split()))
goRight(a)
print(*a)
