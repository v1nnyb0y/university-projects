'''
Удалить каждый третий символ
'''
inp_str = input()
i = 0
count = 0
while(i - count < len(inp_str)):
    inp_str = inp_str.replace(inp_str[i - count], '', 1)
    count += 1
    i += 3
print(inp_str)
