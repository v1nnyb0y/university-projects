#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#undef seven_score
#undef  eight_score
#define  nine_score
#undef  ten_score

void task_3() {
	setlocale(LC_ALL, "RUS");	//���������� ������� ���� � �������
	static string inp;			//��������� �����
	static int machine_code[32];
	for (auto& i : machine_code) {
		i = -1;
	}
	static int is_minus = 0;

	//Input inp
	printf("Input machine code to translate > ");
	getline(cin, inp);

	static int inp_size;
	inp_size = inp.size();	//������ ��������� ������
	static int parsed_digit[10];

	for (auto& i : parsed_digit) {
		i = -1;
	}

#ifdef seven_score

//������ ������������ �������
	__asm {
		lea edi, inp	//���������� ������ �� ������ �����
		add edi, 4		//���������� ������ �� ������ �������
		xor ecx, ecx	//�������� �������

		mov dl, [edi]			//������ ������
		sub dl, 48				//��������� �����

		//����� ������������� ��� �������������
		cmp edx, 0
		je TO_ARRAY

		//����� ��������� �������������
		mov is_minus, 1

		//���� ��� ������ ����� � ������
	TO_ARRAY:
		mov dl, [edi]			//������ ������
		push edi				//���������� ������ � ������
		sub dl, 48				//��������� �����
		lea edi, machine_code	//������ ���� ���������� ��������������� �����

		mov [edi + 4*ecx], edx	//���������� �����

		//����������� �����
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

		//���� ��� ���������� ������� �������
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

	//������ ������������ �������
	__asm {
		lea edi, inp	//���������� ������ �� ������ �����
		add edi, 4		//���������� ������ �� ������ �������
		xor ecx, ecx	//�������� �������

		mov dl, [edi]			//������ ������
		sub dl, 48				//��������� �����

		//����� ������������� ��� �������������
		cmp edx, 0
		je TO_ARRAY

		//����� ��������� �������������
		mov is_minus, 1

		//���� ��� ������ ����� � ������
		TO_ARRAY:
		mov dl, [edi]			//������ ������
			push edi				//���������� ������ � ������
			sub dl, 48				//��������� �����
			lea edi, machine_code	//������ ���� ���������� ��������������� �����

			mov[edi + 4 * ecx], edx	//���������� �����

			//����������� �����
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

			//���� ��� ���������� ������� �������
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

	//������ ������������ �������
	__asm {
		lea edi, inp	//���������� ������ �� ������ �����
		add edi, 4		//���������� ������ �� ������ �������
		xor ecx, ecx	//�������� �������

		mov dl, [edi]			//������ ������
		sub dl, 48				//��������� �����

		//����� ������������� ��� �������������
		cmp edx, 0
		je TO_ARRAY

		//����� ��������� �������������
		mov is_minus, 1

		//���� ��� ������ ����� � ������
		TO_ARRAY:
		mov dl, [edi]			//������ ������
			push edi				//���������� ������ � ������
			sub dl, 48				//��������� �����
			lea edi, machine_code	//������ ���� ���������� ��������������� �����

			mov[edi + 4 * ecx], edx	//���������� �����

			//����������� �����
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

			//���� ��� ���������� ������� �������
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
	//����� �� �����, ������� � ��.
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