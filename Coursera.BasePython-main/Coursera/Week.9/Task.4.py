'''
Умножение
'''
from sys import stdin
from copy import deepcopy


class MatrixError(BaseException):
    def __init__(self, matrix_1, matrix_2):
        self.matrix1 = matrix_1
        self.matrix2 = matrix_2


class Matrix:
    def __init__(self, a):
        self.matr = deepcopy(a)

    def __str__(self):
        return '\n'.join(['\t'.join(map(str, list)) for list in self.matr])

    def size(self):
        return (len(self.matr), len(self.matr[0]))

    def __add__(self, add_matr):
        if len(self.matr) == len(add_matr.matr):
            lenght = len(self.matr[0])
            for row in self.matr:
                if len(row) != lenght:
                    raise MatrixError(self, add_matr)
            for row2 in add_matr.matr:
                if len(row2) != lenght:
                    raise MatrixError(self, add_matr)
            return Matrix(list(map(
                        lambda x, y: list(map(lambda z, w: z + w, x, y)),
                        self.matr, add_matr.matr)))
        else:
            raise MatrixError(self, add_matr)

    def __mul__(self, other):
        if (isinstance(other, int) or isinstance(other, float)):
            return Matrix([[i * other for i in list] for list in self.matr])
        elif (isinstance(other, Matrix)):
            if (len(self.matr[0]) == len(other.matr)):
                trans = Matrix.transposed(other)
                res = [[sum(elA * elB for elA, elB in zip(row, col))
                       for col in trans.matr] for row in self.matr]
                return Matrix(res)
            else:
                raise MatrixError(self, other)
        else:
            raise MatrixError(self, other)

    def transpose(self):
        self.matr = list(zip(*self.matr))
        return Matrix(self.matr)

    @staticmethod
    def transposed(matrix):
        return Matrix(list(zip(*matrix.matr)))

    __rmul__ = __mul__

exec(stdin.read())
