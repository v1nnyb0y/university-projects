'''
Сумма факториалов
'''
from math import factorial


def sumFact(n):
    sum = 0
    for i in range(1, n + 1):
        sum += factorial(i)
    return sum

n = int(input())
print(sumFact(n))
