'''
Лесенка
'''


def ladder(n):
    string = ''
    for i in range(1, n + 1):
        string += str(i)
        print(string)

n = int(input())
ladder(n)
