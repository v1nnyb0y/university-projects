'''
Next and previous number of the input number
'''

number = int(input())
next = 'The next number for the number '
prev = 'The previous number for the number '
_is_ = ' is '
dot = '.'
print(next, number, _is_, number + 1, dot, sep='', end='\n')
print(prev, number, _is_, number - 1, dot, sep='', end='\n')
