#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>

using namespace std;

#undef seven_score
#undef  eight_score
#define   nine_score
#undef  ten_score

void task_2() {
	setlocale(LC_ALL, "RUS");	//���������� ������� ���� � �������
	static string inp;			//��������� �����
	static int machine_code[32];
	for(int i = 0;i<32;++i) {
		machine_code[i] = -1;
	}

	//Input inp
	printf("Input digit to translate > ");
	getline(cin, inp);

	static int inp_size;
	inp_size = inp.size();	//������ ��������� ������


	#ifdef seven_score

	//������ ������������ �������
	__asm {
		lea edi, inp			//��������� �� ������ �������
		add edi, 5				//������� �� ������ ������
		xor eax, eax			//��������� ���������� ��� �����
		mov ecx, inp_size		//���������� �������
		dec ecx					//���������� ������ ����� �� 1 ��������
		mov ebx, 1				//������ ����� ��� �������
		cmp ecx, 1				//���� 1 ����� � �����
		jz TO_DIGIT
		push ecx				//��������� ������ � �������� ���������� �����
		dec ecx					//��������� �� 1
		//���� �������� ���-�� ����� �������� � ��.
	KS:
		imul ebx, 10			//������������ ������
		dec ecx					//��������� �������
		cmp ecx, 0
		jnz KS

		pop ecx					//������� ������ ������ �����
		

		//���� �������� ����� � 10��
	TO_DIGIT:
		mov dl, [edi]			//������ ������
		sub dl, 48				//��������� �����
		imul edx, ebx			//������� ������� ����� � ��.
		add eax, edx			//��������� � ��������������� �����

		push eax				//��������� �����
		mov edx, 0				//�������� ������� �����
		mov eax, ebx			//���������� ��������
		mov ebx, 10				//���������� �������
		div ebx					//�������
		mov ebx, eax			//���������� ���������
		pop eax					//���������� �������� ����� (��������������)

		add edi, 1				//���������� �� 1 ������

		dec ecx					//��������� ������
		cmp ecx, 0				
		jnz TO_DIGIT

		xor ecx, ecx			//�������� ����������
		xor edx, edx			//�������� ����������
		mov ebx, 2				//������������� ��

		lea edi, inp			//������������� ������ ������
		add edi, 4				//������������� �������� �� ����
		cmp [edi], 0x2d			//�������� �� ������������� �����
		jz MINUS

		//���� (���� �������������) ����������� ����� � 2��
	TO_2CC:
		inc ecx					//����������� �������
		div ebx					//����� �� 2
		push edx				//���������� �������
		xor edx, edx			//�������� �������

		cmp eax, 0
		jnz TO_2CC

		lea edi, machine_code	//������������� ��������� �� ������ ������� (�����)
		push ecx				//���������� ������ 
		imul ecx, 4				//������� ������ ����� ������� ����� ������ ��� �����
		add edi, 128			//������������� ��������� �� ����� �������
		sub edi, ecx			//������������� ������� �� ����� ������� ��� ����� ��������� ���� ����������� ��������
		pop ecx					//���������� ���������� ��������
		mov eax, 0				//������������� ���������� �� ����
		jmp OUTPUT

		//���� (��� ��������������) ��������� ����� � 2�� (���������������)
	MINUS:
		inc ecx					//������� ���������� ��������
		div ebx					//������� 
		cmp edx, 0				//��������������
		jz CONVERT_ONE
		push 0					//�������� ����
		jmp CONT
	CONVERT_ONE:
		push 1					//�������� �������
	CONT:
		xor edx, edx			//�������� �������

		cmp eax, 0
		jnz MINUS

		mov eax, ecx			//���������� ���������� ��������
		lea edi, machine_code	//������������� �������� �� ������ �������
		add edi, 128			//������������� ������� �� ����� �������
		imul ecx, 4				//��������� ������ ����� �������� ������ � ������ ��� �������
		sub edi, ecx			//��������� �� ���� ������
		mov ecx, eax			//���������� ���������� ��������

		//���� ��� ������ ���������������� ����� � ������
	PLUS_ONE:
		pop ebx					//������� �������
		mov [edi], ebx			//������ � ������ �����
		add edi, 4				//���������� �� ����. ������� �������

		dec ecx					//��������� ���������� ����������� ���������
		cmp ecx, 0
		jnz PLUS_ONE

		mov ecx, eax			//���������� ���������� ��������
		lea edi, machine_code	//������������� �������� �� ������ �������
		add edi, 124			//������������� ��������� �� ����� �������

		//���� ��� ����������� ������� � ��������������� 2��
	EQUAL :
		cmp [edi], 0			//�������� �� ����
		jz ZERO

		mov dword ptr [edi], 0	//�������� ����
		sub edi, 4				//���������� ������� �����
		dec ecx					//��������� ����� ��� ������ ��� �����
		cmp ecx, 0
		jnz EQUAL

	ZERO:
		mov dword ptr [edi], 1	//�������� 1 (������� ���������)

		imul ecx, 4				//����� ������ ����� ������� ���� ����������� ��������� (��� ��� ��������������� �����)
		sub edi, ecx			//������� �� ���� ������	
		mov ecx, eax			//��������� ���������� ��������

	jmp TYPE_SETKA

		//���� ��� ���������� ������� (������������� �����)
	OUTPUT:
		inc eax			//����������� ���������� ����������� ��������� ����� � 2��
		pop ebx			//������� ����� �� �����
		mov [edi], ebx	//�������� ������� � ������-�����
		add edi, 4		//������� �� ����. ������� �������

		dec ecx			//���������� ������ ������ �� �������
		cmp ecx, 0
		jnz OUTPUT

		push eax		//��������� ���������� ����������� ���������
		inc eax			//����������� ������ ����� ������� ���� ��������� ����
		imul eax, 4		//����������� �����������
		sub edi, eax	//��������� �� ���� ������
		pop eax			//���������� ���������� ����������� ���������

	TYPE_SETKA:
		cmp eax, 8		//��� ����� � ������� ����� (���������)
		jz END

		cmp eax, 8		//��� ����� � ������� ����� (��������� ����������)
		jl _BYTE

		cmp eax, 16		//��� ����� � ������� ����� (���������)
		jz END

		cmp eax, 16		//��� ����� � ������� ����� (��������� ����������)
		jl _WORD

		cmp eax, 32		//��� ����� � ������� 2����� (���������)
		jz END

		cmp eax, 32		//��� ����� � ������� 2����� (��������� ����������)
		jl _2WORD

		//���� ��� ���������� ����� (�������������� ������ ���� ����, ��� ��� ��������� ����������� ������)
	_BYTE:
		mov ecx, 8		//��������� ���������� ��������� � �����
		sub ecx, eax	//����������� ������� �������� ���� ���
		push edi		//���������� ������ �������-�����
		lea edi, inp	//������������� ������ �� ������ ������� ��������
		add edi, 4		//������������� ������ �� ���� �����
		cmp [edi], 0x2d	//���� ����� �������������
		jz _MM1

		pop edi			//���������� ������

		//���� ��� ������������ ������ (��� �������������� �����)
	POSITIVE_BYTE:
		mov dword ptr [edi], 0	//�������� ����
		dec ecx					//��������� ������ ��� ������ �� �����
		sub edi, 4				//������� �� ������ ������� �������
		cmp ecx, 0
		jnz POSITIVE_BYTE
		jmp END

	_MM1:
		pop edi			//���������� ������

		//���� ��� ������������� ��������� (��� ������������� �����)
	NEGATIVE_BYTE:
		mov dword ptr [edi], 1	//��������� �������
		dec ecx					//��������� ������ ��� ������ �� �����
		sub edi, 4				//������� �� ������ ������� �������
		cmp ecx, 0				
		jnz NEGATIVE_BYTE
		jmp END

		//���� ��� ���������� �����
	_WORD:
		mov ecx, 16
		sub ecx, eax
		push edi
		lea edi, inp
		add edi, 4
		cmp [edi], 0x2d
		jz _MM2

		pop edi
	POSITIVE_WORD :
		mov dword ptr [edi], 0
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz POSITIVE_WORD
		jmp END

	_MM2 :
		pop edi
	NEGATIVE_WORD :
		mov dword ptr [edi], 1
		dec ecx
		sub edi, 4
		cmp ecx,0
		jnz NEGATIVE_WORD
		jmp END

		//���� ��� ���������� �������� �����
	_2WORD:
		mov ecx, 32
		sub ecx, eax
		push edi
		lea edi, inp
		add edi, 4
		cmp [edi], 0x2d
		jz _MM3

		pop edi
	POSITIVE_2WORD :
		mov dword ptr [edi], 0
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz POSITIVE_2WORD
		jmp END

	_MM3:
		pop edi
	NEGATIVE_2WORD :
		mov dword ptr [edi], 1
		dec ecx
		sub edi, 4
		cmp ecx, 0
		jnz NEGATIVE_2WORD
		jmp END
	END:
	}

	#endif

	#ifdef eight_score

	//������ ������������ �������
	__asm {
		lea edi, inp			//��������� �� ������ �������		
		add edi, 4				//������� �� ������ ������
		mov ecx, inp_size		//���������� �������
		dec ecx					//��������� ������� (��� ��� ������ ������ ����������� ��� �����)

		cmp [edi], 0x2b			//�������� �� ������������� ������
		jz CHECK_RIGHT_INPUT

		cmp [edi], 0x2d			//�������� �� ������������� ������
		jz CHECK_RIGHT_INPUT

		jmp END

		//���� ��� �������� ����� (�� ������� �������)
	CHECK_RIGHT_INPUT:
		inc edi					//��������� �� ��������� ������
		dec ecx					//��������� �������

		cmp[edi], 0x30			//�������� �� ������ 0
		jl END
		cmp[edi], 0x39			//�������� �� ������ 9
		jg END

		cmp ecx, 0
		jnz CHECK_RIGHT_INPUT

		lea edi, inp			//��������� �� ������ �������
		add edi, 5				//������� �� ������ ������
		xor eax, eax			//��������� ���������� ��� �����
		mov ecx, inp_size		//���������� �������
		dec ecx					//���������� ������ ����� �� 1 ��������
		mov ebx, 1				//������ ����� ��� �������
		cmp ecx, 1				//���� 1 ����� � �����
		jz TO_DIGIT
		push ecx				//��������� ������ � �������� ���������� �����
		dec ecx					//��������� �� 1
		//���� �������� ���-�� ����� �������� � ��.
		KS :
		imul ebx, 10			//������������ ������
			dec ecx					//��������� �������
			cmp ecx, 0
			jnz KS

			pop ecx					//������� ������ ������ �����


			//���� �������� ����� � 10��
			TO_DIGIT :
		mov dl, [edi]			//������ ������
			sub dl, 48				//��������� �����
			imul edx, ebx			//������� ������� ����� � ��.
			add eax, edx			//��������� � ��������������� �����

			push eax				//��������� �����
			mov edx, 0				//�������� ������� �����
			mov eax, ebx			//���������� ��������
			mov ebx, 10				//���������� �������
			div ebx					//�������
			mov ebx, eax			//���������� ���������
			pop eax					//���������� �������� ����� (��������������)

			add edi, 1				//���������� �� 1 ������

			dec ecx					//��������� ������
			cmp ecx, 0
			jnz TO_DIGIT

			xor ecx, ecx			//�������� ����������
			xor edx, edx			//�������� ����������
			mov ebx, 2				//������������� ��

			lea edi, inp			//������������� ������ ������
			add edi, 4				//������������� �������� �� ����
			cmp[edi], 0x2d			//�������� �� ������������� �����
			jz MINUS

			//���� (���� �������������) ����������� ����� � 2��
			TO_2CC :
		inc ecx					//����������� �������
			div ebx					//����� �� 2
			push edx				//���������� �������
			xor edx, edx			//�������� �������

			cmp eax, 0
			jnz TO_2CC

			lea edi, machine_code	//������������� ��������� �� ������ ������� (�����)
			push ecx				//���������� ������ 
			imul ecx, 4				//������� ������ ����� ������� ����� ������ ��� �����
			add edi, 128			//������������� ��������� �� ����� �������
			sub edi, ecx			//������������� ������� �� ����� ������� ��� ����� ��������� ���� ����������� ��������
			pop ecx					//���������� ���������� ��������
			mov eax, 0				//������������� ���������� �� ����
			jmp OUTPUT

			//���� (��� ��������������) ��������� ����� � 2�� (���������������)
			MINUS :
		inc ecx					//������� ���������� ��������
			div ebx					//������� 
			cmp edx, 0				//��������������
			jz CONVERT_ONE
			push 0					//�������� ����
			jmp CONT
			CONVERT_ONE :
		push 1					//�������� �������
			CONT :
			xor edx, edx			//�������� �������

			cmp eax, 0
			jnz MINUS

			mov eax, ecx			//���������� ���������� ��������
			lea edi, machine_code	//������������� �������� �� ������ �������
			add edi, 128			//������������� ������� �� ����� �������
			imul ecx, 4				//��������� ������ ����� �������� ������ � ������ ��� �������
			sub edi, ecx			//��������� �� ���� ������
			mov ecx, eax			//���������� ���������� ��������

			//���� ��� ������ ���������������� ����� � ������
			PLUS_ONE :
		pop ebx					//������� �������
			mov[edi], ebx			//������ � ������ �����
			add edi, 4				//���������� �� ����. ������� �������

			dec ecx					//��������� ���������� ����������� ���������
			cmp ecx, 0
			jnz PLUS_ONE

			mov ecx, eax			//���������� ���������� ��������
			lea edi, machine_code	//������������� �������� �� ������ �������
			add edi, 124			//������������� ��������� �� ����� �������

			//���� ��� ����������� ������� � ��������������� 2��
			EQUAL :
		cmp[edi], 0			//�������� �� ����
			jz ZERO

			mov dword ptr[edi], 0	//�������� ����
			sub edi, 4				//���������� ������� �����
			dec ecx					//��������� ����� ��� ������ ��� �����
			cmp ecx, 0
			jnz EQUAL

			ZERO :
		mov dword ptr[edi], 1	//�������� 1 (������� ���������)

			imul ecx, 4				//����� ������ ����� ������� ���� ����������� ��������� (��� ��� ��������������� �����)
			sub edi, ecx			//������� �� ���� ������	
			mov ecx, eax			//��������� ���������� ��������

			jmp TYPE_SETKA

			//���� ��� ���������� ������� (������������� �����)
			OUTPUT :
		inc eax			//����������� ���������� ����������� ��������� ����� � 2��
			pop ebx			//������� ����� �� �����
			mov[edi], ebx	//�������� ������� � ������-�����
			add edi, 4		//������� �� ����. ������� �������

			dec ecx			//���������� ������ ������ �� �������
			cmp ecx, 0
			jnz OUTPUT

			push eax		//��������� ���������� ����������� ���������
			inc eax			//����������� ������ ����� ������� ���� ��������� ����
			imul eax, 4		//����������� �����������
			sub edi, eax	//��������� �� ���� ������
			pop eax			//���������� ���������� ����������� ���������

			TYPE_SETKA :
		cmp eax, 8		//��� ����� � ������� ����� (���������)
			jz END

			cmp eax, 8		//��� ����� � ������� ����� (��������� ����������)
			jl _BYTE

			cmp eax, 16		//��� ����� � ������� ����� (���������)
			jz END

			cmp eax, 16		//��� ����� � ������� ����� (��������� ����������)
			jl _WORD

			cmp eax, 32		//��� ����� � ������� 2����� (���������)
			jz END

			cmp eax, 32		//��� ����� � ������� 2����� (��������� ����������)
			jl _2WORD

			//���� ��� ���������� ����� (�������������� ������ ���� ����, ��� ��� ��������� ����������� ������)
			_BYTE :
		mov ecx, 8		//��������� ���������� ��������� � �����
			sub ecx, eax	//����������� ������� �������� ���� ���
			push edi		//���������� ������ �������-�����
			lea edi, inp	//������������� ������ �� ������ ������� ��������
			add edi, 4		//������������� ������ �� ���� �����
			cmp[edi], 0x2d	//���� ����� �������������
			jz _MM1

			pop edi			//���������� ������

			//���� ��� ������������ ������ (��� �������������� �����)
			POSITIVE_BYTE :
		mov dword ptr[edi], 0	//�������� ����
			dec ecx					//��������� ������ ��� ������ �� �����
			sub edi, 4				//������� �� ������ ������� �������
			cmp ecx, 0
			jnz POSITIVE_BYTE
			jmp END

			_MM1 :
		pop edi			//���������� ������

		//���� ��� ������������� ��������� (��� ������������� �����)
			NEGATIVE_BYTE :
		mov dword ptr[edi], 1	//��������� �������
			dec ecx					//��������� ������ ��� ������ �� �����
			sub edi, 4				//������� �� ������ ������� �������
			cmp ecx, 0
			jnz NEGATIVE_BYTE
			jmp END

			//���� ��� ���������� �����
			_WORD :
		mov ecx, 16
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM2

			pop edi
			POSITIVE_WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_WORD
			jmp END

			_MM2 :
		pop edi
			NEGATIVE_WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_WORD
			jmp END

			//���� ��� ���������� �������� �����
			_2WORD :
		mov ecx, 32
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM3

			pop edi
			POSITIVE_2WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_2WORD
			jmp END

			_MM3 :
		pop edi
			NEGATIVE_2WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_2WORD
			jmp END
		END :
	}

	#endif

	#ifdef nine_score

	//������ ������������ �������
	__asm {
			lea edi, inp			//��������� �� ������ �������
			add edi, 5				//������� �� ������ ������
			xor eax, eax			//��������� ���������� ��� �����
			mov ecx, inp_size		//���������� �������
			dec ecx					//���������� ������ ����� �� 1 ��������
			mov ebx, 1				//������ ����� ��� �������
			cmp ecx, 1				//���� 1 ����� � �����
			jz TO_DIGIT
			push ecx				//��������� ������ � �������� ���������� �����
			dec ecx					//��������� �� 1

			//���� �������� ���-�� ����� �������� � ��.
		KS :
			cmp [edi], 0x20
			jz SKIP_SPACE
			imul ebx, 10			//������������ ������
		SKIP_SPACE:
			inc edi
			dec ecx					//��������� �������
			cmp ecx, 0
			jnz KS

			lea edi, inp
			add edi, 5
			pop ecx					//������� ������ ������ �����


			//���� �������� ����� � 10��
			TO_DIGIT :
		mov dl, [edi]			//������ ������
		cmp dl, 0x20
		jz SKIP_SPACE_PARSE
			sub dl, 48				//��������� �����
			imul edx, ebx			//������� ������� ����� � ��.
			add eax, edx			//��������� � ��������������� �����

			push eax				//��������� �����
			mov edx, 0				//�������� ������� �����
			mov eax, ebx			//���������� ��������
			mov ebx, 10				//���������� �������
			div ebx					//�������
			mov ebx, eax			//���������� ���������
			pop eax					//���������� �������� ����� (��������������)

		SKIP_SPACE_PARSE:
			add edi, 1				//���������� �� 1 ������

			dec ecx					//��������� ������
			cmp ecx, 0
			jnz TO_DIGIT

			xor ecx, ecx			//�������� ����������
			xor edx, edx			//�������� ����������
			mov ebx, 2				//������������� ��

			lea edi, inp			//������������� ������ ������
			add edi, 4				//������������� �������� �� ����
			cmp[edi], 0x2d			//�������� �� ������������� �����
			jz MINUS

			//���� (���� �������������) ����������� ����� � 2��
			TO_2CC :
		inc ecx					//����������� �������
			div ebx					//����� �� 2
			push edx				//���������� �������
			xor edx, edx			//�������� �������

			cmp eax, 0
			jnz TO_2CC

			lea edi, machine_code	//������������� ��������� �� ������ ������� (�����)
			push ecx				//���������� ������ 
			imul ecx, 4				//������� ������ ����� ������� ����� ������ ��� �����
			add edi, 128			//������������� ��������� �� ����� �������
			sub edi, ecx			//������������� ������� �� ����� ������� ��� ����� ��������� ���� ����������� ��������
			pop ecx					//���������� ���������� ��������
			mov eax, 0				//������������� ���������� �� ����
			jmp OUTPUT

			//���� (��� ��������������) ��������� ����� � 2�� (���������������)
			MINUS :
		inc ecx					//������� ���������� ��������
			div ebx					//������� 
			cmp edx, 0				//��������������
			jz CONVERT_ONE
			push 0					//�������� ����
			jmp CONT
			CONVERT_ONE :
		push 1					//�������� �������
			CONT :
			xor edx, edx			//�������� �������

			cmp eax, 0
			jnz MINUS

			mov eax, ecx			//���������� ���������� ��������
			lea edi, machine_code	//������������� �������� �� ������ �������
			add edi, 128			//������������� ������� �� ����� �������
			imul ecx, 4				//��������� ������ ����� �������� ������ � ������ ��� �������
			sub edi, ecx			//��������� �� ���� ������
			mov ecx, eax			//���������� ���������� ��������

			//���� ��� ������ ���������������� ����� � ������
			PLUS_ONE :
		pop ebx					//������� �������
			mov[edi], ebx			//������ � ������ �����
			add edi, 4				//���������� �� ����. ������� �������

			dec ecx					//��������� ���������� ����������� ���������
			cmp ecx, 0
			jnz PLUS_ONE

			mov ecx, eax			//���������� ���������� ��������
			lea edi, machine_code	//������������� �������� �� ������ �������
			add edi, 124			//������������� ��������� �� ����� �������

			//���� ��� ����������� ������� � ��������������� 2��
			EQUAL :
		cmp[edi], 0			//�������� �� ����
			jz ZERO

			mov dword ptr[edi], 0	//�������� ����
			sub edi, 4				//���������� ������� �����
			dec ecx					//��������� ����� ��� ������ ��� �����
			cmp ecx, 0
			jnz EQUAL

			ZERO :
		mov dword ptr[edi], 1	//�������� 1 (������� ���������)

			imul ecx, 4				//����� ������ ����� ������� ���� ����������� ��������� (��� ��� ��������������� �����)
			sub edi, ecx			//������� �� ���� ������	
			mov ecx, eax			//��������� ���������� ��������

			jmp TYPE_SETKA

			//���� ��� ���������� ������� (������������� �����)
			OUTPUT :
		inc eax			//����������� ���������� ����������� ��������� ����� � 2��
			pop ebx			//������� ����� �� �����
			mov[edi], ebx	//�������� ������� � ������-�����
			add edi, 4		//������� �� ����. ������� �������

			dec ecx			//���������� ������ ������ �� �������
			cmp ecx, 0
			jnz OUTPUT

			push eax		//��������� ���������� ����������� ���������
			inc eax			//����������� ������ ����� ������� ���� ��������� ����
			imul eax, 4		//����������� �����������
			sub edi, eax	//��������� �� ���� ������
			pop eax			//���������� ���������� ����������� ���������

			TYPE_SETKA :
		cmp eax, 8		//��� ����� � ������� ����� (���������)
			jz END

			cmp eax, 8		//��� ����� � ������� ����� (��������� ����������)
			jl _BYTE

			cmp eax, 16		//��� ����� � ������� ����� (���������)
			jz END

			cmp eax, 16		//��� ����� � ������� ����� (��������� ����������)
			jl _WORD

			cmp eax, 32		//��� ����� � ������� 2����� (���������)
			jz END

			cmp eax, 32		//��� ����� � ������� 2����� (��������� ����������)
			jl _2WORD

			//���� ��� ���������� ����� (�������������� ������ ���� ����, ��� ��� ��������� ����������� ������)
			_BYTE :
		mov ecx, 8		//��������� ���������� ��������� � �����
			sub ecx, eax	//����������� ������� �������� ���� ���
			push edi		//���������� ������ �������-�����
			lea edi, inp	//������������� ������ �� ������ ������� ��������
			add edi, 4		//������������� ������ �� ���� �����
			cmp[edi], 0x2d	//���� ����� �������������
			jz _MM1

			pop edi			//���������� ������

			//���� ��� ������������ ������ (��� �������������� �����)
			POSITIVE_BYTE :
		mov dword ptr[edi], 0	//�������� ����
			dec ecx					//��������� ������ ��� ������ �� �����
			sub edi, 4				//������� �� ������ ������� �������
			cmp ecx, 0
			jnz POSITIVE_BYTE
			jmp END

			_MM1 :
		pop edi			//���������� ������

		//���� ��� ������������� ��������� (��� ������������� �����)
			NEGATIVE_BYTE :
		mov dword ptr[edi], 1	//��������� �������
			dec ecx					//��������� ������ ��� ������ �� �����
			sub edi, 4				//������� �� ������ ������� �������
			cmp ecx, 0
			jnz NEGATIVE_BYTE
			jmp END

			//���� ��� ���������� �����
			_WORD :
		mov ecx, 16
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM2

			pop edi
			POSITIVE_WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_WORD
			jmp END

			_MM2 :
		pop edi
			NEGATIVE_WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_WORD
			jmp END

			//���� ��� ���������� �������� �����
			_2WORD :
		mov ecx, 32
			sub ecx, eax
			push edi
			lea edi, inp
			add edi, 4
			cmp[edi], 0x2d
			jz _MM3

			pop edi
			POSITIVE_2WORD :
		mov dword ptr[edi], 0
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz POSITIVE_2WORD
			jmp END

			_MM3 :
		pop edi
			NEGATIVE_2WORD :
		mov dword ptr[edi], 1
			dec ecx
			sub edi, 4
			cmp ecx, 0
			jnz NEGATIVE_2WORD
			jmp END
			END :
	}

	#endif

	printf("Machine code > ");
	for(int i = 0;i < 32; ++i) {
		if (machine_code[i] != -1) {
			printf("%d", machine_code[i]);
		}
	}

	printf("\n");
	system("pause");
}