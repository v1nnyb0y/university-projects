'''
Угадай число - 2
'''
size = int(input())
conceivedset = set(range(1, size+1))
for guess_str in iter(input, 'HELP'):
    guess = set(int(e) for e in guess_str.split())
    yes = conceivedset & guess
    if len(conceivedset) / 2 < len(yes):
        print('YES')
        conceivedset = yes
    else:
        print('NO')
        conceivedset.difference_update(guess)
print(' '.join(map(str, sorted(conceivedset))))
