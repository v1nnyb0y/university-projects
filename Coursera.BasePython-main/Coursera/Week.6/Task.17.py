'''
Такси
'''
km = [int(j) for j in input().split()]
pay = [int(j) for j in input().split()]
km.sort()
pay.sort(reverse=True)
sum = 0
for i in range(len(km)):
    sum += km[i] * pay[i]
print(sum)
