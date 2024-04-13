'''
Уникальные элементы
'''
a = list(map(int, input().split()))
for i in range(len(a)):
    flag = 1
    for j in range(len(a)):
        if a[i] == a[j] and i != j:
            flag = 0
            break
    if flag:
        print(a[i], end=' ')

print('')
