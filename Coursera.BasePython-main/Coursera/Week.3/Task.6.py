'''
Проценты
'''
from math import floor
p = int(input())
x = int(input())
y = int(input())

total = x * 100 + y
proc = total + (total * p / 100)
r = int(proc // 100)
k = int(proc % 100)

print(r, k, sep=' ')
