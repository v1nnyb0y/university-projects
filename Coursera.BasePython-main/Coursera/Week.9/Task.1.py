'''
Класс
'''
from sys import stdin
from copy import deepcopy


class Matrix:
    def __init__(self, a):
        self.matr = deepcopy(a)

    def __str__(self):
        return '\n'.join(['\t'.join(map(str, list)) for list in self.matr])

    def size(self):
        return (len(self.matr), len(self.matr[0]))

exec(stdin.read())
