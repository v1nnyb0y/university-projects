'''
Точная степень двойки
'''

N = int(input())
res = 1
while (res < N):
    res *= 2
if (res == N):
    print('YES')
else:
    print('NO')
