'''
Утренняя пробежка
'''
X = int(input())
Y = int(input())
inc = 1.1
day = 1

while (X < Y):
    X *= inc
    day += 1

print(day)
