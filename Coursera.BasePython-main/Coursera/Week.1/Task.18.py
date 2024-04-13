'''
Electronic clock in seconds
'''

seconds = int(input())
hours = seconds // 3600

minutes = seconds // 60 % 60
m_ten = minutes // 10
minutes = minutes % 10

seconds = seconds % 60
s_ten = seconds // 10
seconds = seconds % 10

print(hours % 24, ':', m_ten, minutes, ':', s_ten, seconds, sep='', end='\n')
