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

class my_stack
{
	vector<int> arr;
	int top;

public:
	my_stack() {
		top = 0;
		arr.resize(n);
	}

	void pop() {
		if (top != 0) {
			top--;
			printf("%d\n", arr[top]);
		}
	}

	void Add(int x) {
		if (top + 1 != n)
			arr[top++] = x;
	}
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	scanf("%d", &n);
	my_stack sInt;
	for (int i = 0; i < n; i++) {
		char str;
		scanf("\n%c", &str);
		if (str == '-') {
			sInt.pop();
		}
		else {
			int temp;
			scanf("%d", &temp);
			sInt.Add(temp);
		}
	}
}
