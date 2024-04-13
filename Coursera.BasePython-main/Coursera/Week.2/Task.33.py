'''
Длина последовательности
'''
_input = int(input())
cond = _input != 0
count = 0
if (cond):
    count = 1

while (cond):
    _input = int(input())
    cond = _input != 0
    if (cond):
        count += 1
print(count)
