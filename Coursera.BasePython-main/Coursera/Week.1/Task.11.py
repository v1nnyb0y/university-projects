'''
Electronic clocks
'''

minutes = int(input())
print(minutes // 60 % 24, minutes % 60, sep=' ', end='\n')
