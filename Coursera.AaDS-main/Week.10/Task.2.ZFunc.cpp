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
//#include "edx-io.hpp"
using namespace std;
string currStr;

vector<int> z_function(string str) {
	size_t n = str.length();
	vector<int> z(n);
	for (size_t i = 1, left = 0, right = 0; i < n; ++i) {
		if (i <= right)
			z[i] = min((int)(right - i + 1), z[i - left]);
		while (i + z[i] < n && str[z[i]] == str[i + z[i]])
			++z[i];
		if (i + z[i] - 1 > right)
			left = i, right = i + z[i] - 1;
	}

	return z;
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;

	cin >> currStr;
	vector<int> z = z_function(currStr);

	for (size_t i = 1; i < z.size(); ++i) {
		cout << z[i] << " ";
	}
	return 0;
}
