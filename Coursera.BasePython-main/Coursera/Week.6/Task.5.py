'''
Создание архива
'''
s, n = [int(e) for e in input().split()]
a = []
for i in range(n):
    a.append(int(input()))
a.sort()
new = 0
for i in a:
    if (i <= s):
        s -= i
        new += 1
print(new)
