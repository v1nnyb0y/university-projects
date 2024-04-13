'''
Полиглоты
'''
n = int(input())
students = list()
for i in range(n):
    m = int(input())
    temp = []
    for el in range(m):
        temp.append(input())
    students.append(set(temp))
know_by_someone = set.union(*students)
know_by_everyone = set.intersection(*students)
print(len(know_by_everyone), *sorted(know_by_everyone), sep='\n')
print(len(know_by_someone), *sorted(know_by_someone), sep='\n')
