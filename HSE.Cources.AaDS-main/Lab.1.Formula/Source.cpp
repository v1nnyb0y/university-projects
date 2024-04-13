#include <iostream>
#include <algorithm>
#include <string>
#include <cmath>
#include <iomanip>
#include <vector>
#include <set>
#include <Windows.h>
#include <fstream>
#include <cstdlib>
#include <conio.h>
#include <map>
#include <time.h>

using namespace std;

//Описание программы
/*Программа для подсчет N-го элемента последовательности 3-мя разными способами:
1. По формуле;

2. Рекурсией (через подсчет предыдущих элементов);
3. Без рекурсии (через подсчет предыдущих элементов);
Программа получает на вход число int;
Выводит 3 числа int*/

//Вывод меню
int Menu(int k, vector<string>&pechat)
{
	system("cls");

	CONSOLE_CURSOR_INFO CCI;
	CCI.bVisible = false;
	CCI.dwSize = 1;
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &CCI);

	HANDLE stdoutput = GetStdHandle(STD_OUTPUT_HANDLE);
	int y = 1;
	int tek = 0, tekold = 0;
	int x = 1;
	int ok = false;
	for (int i = 0; i < pechat.size(); i++)
	{
		COORD pos = { x, y + i };
		SetConsoleCursorPosition(stdoutput, pos);
		if (i % (k + 1) == 0)
		{
			SetConsoleTextAttribute(stdoutput, FOREGROUND_INTENSITY | FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE |
				0);
		}
		else
		{
			SetConsoleTextAttribute(stdoutput, FOREGROUND_INTENSITY | 0);
		}

		cout << pechat[i];
	}

	do
	{
		SetConsoleTextAttribute(stdoutput, FOREGROUND_INTENSITY | FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE |
			0);

		COORD pos = { x, y + tekold };
		SetConsoleCursorPosition(stdoutput, pos);

		cout << pechat[tekold] << " ";
		SetConsoleTextAttribute(stdoutput, BACKGROUND_INTENSITY | BACKGROUND_RED | BACKGROUND_GREEN | BACKGROUND_BLUE |
			0);
		pos = { (SHORT)x, (SHORT)(y + tek) };
		SetConsoleCursorPosition(stdoutput, pos);

		cout << pechat[tek];
		tekold = tek;
		char key = _getch();
		if (key == 0 || key == 0xE0) key = _getch();
		switch (key)
		{
		case 80:
			tek += k + 1;
			break;
		case 72:
			tek -= k + 1;
			break;
		case 13:
			ok = true;
			break;
		}

		if (tek >= pechat.size())
			tek = 0;
		else if (tek < 0)
			tek = pechat.size() - 1;
	} while (!ok);


	SetConsoleTextAttribute(stdoutput, FOREGROUND_INTENSITY | FOREGROUND_RED | FOREGROUND_GREEN | FOREGROUND_BLUE |
		0);
	system("cls");
	return tek + 1;
}  //Вывод меню

//Получение элемента N-го номера рекурсией
int Recursion(int numerous) {
	switch (numerous) {
		//Если номер равен 0
	case 0:
		return 0;
		//Если номер равен 1
	case 1:
		return 1;
		//Во всех остальных случаях
	default:
		return (12 * (numerous - 2) - 3 * Recursion(numerous - 1) - 2 * Recursion(numerous - 2));
	}
}

//Получение элемента N-го номера не рекурсией
int Non_recursion(int first, int second, int number, int present_number) {
	switch (number) {
		//Если номер равен 0
	case 0:
		return 0;
		//Если номер равен 1
	case 1:
		return 1;
		//Во всех остальных случаях
	default:
		//Пока элемент не будет найден (сравниваем номера элементов)
		for (int i = present_number; i <= number; i++) {
			int tmp = 12 * (i - 2) - 3 * second - 2 * first;
			first = second;
			second = tmp;
		}
		return second;
	}
}

int main() {
	vector<string> main_manu = { "Enter the number of the item you want to find: ", "Exit." };
	while (true) {
		int sw = Menu(0, main_manu);
		switch (sw)
		{
			//Подсчет N-го элемента 
		case 1: {
			int number;
			cout << "Enter the number of the item you want to find: ";
			cin >> number;
			LARGE_INTEGER s, e;
			QueryPerformanceCounter(&s);
			cout << "\nCounting by recursion: " << Recursion(number);
			QueryPerformanceCounter(&e);
			cout << " Time: " << (e.QuadPart - s.QuadPart) << endl;
			QueryPerformanceCounter(&s);
			cout << "Counting by non recursion: " << Non_recursion(0, 1, number, 2);
			QueryPerformanceCounter(&e);
			cout << " Time: " << (e.QuadPart - s.QuadPart) << endl;
			QueryPerformanceCounter(&s);
			cout << "Counting by formula: " << (pow(-2.0, number)*(-7.0) - 5.0) / 3 + pow(-1.0, number) * 4 + 2 * number;
			QueryPerformanceCounter(&e);
			cout << " Time: " << (e.QuadPart - s.QuadPart) << endl;

			return 0;
		}
				//Выход из программы
		case 2: {
			return 0;
		}
		}
	}
	return 0;
}