'''
Сократите дробь
'''


def gcd(a, b):
    if (b == 0):
        return a
    else:
        return gcd(b, a % b)


def ReduceFraction(n, m):
    digit = gcd(n, m)
    return n // digit, m // digit

a = int(input())
b = int(input())
print(*ReduceFraction(a, b))
