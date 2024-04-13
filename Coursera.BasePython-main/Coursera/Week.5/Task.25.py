'''
Переставить в обратном порядке
'''


def exchange(a, id):
    temp = a[id]
    a[id] = a[len(a) - id - 1]
    a[len(a) - id - 1] = temp

a = list(map(int, input().split()))
count = len(a) // 2
i = 0
while(i < count):
    exchange(a, i)
    i += 1
print(*a)
