'''
End of line
'''

high = int(input())
up = int(input())
down = int(input())
step = up - down
days = ((high - up) / step + 1)
k = days % 1
k = (k + 2) // (k + 1)
k = k % 2
print(int(days // 1 + k))
