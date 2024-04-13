'''
Sum of the digits in the number
'''

number = int(input())
print((number // 100) + (number // 10 % 10) + (number % 10))
