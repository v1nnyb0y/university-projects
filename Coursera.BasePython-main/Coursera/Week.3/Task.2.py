'''
Сумма ряда
'''

N = int(input())
res = 0.0
while(N != 0):
    res += 1 / N**2
    N -= 1
print(res)
