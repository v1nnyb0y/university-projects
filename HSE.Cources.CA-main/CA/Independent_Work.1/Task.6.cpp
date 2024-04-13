#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#define seven_score
#undef  eight_score
#undef  nine_score
#undef  ten_score

void task_6() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	static string digit_1;			//Первое число
	static string digit_2;			//Второе число

	//Input
	printf("Input first digit > ");
	cin.ignore(100, '\n');
	getline(cin, digit_1);

	printf("Input second digit > ");
	getline(cin, digit_2);

	static int size_first;
	size_first= digit_1.size() - 1;
	static int size_second;
	size_second= digit_2.size() - 1;
	static int result;
	result = 0;
	static int is_neg;
	is_neg = 0;

#ifdef seven_score

	//Начало ассемблерной вставки
	__asm {
		lea edi, digit_1
		add edi, 4

		lea esi, digit_2
		add esi, 4

		mov eax, size_first
		mov ebx, size_second

		cmp [edi], 45
		je NEG_FIRST

		cmp [esi], 45
		je FIRST
		jmp EQ_SIGN

	NEG_FIRST:
		cmp [esi], 45
		jne SECOND

		mov is_neg, 1
		jmp EQ_SIGN

	EQ_SIGN:
		cmp eax, ebx
		jnle FIRST

		cmp ebx, eax
		jnle SECOND

		xor eax, eax
		xor ebx, ebx
		xor ecx, ecx
		xor edx, edx

	COMPARISON:
		mov al, [edi]
		sub eax, 48

		mov dl, [esi]
		sub edx, 48

		cmp eax, edx
		jnle FIRST

		cmp edx, eax
		jnle SECOND

		xor eax, eax
		xor edx, edx

		inc edi
		inc esi
		inc ecx

		cmp ecx, size_first
		jnz COMPARISON

		jmp END


	FIRST:
		mov result, 1
		jmp ON_END

	SECOND:
		mov result, -1
		jmp ON_END

	ON_END:
		cmp is_neg, 1
		je CHANGE
		jmp END
	CHANGE:
		cmp result, 1
		je ON_MINUS

		mov result, 1
		jmp END
	ON_MINUS:
		mov result, -1
	END:
	}

#endif

	printf(result == 1 ? "Compare > +%d" : "Digit > %d", result);
	printf("\n");
	result = 0;
	system("pause");
}