'''
Квадратное уравнение - 2
'''
import math
a = float(input())
b = float(input())
c = float(input())
if (a == 0 and b == 0 and c == 0):
    print(3)
elif (a == 0 and b == 0 and c != 0):
    print(0)
elif (a == 0 and b != 0 and c == 0):
    print(1, 0, sep=' ')
elif (a == 0 and b != 0 and c != 0):
    print(1, -c / b, sep=' ')
else:
    discr = b ** 2 - 4 * a * c
    if discr > 0:
        x1 = (-b + math.sqrt(discr)) / (2 * a)
        x2 = (-b - math.sqrt(discr)) / (2 * a)
        if (a < 0):
            print(2, x1, x2, sep=' ')
        else:
            print(2, x2, x1, sep=' ')
    elif discr == 0:
        x = -b / (2 * a)
        print(1, x, sep=' ')
    else:
        print(0)
