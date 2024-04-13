#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "task_10_header.h"

using namespace std;

void task_10() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	int task;
	printf("Input under-task number (3 for exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 1: task_10_1(); break;
	case 2: task_10_2(); break;
	case 3: return;
	default: return;
	}

}

void task_10_1() {
	static int M, N, result;
	result = -1;


	printf("Insert M > ");
	scanf("%d", &M);

	printf("Insert N > ");
	scanf("%d", &N);

	if (N<0) {
		printf("Wrong N\n");
		return;
	}

	__asm {
		mov eax, M
		mov ecx, N

		cmp N, 0
		je END

	SHIFT:
		sal eax, 1
		dec ecx

		cmp ecx, 0
		jne SHIFT

	END:

		mov result, eax
	}

	printf("Answer > %d\n", result);
	system("pause");
}

void task_10_2() {
	static int M, K, result, is_not;
	result = -1;
	is_not = -1;

	printf("Insert M > ");
	scanf("%d", &M);

	printf("Insert K > ");
	scanf("%d", &K);

	if (K<=0) {
		printf("Wrong K\n");
		return;
	}

	__asm {
		mov eax, M
		xor ecx, ecx

		cmp K, 1
		je END

		mov edx, 1

	CHECK_ON_POWER_TWO:
		sal edx, 1
		inc ecx

		cmp edx, K
		jnle NOT_EQUAL
		jne CHECK_ON_POWER_TWO

	SHIFT :
		mov edx, eax
		shr edx, 1
		sub eax, edx

		dec ecx

		cmp ecx, 0
		jne SHIFT

		jmp END

	NOT_EQUAL:
		mov is_not, 1

	END: 
		mov result, eax
	}

	is_not == -1 ? printf("Answer > %d\n", result) : printf("K=%d is not a power of 2\n", K);
	system("pause");
}