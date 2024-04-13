#define _CRT_SECURE_NO_WARNINGS
#include <iostream>

using namespace std;

#undef seven_score
#undef eight_score
#undef  nine_score
#define  ten_score

void task_1() {	
#ifdef seven_score

	static int find_digit_string = -1;	//Индекс седловой точки строка
	static int find_digit_column = -1;	//Индекс седловоной точки столбец

	setlocale(LC_ALL, "RUS");	//Установить Русский язык

	static int arr[5][5];	//Объявление матрицы

	//Заполнение массива
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//Начало ассемблерной вставки
	__asm {
		lea edi, arr	//Установить начало массива
		mov ecx, 0	//Индекс по строке
		mov edx, 0	//Индекс по столбцу

	NEW_STRING:

		mov eax, 2147483647	//Установить максимальное значение для поиска минимума
	NEW_COLUMN:
		cmp eax, [edi + edx * 4]	//Сравнение текущего минимума и претендента
		jle OLD_MIN

		mov eax, [edi + edx * 4]	//Запомнить новый минимум

	OLD_MIN:
		inc edx		//Увеличить индекс строки
		cmp edx, 5		//Прошли ли все столбцы?
		jl NEW_COLUMN

		push eax		//Запомнить минимум
		xor edx, edx	//Обнуляем строку
		push ecx		//Запомнить строку
		xor ecx, ecx	//Очистить переменную

	CHECK_EVERY_MIN:
		cmp eax, [edi + edx * 4]	//Если два одинаковых минимума
		jz FIND_MAX_COLUMN	//Переход на поиск максимального элемента в столбце

	CONTINUE_CHECK:
		inc edx		//Увеличить на 1 индекс
		cmp edx, 5	//Перейти на след. элемент
		jl CHECK_EVERY_MIN	

		pop ecx		//достать индекс строки
		inc ecx		//Увеличить индекс
		add edi, 20		//Перейти на новую строку
		xor edx, edx	//Обнуляем строку
		pop eax		//Очищаем стэк

		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING

		jmp END_WITHOUT		//Если не нашелся?

	FIND_MAX_COLUMN:
		mov eax, -2147483648	//Установить минимальное значение для поиска максимума
		lea edi, arr	//Установить начало массива

	NEW_STRING_FIND_MAX:
		cmp eax, [edi + edx * 4]	//Сравнение текущего максимума и претендента
		jge OLD_MAX

		mov eax, [edi + edx * 4]	//Запомнить новый максимум

	OLD_MAX:
		inc ecx		//Увеличиваем индекс строки
		add edi, 20	//Переход на новую строку
		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING_FIND_MAX

	lea edi, arr	//Установить начало массива	
	xor ecx, ecx	//Обнулить переменную
	CHECK_EVERY_MAX:
		cmp eax, [edi + edx * 4]	//Совпадающий максимум
		jnz NON_COMPARE_STRINGS

		pop ebx		//Достать индекс строки 
		cmp ecx, ebx		//Сравнить индекс строки максимального и минимального
		jz END		//Завершить программу
		push ebx	//Запомнить строку

	NON_COMPARE_STRINGS:
		inc ecx		//Увеличть индекс строки на 1
		add edi, 20		//Перейти на след. строку
		cmp ecx, 5		//Прошли ли все строки?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		mov eax, 5	//Устанавливаем значение размерность массива (строка)
		sub eax, ebx	//Устанавливаем количество индексев до нужной строки
		imul eax, 20	//Назодим число, чтобы вернуться назад
		sub edi, eax	//Возвращаемся назад
		pop eax		//Очищаем стэк
		pop eax		//Возвращаем минимум
		push eax	//Запоминаем минимум
		push ebx	//Запомнить индекс строки реальный

		jmp CONTINUE_CHECK

	END:
		pop eax		//Очищаем стэк
		mov find_digit_column, edx		//Выводим индекс столбца
		mov find_digit_string, ecx		//Выводим индекс строки

	END_WITHOUT:

	}

#endif

#ifdef eight_score

	static int find_digit_string = -1;	//Индекс седловой точки строка
	static int find_digit_column = -1;	//Индекс седловоной точки столбец

	setlocale(LC_ALL, "RUS");	//Установить Русский язык

	static int arr[5][5];	//Объявление матрицы

	//Заполнение массива
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//Начало ассемблерной вставки
	__asm {
		lea edi, arr	//Установить начало массива
		mov ecx, 0	//Индекс по строке
		mov edx, 0	//Индекс по столбцу

	NEW_STRING:
		push ecx	//Запомнить текущую строку
		mov ecx, 5	//Добавить индекс для столбца для цикла loop

		mov eax, 2147483647	//Установить максимальное значение для поиска минимума
	NEW_COLUMN :
		cmp eax, [edi + ecx * 4]	//Сравнение текущего минимума и претендента
		jl OLD_MIN

		mov eax, [edi + ecx * 4]	//Запомнить новый минимум

	OLD_MIN:
		loop NEW_COLUMN

		pop ecx		//Вернуть текущуую строку

		push eax		//Запомнить минимум
		push ecx		//Запомнить строку
		xor ecx, ecx	//Очистить переменную

	CHECK_EVERY_MIN :
		cmp eax, [edi + edx * 4]	//Если два одинаковых минимума
		jz FIND_MAX_COLUMN	//Переход на поиск максимального элемента в столбце

	CONTINUE_CHECK :
		inc edx		//Увеличить на 1 индекс
		cmp edx, 5	//Перейти на след. элемент
		jl CHECK_EVERY_MIN

		pop ecx		//достать индекс строки
		inc ecx		//Увеличить индекс
		add edi, 20		//Перейти на новую строку
		xor edx, edx	//Обнуляем строку
		pop eax		//Очищаем стэк

		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING

		jmp END_WITHOUT		//Если не нашелся?

	FIND_MAX_COLUMN :
		mov eax, -2147483648	//Установить минимальное значение для поиска максимума
		lea edi, arr	//Установить начало массива

	NEW_STRING_FIND_MAX :
		cmp eax, [edi + edx * 4]	//Сравнение текущего максимума и претендента
		jge OLD_MAX

		mov eax, [edi + edx * 4]	//Запомнить новый максимум

	OLD_MAX :
		inc ecx		//Увеличиваем индекс строки
		add edi, 20	//Переход на новую строку
		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING_FIND_MAX

		lea edi, arr	//Установить начало массива	
		xor ecx, ecx	//Обнулить переменную
	CHECK_EVERY_MAX :
		cmp eax, [edi + edx * 4]	//Совпадающий максимум
		jnz NON_COMPARE_STRINGS

		pop ebx		//Достать индекс строки 
		cmp ecx, ebx		//Сравнить индекс строки максимального и минимального
		jz END		//Завершить программу
		push ebx	//Запомнить строку

	NON_COMPARE_STRINGS :
		inc ecx		//Увеличть индекс строки на 1
		add edi, 20		//Перейти на след. строку
		cmp ecx, 5		//Прошли ли все строки?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		mov eax, 5	//Устанавливаем значение размерность массива (строка)
		sub eax, ebx	//Устанавливаем количество индексев до нужной строки
		imul eax, 20	//Назодим число, чтобы вернуться назад
		sub edi, eax	//Возвращаемся назад
		pop eax		//Очищаем стэк
		pop eax		//Возвращаем минимум
		push eax	//Запоминаем минимум
		push ebx	//Запомнить индекс строки реальный

		jmp CONTINUE_CHECK

	END :
		pop eax		//Очищаем стэк
		mov find_digit_column, edx		//Выводим индекс столбца
		mov find_digit_string, ecx		//Выводим индекс строки

	END_WITHOUT :
	}

#endif

#ifdef nine_score

	static int find_digit_string = -1;	//Индекс седловой точки строка
	static int find_digit_column = -1;	//Индекс седловоной точки столбец

	setlocale(LC_ALL, "RUS");	//Установить Русский язык

	static int arr[5][5];	//Объявление матрицы

	//Заполнение массива
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//Начало ассемблерной вставки
	__asm {
		mov ecx, 0	//Индекс по строке
		mov edx, 0	//Индекс по столбцу

	NEW_STRING:
		mov ebx, 0
		mov eax, 2147483647	//Установить максимальное значение для поиска минимума
	NEW_COLUMN :
		cmp eax, arr[edx * 4]	//Сравнение текущего минимума и претендента
		jle OLD_MIN

		mov eax, arr[edx * 4]	//Запомнить новый минимум

	OLD_MIN :
		inc ebx		//Индексер увеличиваем для цикла
		inc edx		//Увеличить индекс строки
		cmp ebx, 5		//Прошли ли все столбцы?
		jl NEW_COLUMN

		push eax		//Запомнить минимум
		sub edx, 5		//Возвращаемся обратно на начало строки
		push ecx		//Запомнить строку
		xor ecx, ecx	//Очистить переменную
		xor ebx, ebx	//Обнуляем переменную

	CHECK_EVERY_MIN :
		cmp eax, arr[edx * 4]	//Если два одинаковых минимума
		jz FIND_MAX_COLUMN	//Переход на поиск максимального элемента в столбце

	CONTINUE_CHECK :
		inc ebx		//Индексер увеличиваем для цикла
		inc edx		//Увеличить на 1 индекс
		cmp ebx, 5	//Перейти на след. элемент
		jl CHECK_EVERY_MIN

		pop ecx		//достать индекс строки
		inc ecx		//Увеличить индекс
		xor edx, edx		//Обнуляем перменную
		add edx, 5		//Перейти на новую строку
		imul edx, ecx	//Увеличиваем переменную для смещения
		pop eax		//Очищаем стэк

		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING

		jmp END_WITHOUT		//Если не нашелся?

	FIND_MAX_COLUMN :
		pop ecx		//Возвращаем текущую строку
		mov eax, ecx		//Переносим в другую переменную
		push ecx		//Запоминаем строку
		imul eax, 5		//Высчитываем смещение
		push edx		//Запоминаем начало для прохода по строке
		sub edx, eax		//Высчитываем начало для прохода по столбцам
		mov eax, -2147483648	//Установить минимальное значение для поиска максимума

	NEW_STRING_FIND_MAX :
		cmp eax, arr[edx * 4]	//Сравнение текущего максимума и претендента
		jge OLD_MAX

		mov eax, arr[edx * 4]	//Запомнить новый максимум

	OLD_MAX :
		inc ecx		//Увеличиваем индекс строки
		add edx, 5	//Переход на новую строку
		cmp ecx, 5	//Прошли ли все строки?
		jl NEW_STRING_FIND_MAX

		pop edx		//Возвращаем начало строки (индекс смещения)
		pop ecx		//Возвращаем текущую строку
		mov ebx, ecx	//Переносим индекс строки в другую переменную
		imul ebx, 5		//Высчитываем смещение
		push edx		//Запоминаем начало строки (индекс смещения)
		sub edx,  ebx		//Высчитываем начало отчета для цикла
		push ecx		//Запоминаем строку
		xor ecx, ecx	//Обнулить переменную

	CHECK_EVERY_MAX :
		cmp eax, arr[edx * 4]	//Совпадающий максимум
		jnz NON_COMPARE_STRINGS

		pop ebx		//Достать индекс строки 
		cmp ecx, ebx		//Сравнить индекс строки максимального и минимального
		jz END		//Завершить программу
		push ebx	//Запомнить строку

	NON_COMPARE_STRINGS :
		inc ecx		//Увеличть индекс строки на 1
		add edx, 5		//Перейти на след. строку
		cmp ecx, 5		//Прошли ли все строки?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		pop eax		//Очищаем стэк
		pop edx		//Вернуть индекс в строке для продолжения
		pop eax		//Возвращаем минимум
		push eax	//Запоминаем минимум
		push ebx	//Запомнить индекс строки реальный

		jmp CONTINUE_CHECK

	END :
		pop eax		//Очищаем стэк
		pop eax		//Очищаем стэк
		mov eax, ecx	//Запоминаем строку
		imul eax, 5		//Умножаем строку на 5 (индекс смещения)
		sub edx, eax	//Получаем реальный индекс
		mov find_digit_column, edx		//Выводим индекс столбца
		mov find_digit_string, ecx		//Выводим индекс строки

	END_WITHOUT :

	}

#endif

#ifdef ten_score

	static unsigned int find_digit_string = -1;	//Индекс седловой точки строка
	static unsigned int find_digit_column = -1;	//Индекс седловоной точки столбец

	setlocale(LC_ALL, "RUS");	//Установить Русский язык

	static unsigned int arr[5][5];	//Объявление матрицы

	//Заполнение массива
	for (auto& i : arr) {
		for (auto& j : i) {
			cout << "Input an element of matrix > ";
			cin >> j;
		}
	}


	//Начало ассемблерной вставки
	__asm {
		mov al, 0
		sub al, 0
		lea edi, arr	//Установить начало массива
		mov ecx, 0	//Индекс по строке
		mov edx, 0	//Индекс по столбцу

		NEW_STRING:

		mov eax, 2147483647	//Установить максимальное значение для поиска минимума
			NEW_COLUMN :
			cmp eax, [edi + edx * 4]	//Сравнение текущего минимума и претендента
			jle OLD_MIN

			mov eax, [edi + edx * 4]	//Запомнить новый минимум

			OLD_MIN :
			inc edx		//Увеличить индекс строки
			cmp edx, 5		//Прошли ли все столбцы?
			jl NEW_COLUMN

			push eax		//Запомнить минимум
			xor edx, edx	//Обнуляем строку
			push ecx		//Запомнить строку
			xor ecx, ecx	//Очистить переменную

			CHECK_EVERY_MIN :
		cmp eax, [edi + edx * 4]	//Если два одинаковых минимума
			jz FIND_MAX_COLUMN	//Переход на поиск максимального элемента в столбце

			CONTINUE_CHECK :
		inc edx		//Увеличить на 1 индекс
			cmp edx, 5	//Перейти на след. элемент
			jl CHECK_EVERY_MIN

			pop ecx		//достать индекс строки
			inc ecx		//Увеличить индекс
			add edi, 20		//Перейти на новую строку
			xor edx, edx	//Обнуляем строку
			pop eax		//Очищаем стэк

			cmp ecx, 5	//Прошли ли все строки?
			jl NEW_STRING

			jmp END_WITHOUT		//Если не нашелся?

			FIND_MAX_COLUMN :
		mov eax, -2147483648	//Установить минимальное значение для поиска максимума
			lea edi, arr	//Установить начало массива

			NEW_STRING_FIND_MAX :
		cmp eax, [edi + edx * 4]	//Сравнение текущего максимума и претендента
			jge OLD_MAX

			mov eax, [edi + edx * 4]	//Запомнить новый максимум

			OLD_MAX :
			inc ecx		//Увеличиваем индекс строки
			add edi, 20	//Переход на новую строку
			cmp ecx, 5	//Прошли ли все строки?
			jl NEW_STRING_FIND_MAX

			lea edi, arr	//Установить начало массива	
			xor ecx, ecx	//Обнулить переменную
			CHECK_EVERY_MAX :
		cmp eax, [edi + edx * 4]	//Совпадающий максимум
			jnz NON_COMPARE_STRINGS

			pop ebx		//Достать индекс строки 
			cmp ecx, ebx		//Сравнить индекс строки максимального и минимального
			jz END		//Завершить программу
			push ebx	//Запомнить строку

			NON_COMPARE_STRINGS :
		inc ecx		//Увеличть индекс строки на 1
			add edi, 20		//Перейти на след. строку
			cmp ecx, 5		//Прошли ли все строки?
			jl CHECK_EVERY_MAX

			xor ecx, ecx
			mov eax, 5	//Устанавливаем значение размерность массива (строка)
			sub eax, ebx	//Устанавливаем количество индексев до нужной строки
			imul eax, 20	//Назодим число, чтобы вернуться назад
			sub edi, eax	//Возвращаемся назад
			pop eax		//Очищаем стэк
			pop eax		//Возвращаем минимум
			push eax	//Запоминаем минимум
			push ebx	//Запомнить индекс строки реальный

			jmp CONTINUE_CHECK

			END :
		pop eax		//Очищаем стэк
			mov find_digit_column, edx		//Выводим индекс столбца
			mov find_digit_string, ecx		//Выводим индекс строки

			END_WITHOUT :

	}

#endif

	//Вывод массива
	if (find_digit_string == -1) {
		cout << 0;
	}else {
		cout << "Found digit > [" << find_digit_string + 1 << ";" << find_digit_column + 1 << "]";
	}


	find_digit_string = -1;	//Индекс седловой точки строка
	find_digit_column = -1;	//Индекс седловоной точки столбец

	cout << endl;
	system("pause");
}