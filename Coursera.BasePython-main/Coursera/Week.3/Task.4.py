'''
Цена товара
'''
import math

N = float(input())
print(int(N), int(round(N - int(N), 2) * 100), sep=' ')
