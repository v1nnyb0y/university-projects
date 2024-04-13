'''
Сумма кубов
'''
from math import *


def sum_3(n, a, t):
    c = n
    d = t
    while c > 0:
        if d > 0:
            x = trunc(float('{0:.11f}'.format((c**(1 / 3))))) - 1
            d -= 1
        else:
            x = trunc(float('{0:.11f}'.format((c**(1 / 3)))))
        if x <= 1:
            x = trunc(float('{0:.11f}'.format((c**(1 / 3)))))
        if d > x:
            print(0)
            exit(0)
        a.append(x**3)
        c -= x**3
        if len(a) > 7:
            a.clear()
            return sum_3(n, a, t + 1)
    return a

a = []
n = int(input())
t = 0
a = sum_3(n, a, t)
print(*a)
