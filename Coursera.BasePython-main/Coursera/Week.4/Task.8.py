'''
Исключающее ИЛИ
'''


def XOR(x, y):
    if (x == y == 1 or x == y == 0):
        return 0
    else:
        return 1

x = int(input())
y = int(input())
print(XOR(x, y))
