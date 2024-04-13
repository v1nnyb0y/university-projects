'''
Количество элементов, больше предыдущего
'''
_input = int(input())
count = 0
cond = _input != 0

while (cond):
    a = int(input())
    cond = a != 0
    if (cond and a > _input):
        count += 1
    _input = a
print(count)
