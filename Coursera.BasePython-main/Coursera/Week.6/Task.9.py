'''
Сортировка подсчетом
'''


def countSort(a):
    cnt = [0] * (max(a) + 1)
    for e in a:
        cnt[e] += 1
    a.clear()
    a = []
    for i in range(len(cnt)):
        if (cnt[i] != 0):
            a.append((str(i) + ' ') * cnt[i])
    return a

a = [int(j) for j in input().split()]
print(*countSort(a), sep='')
