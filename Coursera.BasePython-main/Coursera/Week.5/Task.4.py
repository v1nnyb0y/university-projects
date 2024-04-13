'''
Сумма квадратов
'''


def sumSqr(n):
    resSum = 0
    for i in range(1, n + 1):
        resSum += i**2
    return resSum

n = int(input())
print(sumSqr(n))
