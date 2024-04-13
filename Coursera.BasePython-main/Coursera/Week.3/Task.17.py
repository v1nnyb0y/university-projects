'''
Дублирование фрагмента
'''
inp_str = input()
f = -1
l = -1
f = inp_str.find('h')
l = inp_str[::-1].find('h')
l = len(inp_str) - l - 1
print(inp_str[0:f + 1], inp_str[f + 1:l] * 2, inp_str[l:], sep='')
