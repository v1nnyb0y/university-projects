'''
King step
'''

x_1 = int(input())
y_1 = int(input())
x_2 = int(input())
y_2 = int(input())

if ((x_1 + 1 == x_2) or (x_1 - 1 == x_2) or (x_1 == x_2)):
    if ((y_1 + 1 == y_2) or (y_1 - 1 == y_2) or (y_1 == y_2)):
        print('YES')
    else:
        print('NO')
else:
    print('NO')
