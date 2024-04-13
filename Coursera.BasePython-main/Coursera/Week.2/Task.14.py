'''
Even or not even
'''

a = int(input())
b = int(input())
c = int(input())

even = (a % 2 == 0) or (b % 2 == 0) or (c % 2 == 0)
not_even = (a % 2 != 0) or (b % 2 != 0) or (c % 2 != 0)

if (even and not_even):
    print('YES')
else:
    print('NO')
