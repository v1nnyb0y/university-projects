'''
Отрицательная степень
'''
a = float(input())
n = int(input())
if (a == 0):
    print(0)
elif (n < 0):
    res = 1
    while (n != 0):
        res *= 1 / a
        n += 1
    print(res)
else:
    res = 1
    while(n != 0):
        res *= a
        n -= 1
    print(res)
