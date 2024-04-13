'''
Добавить, умножить
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

    def __add__(self, add_matr):
        return Matrix(list(map(
                        lambda x, y: list(map(lambda z, w: z + w, x, y)),
                        self.matr, add_matr.matr)))

    def __mul__(self, mul_matr):
        return Matrix([[i * mul_matr for i in list] for list in self.matr])

    __rmul__ = __mul__

exec(stdin.read())
