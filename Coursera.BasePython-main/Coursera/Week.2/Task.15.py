'''
Sort 3 numbers
'''

a, b, c = int(input()), int(input()), int(input())

if (a > b):
    if (b > c):
        print(c, b, a, sep=' ')
    else:
        (b, c) = (c, b)
        if (a > b):
            print(c, b, a, sep=' ')
        else:
            (a, b) = (b, a)
            print(c, b, a, sep=' ')
else:
    (a, b) = (b, a)
    if (a > c):
        if (b > c):
            print(c, b, a, sep=' ')
        else:
            (b, c) = (c, b)
            print(c, b, a, sep=' ')
    else:
        (a, c) = (c, a)
        if (b > c):
            print(c, b, a, sep=' ')
        else:
            (b, c) = (c, b)
            print(c, b, a, sep=' ')
