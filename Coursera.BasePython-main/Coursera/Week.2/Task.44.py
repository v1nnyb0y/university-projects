'''
Количество палиндромов
'''

N = int(input())
number = 1
count = 0
while(N != number - 1):
    temp = str(number)[::-1]
    if (str(number) == temp):
        count += 1
    number += 1
print(count)
