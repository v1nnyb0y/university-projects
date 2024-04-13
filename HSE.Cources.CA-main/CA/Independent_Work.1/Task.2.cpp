#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#undef seven_score
#undef  eight_score
#define   nine_score
#undef  ten_score

void task_2() {
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	static string inp;			//Введенное число
	static int machine_code[32];
	for(int i = 0;i<32;++i) {
		machine_code[i] = -1;
	}

	//Input inp
	printf("Input digit to translate > ");
	getline(cin, inp);

	static int inp_size;
	inp_size = inp.size();	//Размер введенной строки


	#ifdef seven_score

	//Начало ассемблерной вставки
	__asm {
		lea edi, inp			//Указатель на начало массива
		add edi, 5				//Переход на второй символ
		xor eax, eax			//Обнуление переменной для числа
		mov ecx, inp_size		//Установить счетчик
		dec ecx					//Уменьшение работы цикла на 1 действия
		mov ebx, 1				//Ввести число для десятка
		cmp ecx, 1				//Если 1 цифра в числе
		jz TO_DIGIT
		push ecx				//Запомнить индекс с которого начинаются числа
		dec ecx					//Уменьшаем на 1
		//Цикл рассчета кол-ва сотен десятков и пр.
	KS:
		imul ebx, 10			//Максимальный разряд
		dec ecx					//Уменьшаем счетчик
		cmp ecx, 0
		jnz KS

		pop ecx					//Вернуть индекс начала чисел
		

		//Цикл перевода числа в 10СС
	TO_DIGIT:
		mov dl, [edi]			//читаем символ
		sub dl, 48				//Вычисляем цифру
		imul edx, ebx			//Считаем десятки сотни и пр.
		add eax, edx			//Добавляем к рассчитываемому числу

		push eax				//Запомнить число
		mov edx, 0				//Обнуляем дробную часть
		mov eax, ebx			//Обозначаем делитель
		mov ebx, 10				//Обозначаем делимое
		div ebx					//Деление
		mov ebx, eax			//Запоминаем результат
		pop eax					//Возвращаем реальное число (рассчитываемое)

		add edi, 1				//Сдвигаемся на 1 символ

		dec ecx					//Уменьшаем индекс
		cmp ecx, 0				
		jnz TO_DIGIT

		xor ecx, ecx			//Обнуляем переменную
		xor edx, edx			//Обнуляем переменную
		mov ebx, 2				//Устанавливаем СС

		lea edi, inp			//Устанавливаем начало строки
		add edi, 4				//Устанавливаем значение на знак
		cmp [edi], 0x2d			//Проверка на отрицательное число
		jz MINUS

		//Цикл (если положительное) высчитывает число в 2СС
	TO_2CC:
		inc ecx					//Увеличиваем счетчик
		div ebx					//Делим на 2
		push edx				//Запоминаем остаток
		xor edx, edx			//Обнуляем остаток

		cmp eax, 0
		jnz TO_2CC

		lea edi, machine_code	//Устанавливаем указатель на начало массива (ответ)
		push ecx				//Запоминаем индекс 
		imul ecx, 4				//Считаем индекс конца массива чтобы влезли все числа
		add edi, 128			//Устанавливаем указатель на конец массива
		sub edi, ecx			//Устанавливаем счетчик на конец массива при учете вхождения всех вычисленных остатков
		pop ecx					//Возвращаем количество остатков
		mov eax, 0				//Устанавливаем количество на ноль
		jmp OUTPUT

		//Цикл (для отрицательного) вычисляет число в 2СС (инвертированное)
	MINUS:
		inc ecx					//Считаем количество остатков
		div ebx					//Деление 
		cmp edx, 0				//Инвертирование
		jz CONVERT_ONE
		push 0					//Добавить ноль
		jmp CONT
	CONVERT_ONE:
		push 1					//Добавить единицу
	CONT:
		xor edx, edx			//Обнуляем остаток

		cmp eax, 0
		jnz MINUS

		mov eax, ecx			//Запоминаем количество остатков
		lea edi, machine_code	//Устанавливаем счиетчик на начало массива
		add edi, 128			//Устанавливаем считчик на конец массива
		imul ecx, 4				//Вычисляем индекс после которого влезут в массив все остатки
		sub edi, ecx			//Указываем на этот индекс
		mov ecx, eax			//Запоминаем количество остатков

		//Цикл для записи инвертированного числа в массив
	PLUS_ONE:
		pop ebx					//Достаем остаток
		mov [edi], ebx			//Кладем в массив ответ
		add edi, 4				//Сдвигаемся на след. элемент массива

		dec ecx					//Уменьшаем количество вставленных элементов
		cmp ecx, 0
		jnz PLUS_ONE

		mov ecx, eax			//Запоминаем количество остатков
		lea edi, machine_code	//Устанавливаем значение на начало массива
		add edi, 124			//Устанавливаем указатель на конец массива

		//Цикс для прибавления единицы к инвертированной 2СС
	EQUAL :
		cmp [edi], 0			//Проверка на ноль
		jz ZERO

		mov dword ptr [edi], 0	//Вставить ноль
		sub edi, 4				//Сдвинуться элемент назад
		dec ecx					//Уменьшаем индек для выхода ииз цикла
		cmp ecx, 0
		jnz EQUAL

	ZERO:
		mov dword ptr [edi], 1	//Вставить 1 (которую прибавили)

		imul ecx, 4				//Найти индекс перед которым надо дозаполнить единицами (так как инвертированное число)
		sub edi, ecx			//Указать на этот индекс	
		mov ecx, eax			//Запомнить количество остатков

	jmp TYPE_SETKA

		//Цикл для заполнения массива (положительное число)
	OUTPUT:
		inc eax			//Увеличиваем количество вставленных элементов числа в 2СС
		pop ebx			//Достать число из стэка
		mov [edi], ebx	//Записать остаток в массив-ответ
		add edi, 4		//Перейти на след. элемент массива

		dec ecx			//Уменьашаем индекс выхода из массива
		cmp ecx, 0
		jnz OUTPUT

		push eax		//Запомнить количество вставленных элементов
		inc eax			//Высчитываем индекс перед которым надо вставлять нули
		imul eax, 4		//Высчитываем продолжение
		sub edi, eax	//Указываем на этот индекс
		pop eax			//Возвращаем количество вставленных элементов

	TYPE_SETKA:
		cmp eax, 8		//Это число в формате байта (заполнено)
		jz END

		cmp eax, 8		//Это число в формате байта (требуется заполнение)
		jl _BYTE

		cmp eax, 16		//Это число в формате слова (заполнено)
		jz END

		cmp eax, 16		//Это число в формате слова (требуется заполнение)
		jl _WORD

		cmp eax, 32		//Это число в формате 2слова (заполнено)
		jz END

		cmp eax, 32		//Это число в формате 2слова (требуется заполнение)
		jl _2WORD

		//Цикл для заполнения байта (запоментирован только этот цикл, так как остальные аналогичный способ)
	_BYTE:
		mov ecx, 8		//Указываем количество элементов в байте
		sub ecx, eax	//Высчитываем сколько вставить надо еще
		push edi		//Запоминаем индекс массива-ответ
		lea edi, inp	//Устанавливаем индекс на начало массива входного
		add edi, 4		//Устанавливаем индекс на знак числа
		cmp [edi], 0x2d	//Если число отрицательное
		jz _MM1

		pop edi			//Возвращаем индекс

		//Цикл для дозаполнения нулями (для положительного числа)
	POSITIVE_BYTE:
		mov dword ptr [edi], 0	//Добавить ноль
		dec ecx					//Уменьшить индекс для выхода из цикла
		sub edi, 4				//Переход на другой элемент массива
		cmp ecx, 0
		jnz POSITIVE_BYTE
		jmp END

	_MM1:
		pop edi			//Возвращаем индекс

		//Цикл для дозавполнения елиницами (для отрицательных чисел)
	NEGATIVE_BYTE:
		mov dword ptr [edi], 1	//Добавляем единицу
		dec ecx					//Уменьшаем индекс для выхода из цикла
		sub edi, 4				//Переход на другой элемент массива
		cmp ecx, 0				
		jnz NEGATIVE_BYTE
		jmp END

		//Цикл для заполнения слова
	_WORD:
		mov ecx, 16
		sub ecx, eax
		push edi
		lea edi, inp
		add edi, 4
		cmp [edi], 0x2d
		jz _MM2

		pop edi
	POSITIVE_WORD :
		mov dword ptr [edi], 0
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz POSITIVE_WORD
		jmp END

	_MM2 :
		pop edi
	NEGATIVE_WORD :
		mov dword ptr [edi], 1
		dec ecx
		sub edi, 4
		cmp ecx,0
		jnz NEGATIVE_WORD
		jmp END

		//Цикл для заполнения двойного слова
	_2WORD:
		mov ecx, 32
		sub ecx, eax
		push edi
		lea edi, inp
		add edi, 4
		cmp [edi], 0x2d
		jz _MM3

		pop edi
	POSITIVE_2WORD :
		mov dword ptr [edi], 0
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz POSITIVE_2WORD
		jmp END

	_MM3:
		pop edi
	NEGATIVE_2WORD :
		mov dword ptr [edi], 1
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz NEGATIVE_2WORD
		jmp END
	END:
	}

	#endif

	#ifdef eight_score

	//Начало ассемблерной вставки
	__asm {
		lea edi, inp			//Указатель на начало массива		
		add edi, 4				//Переход на первый символ
		mov ecx, inp_size		//Установить счетчик
		dec ecx					//Уменьшаем счетчик (так как первый символ проверяется вне цикла)

		cmp [edi], 0x2b			//Проверка на положительный символ
		jz CHECK_RIGHT_INPUT

		cmp [edi], 0x2d			//Проверка на отрицательный символ
		jz CHECK_RIGHT_INPUT

		jmp END

		//Цикл для проверки ввода (со второго символа)
	CHECK_RIGHT_INPUT:
		inc edi					//Переходим на следующий символ
		dec ecx					//Уменьшаем счетчик

		cmp[edi], 0x30			//Проверка на меньше 0
		jl END
		cmp[edi], 0x39			//Проверка на больше 9
		jg END

		cmp ecx, 0
		jnz CHECK_RIGHT_INPUT

		lea edi, inp			//Указатель на начало массива
		add edi, 5				//Переход на второй символ
		xor eax, eax			//Обнуление переменной для числа
		mov ecx, inp_size		//Установить счетчик
		dec ecx					//Уменьшение работы цикла на 1 действия
		mov ebx, 1				//Ввести число для десятка
		cmp ecx, 1				//Если 1 цифра в числе
		jz TO_DIGIT
		push ecx				//Запомнить индекс с которого начинаются числа
		dec ecx					//Уменьшаем на 1
		//Цикл рассчета кол-ва сотен десятков и пр.
		KS :
		imul ebx, 10			//Максимальный разряд
			dec ecx					//Уменьшаем счетчик
			cmp ecx, 0
			jnz KS

			pop ecx					//Вернуть индекс начала чисел


			//Цикл перевода числа в 10СС
			TO_DIGIT :
		mov dl, [edi]			//читаем символ
			sub dl, 48				//Вычисляем цифру
			imul edx, ebx			//Считаем десятки сотни и пр.
			add eax, edx			//Добавляем к рассчитываемому числу

			push eax				//Запомнить число
			mov edx, 0				//Обнуляем дробную часть
			mov eax, ebx			//Обозначаем делитель
			mov ebx, 10				//Обозначаем делимое
			div ebx					//Деление
			mov ebx, eax			//Запоминаем результат
			pop eax					//Возвращаем реальное число (рассчитываемое)

			add edi, 1				//Сдвигаемся на 1 символ

			dec ecx					//Уменьшаем индекс
			cmp ecx, 0
			jnz TO_DIGIT

			xor ecx, ecx			//Обнуляем переменную
			xor edx, edx			//Обнуляем переменную
			mov ebx, 2				//Устанавливаем СС

			lea edi, inp			//Устанавливаем начало строки
			add edi, 4				//Устанавливаем значение на знак
			cmp[edi], 0x2d			//Проверка на отрицательное число
			jz MINUS

			//Цикл (если положительное) высчитывает число в 2СС
			TO_2CC :
		inc ecx					//Увеличиваем счетчик
			div ebx					//Делим на 2
			push edx				//Запоминаем остаток
			xor edx, edx			//Обнуляем остаток

			cmp eax, 0
			jnz TO_2CC

			lea edi, machine_code	//Устанавливаем указатель на начало массива (ответ)
			push ecx				//Запоминаем индекс 
			imul ecx, 4				//Считаем индекс конца массива чтобы влезли все числа
			add edi, 128			//Устанавливаем указатель на конец массива
			sub edi, ecx			//Устанавливаем счетчик на конец массива при учете вхождения всех вычисленных остатков
			pop ecx					//Возвращаем количество остатков
			mov eax, 0				//Устанавливаем количество на ноль
			jmp OUTPUT

			//Цикл (для отрицательного) вычисляет число в 2СС (инвертированное)
			MINUS :
		inc ecx					//Считаем количество остатков
			div ebx					//Деление 
			cmp edx, 0				//Инвертирование
			jz CONVERT_ONE
			push 0					//Добавить ноль
			jmp CONT
			CONVERT_ONE :
		push 1					//Добавить единицу
			CONT :
			xor edx, edx			//Обнуляем остаток

			cmp eax, 0
			jnz MINUS

			mov eax, ecx			//Запоминаем количество остатков
			lea edi, machine_code	//Устанавливаем счиетчик на начало массива
			add edi, 128			//Устанавливаем считчик на конец массива
			imul ecx, 4				//Вычисляем индекс после которого влезут в массив все остатки
			sub edi, ecx			//Указываем на этот индекс
			mov ecx, eax			//Запоминаем количество остатков

			//Цикл для записи инвертированного числа в массив
			PLUS_ONE :
		pop ebx					//Достаем остаток
			mov[edi], ebx			//Кладем в массив ответ
			add edi, 4				//Сдвигаемся на след. элемент массива

			dec ecx					//Уменьшаем количество вставленных элементов
			cmp ecx, 0
			jnz PLUS_ONE

			mov ecx, eax			//Запоминаем количество остатков
			lea edi, machine_code	//Устанавливаем значение на начало массива
			add edi, 124			//Устанавливаем указатель на конец массива

			//Цикс для прибавления единицы к инвертированной 2СС
			EQUAL :
		cmp[edi], 0			//Проверка на ноль
			jz ZERO

			mov dword ptr[edi], 0	//Вставить ноль
			sub edi, 4				//Сдвинуться элемент назад
			dec ecx					//Уменьшаем индек для выхода ииз цикла
			cmp ecx, 0
			jnz EQUAL

			ZERO :
		mov dword ptr[edi], 1	//Вставить 1 (которую прибавили)

			imul ecx, 4				//Найти индекс перед которым надо дозаполнить единицами (так как инвертированное число)
			sub edi, ecx			//Указать на этот индекс	
			mov ecx, eax			//Запомнить количество остатков

			jmp TYPE_SETKA

			//Цикл для заполнения массива (положительное число)
			OUTPUT :
		inc eax			//Увеличиваем количество вставленных элементов числа в 2СС
			pop ebx			//Достать число из стэка
			mov[edi], ebx	//Записать остаток в массив-ответ
			add edi, 4		//Перейти на след. элемент массива

			dec ecx			//Уменьашаем индекс выхода из массива
			cmp ecx, 0
			jnz OUTPUT

			push eax		//Запомнить количество вставленных элементов
			inc eax			//Высчитываем индекс перед которым надо вставлять нули
			imul eax, 4		//Высчитываем продолжение
			sub edi, eax	//Указываем на этот индекс
			pop eax			//Возвращаем количество вставленных элементов

			TYPE_SETKA :
		cmp eax, 8		//Это число в формате байта (заполнено)
			jz END

			cmp eax, 8		//Это число в формате байта (требуется заполнение)
			jl _BYTE

			cmp eax, 16		//Это число в формате слова (заполнено)
			jz END

			cmp eax, 16		//Это число в формате слова (требуется заполнение)
			jl _WORD

			cmp eax, 32		//Это число в формате 2слова (заполнено)
			jz END

			cmp eax, 32		//Это число в формате 2слова (требуется заполнение)
			jl _2WORD

			//Цикл для заполнения байта (запоментирован только этот цикл, так как остальные аналогичный способ)
			_BYTE :
		mov ecx, 8		//Указываем количество элементов в байте
			sub ecx, eax	//Высчитываем сколько вставить надо еще
			push edi		//Запоминаем индекс массива-ответ
			lea edi, inp	//Устанавливаем индекс на начало массива входного
			add edi, 4		//Устанавливаем индекс на знак числа
			cmp[edi], 0x2d	//Если число отрицательное
			jz _MM1

			pop edi			//Возвращаем индекс

			//Цикл для дозаполнения нулями (для положительного числа)
			POSITIVE_BYTE :
		mov dword ptr[edi], 0	//Добавить ноль
			dec ecx					//Уменьшить индекс для выхода из цикла
			sub edi, 4				//Переход на другой элемент массива
			cmp ecx, 0
			jnz POSITIVE_BYTE
			jmp END

			_MM1 :
		pop edi			//Возвращаем индекс

		//Цикл для дозавполнения елиницами (для отрицательных чисел)
			NEGATIVE_BYTE :
		mov dword ptr[edi], 1	//Добавляем единицу
			dec ecx					//Уменьшаем индекс для выхода из цикла
			sub edi, 4				//Переход на другой элемент массива
			cmp ecx, 0
			jnz NEGATIVE_BYTE
			jmp END

			//Цикл для заполнения слова
			_WORD :
		mov ecx, 16
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM2

			pop edi
			POSITIVE_WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_WORD
			jmp END

			_MM2 :
		pop edi
			NEGATIVE_WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_WORD
			jmp END

			//Цикл для заполнения двойного слова
			_2WORD :
		mov ecx, 32
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM3

			pop edi
			POSITIVE_2WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_2WORD
			jmp END

			_MM3 :
		pop edi
			NEGATIVE_2WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_2WORD
			jmp END
		END :
	}

	#endif

	#ifdef nine_score

	//Начало ассемблерной вставки
	__asm {
			lea edi, inp			//Указатель на начало массива
			add edi, 5				//Переход на второй символ
			xor eax, eax			//Обнуление переменной для числа
			mov ecx, inp_size		//Установить счетчик
			dec ecx					//Уменьшение работы цикла на 1 действия
			mov ebx, 1				//Ввести число для десятка
			cmp ecx, 1				//Если 1 цифра в числе
			jz TO_DIGIT
			push ecx				//Запомнить индекс с которого начинаются числа
			dec ecx					//Уменьшаем на 1

			//Цикл рассчета кол-ва сотен десятков и пр.
		KS :
			cmp [edi], 0x20
			jz SKIP_SPACE
			imul ebx, 10			//Максимальный разряд
		SKIP_SPACE:
			inc edi
			dec ecx					//Уменьшаем счетчик
			cmp ecx, 0
			jnz KS

			lea edi, inp
			add edi, 5
			pop ecx					//Вернуть индекс начала чисел


			//Цикл перевода числа в 10СС
			TO_DIGIT :
		mov dl, [edi]			//читаем символ
		cmp dl, 0x20
		jz SKIP_SPACE_PARSE
			sub dl, 48				//Вычисляем цифру
			imul edx, ebx			//Считаем десятки сотни и пр.
			add eax, edx			//Добавляем к рассчитываемому числу

			push eax				//Запомнить число
			mov edx, 0				//Обнуляем дробную часть
			mov eax, ebx			//Обозначаем делитель
			mov ebx, 10				//Обозначаем делимое
			div ebx					//Деление
			mov ebx, eax			//Запоминаем результат
			pop eax					//Возвращаем реальное число (рассчитываемое)

		SKIP_SPACE_PARSE:
			add edi, 1				//Сдвигаемся на 1 символ

			dec ecx					//Уменьшаем индекс
			cmp ecx, 0
			jnz TO_DIGIT

			xor ecx, ecx			//Обнуляем переменную
			xor edx, edx			//Обнуляем переменную
			mov ebx, 2				//Устанавливаем СС

			lea edi, inp			//Устанавливаем начало строки
			add edi, 4				//Устанавливаем значение на знак
			cmp[edi], 0x2d			//Проверка на отрицательное число
			jz MINUS

			//Цикл (если положительное) высчитывает число в 2СС
			TO_2CC :
		inc ecx					//Увеличиваем счетчик
			div ebx					//Делим на 2
			push edx				//Запоминаем остаток
			xor edx, edx			//Обнуляем остаток

			cmp eax, 0
			jnz TO_2CC

			lea edi, machine_code	//Устанавливаем указатель на начало массива (ответ)
			push ecx				//Запоминаем индекс 
			imul ecx, 4				//Считаем индекс конца массива чтобы влезли все числа
			add edi, 128			//Устанавливаем указатель на конец массива
			sub edi, ecx			//Устанавливаем счетчик на конец массива при учете вхождения всех вычисленных остатков
			pop ecx					//Возвращаем количество остатков
			mov eax, 0				//Устанавливаем количество на ноль
			jmp OUTPUT

			//Цикл (для отрицательного) вычисляет число в 2СС (инвертированное)
			MINUS :
		inc ecx					//Считаем количество остатков
			div ebx					//Деление 
			cmp edx, 0				//Инвертирование
			jz CONVERT_ONE
			push 0					//Добавить ноль
			jmp CONT
			CONVERT_ONE :
		push 1					//Добавить единицу
			CONT :
			xor edx, edx			//Обнуляем остаток

			cmp eax, 0
			jnz MINUS

			mov eax, ecx			//Запоминаем количество остатков
			lea edi, machine_code	//Устанавливаем счиетчик на начало массива
			add edi, 128			//Устанавливаем считчик на конец массива
			imul ecx, 4				//Вычисляем индекс после которого влезут в массив все остатки
			sub edi, ecx			//Указываем на этот индекс
			mov ecx, eax			//Запоминаем количество остатков

			//Цикл для записи инвертированного числа в массив
			PLUS_ONE :
		pop ebx					//Достаем остаток
			mov[edi], ebx			//Кладем в массив ответ
			add edi, 4				//Сдвигаемся на след. элемент массива

			dec ecx					//Уменьшаем количество вставленных элементов
			cmp ecx, 0
			jnz PLUS_ONE

			mov ecx, eax			//Запоминаем количество остатков
			lea edi, machine_code	//Устанавливаем значение на начало массива
			add edi, 124			//Устанавливаем указатель на конец массива

			//Цикс для прибавления единицы к инвертированной 2СС
			EQUAL :
		cmp[edi], 0			//Проверка на ноль
			jz ZERO

			mov dword ptr[edi], 0	//Вставить ноль
			sub edi, 4				//Сдвинуться элемент назад
			dec ecx					//Уменьшаем индек для выхода ииз цикла
			cmp ecx, 0
			jnz EQUAL

			ZERO :
		mov dword ptr[edi], 1	//Вставить 1 (которую прибавили)

			imul ecx, 4				//Найти индекс перед которым надо дозаполнить единицами (так как инвертированное число)
			sub edi, ecx			//Указать на этот индекс	
			mov ecx, eax			//Запомнить количество остатков

			jmp TYPE_SETKA

			//Цикл для заполнения массива (положительное число)
			OUTPUT :
		inc eax			//Увеличиваем количество вставленных элементов числа в 2СС
			pop ebx			//Достать число из стэка
			mov[edi], ebx	//Записать остаток в массив-ответ
			add edi, 4		//Перейти на след. элемент массива

			dec ecx			//Уменьашаем индекс выхода из массива
			cmp ecx, 0
			jnz OUTPUT

			push eax		//Запомнить количество вставленных элементов
			inc eax			//Высчитываем индекс перед которым надо вставлять нули
			imul eax, 4		//Высчитываем продолжение
			sub edi, eax	//Указываем на этот индекс
			pop eax			//Возвращаем количество вставленных элементов

			TYPE_SETKA :
		cmp eax, 8		//Это число в формате байта (заполнено)
			jz END

			cmp eax, 8		//Это число в формате байта (требуется заполнение)
			jl _BYTE

			cmp eax, 16		//Это число в формате слова (заполнено)
			jz END

			cmp eax, 16		//Это число в формате слова (требуется заполнение)
			jl _WORD

			cmp eax, 32		//Это число в формате 2слова (заполнено)
			jz END

			cmp eax, 32		//Это число в формате 2слова (требуется заполнение)
			jl _2WORD

			//Цикл для заполнения байта (запоментирован только этот цикл, так как остальные аналогичный способ)
			_BYTE :
		mov ecx, 8		//Указываем количество элементов в байте
			sub ecx, eax	//Высчитываем сколько вставить надо еще
			push edi		//Запоминаем индекс массива-ответ
			lea edi, inp	//Устанавливаем индекс на начало массива входного
			add edi, 4		//Устанавливаем индекс на знак числа
			cmp[edi], 0x2d	//Если число отрицательное
			jz _MM1

			pop edi			//Возвращаем индекс

			//Цикл для дозаполнения нулями (для положительного числа)
			POSITIVE_BYTE :
		mov dword ptr[edi], 0	//Добавить ноль
			dec ecx					//Уменьшить индекс для выхода из цикла
			sub edi, 4				//Переход на другой элемент массива
			cmp ecx, 0
			jnz POSITIVE_BYTE
			jmp END

			_MM1 :
		pop edi			//Возвращаем индекс

		//Цикл для дозавполнения елиницами (для отрицательных чисел)
			NEGATIVE_BYTE :
		mov dword ptr[edi], 1	//Добавляем единицу
			dec ecx					//Уменьшаем индекс для выхода из цикла
			sub edi, 4				//Переход на другой элемент массива
			cmp ecx, 0
			jnz NEGATIVE_BYTE
			jmp END

			//Цикл для заполнения слова
			_WORD :
		mov ecx, 16
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM2

			pop edi
			POSITIVE_WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_WORD
			jmp END

			_MM2 :
		pop edi
			NEGATIVE_WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_WORD
			jmp END

			//Цикл для заполнения двойного слова
			_2WORD :
		mov ecx, 32
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM3

			pop edi
			POSITIVE_2WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_2WORD
			jmp END

			_MM3 :
		pop edi
			NEGATIVE_2WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_2WORD
			jmp END
			END :
	}

	#endif

	printf("Machine code > ");
	for(int i = 0;i < 32; ++i) {
		if (machine_code[i] != -1) {
			printf("%d", machine_code[i]);
		}
	}

	printf("\n");
	system("pause");
}