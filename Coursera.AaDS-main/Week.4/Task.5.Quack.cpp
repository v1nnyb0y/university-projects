#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <queue>
#include <string>
#include <map>
#include <set>
#include <stdio.h>
#include <iomanip>
#include <deque>
#include <stack>
#include <unordered_map>
using namespace std;

#define QMAX 100000

struct Queue
{
	int qu[QMAX];
	int rear, frnt;

	Queue() {
		rear = 0;
		frnt = 1;
	}

	void push(int x) {
		if (rear < QMAX - 1) {
			rear++;
			qu[rear] = x;
		}
	}

	int is_empty() {
		if (rear < frnt) return (1);
		return (0);
	}

	int pop() {
		int x;
		if (is_empty() == 1) {
			return 0;
		}
		x = qu[frnt];
		frnt++;
		return x;
	}
};

struct Quack
{
	unordered_map<char, int> Register; //Регистр буква/число
	Queue qLanguage; //Очередь языка

	Quack() {
		char word = 'a';
		for (int i = 0; i < 26; i++) {
			Register.insert(make_pair(word++, 0));
		}
	};

	//Сложение двух последних элементов в очереди
	void Plus() {
		int a = qLanguage.pop();
		int b = qLanguage.pop();
		qLanguage.push((a + b) & 65535);
	}

	//Разность двух последних элементов в очереди
	void Minus() {
		int a = qLanguage.pop();
		int b = qLanguage.pop();
		qLanguage.push((a - b) & 65535);
	}

	//Произведение двух последних элементов в очереди
	void Multiplication() {
		int a = qLanguage.pop();
		int b = qLanguage.pop();
		unsigned long long tmp = a * b;
		qLanguage.push(tmp & 65535);
	}

	//Целочисленное деление двух последних элементов в очереди
	void QuotientUnit() {
		int a = qLanguage.pop();
		int b = qLanguage.pop();
		if (b == 0) {
			qLanguage.push(0);
		}
		else {
			qLanguage.push(a / b);
		}
	}

	//Взятие по модулю двух последних элементов в очереди
	void QuotientModul() {
		int a = qLanguage.pop();
		int b = qLanguage.pop();
		if (b == 0) {
			qLanguage.push(0);
		}
		else {
			qLanguage.push(a % b);
		}
	}

	//Положить в регистр
	void Put(char word) {
		int a = qLanguage.pop();
		Register[word] = a;
	}

	//Получить регистр
	void Get(char word) {
		qLanguage.push(Register[word]);
	}

	//Печать последнего элемента в очереди
	void Print() {
		printf("%d\n", qLanguage.pop());
	}

	//Печать регистра
	void Print(char word) {
		printf("%d\n", Register[word]);
	}

	//Печать последнего элемента в очереди ASCII-кодом
	void PrintASCII() {
		char c = qLanguage.pop() & 255;
		cout << c;
	}

	//Печать значения регистра ASCII-кодом
	void PrintASCII(char word) {
		char c = Register[word] & 255;
		cout << c;
		//printf("%s\n", c);
	}

	//Проверка регистр на ноль
	bool is_register_zero(char word) {
		return (Register[word] == 0);
	}

	//Проверка на равенство регистров
	bool is_registers_equals(char word1, char word2) {
		return (Register[word1] == Register[word2]);
	}

	//Проверка большего регистра
	bool is_register_bigger(char word1, char word2) {
		return (Register[word1] > Register[word2]);
	}

	//Добавление элемента в очередь
	void Put(int number) {
		qLanguage.push(number);
	}
};

struct Language
{
	vector<string> cmd;
	Quack quack;
	map<string, int> indexes;

	Language() { }

	void Add(string str) {
		cmd.push_back(str);
		if (str[0] == ':') {
			indexes[str.replace(0, 1, "")] = cmd.size() - 1;
		}
	}

	void Commande() {
		int id = 0;
		while (id < cmd.size()) {
			string line = cmd[id];
			switch (line[0]) {
			case '+': {
				quack.Plus();
				break;
			}
			case '-': {
				quack.Minus();
				break;
			}
			case '/': {
				quack.QuotientUnit();
				break;
			}
			case '%': {
				quack.QuotientModul();
				break;
			}
			case '*': {
				quack.Multiplication();
				break;
			}
			case '>': {
				quack.Put(line[1]);
				break;
			}
			case '<': {
				quack.Get(line[1]);
				break;
			}
			case 'P': {
				if (line.size() == 1) {
					quack.Print();
				}
				else {
					quack.Print(line[1]);
				}
				break;
			}
			case 'C': {
				if (line.size() == 1) {
					quack.PrintASCII();
				}
				else {
					quack.PrintASCII(line[1]);
				}
				break;
			}
			case ':': {
				break;
			}
			case 'J': {
				id = indexes[line.replace(0, 1, "")];
				break;
			}
			case 'Z': {
				if (quack.is_register_zero(line[1])) {
					id = indexes[line.replace(0, 2, "")];
				}
				break;
			}
			case 'E': {
				if (quack.is_registers_equals(line[1], line[2])) {
					id = indexes[line.replace(0, 3, "")];
				}
				break;
			}
			case 'G': {
				if (quack.is_register_bigger(line[1], line[2])) {
					id = indexes[line.replace(0, 3, "")];
				}
				break;
			}
			case 'Q': {
				return;
			}
			default: {
				quack.Put(atoi(line.c_str()) & 65535);
				break;
			}
			}
			id++;
		}
	}
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	setlocale(LC_ALL, "Russian");
	//ifstream input("input.txt");
	Language lnd;
	while (true) {
		string str;
		cin >> str;
		if (str == "") {
			break;
		}
		lnd.Add(str);
	}
	lnd.Commande();
}
