'''
Словарь синонимов
'''
n = int(input())
syn = dict()
for i in range(n):
    line = input().split()
    syn[line[0]] = line[1]
    syn[line[1]] = line[0]
word = input()
print(syn[word])
