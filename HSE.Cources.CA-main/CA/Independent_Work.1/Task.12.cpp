#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#define seven_score
#undef  eight_score
#undef  nine_score
#undef  ten_score

void task_12() {
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
	size_first = digit_1.size();
	static int size_second;
	size_second = digit_2.size();
	static string result;
	static string symbol;

#ifdef seven_score

	//Начало ассемблерной вставки
	__asm {
		mov eax, size_first
		mov ebx, size_second

		cmp eax, ebx
		jnle FIRST

		cmp ebx, eax
		jnle SECOND

		xor eax, eax
		xor ebx, ebx
		xor ecx, ecx
		xor edx, edx

		lea edi, digit_1
		add edi, 4

		lea esi, digit_2
		add esi, 4

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

			jmp EQUAL


			FIRST :
		mov result, 1

			lea edi, digit_1
			add edi, 4
			add edi, size_first
			dec edi

			lea esi, digit_2
			add esi, 4
			add esi, size_second
			dec esi

			xor ecx, ecx
			xor ebx, ebx
			xor eax, eax

			jmp DIFF

			SECOND :
		mov result, -1

			lea edi, digit_2
			add edi, 4
			add edi, size_second
			dec edi

			lea esi, digit_1
			add esi, 4
			add esi, size_first
			dec esi

			xor ecx, ecx
			xor ebx, ebx
			xor eax, eax

			jmp DIFF

			EQUAL :
		lea edi, digit_1
			add edi, 4
			add edi, size_first
			dec edi

			lea esi, digit_2
			add esi, 4
			add esi, size_second
			dec esi

			xor ecx, ecx
			xor ebx, ebx
			xor eax, eax

			jmp DIFF


			DIFF :
		xor edx, edx
			cmp ecx, size_second
			jnl IS_SECOND_DIGIT_END

			mov dl, [esi]
			sub dl, 48

			IS_SECOND_DIGIT_END:
		mov al, [edi]
			sub al, 48

			add edx, ebx

			cmp eax, edx
			jnl STAY_ZERO

			cmp edx, eax
			jnle ONE_MORE

			STAY_ZERO :
		xor ebx, ebx
			sub eax, edx
			jmp ADD_DIGIT

			ONE_MORE :
		mov ebx, 1
			add eax, 10
			sub eax, edx
			jmp ADD_DIGIT

			ADD_DIGIT :
		add eax, 48
			push edi
			push esi
			push ecx

			mov ecx, 1

			lea edi, result
			add edi, 3

			LENGHT:
		inc edi
			cmp[edi], 0
			jne LENGHT

			lea esi, symbol
			add esi, 4
			mov[esi], eax

			movs symbol, symbol //Заглушка для избежания ошибки (так как используется функция, а я так и не смог сделать это грамотнее)
			rep movsb

			pop ecx
			pop esi
			pop edi

			xor eax, eax
			dec edi
			dec esi
			inc ecx

			mov eax, size_first

			cmp eax, size_second
			jnle FIRST_COMPARE

			cmp ecx, size_second
			jnz DIFF

			FIRST_COMPARE :
		cmp ecx, size_first
			jnz DIFF


			lea edi, result
			add edi, 4

			lea esi, result
			add esi, 3

			xor eax, eax

			LENGHT_ESI :
		inc esi
			inc eax
			cmp[esi], 0
			jne LENGHT_ESI

			dec esi
			push edi
			cmp eax, 2
			je END

			REVERSE :
		pop edi
			mov dl, [edi]
			mov cl, [esi]
			mov[edi], cl
			mov[esi], dl

			dec esi
			inc edi

			push edi
			sub edi, esi

			cmp edi, 0
			je END

			cmp edi, 1
			je END

			jmp REVERSE

			END :
		pop edi
	}

#endif

	auto is_zero_accept = false;
	printf("Digit > ");
	const char* arr = result.c_str();
	auto i = 0;
	while (arr[i] != '\0') {
		if (arr[i] == '0') {
			if (is_zero_accept) {
				printf("%c", arr[i]);
			}
		}
		else {
			printf("%c", arr[i]);
			is_zero_accept = true;
		}
		i++;
	}
	result = "";
	symbol = "";
	printf("\n");
	system("pause");
}