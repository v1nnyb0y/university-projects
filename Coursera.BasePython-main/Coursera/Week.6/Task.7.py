'''
Средний балл по классам
'''


def midScore():
    sum_nine = 0
    c_nine = 0
    sum_ten = 0
    c_ten = 0
    sum_eleven = 0
    c_eleven = 0
    with open('input.txt', 'r', encoding='utf-8') as file:
        for line in file:
            arr = line.split()
            score = int(arr[3])
            sClass = int(arr[2])
            if (sClass == 9):
                sum_nine += score
                c_nine += 1
            elif (sClass == 10):
                sum_ten += score
                c_ten += 1
            elif (sClass == 11):
                sum_eleven += score
                c_eleven += 1
    return (sum_nine / c_nine, sum_ten / c_ten, sum_eleven / c_eleven)

print(*midScore())
