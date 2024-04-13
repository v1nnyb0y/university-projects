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

string compare(string first, string second) {
	if (first.length() < second.length()) {
		return second;
	}

	if (first.length() > second.length()) {
		return first;
	}

	for (size_t i = 0; i < first.length(); ++i) {
		if (first[i] > second[i]) {
			return first;
		}

		if (first[i] < second[i]) {
			return second;
		}
	}
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	string first, second, third;
	cin >> first >> second >> third;
	first = compare(first, second);
	first = compare(first, third);
	cout << first;
	return 0;
}
