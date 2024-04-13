'''
Шеренга
'''


def findPlace(a, p):
    pos = 0
    while pos < len(a) and a[pos] >= p:
        pos += 1
    print(pos + 1)

a = list(map(int, input().split()))
p = int(input())
findPlace(a, p)
