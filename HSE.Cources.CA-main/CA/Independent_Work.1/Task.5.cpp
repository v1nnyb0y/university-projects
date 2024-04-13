#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#define seven_score
#undef  eight_score
#undef  nine_score
#undef  ten_score

void task_5() {
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
	size_first = digit_1.size() - 1;
	static int size_second;
	size_second = digit_2.size() - 1;

	static string result;
	static string symbol;
	result.clear();
	symbol.clear();

	static int is_neg;
	is_neg = 0;

#ifdef seven_score

	//Начало ассемблерной вставки
	__asm {
		xor dx, dx
		CLC
		pushf
		mov ecx, 0

		lea edi, digit_1
		add edi, 4

		lea esi, digit_2
		add esi, 4

		cmp[esi], 45
		je LOW_NEG

		cmp[edi], 45
		jne ON_COMPARE

		mov is_neg, 1
		jmp ON_SUM

	LOW_NEG :
		cmp[edi], 45
		je ON_COMPARE

		//Real sum
	ON_SUM:
		mov eax, size_first

		cmp eax, size_second
		jg NO_EXCHANGE

		mov ecx, size_second

		lea edi, digit_2
		add edi, 4
		add edi, size_second

		lea esi, digit_1
		add esi, 4
		add esi, size_first

		mov size_first, ecx 
		mov size_second, eax

		xor ecx, ecx
		xor eax, eax

		jmp SUM

	NO_EXCHANGE :
		lea edi, digit_1
		add edi, 4
		add edi, size_first

		lea esi, digit_2
		add esi, 4
		add esi, size_second

		xor eax, eax

		jmp SUM

#ifdef seven_score

	SUM :
		popf
		pushf
		mov al, [edi]
		sub al, 48
		cmp ecx, size_second
		jl SUM_BOTH

		popf
		adc al, 0
		aaa
		pushf
		jmp SKIP_SECOND_SUM

	SUM_BOTH :
		mov bl, [esi]
		sub bl, 48

		popf
		adc al, bl
		aaa
		pushf

	SKIP_SECOND_SUM :

		add al, 48
		push edi
		push esi
		push ecx

		mov ecx, 1

		lea edi, result
		add edi, 3

	LENGHT_SUM:
		inc edi
		cmp[edi], 0
		jne LENGHT_SUM

		lea esi, symbol
		add esi, 4
		mov[esi], al

		movs symbol, symbol //Заглушка для избежания ошибки (так как используется функция, а я так и не смог сделать это грамотнее)
		movsb

		pop ecx
		pop esi
		pop edi

		xor al, al
		dec edi
		dec esi
		inc ecx

		cmp dx, 0
		jne CONTINUE

		cmp ecx, size_first
		jnz SUM

		xor dx, dx
		mov al, 1
		pop dx
		pushf
		test dx, 1
		jnz SKIP_SECOND_SUM

		jmp CONTINUE

#endif

			//Compare module
		ON_COMPARE:
		push edi
			push esi

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
			add edi, 5

			lea esi, digit_2
			add esi, 5

#ifdef  seven_score

			COMPARISON :
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

			pop edi
			jmp ZERO

			FIRST :
		pop esi
			pop edi

			cmp [edi], 45
			je NEGATIVE

			jmp ON_DIFF

	SECOND :
		pop edi
		pop esi

		mov eax, size_first
		mov ecx, size_second
		mov size_first, ecx
		mov size_second, eax

		xor ecx, ecx
		xor eax, eax

			cmp [edi], 45
			jne NEGATIVE

			jmp ON_DIFF

			NEGATIVE :
		mov is_neg, 1

#endif

			//Real diff
		ON_DIFF:
		add edi, size_first
			add esi, size_second

#ifdef seven_score

			DIFF :
		popf
			pushf
			mov al, [edi]
			sub al, 48
			cmp ecx, size_second
			jl DIFF_BOTH

			popf
			sbb al, 0
			aas
			pushf


			jmp SKIP_SECOND_DIFF

			ON_CONTINUE :
		pop ecx
			jmp CONTINUE

			DIFF_BOTH :
		mov bl, [esi]
			sub bl, 48

			popf
			sbb al, bl
			aas
			pushf

			SKIP_SECOND_DIFF :

		add al, 48
			push edi
			push esi
			push ecx

			mov ecx, 1

			lea edi, result
			add edi, 3

			LENGHT_DIFF:
		inc edi
			cmp[edi], 0
			jne LENGHT_DIFF

			lea esi, symbol
			add esi, 4
			mov[esi], al

			movs symbol, symbol //Заглушка для избежания ошибки (так как используется функция, а я так и не смог сделать это грамотнее)
			movsb

			pop ecx
			pop esi
			pop edi

			xor al, al
			dec edi
			dec esi
			inc ecx

			cmp ecx, size_first
			jnz DIFF

			jmp CONTINUE

#endif

		CONTINUE:
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

			ZERO :
		lea edi, result
			add edi, 4

			mov[edi], byte ptr 48

			END :
			pop edi
			popf
	}

#endif

	printf((is_neg == 1 && result[0] != 0) ? "Digit > -%s" : "Digit > %s", result.c_str());
	printf("\n");
	system("pause");
}