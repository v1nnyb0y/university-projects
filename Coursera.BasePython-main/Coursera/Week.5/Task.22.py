'''
Наименьший положительный
'''


def minPositive(a):
    minEl = float('inf')
    for i in a:
        if (minEl > i and i > 0):
            minEl = i
    return minEl

a = list(map(int, input().split()))
print(minPositive(a))
