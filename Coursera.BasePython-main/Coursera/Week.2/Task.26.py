'''
Минимальный делитель
'''

N = int(input())
res = 2

while True:
    if (N % res == 0):
        print(res)
        break
    else:
        res += 1
