'''
Количество элементов, равных максимуму
'''
max = int(input())
_input = max
count = 1
while (_input != 0):
    _input = int(input())
    if (max < _input and _input != 0):
        max = _input
        count = 1
    elif (max == _input and _input != 0):
        count += 1

print(count)
