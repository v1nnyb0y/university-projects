'''
Вставить элемент
'''


def insertEl(a, k, c):
    a.append(0)
    i = len(a) - 1
    while(i > k):
        a[i] = a[i - 1]
        i -= 1
    a[k] = c

a = list(map(int, input().split()))
ins_num = list(map(int, input().split()))
insertEl(a, ins_num[0], ins_num[1])
print(*a)
