'''
Частотный анализ
'''
import sys
text = sys.stdin.read().split()
words = {}
for word in text:
    words[word] = words.get(word, 0) + 1
temp = []
for word, count in words.items():
    temp.append((count, word))
temp.sort(key=lambda x: (-x[0], x[1]))
for t in temp:
    print(t[1])
