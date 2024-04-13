'''
Первое и последнее вхождение
'''
inp_str = input()
first = -1
second = -1

first = inp_str.find('f')
if (first != -1):
    second = inp_str[::-1].find('f')
    second = len(inp_str) - second - 1
    if (second != len(inp_str) and abs(second - first) > 0):
        print(first, second, sep=' ')
    else:
        print(first)
