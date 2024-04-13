'''
Максимум последовательности
'''
max = int(input())
_input = max
while (_input != 0):
    _input = int(input())
    if (max < _input and _input != 0):
        max = _input

print(max)
