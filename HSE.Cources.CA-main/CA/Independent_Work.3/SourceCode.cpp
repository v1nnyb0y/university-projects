#define _CRT_SECURE_NO_WARNINGS
#include <cstdio>
#include "what_task_doing.h"

using namespace std;

int main() {
	int task;
	while (true) {
		printf("Input task number (1: add; 2: sub; 3: mul/div; 4: =|>|<; 5 for exit) > ");
		scanf("%d", &task);
		switch (task) {
		case 1: sum(); break;
		case 2: diff(); break;
		case 3: power(); break;
		case 4: compare(); break;
		case 5: return 0;
		default: return 0;
		}
	}
}
