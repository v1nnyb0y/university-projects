#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

void diff() {
	static long long A[10];
	static long long B[10];
	static int AB_size = 10;

	printf("Insert first array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cin >> A[i];
	}

	printf("\nInsert second array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cin >> B[i];
	}

	int task;
	printf("\nInsert type of diff (0: without saturation; 1: with saturation; 2 for exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 0: {
		__asm {
			lea esi, A									//Указать ссылку на массив А
			lea edi, B									//Указать ссылку на массив В
			xor ecx, ecx								//Обнулить индекс

		SUM :
			movq mm0, qword ptr[esi + 8 * ecx]			//Положить в переменную MMX ММ0 квадро слово по индексу ecx из массива А
			psubq mm0, qword ptr[edi + 8 * ecx]			//Добавить к переменной MMX MM0 квадро слово по индексу ecx из массива В
			movq qword ptr[edi + 8 * ecx], mm0			//Положить переменную ММ0 в массив В по индексу ecx
			inc ecx										//Перейти к след. элементу
			cmp ecx, 10									//Проверка на выход
			jne SUM
		}
		break;
	}
	case 1: {
		__asm {
			lea esi, A									//Указать ссылку на массив А
			lea edi, B									//Указать ссылку на массив В
			xor ecx, ecx								//Обнулить индекс

		SUM_S :
			movq mm0, qword ptr[esi + 8 * ecx]			//Положить в переменную MMX ММ0 квадро слово по индексу ecx из массива А
			psubsw  mm0, qword ptr[edi + 8 * ecx]		//Добавить к переменной MMX MM0 квадро слово по индексу ecx из массива В с насыщением
			movq qword ptr[edi + 8 * ecx], mm0			//Положить переменную ММ0 в массив В по индексу ecx
			inc ecx										//Перейти к след. элементу
			cmp ecx, 10									//Проверка на выход
			jne SUM_S
		}
		break;
	}
	case 2:
		return;
	}



	printf("\nResult array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cout << B[i] << "\n";
	}

	system("pause");
}