'''
Замена внутри фрагмента
'''
inp_str = input()
f = -1
l = -1
f = inp_str.find('h')
l = inp_str.rfind('h')
inp_str = inp_str[:f + 1] + inp_str[f + 1:l].replace('h', 'H') + inp_str[l:]
print(inp_str)
