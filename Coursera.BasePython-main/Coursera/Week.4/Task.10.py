'''
Проверка числа на простоту
'''
from math import sqrt


def IsPrime(n):
    i = 1
    while(i <= sqrt(n)):
        i += 1
        if (n % i == 0):
            return i
            break
    return n

n = int(input())
if (IsPrime(n) != n):
    print('NO')
else:
    print('YES')
