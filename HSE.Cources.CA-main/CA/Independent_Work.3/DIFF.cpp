#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

void diff() {
	static long long A[10];
	static long long B[10];
	static int AB_size = 10;

	printf("Insert first array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cin >> A[i];
	}

	printf("\nInsert second array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cin >> B[i];
	}

	int task;
	printf("\nInsert type of diff (0: without saturation; 1: with saturation; 2 for exit) > ");
	scanf("%d", &task);
	switch (task) {
	case 0: {
		__asm {
			lea esi, A									//������� ������ �� ������ �
			lea edi, B									//������� ������ �� ������ �
			xor ecx, ecx								//�������� ������

		SUM :
			movq mm0, qword ptr[esi + 8 * ecx]			//�������� � ���������� MMX ��0 ������ ����� �� ������� ecx �� ������� �
			psubq mm0, qword ptr[edi + 8 * ecx]			//�������� � ���������� MMX MM0 ������ ����� �� ������� ecx �� ������� �
			movq qword ptr[edi + 8 * ecx], mm0			//�������� ���������� ��0 � ������ � �� ������� ecx
			inc ecx										//������� � ����. ��������
			cmp ecx, 10									//�������� �� �����
			jne SUM
		}
		break;
	}
	case 1: {
		__asm {
			lea esi, A									//������� ������ �� ������ �
			lea edi, B									//������� ������ �� ������ �
			xor ecx, ecx								//�������� ������

		SUM_S :
			movq mm0, qword ptr[esi + 8 * ecx]			//�������� � ���������� MMX ��0 ������ ����� �� ������� ecx �� ������� �
			psubsw  mm0, qword ptr[edi + 8 * ecx]		//�������� � ���������� MMX MM0 ������ ����� �� ������� ecx �� ������� � � ����������
			movq qword ptr[edi + 8 * ecx], mm0			//�������� ���������� ��0 � ������ � �� ������� ecx
			inc ecx										//������� � ����. ��������
			cmp ecx, 10									//�������� �� �����
			jne SUM_S
		}
		break;
	}
	case 2:
		return;
	}



	printf("\nResult array > \n");
	for (int i = 0; i < AB_size; ++i) {
		printf("[%d]=", i + 1);
		cout << B[i] << "\n";
	}

	system("pause");
}