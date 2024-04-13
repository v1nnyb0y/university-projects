'''
Выборы депутатов Государственной Думы
'''
names = list()
votes = list()
sumVotes = 0
with open('input.txt', 'r', encoding='utf8') as in_file:
    for line in in_file:
        line = line.split()
        partyName = line[:-1]
        partyVotes = int(line[-1])
        names.append(partyName)
        votes.append(partyVotes)
        sumVotes += partyVotes
mandates = list()
fracPart = []
sumMandates = 0
for i in range(len(names)):
    partyMandates = (votes[i] * 450) / sumVotes
    sumMandates += int(partyMandates)
    mandates.append(int(partyMandates))
    fracPart.append(partyMandates - int(partyMandates))
while sumMandates < 450:
    i = 0
    for j in range(1, len(fracPart)):
        cond_1 = (fracPart[j] > fracPart[i])
        cond_2 = (fracPart[j] == fracPart[i] and votes[j] > votes[i])
        if (cond_1 or cond_2):
            i = j
    mandates[i] += 1
    sumMandates += 1
    fracPart[i] = 0

for k in range(len(names)):
    print(*names[k], mandates[k])
