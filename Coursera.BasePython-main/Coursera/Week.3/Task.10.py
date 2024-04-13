'''
Квадратное уравнение - 1
'''
import math
a = float(input())
b = float(input())
c = float(input())
discr = b ** 2 - 4 * a * c
if discr > 0:
    x1 = (-b + math.sqrt(discr)) / (2 * a)
    x2 = (-b - math.sqrt(discr)) / (2 * a)
    if (a < 0):
        print(x1, x2, sep=' ')
    else:
        print(x2, x1, sep=' ')
elif discr == 0:
    x = -b / (2 * a)
    print(x)
