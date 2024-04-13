'''
Количество слов в тексте
'''
import sys
text = sys.stdin.read()
print(len(set(text.split())))
