'''
Обращение числа
'''
N = int(input())
while (N != 0):
    digit = N % 10
    N //= 10
    print(digit, end='')
print('')
