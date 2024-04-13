'''
Больше своих соседей
'''


def countOneMark(a):
    size = len(a)
    i = 1
    count = 0
    while(i <= size - 2):
        if (a[i] > a[i + 1] and a[i] > a[i - 1]):
            count += 1
        i += 1
    return count

a = list(map(int, input().split()))
print(countOneMark(a))
