'''
Замечательные числа - 1
'''
for i in range(10, 100):
    string = str(i)
    power = int(string[0]) * int(string[1]) * 2
    if (power == i):
        print(i)
