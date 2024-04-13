'''
Мороженое
'''

k = int(input())

if (k % 5 == 4 and k >= 9):
    print('YES')
elif (k % 5 == 3):
    print('YES')
elif (k % 5 == 2 and k >= 12):
    print('YES')
elif (k % 5 == 1 and k >= 6):
    print('YES')
elif (k % 5 == 0):
    print('YES')
else:
    print('NO')
