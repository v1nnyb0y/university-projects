'''
Обувной магазин
'''


def countShoes(n, a):
    new = 0
    s = 0
    for i in range(len(a)):
        if a[i] < n:
            continue
        elif a[i] == s:
            continue
        elif a[i] == n:
            new += 1
            s = a[i]
        elif a[i] - s >= 3:
            new += 1
            s = a[i]
    return new

n = int(input())
a = [int(j) for j in input().split()]
a.sort()
print(countShoes(n, a))
