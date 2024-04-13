'''
Продажи
'''


def bill(x):
    temp = {}
    for i in x:
        prod, count = i
        if prod in temp:
            temp[prod] += int(count)
        else:
            temp[prod] = int(count)
    return temp

base = {}
with open('input.txt', 'r', encoding='utf8') as in_file:
    for line in in_file:
        man, prod, count = line.split()
        if man in base:
            base[man].append((prod, count))
        else:
            base[man] = [(prod, count)]
for i in sorted(base):
    print(i + ':')
    for x, i in sorted(bill(base[i]).items()):
        print(x, i)
