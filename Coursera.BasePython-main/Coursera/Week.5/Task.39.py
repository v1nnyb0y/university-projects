'''
Ферзи
'''


def pherz():
    n = 8
    x = [0] * n
    y = [0] * n
    for i in range(n):
        x[i], y[i] = [int(j) for j in input().split()]
    for i in range(n):
        for j in range(i + 1, n):
            cond_1 = (x[i] == x[j])
            cond_2 = (y[i] == y[j])
            cond_3 = abs(x[i] - x[j]) == abs(y[i] - y[j])
            if cond_1 or cond_2 or cond_3:
                print('YES')
                exit()
    print('NO')

pherz()
