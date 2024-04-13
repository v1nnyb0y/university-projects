'''
Наибольшее произведение трех чисел
'''


def maxPower(a):
    maxEl_1 = float('-inf')
    maxEl_2 = float('-inf')
    maxEl_3 = float('-inf')
    minEl_1 = float('inf')
    minEl_2 = float('inf')
    minEl_3 = float('inf')
    if (len(a) == 3):
        return (a[0], a[1], a[2])
    for i in a:
        if (i > maxEl_1):
            (maxEl_1, maxEl_2, maxEl_3) = (i, maxEl_1, maxEl_2)
        elif (i > maxEl_2):
            (maxEl_2, maxEl_3) = (i, maxEl_2)
        elif (i > maxEl_3):
            maxEl_3 = i

        if (i < minEl_1):
            (minEl_1, minEl_2, minEl_3) = (i, minEl_1, minEl_2)
        elif (i < minEl_2):
            (minEl_2, minEl_3) = (i, minEl_2)
        elif (i < minEl_3):
            minEl_3 = i
    p1 = maxEl_1 * maxEl_2 * maxEl_3
    p2 = minEl_1 * minEl_2 * minEl_3
    p3 = maxEl_1 * minEl_1 * minEl_2
    p4 = maxEl_1 * minEl_2 * minEl_3
    maxEl = max(p1, p2, p3, p4)
    if (maxEl == p1):
        return (maxEl_1, maxEl_2, maxEl_3)
    elif (maxEl == p2):
        return (minEl_1, minEl_2, minEl_3)
    elif (maxEl == p3):
        return (maxEl_1, minEl_1, minEl_2)
    elif (maxEl == p4):
        return (maxEl_1, minEl_2, minEl_3)

a = list(map(int, input().split()))
print(*maxPower(a))
