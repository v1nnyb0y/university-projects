'''
Минимальный делитель числа
'''
from math import sqrt


def MinDivisor(n):
    i = 1
    while(i <= sqrt(n)):
        i += 1
        if (n % i == 0):
            return i
            break
    return n

n = int(input())
print(MinDivisor(n))
