'''
Среднее значение последовательности
'''
_input = int(input())
sum = _input
count = 1
cond = _input != 0

while (cond):
    _input = int(input())
    cond = _input != 0
    if (cond):
        sum += _input
        count += 1
print(sum / count)
