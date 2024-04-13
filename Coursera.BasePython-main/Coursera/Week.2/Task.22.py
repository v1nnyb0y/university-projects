'''
Котлеты
'''

k, m, n = int(input()), int(input()), int(input())

if (n <= k):
    print(m * 2)
elif (n * 2 % k) == 0:
    print(m * (n * 2 // k))
else:
    print(m * (1 + (n * 2 // k)))
