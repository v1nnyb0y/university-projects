'''
Школы с наибольшим числом участников олимпиады
'''
sch_mem = {}
with open('input.txt', 'r', encoding='utf8') as in_data:
    for data in in_data.readlines():
        school, ball = list(map(int, data.split()[-2:]))
        if school in sch_mem:
            sch_mem[school][0] += 1
            sch_mem[school][1] += ball
        else:
            sch_mem[school] = [1, ball]
max_mem = max(sch_mem.items(), key=lambda x: x[1][0])[1][0]
filtr_max_mem = list(filter(lambda x: x[1][0] == max_mem, sch_mem.items()))
filtr_max_mem.sort(key=lambda x: x[0])
for result in filtr_max_mem:
    print(result[0], end=' ')
