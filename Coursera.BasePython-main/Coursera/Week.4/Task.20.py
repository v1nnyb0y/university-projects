'''
Разворот последовательности
'''


def reverse():
    a = int(input())

    if (a == 0):
        print(a)
    else:
        reverse()
        print(a)

reverse()
