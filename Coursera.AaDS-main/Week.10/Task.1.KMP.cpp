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

vector<int> prefix_function(string str) {
	size_t n = str.length();
	vector<int> pi(n);
	for (size_t i = 1; i < n; ++i) {
		size_t j = pi[i - 1];
		while (j > 0 && str[i] != str[j])
			j = pi[j - 1];
		if (str[i] == str[j])
			++j;
		pi[i] = j;
	}
	return pi;
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
	vector<int> prefix = prefix_function(currStr);

	for (size_t i = 0; i < prefix.size(); ++i) {
		cout << prefix[i] << " ";
	}
	return 0;
}
