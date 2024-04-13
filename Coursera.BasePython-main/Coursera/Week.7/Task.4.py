'''
Встречалось ли число раньше
'''
a = [int(inp) for inp in input().split()]
col = set()
for now in a:
    g = set()
    g.add(now)
    if (g <= col):
        print('YES')
    else:
        print('NO')
    col.add(now)
