'''
Ставки
'''
from itertools import permutations
from _operator import xor
from sys import stdin
print(
            *next(
                  *map(
                        lambda data: filter(
                                            lambda var: all(
                                                             map(
                                                                 lambda stavka: xor(
                                                                                    var.index(stavka[0]) < var.index(stavka[1]),
                                                                                    var.index(stavka[2]) < var.index(stavka[3])
                                                                                   ),
                                                                 data[1]
                                                                )
                                                           ),
                                            data[0]
                                           ),
                        map(
                            lambda x: (
                                        permutations(
                                                     range(1, x[0] + 1)
                                                    ),
                                        x[1]
                                      ),
                            map(
                                lambda inp: (
                                              next(inp),
                                              tuple(
                                                     set(
                                                         map(
                                                             lambda x: tuple(
                                                                              map(
                                                                                  int,
                                                                                  stdin.readline().split()
                                                                                 )
                                                                            ),
                                                             range(next(inp))
                                                            )
                                                        )
                                                   )
                                            ),
                                [map(
                                     int,
                                     stdin.readline().split()
                                    )]
                               )
                           )
                        ),
                   [0]
                  )
     )
