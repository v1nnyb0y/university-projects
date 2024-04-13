'''
Chocolate
'''

size_n = int(input())
size_m = int(input())
count = int(input())

if (count % size_n == 0 and count // size_n <= size_m):
    print('YES')
elif (count % size_m == 0 and count // size_m <= size_n):
    print('YES')
else:
    print('NO')
