'''
Угадай число
'''
max = int(input())
s = set(range(1, max + 1))
temp = set()
with open('input.txt', 'r', encoding='utf8') as file:
    for line in file:
        if ('YES' in line):
            s &= temp
        elif ('NO' in line):
            s -= temp
        elif ('HELP' not in line):
            temp = set([int(el) for el in line.split()])
print(*sorted(s))
