'''
Периметр треугольника
'''
from math import sqrt


def lenght(x1, y1, x2, y2):
    return sqrt((x2 - x1)**2 + (y2 - y1)**2)


def P(x, y, z):
    return x + y + z

x1 = int(input())
y1 = int(input())
x2 = int(input())
y2 = int(input())
x3 = int(input())
y3 = int(input())

a = lenght(x1, y1, x2, y2)
b = lenght(x2, y2, x3, y3)
c = lenght(x1, y1, x3, y3)

print(P(a, b, c))
