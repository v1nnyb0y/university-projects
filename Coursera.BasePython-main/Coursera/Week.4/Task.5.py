'''
Принадлежит ли точка квадрату - 2
'''


def IsPointInSquare(x, y):
    if (abs(y) <= -abs(x) + 1):
        print('YES')
    else:
        print('NO')

x = float(input())
y = float(input())
IsPointInSquare(x, y)
