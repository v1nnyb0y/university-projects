#define _CRT_SECURE_NO_WARNINGS
#include <cstdio>
#include "what_task_doing.h"

using namespace std;

int main() {
	int task;
	while (true) {
		printf("Input task number (1: rec; 2: input n; 3: epsilon; 4 for exit) > ");
		scanf("%d", &task);
		switch (task) {
		case 1: rec(); break;
		case 2: count_n(); break;
		case 3: count_e(); break;
		case 4: return 0;
		default: return 0;
		}
	}
}
