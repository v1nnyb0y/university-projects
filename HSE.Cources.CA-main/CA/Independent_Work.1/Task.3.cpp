#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#undef seven_score
#undef  eight_score
#define  nine_score
#undef  ten_score

void task_3() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	static string inp;			//Введенное число
	static int machine_code[32];
	for (auto& i : machine_code) {
		i = -1;
	}
	static int is_minus = 0;

	//Input inp
	printf("Input machine code to translate > ");
	getline(cin, inp);

	static int inp_size;
	inp_size = inp.size();	//Размер введенной строки
	static int parsed_digit[10];

	for (auto& i : parsed_digit) {
		i = -1;
	}

#ifdef seven_score

//Начало ассемблерной вставки
	__asm {
		lea edi, inp	//Установить ссылку на массив строк
		add edi, 4		//Установить ссылку на начало массива
		xor ecx, ecx	//Обнулить счетчик

		mov dl, [edi]			//читаем символ
		sub dl, 48				//Вычисляем цифру

		//Число положительное или отрицательное
		cmp edx, 0
		je TO_ARRAY

		//Число оказалось отрицательным
		mov is_minus, 1

		//Цикл для записи числа в массив
	TO_ARRAY:
		mov dl, [edi]			//читаем символ
		push edi				//Запоминаем индекс в строке
		sub dl, 48				//Вычисляем цифру
		lea edi, machine_code	//Массив куда записываем инвертированное число

		mov [edi + 4*ecx], edx	//Записываем число

		//Инвертируем число
		cmp [edi + 4*ecx], 1
		je IS_ONE

		mov [edi+4*ecx], 1
		jmp CONTINUE_ARRAY

	IS_ONE:
		mov [edi+4*ecx], 0
	
	CONTINUE_ARRAY:

		inc ecx
		pop edi
		add edi, 1
		
		cmp ecx, inp_size
		jne TO_ARRAY


		lea edi, machine_code
		dec ecx

		//Цикл для добавления битовой единицы
	ADD_ONE_BYTE:
		cmp [edi + 4*ecx], 0
		je TO_DECIMAL

		mov[edi + 4 * ecx], 0
		dec ecx
		cmp ecx, -1
		jne ADD_ONE_BYTE

		mov eax, 0
		jmp END

	TO_DECIMAL:
		mov [edi+4*ecx], 1
		mov ecx, inp_size
		dec ecx
		mov ebx, 1
		mov eax, [edi+4*ecx]

	LOOP_TO_DIGIT:
		imul ebx, 2
		push ebx
		push ecx
		dec ecx
		imul ebx, [edi+4*ecx]
		pop ecx
		add eax, ebx
		pop ebx

		loop LOOP_TO_DIGIT

	END:
		mov digit, eax
	}

#endif

#ifdef eight_score

	//Начало ассемблерной вставки
	__asm {
		lea edi, inp	//Установить ссылку на массив строк
		add edi, 4		//Установить ссылку на начало массива
		xor ecx, ecx	//Обнулить счетчик

		mov dl, [edi]			//читаем символ
		sub dl, 48				//Вычисляем цифру

		//Число положительное или отрицательное
		cmp edx, 0
		je TO_ARRAY

		//Число оказалось отрицательным
		mov is_minus, 1

		//Цикл для записи числа в массив
		TO_ARRAY:
		mov dl, [edi]			//читаем символ
			push edi				//Запоминаем индекс в строке
			sub dl, 48				//Вычисляем цифру
			lea edi, machine_code	//Массив куда записываем инвертированное число

			mov[edi + 4 * ecx], edx	//Записываем число

			//Инвертируем число
			cmp[edi + 4 * ecx], 1
			je IS_ONE

			mov[edi + 4 * ecx], 1
			jmp CONTINUE_ARRAY

			IS_ONE :
		mov[edi + 4 * ecx], 0

			CONTINUE_ARRAY :

			inc ecx
			pop edi
			add edi, 1

			cmp ecx, inp_size
			jne TO_ARRAY


			lea edi, machine_code
			dec ecx

			//Цикл для добавления битовой единицы
			ADD_ONE_BYTE :
		cmp[edi + 4 * ecx], 0
			je TO_DECIMAL

			mov[edi + 4 * ecx], 0
			dec ecx
			cmp ecx, -1
			jne ADD_ONE_BYTE

			mov eax, 0
			jmp END

			TO_DECIMAL :
		mov[edi + 4 * ecx], 1
			mov ecx, inp_size
			dec ecx
			mov ebx, 1
			mov eax, [edi + 4 * ecx]

			LOOP_TO_DIGIT :
			imul ebx, 2
			push ebx
			push ecx
			dec ecx
			imul ebx, [edi + 4 * ecx]
			pop ecx
			add eax, ebx
			pop ebx

			loop LOOP_TO_DIGIT

			END :
		mov digit, eax
	}

#endif

#ifdef nine_score

	//Начало ассемблерной вставки
	__asm {
		lea edi, inp	//Установить ссылку на массив строк
		add edi, 4		//Установить ссылку на начало массива
		xor ecx, ecx	//Обнулить счетчик

		mov dl, [edi]			//читаем символ
		sub dl, 48				//Вычисляем цифру

		//Число положительное или отрицательное
		cmp edx, 0
		je TO_ARRAY

		//Число оказалось отрицательным
		mov is_minus, 1

		//Цикл для записи числа в массив
		TO_ARRAY:
		mov dl, [edi]			//читаем символ
			push edi				//Запоминаем индекс в строке
			sub dl, 48				//Вычисляем цифру
			lea edi, machine_code	//Массив куда записываем инвертированное число

			mov[edi + 4 * ecx], edx	//Записываем число

			//Инвертируем число
			cmp[edi + 4 * ecx], 1
			je IS_ONE

			mov[edi + 4 * ecx], 1
			jmp CONTINUE_ARRAY

			IS_ONE :
		mov[edi + 4 * ecx], 0

			CONTINUE_ARRAY :

			inc ecx
			pop edi
			add edi, 1

			cmp ecx, inp_size
			jne TO_ARRAY


			lea edi, machine_code
			dec ecx

			//Цикл для добавления битовой единицы
			ADD_ONE_BYTE :
		cmp[edi + 4 * ecx], 0
			je TO_DECIMAL

			mov[edi + 4 * ecx], 0
			dec ecx
			cmp ecx, -1
			jne ADD_ONE_BYTE

			mov eax, 0
			jmp END

			TO_DECIMAL :
		mov[edi + 4 * ecx], 1
			mov ecx, inp_size
			dec ecx
			mov ebx, 1
			mov eax, [edi + 4 * ecx]

			LOOP_TO_DIGIT :
			imul ebx, 2
			push ebx
			push ecx
			dec ecx
			imul ebx, [edi + 4 * ecx]
			pop ecx
			add eax, ebx
			pop ebx

			loop LOOP_TO_DIGIT

		END :
	//Делим на сотни, десятки и тд.
		mov ecx, 9
		lea edi, parsed_digit

	NEW_MODULE:
		cmp eax, 1000
		jnge END_PROG

		mov edx, 0
		mov ebx, 1000

		div ebx
		mov [edi+4*ecx], edx

		dec ecx
		cmp ecx, -1
		jne NEW_MODULE

	   

	END_PROG:
		mov [edi + 4*ecx], eax
	}

#endif


	printf("Digit > ");

#ifndef nine_score
	printf((is_minus == 1) ? "-" : "+");
	printf("%d\n", digit);
#endif
	

#ifdef nine_score

	printf((is_minus == 1) ? "-" : "+");
	for (auto i : parsed_digit) {
		if (i != -1) {
			printf("%d ", i);
		}
	}

#endif

	printf("\n");
	is_minus = 0;
	system("pause");
}