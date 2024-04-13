'''
Теорема Лагранжа
'''


def lagr(n, k):
    n1 = int(n**0.5)
    if n1 ** 2 == n:
        print(n1, end='')
        return int(n1**2)
    while n1 >= 1:
        s = n1**2
        if k + 1 <= 3:
            s += lagr(n - n1**2, k + 1)
        if s == n:
            print(' ', n1, sep='', end='')
            return int(s)
        n1 -= 1
    return 0


n = int(input())
lagr(n, 0)
print()
