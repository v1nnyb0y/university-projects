'''
Сложное уравнение
'''

a = int(input())
b = int(input())
c = int(input())
d = int(input())

if (a == 0 and b == 0):
    print('INF')
elif (b * c != d * a or c == 0):
    if (b / a == int(b / a)):
        print(int(- b / a))
    else:
        print('NO')
else:
    print('NO')
