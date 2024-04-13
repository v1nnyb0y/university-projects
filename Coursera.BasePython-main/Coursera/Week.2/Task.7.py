'''
Color of chess
'''

x_1 = int(input())
y_1 = int(input())
x_2 = int(input())
y_2 = int(input())

k_1 = abs(x_1 - y_1)
k_2 = abs(x_2 - y_2)

if (k_1 % 2 == k_2 % 2):
    print('YES')
else:
    print('NO')
