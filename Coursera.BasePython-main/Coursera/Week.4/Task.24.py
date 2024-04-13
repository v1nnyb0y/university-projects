'''
Только квадраты
'''
from math import sqrt


def quads(string):
    n = int(input())
    if (n == 0):
        if (string == ''):
            print(0)
        else:
            print(string[::-1][1:])
        exit()
    if (int(sqrt(n))**2 == n):
        string += str(n)[::-1] + ' '
    quads(string)

quads('')
