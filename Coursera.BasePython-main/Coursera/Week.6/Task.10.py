'''
Клавиатура
'''


def isBroken(a, k):
    cnt = [0] * len(a)
    for e in k:
        cnt[e - 1] += 1
    for e in range(len(a)):
        if (a[e] < cnt[e]):
            print('YES', end='\n')
        else:
            print('NO', end='\n')

n = int(input())
a = [int(j) for j in input().split()]
k = int(input())
p = [int(j) for j in input().split()]
isBroken(a, p)
