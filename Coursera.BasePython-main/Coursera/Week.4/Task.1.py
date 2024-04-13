'''
Минимум 4 чисел
'''


def min_4(a, b, c, d):
    return min(min(a, b), min(d, c))


a = int(input())
b = int(input())
c = int(input())
d = int(input())

print(min_4(a, b, c, d))
