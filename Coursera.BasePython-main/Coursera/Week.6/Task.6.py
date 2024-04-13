'''
Гражданская оборона
'''
n = int(input())
h = [int(e) for e in input().split()]
m = int(input())
s = [int(e) for e in input().split()]
for e in range(len(h)):
    h[e] = (h[e], e)
for e in range(len(s)):
    s[e] = (s[e], e)
h.sort()
s.sort()
i = 1
for j in range(len(h)):
    min = abs(s[i - 1][0] - h[j][0])
    while (i < len(s) and abs(s[i][0] - h[j][0]) < min):
        min = abs(s[i][0] - h[j][0])
        i += 1
    h[j] = (h[j][1], s[i - 1][1] + 1)
h.sort()
for eh in h:
    print(eh[1], end=' ')
print('')
