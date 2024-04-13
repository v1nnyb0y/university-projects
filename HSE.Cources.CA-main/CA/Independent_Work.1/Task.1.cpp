#define _CRT_SECURE_NO_WARNINGS
#include <iostream>

using namespace std;

#undef seven_score
#undef eight_score
#undef  nine_score
#define  ten_score

void task_1() {	
#ifdef seven_score

	static int find_digit_string = -1;	//������ �������� ����� ������
	static int find_digit_column = -1;	//������ ���������� ����� �������

	setlocale(LC_ALL, "RUS");	//���������� ������� ����

	static int arr[5][5];	//���������� �������

	//���������� �������
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//������ ������������ �������
	__asm {
		lea edi, arr	//���������� ������ �������
		mov ecx, 0	//������ �� ������
		mov edx, 0	//������ �� �������

	NEW_STRING:

		mov eax, 2147483647	//���������� ������������ �������� ��� ������ ��������
	NEW_COLUMN:
		cmp eax, [edi + edx * 4]	//��������� �������� �������� � �����������
		jle OLD_MIN

		mov eax, [edi + edx * 4]	//��������� ����� �������

	OLD_MIN:
		inc edx		//��������� ������ ������
		cmp edx, 5		//������ �� ��� �������?
		jl NEW_COLUMN

		push eax		//��������� �������
		xor edx, edx	//�������� ������
		push ecx		//��������� ������
		xor ecx, ecx	//�������� ����������

	CHECK_EVERY_MIN:
		cmp eax, [edi + edx * 4]	//���� ��� ���������� ��������
		jz FIND_MAX_COLUMN	//������� �� ����� ������������� �������� � �������

	CONTINUE_CHECK:
		inc edx		//��������� �� 1 ������
		cmp edx, 5	//������� �� ����. �������
		jl CHECK_EVERY_MIN	

		pop ecx		//������� ������ ������
		inc ecx		//��������� ������
		add edi, 20		//������� �� ����� ������
		xor edx, edx	//�������� ������
		pop eax		//������� ����

		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING

		jmp END_WITHOUT		//���� �� �������?

	FIND_MAX_COLUMN:
		mov eax, -2147483648	//���������� ����������� �������� ��� ������ ���������
		lea edi, arr	//���������� ������ �������

	NEW_STRING_FIND_MAX:
		cmp eax, [edi + edx * 4]	//��������� �������� ��������� � �����������
		jge OLD_MAX

		mov eax, [edi + edx * 4]	//��������� ����� ��������

	OLD_MAX:
		inc ecx		//����������� ������ ������
		add edi, 20	//������� �� ����� ������
		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING_FIND_MAX

	lea edi, arr	//���������� ������ �������	
	xor ecx, ecx	//�������� ����������
	CHECK_EVERY_MAX:
		cmp eax, [edi + edx * 4]	//����������� ��������
		jnz NON_COMPARE_STRINGS

		pop ebx		//������� ������ ������ 
		cmp ecx, ebx		//�������� ������ ������ ������������� � ������������
		jz END		//��������� ���������
		push ebx	//��������� ������

	NON_COMPARE_STRINGS:
		inc ecx		//�������� ������ ������ �� 1
		add edi, 20		//������� �� ����. ������
		cmp ecx, 5		//������ �� ��� ������?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		mov eax, 5	//������������� �������� ����������� ������� (������)
		sub eax, ebx	//������������� ���������� �������� �� ������ ������
		imul eax, 20	//������� �����, ����� ��������� �����
		sub edi, eax	//������������ �����
		pop eax		//������� ����
		pop eax		//���������� �������
		push eax	//���������� �������
		push ebx	//��������� ������ ������ ��������

		jmp CONTINUE_CHECK

	END:
		pop eax		//������� ����
		mov find_digit_column, edx		//������� ������ �������
		mov find_digit_string, ecx		//������� ������ ������

	END_WITHOUT:

	}

#endif

#ifdef eight_score

	static int find_digit_string = -1;	//������ �������� ����� ������
	static int find_digit_column = -1;	//������ ���������� ����� �������

	setlocale(LC_ALL, "RUS");	//���������� ������� ����

	static int arr[5][5];	//���������� �������

	//���������� �������
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//������ ������������ �������
	__asm {
		lea edi, arr	//���������� ������ �������
		mov ecx, 0	//������ �� ������
		mov edx, 0	//������ �� �������

	NEW_STRING:
		push ecx	//��������� ������� ������
		mov ecx, 5	//�������� ������ ��� ������� ��� ����� loop

		mov eax, 2147483647	//���������� ������������ �������� ��� ������ ��������
	NEW_COLUMN :
		cmp eax, [edi + ecx * 4]	//��������� �������� �������� � �����������
		jl OLD_MIN

		mov eax, [edi + ecx * 4]	//��������� ����� �������

	OLD_MIN:
		loop NEW_COLUMN

		pop ecx		//������� �������� ������

		push eax		//��������� �������
		push ecx		//��������� ������
		xor ecx, ecx	//�������� ����������

	CHECK_EVERY_MIN :
		cmp eax, [edi + edx * 4]	//���� ��� ���������� ��������
		jz FIND_MAX_COLUMN	//������� �� ����� ������������� �������� � �������

	CONTINUE_CHECK :
		inc edx		//��������� �� 1 ������
		cmp edx, 5	//������� �� ����. �������
		jl CHECK_EVERY_MIN

		pop ecx		//������� ������ ������
		inc ecx		//��������� ������
		add edi, 20		//������� �� ����� ������
		xor edx, edx	//�������� ������
		pop eax		//������� ����

		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING

		jmp END_WITHOUT		//���� �� �������?

	FIND_MAX_COLUMN :
		mov eax, -2147483648	//���������� ����������� �������� ��� ������ ���������
		lea edi, arr	//���������� ������ �������

	NEW_STRING_FIND_MAX :
		cmp eax, [edi + edx * 4]	//��������� �������� ��������� � �����������
		jge OLD_MAX

		mov eax, [edi + edx * 4]	//��������� ����� ��������

	OLD_MAX :
		inc ecx		//����������� ������ ������
		add edi, 20	//������� �� ����� ������
		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING_FIND_MAX

		lea edi, arr	//���������� ������ �������	
		xor ecx, ecx	//�������� ����������
	CHECK_EVERY_MAX :
		cmp eax, [edi + edx * 4]	//����������� ��������
		jnz NON_COMPARE_STRINGS

		pop ebx		//������� ������ ������ 
		cmp ecx, ebx		//�������� ������ ������ ������������� � ������������
		jz END		//��������� ���������
		push ebx	//��������� ������

	NON_COMPARE_STRINGS :
		inc ecx		//�������� ������ ������ �� 1
		add edi, 20		//������� �� ����. ������
		cmp ecx, 5		//������ �� ��� ������?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		mov eax, 5	//������������� �������� ����������� ������� (������)
		sub eax, ebx	//������������� ���������� �������� �� ������ ������
		imul eax, 20	//������� �����, ����� ��������� �����
		sub edi, eax	//������������ �����
		pop eax		//������� ����
		pop eax		//���������� �������
		push eax	//���������� �������
		push ebx	//��������� ������ ������ ��������

		jmp CONTINUE_CHECK

	END :
		pop eax		//������� ����
		mov find_digit_column, edx		//������� ������ �������
		mov find_digit_string, ecx		//������� ������ ������

	END_WITHOUT :
	}

#endif

#ifdef nine_score

	static int find_digit_string = -1;	//������ �������� ����� ������
	static int find_digit_column = -1;	//������ ���������� ����� �������

	setlocale(LC_ALL, "RUS");	//���������� ������� ����

	static int arr[5][5];	//���������� �������

	//���������� �������
	for (size_t i = 0; i < 5; ++i) {
		for (size_t j = 0; j < 5; ++j) {
			cout << "Input an element of matrix > ";
			cin >> arr[i][j];
		}
	}

	//������ ������������ �������
	__asm {
		mov ecx, 0	//������ �� ������
		mov edx, 0	//������ �� �������

	NEW_STRING:
		mov ebx, 0
		mov eax, 2147483647	//���������� ������������ �������� ��� ������ ��������
	NEW_COLUMN :
		cmp eax, arr[edx * 4]	//��������� �������� �������� � �����������
		jle OLD_MIN

		mov eax, arr[edx * 4]	//��������� ����� �������

	OLD_MIN :
		inc ebx		//�������� ����������� ��� �����
		inc edx		//��������� ������ ������
		cmp ebx, 5		//������ �� ��� �������?
		jl NEW_COLUMN

		push eax		//��������� �������
		sub edx, 5		//������������ ������� �� ������ ������
		push ecx		//��������� ������
		xor ecx, ecx	//�������� ����������
		xor ebx, ebx	//�������� ����������

	CHECK_EVERY_MIN :
		cmp eax, arr[edx * 4]	//���� ��� ���������� ��������
		jz FIND_MAX_COLUMN	//������� �� ����� ������������� �������� � �������

	CONTINUE_CHECK :
		inc ebx		//�������� ����������� ��� �����
		inc edx		//��������� �� 1 ������
		cmp ebx, 5	//������� �� ����. �������
		jl CHECK_EVERY_MIN

		pop ecx		//������� ������ ������
		inc ecx		//��������� ������
		xor edx, edx		//�������� ���������
		add edx, 5		//������� �� ����� ������
		imul edx, ecx	//����������� ���������� ��� ��������
		pop eax		//������� ����

		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING

		jmp END_WITHOUT		//���� �� �������?

	FIND_MAX_COLUMN :
		pop ecx		//���������� ������� ������
		mov eax, ecx		//��������� � ������ ����������
		push ecx		//���������� ������
		imul eax, 5		//����������� ��������
		push edx		//���������� ������ ��� ������� �� ������
		sub edx, eax		//����������� ������ ��� ������� �� ��������
		mov eax, -2147483648	//���������� ����������� �������� ��� ������ ���������

	NEW_STRING_FIND_MAX :
		cmp eax, arr[edx * 4]	//��������� �������� ��������� � �����������
		jge OLD_MAX

		mov eax, arr[edx * 4]	//��������� ����� ��������

	OLD_MAX :
		inc ecx		//����������� ������ ������
		add edx, 5	//������� �� ����� ������
		cmp ecx, 5	//������ �� ��� ������?
		jl NEW_STRING_FIND_MAX

		pop edx		//���������� ������ ������ (������ ��������)
		pop ecx		//���������� ������� ������
		mov ebx, ecx	//��������� ������ ������ � ������ ����������
		imul ebx, 5		//����������� ��������
		push edx		//���������� ������ ������ (������ ��������)
		sub edx,  ebx		//����������� ������ ������ ��� �����
		push ecx		//���������� ������
		xor ecx, ecx	//�������� ����������

	CHECK_EVERY_MAX :
		cmp eax, arr[edx * 4]	//����������� ��������
		jnz NON_COMPARE_STRINGS

		pop ebx		//������� ������ ������ 
		cmp ecx, ebx		//�������� ������ ������ ������������� � ������������
		jz END		//��������� ���������
		push ebx	//��������� ������

	NON_COMPARE_STRINGS :
		inc ecx		//�������� ������ ������ �� 1
		add edx, 5		//������� �� ����. ������
		cmp ecx, 5		//������ �� ��� ������?
		jl CHECK_EVERY_MAX

		xor ecx, ecx
		pop eax		//������� ����
		pop edx		//������� ������ � ������ ��� �����������
		pop eax		//���������� �������
		push eax	//���������� �������
		push ebx	//��������� ������ ������ ��������

		jmp CONTINUE_CHECK

	END :
		pop eax		//������� ����
		pop eax		//������� ����
		mov eax, ecx	//���������� ������
		imul eax, 5		//�������� ������ �� 5 (������ ��������)
		sub edx, eax	//�������� �������� ������
		mov find_digit_column, edx		//������� ������ �������
		mov find_digit_string, ecx		//������� ������ ������

	END_WITHOUT :

	}

#endif

#ifdef ten_score

	static unsigned int find_digit_string = -1;	//������ �������� ����� ������
	static unsigned int find_digit_column = -1;	//������ ���������� ����� �������

	setlocale(LC_ALL, "RUS");	//���������� ������� ����

	static unsigned int arr[5][5];	//���������� �������

	//���������� �������
	for (auto& i : arr) {
		for (auto& j : i) {
			cout << "Input an element of matrix > ";
			cin >> j;
		}
	}


	//������ ������������ �������
	__asm {
		mov al, 0
		sub al, 0
		lea edi, arr	//���������� ������ �������
		mov ecx, 0	//������ �� ������
		mov edx, 0	//������ �� �������

		NEW_STRING:

		mov eax, 2147483647	//���������� ������������ �������� ��� ������ ��������
			NEW_COLUMN :
			cmp eax, [edi + edx * 4]	//��������� �������� �������� � �����������
			jle OLD_MIN

			mov eax, [edi + edx * 4]	//��������� ����� �������

			OLD_MIN :
			inc edx		//��������� ������ ������
			cmp edx, 5		//������ �� ��� �������?
			jl NEW_COLUMN

			push eax		//��������� �������
			xor edx, edx	//�������� ������
			push ecx		//��������� ������
			xor ecx, ecx	//�������� ����������

			CHECK_EVERY_MIN :
		cmp eax, [edi + edx * 4]	//���� ��� ���������� ��������
			jz FIND_MAX_COLUMN	//������� �� ����� ������������� �������� � �������

			CONTINUE_CHECK :
		inc edx		//��������� �� 1 ������
			cmp edx, 5	//������� �� ����. �������
			jl CHECK_EVERY_MIN

			pop ecx		//������� ������ ������
			inc ecx		//��������� ������
			add edi, 20		//������� �� ����� ������
			xor edx, edx	//�������� ������
			pop eax		//������� ����

			cmp ecx, 5	//������ �� ��� ������?
			jl NEW_STRING

			jmp END_WITHOUT		//���� �� �������?

			FIND_MAX_COLUMN :
		mov eax, -2147483648	//���������� ����������� �������� ��� ������ ���������
			lea edi, arr	//���������� ������ �������

			NEW_STRING_FIND_MAX :
		cmp eax, [edi + edx * 4]	//��������� �������� ��������� � �����������
			jge OLD_MAX

			mov eax, [edi + edx * 4]	//��������� ����� ��������

			OLD_MAX :
			inc ecx		//����������� ������ ������
			add edi, 20	//������� �� ����� ������
			cmp ecx, 5	//������ �� ��� ������?
			jl NEW_STRING_FIND_MAX

			lea edi, arr	//���������� ������ �������	
			xor ecx, ecx	//�������� ����������
			CHECK_EVERY_MAX :
		cmp eax, [edi + edx * 4]	//����������� ��������
			jnz NON_COMPARE_STRINGS

			pop ebx		//������� ������ ������ 
			cmp ecx, ebx		//�������� ������ ������ ������������� � ������������
			jz END		//��������� ���������
			push ebx	//��������� ������

			NON_COMPARE_STRINGS :
		inc ecx		//�������� ������ ������ �� 1
			add edi, 20		//������� �� ����. ������
			cmp ecx, 5		//������ �� ��� ������?
			jl CHECK_EVERY_MAX

			xor ecx, ecx
			mov eax, 5	//������������� �������� ����������� ������� (������)
			sub eax, ebx	//������������� ���������� �������� �� ������ ������
			imul eax, 20	//������� �����, ����� ��������� �����
			sub edi, eax	//������������ �����
			pop eax		//������� ����
			pop eax		//���������� �������
			push eax	//���������� �������
			push ebx	//��������� ������ ������ ��������

			jmp CONTINUE_CHECK

			END :
		pop eax		//������� ����
			mov find_digit_column, edx		//������� ������ �������
			mov find_digit_string, ecx		//������� ������ ������

			END_WITHOUT :

	}

#endif

	//����� �������
	if (find_digit_string == -1) {
		cout << 0;
	}else {
		cout << "Found digit > [" << find_digit_string + 1 << ";" << find_digit_column + 1 << "]";
	}


	find_digit_string = -1;	//������ �������� ����� ������
	find_digit_column = -1;	//������ ���������� ����� �������

	cout << endl;
	system("pause");
}