'''
Polyndrom of 4 number
'''

number = abs(int(input()))
f_check = abs(number // 1000 - number % 10)
s_check = abs(number // 100 % 10 - number % 100 // 10)
print(f_check + s_check + 1)
