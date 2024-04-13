#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "Comparison.h"

using namespace std;

static int result;
static long long A[10];
static long long B[10];

void compare() {
	printf("Insert first array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cin >> A[i];
	}

	printf("\nInsert second array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cin >> B[i];
	}

	int task;
	printf("Insert check 1:(First == Second) or 2:(First > Second) or 3:(First < Second) or 4:exit > ");
	scanf("%d", &task);
	switch (task) {
	case 1: equal(); break;
	case 2: bigger(1); break;
	case 3: bigger(2); break;
	case 4: return;
	default: return;
	}
}

void equal() {
	result = -1;

	__asm {
		xor ecx, ecx								//Обнулить индекс
		lea esi, A									//Указать ссылку на массив А
		lea edi, B									//Указать ссылку на массив В

	EQUAL:
		movq mm0, qword ptr [esi+8*ecx]				//Положить в переменную MMX элемент массива А
		pcmpeqd mm0, qword ptr [edi+8*ecx]			//Сравнить элемент массива В с ММХ (на равенство)

		movd eax, mm0								//Вернуть результат сравнения
		cmp eax, 0									//Если ноль, то различные элементы
		je END

		inc ecx										//Увеличить индекс
		cmp ecx, 10									//Условие выхода
		jne EQUAL

		mov result, 1								//Если массивы равны
	END:
	}

	result == 1 ? printf("TRUE\n") : printf("FALSE\n");

	system("pause");
}

void bigger(int number) {
	result = -1;
	switch (number) {
	case 1: {
		__asm {
			xor ecx, ecx							//Обнулить индекс
			lea esi, A								//Указать ссылку на массив А
			lea edi, B								//Указать ссылку на массив В

		Bigger_1 :
			movq mm0, qword ptr[esi + 8 * ecx]		//Положить в переменную ММХ элемент массива А
			pcmpgtd  mm0, qword ptr[edi + 8 * ecx]	//Сравнить элемент массива В с ММХ

			movd eax, mm0							//Вернуть результат сравнения
			cmp eax, 0								//Если число в массиве В оказалось больше
			je END_1

			inc ecx									//Увеличить индекс
			cmp ecx, 10								//Условие выхода
			jne Bigger_1

			mov result, 1
		END_1:
		}
	}
	case 2: {
		__asm {
			xor ecx, ecx							//Обнулить индекс
			lea esi, A								//Указать ссылку на массив А
			lea edi, B								//Указать ссылку на массив В

		Bigger_2 :
			movq mm0, qword ptr[edi + 8 * ecx]		//Положить в переменную ММХ элемент массива В
			pcmpgtd  mm0, qword ptr[esi + 8 * ecx]	//Сравнить элемент массива А с ММХ

			movd eax, mm0							//Вернуть результат сравнения
			cmp eax, 0								//Если число в массиве А оказалось больше
			je END_2

			inc ecx									//Увеличить индекс
			cmp ecx, 10								//Условие выхода
			jne Bigger_2

			mov result, 1
		END_2:
		}
	}
	}

	result == 1 ? printf("TRUE\n") : printf("FALSE\n");

	system("pause");
}