'''
Наибольшее произведение двух чисел
'''


def maxPower(a):
    maxEl_1 = float('-inf')
    maxEl_2 = float('-inf')
    minEl_1 = float('inf')
    minEl_2 = float('inf')
    for i in a:
        if (i > maxEl_1):
            (maxEl_1, maxEl_2) = (i, maxEl_1)
        elif (i > maxEl_2):
            maxEl_2 = i

        if (i < minEl_1):
            (minEl_1, minEl_2) = (i, minEl_1)
        elif (i < minEl_2):
            minEl_2 = i
    if (maxEl_1 * maxEl_2 > minEl_1 * minEl_2):
        return (maxEl_2, maxEl_1)
    else:
        return (minEl_1, minEl_2)

a = list(map(int, input().split()))
print(*maxPower(a))
