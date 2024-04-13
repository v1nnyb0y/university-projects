'''
Замечательные числа - 4
'''


def isPalindrom(n):
    return str(n) == str(n)[::-1]


def printAllPalindroms(a, b):
    for i in range(a, b + 1):
        if (isPalindrom(i)):
            print(i)

a = int(input()), int(input())
printAllPalindroms(a[0], a[1])
