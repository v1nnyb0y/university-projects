'''
Пересечение множеств
'''
a = [int(inp) for inp in input().split()]
b = [int(inp) for inp in input().split()]
print(*sorted(set(a) & set(b)))
