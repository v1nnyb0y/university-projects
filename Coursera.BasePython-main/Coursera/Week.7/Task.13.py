'''
Страны и города
'''
n = int(input())
cities = dict()
for line in range(n):
    line = input()
    line = line.split()
    country = line[0]
    for city in line[1:]:
        cities[city] = country
m = int(input())
for city in range(m):
    print(cities[input()])
