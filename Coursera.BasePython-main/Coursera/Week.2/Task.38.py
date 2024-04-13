'''
Второй максимум
'''
max = int(input())
max_2 = -1
_input = max
while (_input != 0):
    _input = int(input())
    if (max < _input and _input != 0):
        max_2 = max
        max = _input
    else:
        if (max_2 < _input):
            max_2 = _input

print(max_2)
