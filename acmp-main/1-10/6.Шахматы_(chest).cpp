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

bool check(string move) {
	basic_regex<char> basic;
	basic.assign("[A-H][1-8]-[A-H][1-8]");
	return regex_match(move.begin(), move.end(), basic);
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	string inp_s;
	cin >> inp_s;
	if (check(inp_s)) {
		if (inp_s[0] == inp_s[3] - 1 ||
			inp_s[0] == inp_s[3] + 1) {
			if (inp_s[1] == inp_s[4] + 2 ||
				inp_s[1] == inp_s[4] - 2) {
				cout << "YES";
				return 0;
			}
		}

		if (inp_s[1] == inp_s[4] - 1 ||
			inp_s[1] == inp_s[4] + 1) {
			if (inp_s[0] == inp_s[3] + 2 ||
				inp_s[0] == inp_s[3] - 2) {
				cout << "YES";
				return 0;
			}
		}

		cout << "NO";
	}
	else {
		cout << "ERROR";
	}
	return 0;
}
