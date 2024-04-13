'''
Номер появления слова
'''
with open('input.txt', 'r', encoding='utf8') as in_file:
    text = in_file.read()
counter = {}
for word in text.split():
    counter[word] = counter.get(word, 0) + 1
    print(counter[word] - 1, end=' ')
print('')
