#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#define seven_score
#undef  eight_score
#undef  nine_score
#undef  ten_score

void task_11() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	static string digit_1;			//Первое число
	static string digit_2;			//Второе число

	//Input
	printf("Input first digit > ");
	cin.ignore(100, '\n');
	getline(cin, digit_1);

	printf("Input second digit > ");
	getline(cin, digit_2);

	if (digit_1.size() < digit_2.size()) {
		swap(digit_1, digit_2);
	}

	static int size_first;
	size_first = digit_1.size();
	static int size_second;
	size_second = digit_2.size();
	static string result;
	static string symbol;

#ifdef seven_score

	//Начало ассемблерной вставки
	__asm {
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

		SUM :
		xor edx, edx
			cmp ecx, size_second
			jnl IS_SECOND_DIGIT_END

			//Чтение цифры из второго числа
			mov dl, [esi]			//читаем символ
			sub dl, 48				//Вычисляем цифру

			IS_SECOND_DIGIT_END:
		//Чтение цифры из первого числа
		mov al, [edi]
			sub al, 48
			add eax, edx
			add eax, ebx
			xor ebx, ebx

			cmp eax, 10
			jnge ADD_DIGIT

			mov ebx, 1
			sub eax, 10

			ADD_DIGIT:
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
			cmp ecx, size_first
			jnz SUM


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

	printf("Digit > %s", result.c_str());
	result = "";
	symbol = "";
	printf("\n");
	system("pause");
}