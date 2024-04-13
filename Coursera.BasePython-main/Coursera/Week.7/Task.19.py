'''
Выборы Президента
'''
num_votes = 0
candidates = {}
with open('input.txt', 'r', encoding='utf8') as in_file:
    for line in in_file:
        num_votes += 1
        candidates[line] = candidates.get(line, 0) + 1

candidates = sorted(candidates.items(), key=lambda x: x[1], reverse=True)

with open("output.txt", "wt", encoding="utf8") as f:
    percent = candidates[0][1] / num_votes * 100
    if percent > 50:
            print(candidates[0][0], file=f)
    else:
        for name, _ in candidates[:2]:
            print(name, file=f)
