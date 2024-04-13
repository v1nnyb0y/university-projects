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

void example_int(int digit) {
	int some_digit = 0;

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

	short a = 10;
	//Exception: some_digit = a;
	some_digit = (int)a;

	for (long i = 0; i < 4000000000000; i++)
		some_digit++;
	printf("%d - пример использования переполнения\n", some_digit);
}

void example_unsigned_short(unsigned short digit) {
	unsigned short some_digit = 0;

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
		unsigned short zero = 0;
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
	some_digit = (unsigned short)a;

	for (int i = 0; i < 400000; i++)
		some_digit++;
	printf("%d - пример использования переполнения\n", some_digit);
}

void example_unsigned_int(unsigned int digit) {
	unsigned int some_digit = 0;

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
		unsigned int zero = 0;
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

	short a = 10;
	//Exception: some_digit = a;
	some_digit = (unsigned int)a;

	for (long i = 0; i < 4000000000000; i++)
		some_digit++;
	printf("%d - пример использования переполнения\n", some_digit);
}

void example_float(float se, float e, float x) {
	float ne = 1;
	float ae = se;

	do
	{
		ae *= (float)(pow(-1, ne) * pow(x, 2) / (2 * ne + 1));
		se += ae;
		ne++;
	} while (ae > e);
	cout << se << " - отличная точность\n";

	float some_digit = x;
	//Check divided by zero
	try
	{
		short zero = 0;
		if (zero == 0) {
			throw 0;
		}
		some_digit /= 0;
		cout << some_digit << " - пример использования float = 5/0\n";
	}
	catch (int exception)
	{
		cout << exception << " - ошибка\n";
	}

	double a = 10.1;
	//Exception: some_digit = a;
	some_digit = (float)a;

	for (long i = 0; i < 4000000000000000000; i++)
		some_digit++;
	cout << some_digit << " - пример использования переполнения\n";
}

void example_double(double se, double e, double x) {
	double ne = 1;
	double ae = se;

	do
	{
		ae *= (pow(-1, ne) * pow(x, 2) / (2 * ne + 1));
		se += ae;
		ne++;
	} while (ae > e);
	cout << se << " - отличная точность\n";

	double some_digit = x;
	//Check divided by zero
	try
	{
		short zero = 0;
		if (zero == 0) {
			throw 0;
		}
		some_digit /= 0;
		cout << some_digit << " - пример использования float = 5/0\n";
	}
	catch (int exception)
	{
		cout << exception << " - ошибка\n";
	}

	float a = (float)10.1;
	//Exception: some_digit = a;
	some_digit = (double)a;

	for (long i = 0; i < 4000000000000000000; i++)
		some_digit++;
	cout << some_digit << " - пример использования переполнения\n";
}

void example_char() {
	char a = 256;
	cout << a;
}

int main() {
	setlocale(LC_ALL, "Russian");

	int sw;
	scanf("%d", &sw);
	switch (sw)
	{
	case 1:
		example_short(12);
		break;
	case 2:
		example_int(15);
		break;
	case 3:
		example_unsigned_short(11);
		break;
	case 4:
		example_unsigned_int(10000);
		break;
	case 5:
		example_char();
		break;
	}
	system("pause");
	return 1;
}