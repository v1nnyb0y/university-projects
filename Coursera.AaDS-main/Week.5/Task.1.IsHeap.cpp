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
#include <unordered_map>
using namespace std;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	setlocale(LC_ALL, "Russian");
	//ifstream input("input.txt");
	int n;
	scanf("%d\n", &n);
	vector<int> vBunch(n);
	for (int i = 0; i < n; i++) {
		scanf("%d ", &vBunch[i]);
	}
	for (int i = 0; i < n; i++) {
		if (2 * i + 1 < n) {
			if (vBunch[i] > vBunch[2 * i + 1]) {
				printf("NO");
				return 0;
			}
		}
		if (2 * i + 2 < n) {
			if (vBunch[i] > vBunch[2 * i + 2]) {
				printf("NO");
				return 0;
			}
		}
	}
	printf("YES");
	return 0;
}
