'''
Наименьший нечетный
'''


def minOdd(a):
    minEl = float('inf')
    for i in a:
        if (i % 2 != 0 and i < minEl):
            minEl = i
    return minEl

a = list(map(int, input().split()))
print(minOdd(a))
