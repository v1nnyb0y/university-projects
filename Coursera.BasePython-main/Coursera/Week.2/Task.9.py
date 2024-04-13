'''
Cows
'''

cows = int(input())

if cows >= 11 and cows <= 14:
        print(cows, 'korov')
else:
        temp = cows % 10
        if temp == 0 or (temp >= 5 and temp <= 9):
                print(cows, 'korov')
        if temp == 1:
                print(cows, 'korova')
        if temp >= 2 and temp <= 4:
                print(cows, 'korovy')
