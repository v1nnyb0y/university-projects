'''
Список степеней двойки
'''

N = int(input())
res = 1
while True:
    if (res <= N):
        print(res, end=' ')
        res *= 2
    else:
        break
