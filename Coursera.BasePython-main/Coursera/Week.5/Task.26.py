'''
Удалить элемент
'''


def removeEl(a, k):
    i = k
    while(i < len(a) - 1):
        a[i] = a[i + 1]
        i += 1
    a.pop()

a = list(map(int, input().split()))
k = int(input())
removeEl(a, k)
print(*a)
