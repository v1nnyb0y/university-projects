'''
Кубики
'''
ani = set()
bor = set()
with open('input.txt', 'r') as file:
    flag = True
    for line in file:
        if (flag):
            splitted = line.split()
            n, m = int(splitted[0]), int(splitted[1])
            flag = False
            isF = True
        elif (n > 0):
            ani.add(int(line))
            n -= 1
        elif (n == 0):
            bor.add(int(line))
u = ani & bor
with open('output.txt', 'w') as file:
    print(len(u), file=file)
    print(*sorted(u), sep=' ', end='\n', file=file)
    print(len(ani) - len(u), file=file)
    print(*sorted(ani - u), sep=' ', end='\n', file=file)
    print(len(bor) - len(u), file=file)
    print(*sorted(bor - u), sep=' ', end='\n', file=file)
