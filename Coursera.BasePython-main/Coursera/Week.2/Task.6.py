'''
Rooms
'''

f_number = int(input())
l_number = int(input())

step = l_number - f_number + 1
if ((l_number % step == 0) and (f_number % step == 1)):
    print('YES')
elif (step == 1):
    print('YES')
else:
    print('NO')
