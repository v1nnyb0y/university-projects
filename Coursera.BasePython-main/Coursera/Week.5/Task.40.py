'''
Сжатие списка
'''
a = list(map(int, input().split()))
i = 0
id = -1
temp = -1
while(i < len(a)):
    if (a[i] == 0):
        id = i
    if (id != -1):
        if (temp == -1):
            temp = i + 1
        while(temp < len(a)):
            if (a[temp] != 0):
                (a[id], a[temp]) = (a[temp], a[id])
                break
            temp += 1
        if (temp == len(a)):
            print(*a)
            exit()
        id = -1
    i += 1
print(*a)
