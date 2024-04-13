'''
Переставить два слова
'''
inp_str = input()
space = inp_str.find(' ')
print(inp_str[space + 1:], inp_str[:space], sep=' ')
