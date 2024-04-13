'''
Система линейных уравнений - 1
'''
a = float(input())
b = float(input())
c = float(input())
d = float(input())
e = float(input())
f = float(input())
if (a != 0):
    y = (a * f - c * e) / (a * d - c * b)
    x = (e - b * y) / a
else:
    y = e / b
    x = (f - d * y) / c
print(x, y, sep=' ')
