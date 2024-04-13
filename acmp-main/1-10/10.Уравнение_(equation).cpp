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
#include <cassert>
#include <cctype>
#include <regex>

using namespace std;

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	int a, b, c, d;
	scanf("%d %d %d %d", &a, &b, &c, &d);

	for (int i = -100; i <= 100; ++i) {
		if (pow(i, 3)*a + pow(i, 2)*b + i * c + d == 0)
			printf("%d ", i);
	}
	return 0;
}
