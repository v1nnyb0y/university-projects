#define _CRT_SECURE_NO_WARNINGS
#include "what_task_doing.h"
#include <cstdio>

using namespace std;

int main() {
	int task;

	while (true) {
		printf("Input task number (13 for exit) > ");
		scanf("%d", &task);
		switch (task) {
		case 1: task_1(); break;
		case 2: task_2(); break;
		case 3: task_3(); break;
		case 4: task_4(); break;
		case 5: task_5(); break;
		case 6: task_6(); break;
		case 7: task_7(); break;
		case 8: task_8(); break;
		case 9: task_9(); break;
		case 10: task_10(); break;
		case 11: task_11(); break;
		case 12: task_12(); break;
		default: return 0;
		}
	}
}
