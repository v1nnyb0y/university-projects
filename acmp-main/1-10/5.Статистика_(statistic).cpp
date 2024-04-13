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

void outp(vector<int> outp_v) {
	for (int digit : outp_v) {
		printf("%d ", digit);
	}
	printf("\n");
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	vector<int> even;
	vector<int> odd;
	int n;
	scanf("%d\n", &n);
	for (int i = 0; i < n; ++i) {
		int digit;
		scanf("%d ", &digit);
		if (digit % 2 == 0)
			even.push_back(digit);
		else
			odd.push_back(digit);
	}

	outp(odd);
	outp(even);
	printf((odd.size() > even.size()) ? "NO" : "YES");

	return 0;
}
