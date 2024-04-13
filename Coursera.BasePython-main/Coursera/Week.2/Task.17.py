'''
Узник замка Иф
'''

a, b, c = int(input()), int(input()), int(input())
d, e = int(input()), int(input())

sq_1 = (a <= d and b <= e) or (a <= e and b <= d)
sq_2 = (b <= d and c <= e) or (b <= e and c <= d)
sq_3 = (a <= d and c <= e) or (a <= e and c <= d)

if (sq_1 or sq_2 or sq_3):
    print('YES')
else:
    print('NO')
