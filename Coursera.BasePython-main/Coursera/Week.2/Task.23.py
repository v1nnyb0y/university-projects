'''
Спички
'''
a = 1
b = 2
c = 3
l1, r1 = int(input()), int(input())
l2, r2 = int(input()), int(input())
l3, r3 = int(input()), int(input())

if (l1 > l2):
    l1, r1, a, l2, r2, b = l2, r2, b, l1, r1, a

if (l1 > l3):
    l1, r1, a, l3, r3, c = l3, r3, c, l1, r1, a

if (l2 > l3):
    l2, r2, b, l3, r3, c = l3, r3, c, l2, r2, b

if (r1 >= l2 and (r2 >= l3 or r1 >= r3)):
    print(0)
else:
    p0 = False
    p2 = False
    if (r2 + r1 - l1 >= l3):
        p0 = True
    if (r1 + r3 - l3 >= l2):
        p2 = True
    if (p0 and p2):
        print(min(a, c))
    elif (p0):
        print(a)
    elif (p2):
        print(c)
    else:
        print(-1)
