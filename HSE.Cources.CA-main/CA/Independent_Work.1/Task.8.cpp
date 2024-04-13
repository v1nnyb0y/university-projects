#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "task_8_header.h"

using namespace std;

static string decrypt_table;

void task_8() {
	decrypt_table =
		" !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
	setlocale(LC_ALL, "RUS");	//Установить Русский язык в консоли
	int task;
	printf("Input under-task number ([1]=encrypt; [2]=decrypt; [3]=exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 1: task_8_encrypt(); break;
	case 2: task_8_decrypt(); break;
	default: return;
	}
}

void task_8_encrypt() {
	string inp_text;
	printf("Insert text > ");
	cin.ignore(100, '\n');
	getline(cin, inp_text);

	static int text_size, alphabet_size;
	static const char* text;
	static const char* table;

	alphabet_size = decrypt_table.size();
	text_size = inp_text.size();
	text = inp_text.c_str();
	table = decrypt_table.c_str();


	//Начало ассемблерной вставки
	__asm {
		mov ecx, alphabet_size
		mov esi, table

	ENCRYPT_TABLE:
		mov al, [esi]
		mov bl, 126
		add bl, 32
		sub bl, al
		mov [esi], bl

		inc esi
		dec ecx
		cmp ecx, 0
		jne ENCRYPT_TABLE


		mov ebx, table
		mov esi, text
		mov ecx, text_size
	ENCRYPT:
		mov al, [esi]
		sub al, 32
		xlatb 

		mov [esi], al

		dec ecx
		inc esi

		cmp ecx, 0
		jne ENCRYPT
	}


	printf("Encrypt > %s", text);
	printf("\n");
	system("pause");
}

void task_8_decrypt() {
	string inp_text;
	printf("Insert text > ");
	cin.ignore(100, '\n');
	getline(cin, inp_text);

	static int text_size, alphabet_size;
	static const char* text;
	static const char* table;

	alphabet_size = decrypt_table.size();
	text_size = inp_text.size();
	text = inp_text.c_str();
	table = decrypt_table.c_str();


	//Начало ассемблерной вставки
	__asm {
		mov ecx, alphabet_size
		mov esi, table

	DECRYPT_TABLE :
		mov al, [esi]
		mov bl, 126
		add bl, 32
		sub bl, al
		mov[esi], bl

		inc esi
		dec ecx
		cmp ecx, 0
		jne DECRYPT_TABLE


		mov ebx, table
		mov esi, text
		mov ecx, text_size
	DECRYPT :
		mov al, [esi]
		sub al, 32
		xlatb

		mov[esi], al

		dec ecx
		inc esi

		cmp ecx, 0
		jne DECRYPT
	}


	printf("Decrypt > %s", text);
	printf("\n");
	system("pause");
}