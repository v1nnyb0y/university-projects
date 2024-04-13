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

string strA = "", strB = "";

string reverse(string str) {
	for (size_t i = 0; i < str.size(); i++)
	{
		str[i] = (str[i] == 'a') ? 'b' : 'a';
	}
	return str;
}

string GenerateString(int i, char start) {
	string sequence = "";
	sequence += start;
	while (sequence.size() < pow(2, i)) {
		sequence += reverse(sequence);
	}
	return sequence;
}

void Concat(int n) {
	int newN = n;
	string newStr = "";

	for (size_t i = 0; i < 16; ++i) {
		if ((newN & 1) != 1) {
			newStr += strA;
		}
		else {
			newStr += strB;
		}
		newN >>= 1;
	}
	cout << newStr << "\n";
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;

	int n;
	cin >> n;

	strA = GenerateString(6, 'a') + GenerateString(6, 'b');
	strB = GenerateString(6, 'b') + GenerateString(6, 'a');

	for (size_t i = 0; i < n; ++i) {
		Concat(i);
	}
	return 0;
}
