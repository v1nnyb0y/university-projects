'''
XOR
'''
print(*map(lambda x: int(x[0] != x[1]), zip(map(int, input().split()), map(int, input().split()))))
