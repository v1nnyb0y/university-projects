#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include "Comparison.h"

using namespace std;

static int result;
static long long A[10];
static long long B[10];

void compare() {
	printf("Insert first array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cin >> A[i];
	}

	printf("\nInsert second array > \n");
	for (int i = 0; i < 10; ++i) {
		printf("[%d]=", i + 1);
		cin >> B[i];
	}

	int task;
	printf("Insert check 1:(First == Second) or 2:(First > Second) or 3:(First < Second) or 4:exit > ");
	scanf("%d", &task);
	switch (task) {
	case 1: equal(); break;
	case 2: bigger(1); break;
	case 3: bigger(2); break;
	case 4: return;
	default: return;
	}
}

void equal() {
	result = -1;

	__asm {
		xor ecx, ecx								//�������� ������
		lea esi, A									//������� ������ �� ������ �
		lea edi, B									//������� ������ �� ������ �

	EQUAL:
		movq mm0, qword ptr [esi+8*ecx]				//�������� � ���������� MMX ������� ������� �
		pcmpeqd mm0, qword ptr [edi+8*ecx]			//�������� ������� ������� � � ��� (�� ���������)

		movd eax, mm0								//������� ��������� ���������
		cmp eax, 0									//���� ����, �� ��������� ��������
		je END

		inc ecx										//��������� ������
		cmp ecx, 10									//������� ������
		jne EQUAL

		mov result, 1								//���� ������� �����
	END:
	}

	result == 1 ? printf("TRUE\n") : printf("FALSE\n");

	system("pause");
}

void bigger(int number) {
	result = -1;
	switch (number) {
	case 1: {
		__asm {
			xor ecx, ecx							//�������� ������
			lea esi, A								//������� ������ �� ������ �
			lea edi, B								//������� ������ �� ������ �

		Bigger_1 :
			movq mm0, qword ptr[esi + 8 * ecx]		//�������� � ���������� ��� ������� ������� �
			pcmpgtd  mm0, qword ptr[edi + 8 * ecx]	//�������� ������� ������� � � ���

			movd eax, mm0							//������� ��������� ���������
			cmp eax, 0								//���� ����� � ������� � ��������� ������
			je END_1

			inc ecx									//��������� ������
			cmp ecx, 10								//������� ������
			jne Bigger_1

			mov result, 1
		END_1:
		}
	}
	case 2: {
		__asm {
			xor ecx, ecx							//�������� ������
			lea esi, A								//������� ������ �� ������ �
			lea edi, B								//������� ������ �� ������ �

		Bigger_2 :
			movq mm0, qword ptr[edi + 8 * ecx]		//�������� � ���������� ��� ������� ������� �
			pcmpgtd  mm0, qword ptr[esi + 8 * ecx]	//�������� ������� ������� � � ���

			movd eax, mm0							//������� ��������� ���������
			cmp eax, 0								//���� ����� � ������� � ��������� ������
			je END_2

			inc ecx									//��������� ������
			cmp ecx, 10								//������� ������
			jne Bigger_2

			mov result, 1
		END_2:
		}
	}
	}

	result == 1 ? printf("TRUE\n") : printf("FALSE\n");

	system("pause");
}