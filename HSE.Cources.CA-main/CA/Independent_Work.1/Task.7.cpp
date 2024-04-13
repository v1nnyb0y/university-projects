#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "task_7_header.h"
#include <experimental/filesystem>

using namespace std;

#define seven_score
#undef  eight_score
#undef  nine_score
#undef  ten_score

void task_7() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	int task;
	printf("Input under-task number (9 for exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 1: task_7_1(); break;
	case 2: task_7_2(); break;
	case 3: task_7_3(); break;
	case 4: task_7_4(); break;
	case 5: task_7_5(); break;
	case 6: task_7_6(); break;
	case 7: task_7_7(); break;
	case 8: task_7_8(); break;
	case 9: return;
	default: return;
	}
}

void task_7_1() {
	static int inp_digit;
	printf("Insert number, which should fill the table > ");
	scanf("%d", &inp_digit);

	static int outp_array[5];

	//Начало ассемблерной вставки
	__asm {
		cld
		lea edi, outp_array
		mov eax, inp_digit
		mov ecx, 5

	rep stosd
	}

	printf("Array > ");
	for(int i : outp_array) {
		printf("%d ", i);
	}
	printf("\n");
	system("pause");
}

void task_7_2() {
	static const char* first_arr;
	static string temp;


	int size_second;

	//Input
	printf("Input first array (size = 5) > ");
	cin.ignore(100, '\n');
	getline(cin, temp);
	temp += " ";
	first_arr = temp.c_str();

	printf("Insert size of the second array > ");
	scanf("%d", &size_second);

	size_second /= 5;

	static int size_first;
	size_first = temp.size();
	static char* second_arr;
	second_arr = new char[size_first*size_second];

	//Начало ассемблерной вставки
	__asm {
		mov ebx, size_second
		cld
		mov edi, second_arr

	COPY:
		mov esi, first_arr
		mov ecx, size_first
	rep movsb
		dec ebx
		cmp ebx, 0
		jne COPY

		mov byte ptr [edi], 0
	}


	printf("Array > %s", second_arr);
	second_arr = nullptr;
	first_arr = nullptr;
	printf("\n");
	system("pause");
}

void task_7_3() {
	printf("Insert digit > ");
	static int inp_digit;
	scanf("%d", &inp_digit);
	printf("\n");

	static int inp_array[5];
	printf("Insert array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &inp_array[i]);
	}

	printf("\n");

	static int index;
	index= -1;
	static int value;
	value= 0;

	__asm {
		lea esi, inp_array
		lea edi, inp_digit
		xor ecx, ecx
		xor eax, eax
		cld

	FIND_SAME_VALUE:
		cmpsd
		je EXIT_WITH_VALUE

		sub edi, 4
		inc ecx
		cmp ecx, 5
		jnz FIND_SAME_VALUE

		jmp END

	EXIT_WITH_VALUE:
		sub esi, 4
		lodsd
		mov index, ecx
		mov value, eax
		jmp END

	END:
	}

	index == -1 ? printf("Index = %d", index) : printf("[%d]=%d", index+1, value);
	printf("\n");
	system("pause");
}

void task_7_4() {
	printf("Insert digit > ");
	static int inp_digit;
	scanf("%d", &inp_digit);
	printf("\n");

	static int inp_array[5];
	printf("Insert array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &inp_array[i]);
	}

	printf("\n");

	static int index = -1;
	static int _value = 0;

	__asm {
		mov eax, inp_digit

		lea edi, inp_array
		add edi, 20

		mov ecx, 5

		std
	repne scasd

		cmp ecx, 0
		je ZERO

		add ecx, 1
		jmp IS_FIND

	ZERO:
		cmp [edi], eax
		jne END

	IS_FIND:
		mov index, ecx
		mov _value, eax

	END :
		cld
	}

	
	index == -1 ? printf("Index = %d", index) : printf("[%d]=%d", index + 1, _value);
	index = -1;
	_value = 0;
	printf("\n");
	system("pause");
}

void task_7_5() {
	string first_array;
	printf("Insert first array > ");
	cin.ignore(100, '\n');
	getline(cin, first_array);
	

	string second_array;
	printf("Insert second array > ");
	getline(cin, second_array);

	printf("\n");

	static const char* arr_1;
	static const char* arr_2;
	arr_1 = first_array.c_str();
	arr_2 = second_array.c_str();

	static int compare = -1;
	static int size_arr;
	size_arr = first_array.size();

	__asm {
		mov esi, arr_1
		mov edi, arr_2

		mov ecx, size_arr
		
	repe cmpsb
		je EQUAL

		jmp END

	EQUAL:
		mov compare, 1

	END:
	}

	compare == -1 ? printf("NO") : printf("YES");
	compare = -1;
	printf("\n");
	system("pause");
}

void task_7_6() {
	static int first_array[5];
	printf("Insert first array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &first_array[i]);
	}

	static int second_array[5];
	printf("Insert second array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &second_array[i]);
	}

	printf("\n");

	static int index = -1;
	static int value = 0;

	__asm {
		lea esi, first_array
		lea edi, second_array
		xor ecx, ecx
		xor eax, eax
		cld

	FIND_SAME_VALUE :
		cmpsd
		je EXIT_WITH_VALUE

		inc ecx
		cmp ecx, 5
		
		jnz FIND_SAME_VALUE

		jmp END

	EXIT_WITH_VALUE :
		sub esi, 4
		lodsd
		mov index, ecx
		mov value, eax
		jmp END

	END :
	}

	index == -1 ? printf("Index = %d", index) : printf("[%d]=%d", index + 1, value);
	index = -1;
	value = 0;
	printf("\n");
	system("pause");
}

void task_7_7() {
	static int first_array[5];
	printf("Insert first array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &first_array[i]);
	}

	static int second_array[5];
	printf("Insert second array > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &second_array[i]);
	}

	printf("\n");

	static int index = -1;
	static int value = 0;

	__asm {
		lea esi, first_array
		add esi, 20

		lea edi, second_array
		add edi, 20

		mov ecx, 5
		xor eax, eax
		std

	FIND_SAME_VALUE :
		cmpsd
		je EXIT_WITH_VALUE

		dec ecx
		cmp ecx, -1
		jnz FIND_SAME_VALUE

		jmp END

	EXIT_WITH_VALUE :
		add esi, 4
		lodsd
		mov index, ecx
		mov value, eax
		jmp END

	END :
		cld
	}

	index == -1 ? printf("Index = %d", index) : printf("[%d]=%d", index + 1, value);
	index = -1;
	value = 0;
	printf("\n");
	system("pause");
}

void task_7_8() {
	static int inp_array[15];
	printf("Insert array (size = 15) > \n");
	for (int i = 0; i < 15; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &inp_array[i]);
	}

	static int search_array[5]; 
	printf("Insert array for search (size = 5) > \n");
	for (int i = 0; i < 5; ++i) {
		printf("[%d]=", i + 1);
		scanf("%d", &search_array[i]);
	}

	printf("\n");

	static int index;

	index = -1;

	__asm {
		xor ebx, ebx
		

	FIND:
		lea edi, inp_array

		mov eax, ebx
		imul eax, 4
		add edi, eax

		lea esi, search_array
		mov ecx, 5

	repe cmpsd
		je EQUAL

		inc ebx
		cmp ebx, 11
		jne FIND

		jmp END


	EQUAL:
		add ebx, ecx
		mov index, ebx

	END:
	}

	index == -1 ? printf("Index = %d", index) : printf("Start index = [%d]", index + 1);
	printf("\n");
	system("pause");
}