'''
Кегельбан
'''


def kegles(n, k):
    arr = []
    for i in range(n):
        arr.append('I')
    for i in range(k):
        lri = list(map(int, input().split()))
        for i in range(lri[0] - 1, lri[1]):
            arr[i] = '.'
    return arr

a = list(map(int, input().split()))
print(*kegles(a[0], a[1]), sep='')
