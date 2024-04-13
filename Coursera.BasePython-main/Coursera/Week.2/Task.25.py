'''
Список квадратов
'''

N = int(input())
i = 1
while True:
    if (i**2 <= N):
        print(i**2, end=' ')
        i += 1
    else:
        break
