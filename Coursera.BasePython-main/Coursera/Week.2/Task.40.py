'''
Числа Фибоначчи
'''

N = int(input())
number = 2
first = 0
second = 1

if (N == 0):
    print(first)
elif (N == 1):
    print(second)
else:
    while (number - 1 != N):
        temp = second
        second += first
        first = temp
        number += 1
    print(second)
