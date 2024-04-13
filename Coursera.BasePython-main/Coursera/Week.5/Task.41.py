'''
Самое частое число
'''
a = list(map(int, input().split()))
a_set = set(a)

most_common = None
qty_most_common = 0

for item in a_set:
    qty = a.count(item)
    if qty > qty_most_common:
        qty_most_common = qty
        most_common = item

print(most_common)
