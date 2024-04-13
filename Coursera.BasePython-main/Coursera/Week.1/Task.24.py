'''
The div between one number on another
'''

number_1 = int(input())
number_2 = int(input())
res = 0**(number_1 % number_2)
print('YES' * res + 'NO' * (1 - res))
