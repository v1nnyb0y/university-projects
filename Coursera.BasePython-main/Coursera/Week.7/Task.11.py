'''
Забастовки
'''
n, k = [int(el) for el in input().split()]
days = set()
weekend = {int(el) for el in range(6, n + 1, 7)}
weekend.update({int(el) for el in range(7, n + 1, 7)})
for i in range(k):
    ai, bi = [int(el) for el in input().split()]
    days.update({x for x in range(ai, n + 1, bi)})
print(len(days.difference(weekend)))
