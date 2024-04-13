'''
Потерянная карточка
'''


def numCard(n):
    sum_inp = 0
    sum_real = sum(range(1, n + 1), 0)
    while(n - 1 > 0):
        sum_inp += int(input())
        n -= 1
    return (sum_real - sum_inp)

n = int(input())
print(numCard(n))
