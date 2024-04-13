'''
Наименьшее расстояние между локальными максимумами
'''
x = 1
i = 0
Val1 = 1
Val2 = 0
Val3 = 0
max1_n = 0
max2_n = 0
Res = 1000000
while Val1 != 0:
    i += 1
    Val3 = Val2
    Val2 = Val1
    Val1 = int(input())
    if i > 2 and Val2 > Val1 and Val2 > Val3 and Val1 != 0:
        max2_n = max1_n
        max1_n = i
        if (max1_n > 0) and (max2_n > 0) and (max1_n - max2_n < Res):
            Res = max1_n - max2_n
if Res == 1000000:
    Res = 0
print(Res)
