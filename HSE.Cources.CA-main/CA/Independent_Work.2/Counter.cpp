#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

void count_e() {
	static double X, E, result;
	printf("Insert epsilon > ");
	scanf("%lf", &E);

	if (E <= 0) {
		printf("Wrong epsilon\n");
		return;
	}

	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//Инициализировать FPU
		xor ecx, ecx			//Обнулить номер

	COUNTER:
		sahf					//Загрузить флаги
		inc ecx					//Переход к след. номеру
		push ecx				//Добавить в стэк текущий N

		fld X					//Добавить число в st(0)

		fimul dword ptr[esp]	//Умножить st(0) = st(0) * src
		fcos					//Вычисление st(0) = cos(st(0))

		fidiv dword ptr[esp]	//Умножить st(0) = st(0)/src

		fld st(0)				//Дублируем посчитанное число на N шаге

		fadd result				//Сумма st(0) = st(0) + result
		fstp result				//Выгрузить st(0) в result

		fabs					//Берем число по модулю для сравнения точности
		fstsw ax				//Сохранить флаги

		pop ecx					//Выгружаем ecx
		fcomp E					//Сравниваем st(0) и Е
		jae COUNTER
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}

void count_n() {
	static double X, result;
	static int N;
	result = 0;
	printf("Insert N > ");
	scanf("%d", &N);

	if (N <= 0) {
		printf("Wrong N\n");
		return;
	}

	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//Инициализировать FPU
		mov ecx, N				//Вычисление N члена (энумератор)

	COUNTER:
		push ecx				//Добавить в стэк текущий N

		fld  X					//Добавить число в st(0)
		
		fimul dword ptr [esp]	//Умножить st(0) = st(0) * src
		fcos					//Вычисление st(0) = cos(st(0))

		fidiv dword ptr [esp]	//Умножить st(0) = st(0)/src

		fadd result				//Сумма st(0) = st(0) + result
		fstp result				//Выгрузить st(0) в result

		pop ecx
		loop COUNTER
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}

void rec() {
	static double X, result;
	result = 0;
	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//Инициализировать FPU
			
		mov ecx, 2				//Добавить константу
		push ecx				//Добавить ее в стэк

		FLD1					//Вставить на место st(0) 1.0
		fld  X					//Добавить число в st(0)

		fidiv dword ptr[esp]	//Деление st(0) = st(0)/2
		fsin					//Вычисление st(0) = sin(st(0))
		fimul dword ptr[esp]	//Произведение st(0) = 2*st(0)
		fabs					//Поменять знак на положительный, если отрицательное в st(0)

		fyl2x					//Вычислить логарифм по основанию 2 от st(1) и умножить на st(0)
		fldln2					//Добавить натуральный логарифм в st(0)
		fmul					//Умножить st(1) на st(0)
			
		fchs					//Изменить знак st(0)

		pop ecx					//Очистить стэк
		fstp result				//вернуть результат
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}