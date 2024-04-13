'''
Контрольная по ударениям
'''
n = int(input())
dct = {}
counter = {}
for _ in range(n):
    word = input()
    low_word = str.lower(word)
    counter[low_word] = counter.get(low_word, 0) + 1
    dct[word] = low_word
text = input().split()
count = 0
for word in text:
    low_word = str.lower(word)
    nin_dict = counter.get(low_word, -1) == -1
    if (nin_dict and sum(map(str.isupper, word)) == 1):
        count += 1
    elif (not nin_dict and dct.get(word, '0') == low_word):
        count += 1

print(len(text) - count)
