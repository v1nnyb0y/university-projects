'''
Максимальное число подряд идущих равных
'''
_inp = int(input())
max_c = 1
count = 1
while (_inp != 0):
    a = int(input())
    if (a == _inp):
        count += 1
        if (max_c < count):
            max_c = count
    else:
        if (max_c < count):
            max_c = count
        count = 1
    _inp = a
print(max_c)
