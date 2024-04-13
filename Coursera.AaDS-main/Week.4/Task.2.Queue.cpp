#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <queue>
#include <string>
#include <map>
#include <set>
#include <stdio.h>
#include <iomanip>
#include <deque>
#include <stack>
using namespace std;

int n;

#define QMAX 1000000

struct Queue
{
	int qu[QMAX];
	int rear, frnt;

	Queue() {
		rear = 0;
		frnt = 1;
	}

	void push(int x) {
		if (rear < QMAX - 1) {
			rear++;
			qu[rear] = x;
		}
	}

	int is_empty() {
		if (rear < frnt) return (1);
		return (0);
	}

	void pop() {
		int x;
		if (is_empty() == 1) {
			return;
		}
		x = qu[frnt];
		frnt++;
		printf("%d\n", x);
	}
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	scanf("%d", &n);
	Queue qInt;
	for (int i = 0; i < n; i++) {
		char str;
		scanf("\n%c", &str);
		if (str == '-') {
			qInt.pop();
		}
		else {
			int temp;
			scanf("%d", &temp);
			qInt.push(temp);
		}
	}
}
