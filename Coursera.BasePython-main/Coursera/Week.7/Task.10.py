'''
Пересадки
'''
a, b, c, d = [int(el) for el in input().split()]
f = {*range(min(a, b), max(a, b) + 1)}
s = {*range(min(c, d), max(c, d) + 1)}
print(len(f & s))
