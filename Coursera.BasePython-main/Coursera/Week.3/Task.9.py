'''
Стандартное отклонение
'''
from math import sqrt
a = int(input())
sum = 0
sum_kv = 0
count = 0
while(a != 0):
    sum += a
    sum_kv += a**2
    count += 1
    a = int(input())
s = sum / count
print(sqrt((sum_kv - 2 * s * sum + count * s**2) / (count - 1)))
