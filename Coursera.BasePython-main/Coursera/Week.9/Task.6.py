'''
Наследование
'''
from sys import stdin
from copy import deepcopy


class MatrixError(Exception):
    def __init__(self, mistake):
        self.m = mistake


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

    def solve(self, free):
        eps = 0.00001
        matrix = self.matr
        n = len(matrix)
        x = [0] * n
        k = 0
        max = -1
        while k < n:
            max = abs(matrix[k][k])
            id = k
            for i in range(k + 1, n):
                if (abs(matrix[i][k]) > max):
                    max = matrix[i][k]
                    id = i
            if (max < eps):
                raise MatrixError(0)
            for j in range(n):
                (matrix[k][j], matrix[id][j]) = (matrix[id][j], matrix[k][j])
            (free[k], free[id]) = (free[id], free[k])
            for i in range(k, n):
                temp = matrix[i][k]
                if (abs(temp) < eps):
                    continue
                for j in range(n):
                    matrix[i][j] /= temp
                free[i] /= temp
                if (i == k):
                    continue
                for j in range(n):
                    matrix[i][j] -= matrix[k][j]
                free[i] -= free[k]
            k += 1
        for k in range(n - 1, -1, -1):
            x[k] = free[k]
            for i in range(k):
                free[i] -= matrix[i][k] * x[k]
        return x


class SquareMatrix(Matrix):
    def __pow__(self, power):
        if (power == 0):
            return self
        elif (power == 1):
            return self
        elif (power % 2 == 0):
            return SquareMatrix((self * self).matr) ** (power / 2)
        else:
            return self * self ** (power - 1)

exec(stdin.read())
