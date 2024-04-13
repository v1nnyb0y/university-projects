'''
Количество совпадающих
'''
a = [int(inp) for inp in input().split()]
b = [int(inp) for inp in input().split()]
print(len(set(a) & set(b)))
