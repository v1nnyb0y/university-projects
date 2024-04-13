'''
Days for count
'''

speed = int(input())
current = int(input())
days = current // speed
if (current - speed * days != 0):
    days = days + 1

print(days, end='\n')
