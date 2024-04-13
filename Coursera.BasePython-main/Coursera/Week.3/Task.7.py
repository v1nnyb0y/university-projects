'''
Сложные проценты
'''
from math import floor
p = int(input())
x = int(input())
y = int(input())
k = int(input())

while(k != 0):
    total = x * 100 + y
    proc = total + (total * p / 100)
    x = int(proc // 100)
    y = int(proc % 100)
    k -= 1
print(x, y, sep=' ')
