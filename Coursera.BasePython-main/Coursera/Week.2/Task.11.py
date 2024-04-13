'''
Coordinates
'''

x_1 = int(input())
y_1 = int(input())
x_2 = int(input())
y_2 = int(input())

if (x_1 > 0 and y_1 > 0 and x_2 > 0 and y_2 > 0):
    print('YES')
elif (x_1 > 0 and y_1 < 0 and x_2 > 0 and y_2 < 0):
    print('YES')
elif (x_1 < 0 and y_1 > 0 and x_2 < 0 and y_2 > 0):
    print('YES')
elif (x_1 < 0 and y_1 < 0 and x_2 < 0 and y_2 < 0):
    print('YES')
else:
    print('NO')
