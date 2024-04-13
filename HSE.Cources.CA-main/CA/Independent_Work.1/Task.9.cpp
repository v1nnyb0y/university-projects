#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "task_9_header.h"

using namespace std;

void task_9() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	int task;
	printf("Input under-task number (5 for exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 1: task_9_1(); break;
	case 2: task_9_2(); break;
	case 3: task_9_3(); break;
	case 4: task_9_4(); break;
	case 5: return;
	default: return;
	}
	
}

void task_9_1() {
	static int array_1[5];
	static int array_2[8];
	static int result[13];

	printf("Insert first array (without repeats) > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_1[i]);
	}

	printf("Insert second array (without repeats) > \n");
	for (int i = 0; i < 8; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_2[i]);
	}

	for(auto& i : result) {
		i = NULL;
	}

	printf("\n");

	//Начало ассемблерной вставки
	__asm {
		cld
		lea esi, array_1
		lea edi, result
		mov ecx, 5
		mov ebx, 5

	rep movsd

		lea esi, array_2
		mov ecx, 8

	FIND:
		push edi
		push ecx

		lea edi, result
		mov eax, [esi]

		mov ecx, ebx
	repne scasd

		sub edi, 4
		cmp [edi], eax
		je EQUAL

		pop ecx
		pop edi

		inc ebx
		mov [edi], eax
		add edi, 4

		dec ecx
		add esi, 4

		cmp ecx, 0
		jne FIND

		jmp END

	EQUAL:
		pop ecx
		pop edi

		dec ecx
		add esi, 4

		cmp ecx, 0
		jne FIND

	END:
	}

	printf("Array (+) > ");
	for(int i:result) {
		if (i == NULL) continue;

		printf("%d ", i);
	}
	printf("\n");
	system("pause");
}

void task_9_2() {
	static int array_1[5];
	static int array_2[8];
	static int result[13];

	printf("Insert first array (without repeats) > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_1[i]);
	}

	printf("Insert second array (without repeats) > \n");
	for (int i = 0; i < 8; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_2[i]);
	}

	for (auto& i : result) {
		i = NULL;
	}

	printf("\n");

	//Начало ассемблерной вставки
	__asm {
		cld
		mov ebx, 0

		lea esi, array_2
		mov ecx, 8

	COMPARE:
		lea edi, array_1
		lodsd
		push ecx
		mov ecx, 5
	repne scasd

		sub edi, 4
		cmp [edi], eax
		je EQUAL

		pop ecx
		dec ecx

		cmp ecx, 0
		jne COMPARE

		jmp END

	EQUAL:
		pop ecx
		push esi
		lea esi, result
		add esi, ebx
		mov [esi], eax
		pop esi
		add ebx, 4

		dec ecx
		cmp ecx, 0
		jne COMPARE

	END:
	}

	printf("Array (*) > ");
	for (int i : result) {
		if (i == NULL) continue;

		printf("%d ", i);
	}
	printf("\n");
	system("pause");
}

void task_9_3() {
	static int array_1[5];
	static int array_2[8];
	static int result[13];

	printf("Insert first array (without repeats) > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_1[i]);
	}

	printf("Insert second array (without repeats) > \n");
	for (int i = 0; i < 8; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &array_2[i]);
	}

	for (auto& i : result) {
		i = NULL;
	}

	printf("\n");

	//Начало ассемблерной вставки
	__asm {
		cld
		mov ebx, 0

		lea esi, array_2
		mov ecx, 8

	COMPARE:
		lea edi, array_1
		lodsd
		push ecx
		mov ecx, 5
	repne scasd

		sub edi, 4
		cmp[edi], eax
		je EQUAL
			
		pop ecx
		push esi
		lea esi, result
		add esi, ebx
		mov[esi], eax
		pop esi
		add ebx, 4

		dec ecx
		cmp ecx, 0
		jne COMPARE

		jmp END

	EQUAL :

		pop ecx
		dec ecx

		cmp ecx, 0
		jne COMPARE

	END :
	}

	printf("Array (SECOND-FIRST) > ");
	for (int i : result) {
		if (i == NULL) continue;

		printf("%d ", i);
	}
	printf("\n");
	system("pause");
}

void task_9_4() {
	static int inp_array[8];
	static int N, result;
	result = -1;

	printf("Insert number > ");
	scanf("%d", &N);


	printf("Insert array (without repeats) > \n");
	for (int i = 0; i < 8; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &inp_array[i]);
	}
	printf("\n");

	//Начало ассемблерной вставки
	__asm {
		mov eax, N
		lea edi, inp_array

		mov ecx, 8

		repne scasd

		sub edi, 4
		cmp[edi], eax
		jne END

		mov result, 1

		END:
	}

	result == -1 ? printf("%d IN Array > NO", N)
		: printf("%d IN Array > YES", N);

	printf("\n");
	system("pause");
}