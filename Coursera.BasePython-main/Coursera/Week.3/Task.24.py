'''
Вставка символов
'''
inp_str = input()
i = 0
size = len(inp_str) - 1
while(size > 0):
    inp_str = inp_str[:i + 1] + '*' + inp_str[i + 1:]
    i += 2
    size -= 1
print(inp_str)
