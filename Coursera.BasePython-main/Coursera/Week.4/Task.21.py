'''
Ханойские башни
'''


def move(n, x, y):
    if n > 0:
        temp = 6 - x - y
        move(n - 1, x, temp)
        print(n, x, y)
        move(n - 1, temp, y)

n = int(input())
move(n, 1, 3)
