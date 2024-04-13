'''
Количество нулей
'''


def countZeros(n):
    count = 0
    while(n > 0):
        a = int(input())
        if (a == 0):
            count += 1
        n -= 1
    return count

n = int(input())
print(countZeros(n))
