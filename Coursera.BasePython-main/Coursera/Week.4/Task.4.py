'''
Принадлежит ли точка квадрату - 1
'''


def IsPointInSquare(x, y):
    return (abs(x) <= 1 and abs(y) <= 1)

x = float(input())
y = float(input())

if (IsPointInSquare(x, y)):
    print('YES')
else:
    print('NO')
