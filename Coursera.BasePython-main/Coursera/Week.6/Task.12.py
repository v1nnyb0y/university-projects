'''
Результаты олимпиады
'''
n = int(input())
arr = []
for i in range(n):
    a = list(input().split())
    arr.append((a[0], int(a[1])))
arr.sort(key=lambda x: x[1])
arr.reverse()
for i in arr:
    print(i[0], end='\n')
