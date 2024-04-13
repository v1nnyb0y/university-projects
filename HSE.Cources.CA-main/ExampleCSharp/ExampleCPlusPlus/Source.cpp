#define _CRT_SECURE_NO_WARNINGS
#include "windows.h"
#include <iostream>


using namespace std;

void example_short(short digit) {
	short some_digit = 0;

	some_digit = 23;
	//Check out of type
	try {
		some_digit = digit;
	}
	catch (int exception) {
		printf("%d - ошибка\n", exception);
	}


	//Check divided by zero
	try {
		short zero = 0;
		if (zero == 0) {
			throw 0;
		}
		some_digit /= zero;
		printf("%d - пример использования short = 5/0\n", some_digit);
	}
	catch (int exception)
	{
		printf("%d - ошибка\n", exception);
	}

	int a = 10;
	//Exception: some_digit = a;
	some_digit = (short)a;

		for (int i = 0; i < 400000; i++)
			some_digit++;
		printf("%d - пример использования переполнения\n", some_digit);
}

int main() {
	int sw;
	scanf("%d", &sw);
	switch (sw)
	{
	case 1:
		example_short(12);
		break;
	}
	system("pause");
	return 1;
}