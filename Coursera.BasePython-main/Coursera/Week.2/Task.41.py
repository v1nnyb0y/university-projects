'''
Номер числа Фибоначчи
'''
digit = int(input())
number = 2
first = 0
second = 1

if (digit == 0):
    print(0)
elif (digit == 1):
    print(1)
else:
    while (second < digit):
        temp = second
        second += first
        first = temp
        number += 1
    if (second == digit):
        print(number - 1)
    else:
        print(-1)
