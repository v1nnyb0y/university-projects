'''
Принадлежит ли точка области
'''


def IsPointInCircle(x, y, xc, yc, r):
    return ((x - xc)**2 + (y - yc)**2 <= r**2)


def IsPointOutCircle(x, y, xc, yc, r):
    return ((x - xc)**2 + (y - yc)**2 >= r**2)


def IsPointInArea(x, y):
    u = (y >= 2 * x + 2 and y >= -x and IsPointInCircle(x, y, -1, 1, 2))
    d = (y <= 2 * x + 2 and y <= -x and IsPointOutCircle(x, y, -1, 1, 2))
    if (u or d):
        print('YES')
    else:
        print('NO')

x = float(input())
y = float(input())
IsPointInArea(x, y)
