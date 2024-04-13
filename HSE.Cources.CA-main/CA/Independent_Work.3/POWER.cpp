#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "POWER.h"

using namespace std;

static int two_power;
static bool is_ok[10];
static long long A[10];

void power() {
	printf("Insert array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cin >> A[i];
		if (A[i] != 0) is_ok[i] = true;
	}

	printf("\nInsert power of 2 (type INT) > ");
	scanf("%d", &two_power);

	if (two_power < 0) {
		printf("Wrong power of 2\n");
		return;
	}

	int task;
	printf("Insert 1:div or 2:mul or 3:exit > ");
	scanf("%d", &task);
	switch (task) {
	case 1: div(); break;
	case 2: mul(); break;
	case 3: return;
	default: return;
	}
}

void mul() {
	if (two_power != 0) {
		__asm {
			xor ecx, ecx							//�������� ������ �������
			lea esi, A								//���������� ����� �� ������ �������
			

		MUL_S :
			movq mm0, qword ptr[esi + 8 * ecx]		//��������� � MMX ������� �������

			mov eax, two_power						//��������� � ���������� �����
		SDVIG_LEFT:
			psllq mm0, 1							//����� �����
			dec eax									//��������� ������� �� 1
			cmp eax, 0								//�������� �� ������ ����� ������
			jne SDVIG_LEFT

			movq qword ptr[esi + 8 * ecx], mm0		//��������� ������� � ������
			inc ecx									//������� � ����. �������� �������
			cmp ecx, 10								//�������� �� �����
			jne MUL_S
		}
	}

	printf("\nResult array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		if (A[i] == 0 && is_ok[i]) {
			printf("Overflow\n");
		}
		else {
			cout << A[i] << "\n";
		}
	}

	system("pause");
}

void div() {
	if (two_power != 0) {
		__asm {
			xor ecx, ecx							//�������� ������ �������
			lea esi, A								//���������� ����� �� ������ �������

		DIV_S :
			movq mm0, qword ptr[esi + 8 * ecx]		//���������� � ��� ������� �������

			mov eax, two_power						//���������� ������� ��� ������
		SDVIG:
			psrlq mm0, 1							//�������� ������� �� 1 ������
			dec eax									//��������� ������� �� 1
			cmp eax, 0								//�������� �� ������ ����� ������
			jne SDVIG

			movq qword ptr[esi + 8 * ecx], mm0		//��������� ������� � ������
			inc ecx									//������� � ����. �������� �������
			cmp ecx, 10								//�������� �� �����
			jne DIV_S
		}
	}

	
	printf("\nResult array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cout << A[i] << "\n";
	}

	system("pause");
}