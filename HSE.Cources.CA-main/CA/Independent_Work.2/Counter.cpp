#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

void count_e() {
	static double X, E, result;
	printf("Insert epsilon > ");
	scanf("%lf", &E);

	if (E <= 0) {
		printf("Wrong epsilon\n");
		return;
	}

	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//���������������� FPU
		xor ecx, ecx			//�������� �����

	COUNTER:
		sahf					//��������� �����
		inc ecx					//������� � ����. ������
		push ecx				//�������� � ���� ������� N

		fld X					//�������� ����� � st(0)

		fimul dword ptr[esp]	//�������� st(0) = st(0) * src
		fcos					//���������� st(0) = cos(st(0))

		fidiv dword ptr[esp]	//�������� st(0) = st(0)/src

		fld st(0)				//��������� ����������� ����� �� N ����

		fadd result				//����� st(0) = st(0) + result
		fstp result				//��������� st(0) � result

		fabs					//����� ����� �� ������ ��� ��������� ��������
		fstsw ax				//��������� �����

		pop ecx					//��������� ecx
		fcomp E					//���������� st(0) � �
		jae COUNTER
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}

void count_n() {
	static double X, result;
	static int N;
	result = 0;
	printf("Insert N > ");
	scanf("%d", &N);

	if (N <= 0) {
		printf("Wrong N\n");
		return;
	}

	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//���������������� FPU
		mov ecx, N				//���������� N ����� (����������)

	COUNTER:
		push ecx				//�������� � ���� ������� N

		fld  X					//�������� ����� � st(0)
		
		fimul dword ptr [esp]	//�������� st(0) = st(0) * src
		fcos					//���������� st(0) = cos(st(0))

		fidiv dword ptr [esp]	//�������� st(0) = st(0)/src

		fadd result				//����� st(0) = st(0) + result
		fstp result				//��������� st(0) � result

		pop ecx
		loop COUNTER
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}

void rec() {
	static double X, result;
	result = 0;
	printf("Insert X > ");
	scanf("%lf", &X);

	__asm {
		finit					//���������������� FPU
			
		mov ecx, 2				//�������� ���������
		push ecx				//�������� �� � ����

		FLD1					//�������� �� ����� st(0) 1.0
		fld  X					//�������� ����� � st(0)

		fidiv dword ptr[esp]	//������� st(0) = st(0)/2
		fsin					//���������� st(0) = sin(st(0))
		fimul dword ptr[esp]	//������������ st(0) = 2*st(0)
		fabs					//�������� ���� �� �������������, ���� ������������� � st(0)

		fyl2x					//��������� �������� �� ��������� 2 �� st(1) � �������� �� st(0)
		fldln2					//�������� ����������� �������� � st(0)
		fmul					//�������� st(1) �� st(0)
			
		fchs					//�������� ���� st(0)

		pop ecx					//�������� ����
		fstp result				//������� ���������
	}

	printf("Answer > %.5lf\n", result);
	system("pause");
}