'''
The price for item
'''

rubles = int(input())
part_rubles = int(input())
count = int(input())

rubles = rubles * count
part_rubles = part_rubles * count

print((rubles + part_rubles // 100), part_rubles % 100, sep=' ', end='\n')
