'''
Слияние списков
'''


def merge(a, b):
    c = []
    i = 0
    j = 0
    while (i < len(a) and j < len(b)):
        if (a[i] < b[j]):
            c.append(a[i])
            i += 1
        elif (a[i] == b[j]):
            c.append(a[i])
            c.append(b[j])
            i += 1
            j += 1
        else:
            c.append(b[j])
            j += 1
    if (i == len(a) and j == len(b)):
        return c
    elif (i == len(a)):
        c.extend(b[j:])
        return c
    elif (j == len(b)):
        c.extend(a[i:])
        return c

a = list(map(int, input().split()))
b = list(map(int, input().split()))
print(*merge(a, b))
