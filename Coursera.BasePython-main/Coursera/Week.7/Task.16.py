'''
Выборы в США
'''
candidates = dict()
with open('input.txt', 'r', encoding='utf8') as in_file:
    for line in in_file:
        pair = line.split()
        candidates[pair[0]] = candidates.get(pair[0], 0) + int(pair[1])
for candidate, vote in sorted(candidates.items()):
    print(candidate, vote, sep=' ')
