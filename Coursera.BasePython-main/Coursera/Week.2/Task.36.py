'''
Количество четных элементов последовательности
'''
_input = int(input())
count = 0
cond = _input != 0

while (cond):
    if (_input % 2 == 0):
        count += 1
    _input = int(input())
    cond = _input != 0
print(count)
