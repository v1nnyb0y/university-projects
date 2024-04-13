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
string currStr, findStr;
vector<int> index;

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

void find() {
	size_t n = findStr.length();
	size_t m = currStr.length();
	string tmp = findStr + "#" + currStr;
	vector<int>prefix_tmp = prefix_function(tmp);
	for (size_t i = n + 1; i < prefix_tmp.size(); ++i) {
		if (prefix_tmp[i] == n) {
			index.push_back(i - 2 * n + 1);
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
	//edx_io io;

	cin >> findStr >> currStr;
	find();
	cout << index.size() << "\n";
	for (size_t i = 0; i < index.size(); ++i) {
		cout << index[i] << " ";
	}
	return 0;
}
