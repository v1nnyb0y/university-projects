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

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	string inp_digit;
	string pow;
	cin >> inp_digit;

	if (inp_digit.length() == 1) {
		cout << 25;
		return 0;
	}

	inp_digit.resize(inp_digit.length() - 1);
	int digit = atoi(inp_digit.c_str());
	pow = std::to_string((digit * (digit + 1))) + "25";

	cout << pow;
	return 0;
}
