'''
Ближайшее число
'''


def findClosest(n, a, close):
    minDiff = float('inf')
    res = float('-inf')
    for i in a:
        if (abs(i - close) < minDiff):
            minDiff = abs(i - close)
            res = i
    return res

n = int(input())
a = list(map(int, input().split()))
close = int(input())
print(findClosest(n, a, close))
