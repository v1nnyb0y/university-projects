'''
Второе вхождение
'''
inp_str = input()
f = inp_str.find('f')
if (f != -1):
    f = inp_str.find('f', f + 1)
    if (f != -1):
        print(f)
    else:
        print(-1)
else:
    print(-2)
