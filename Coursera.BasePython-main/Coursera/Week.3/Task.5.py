'''
Округление по российским правилам
'''
import math
N = float(input())
N *= 10
N = math.floor(N) / 10
if (int((N - int(N)) * 10) == 5):
    N = math.ceil(N)
else:
    N = round(N)
print(N)
