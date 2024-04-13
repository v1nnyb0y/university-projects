'''
Упаковка
'''
l1, w1, h1 = int(input()), int(input()), int(input())
l2, w2, h2 = int(input()), int(input()), int(input())
lc, wc, hc = int(input()), int(input()), int(input())

Sum_H = (h1 + h2 <= hc)
H = (h1 <= hc and h2 <= hc)

if (l1 <= lc and l2 <= lc and w1 + w2 <= wc and H):
    print('YES')
elif (w1 <= lc and w2 <= lc and l1 + l2 <= wc and H):
    print('YES')
elif (l1 <= lc and w2 <= lc and w1 + l2 <= wc and H):
    print('YES')
elif (w1 <= lc and l2 <= lc and w2 + l1 <= wc and H):
    print('YES')
elif (l1 <= wc and l2 <= wc and w1 + w2 <= lc and H):
    print('YES')
elif (w1 <= wc and w2 <= wc and l1 + l2 <= lc and H):
    print('YES')
elif (l1 <= wc and w2 <= wc and w1 + l2 <= lc and H):
    print('YES')
elif (w1 <= wc and l2 <= wc and w2 + l1 <= lc and H):
    print('YES')
elif (l1 <= lc and l2 <= lc and w2 <= wc and w1 <= wc and Sum_H):
    print('YES')
elif (l1 <= lc and w2 <= lc and l2 <= wc and w1 <= wc and Sum_H):
    print('YES')
elif (w1 <= lc and l2 <= lc and w2 <= wc and l1 <= wc and Sum_H):
    print('YES')
elif (w1 <= lc and w2 <= lc and l2 <= wc and l1 <= wc and Sum_H):
    print('YES')
else:
    print('NO')
