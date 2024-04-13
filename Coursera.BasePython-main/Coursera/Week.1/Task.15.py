'''
The next even number
'''

number = int(input())
print((number * 2 + 2**(abs(number % 2 - 1) + 1)) // 2, end='\n')
