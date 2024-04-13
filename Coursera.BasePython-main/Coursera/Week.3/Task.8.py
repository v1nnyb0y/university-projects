'''
Схема Горнера
'''
n = int(input())
x = float(input())
curr = float(input())
while(n != 0):
    prev = float(input())
    curr = curr * x + prev
    n -= 1
print(curr)
