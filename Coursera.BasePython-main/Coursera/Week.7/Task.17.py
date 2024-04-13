'''
Самое частое слово
'''
import sys
text = sys.stdin.read().split()
words = {}
for word in text:
    words[word] = words.get(word, 0) + 1
max_count = float('-inf')
max_word = ''
for word, count in sorted(words.items()):
    if (count > max_count):
        max_count = count
        max_word = word
print(max_word)
