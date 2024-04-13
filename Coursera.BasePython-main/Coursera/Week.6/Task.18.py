'''
Семипроцентный барьер
'''
parties = []
votes = []
sum_votes = 0
with open('input.txt', 'r', encoding='utf8') as file:
    flag = False
    for line in file:
        line_el = line.split()
        line = line.replace('\n', '')
        if (line_el[0][1:] == 'PARTIES:'):
            continue
        elif (line_el[0] == 'VOTES:'):
            flag = True
            continue
        if (flag):
            sum_votes += 1
            votes[parties.index(line)] += 1
        else:
            parties.append(line)
            votes.append(0)
for id in range(len(parties)):
    if (votes[id] / sum_votes >= 0.07):
        print(parties[id])
