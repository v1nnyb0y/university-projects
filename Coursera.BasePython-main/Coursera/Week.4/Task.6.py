'''
Принадлежит ли точка кругу
'''


def IsPointInCircle(x, y, xc, yc, r):
    if ((x - xc)**2 + (y - yc)**2 <= r**2):
        print('YES')
    else:
        print('NO')

x, y = float(input()), float(input())
xc, yc = float(input()), float(input())
r = float(input())
IsPointInCircle(x, y, xc, yc, r)
