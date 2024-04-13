'''
Диофантово уравнение - 2
'''


def perebor(a, b, c, d, e):
    count = 0
    for i in range(0, 1001):
        chisl = a * i**3 + b * i**2 + c * i + d
        znam = i - e
        if (znam == 0):
            continue
        if (chisl / znam == 0):
            count += 1
    return count

a = int(input())
b = int(input())
c = int(input())
d = int(input())
e = int(input())
print(perebor(a, b, c, d, e))
