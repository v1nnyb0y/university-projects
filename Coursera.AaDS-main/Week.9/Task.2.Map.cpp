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
using namespace std;

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	string key;
	getline(cin, key);
	long long k[26];
	long long b[26];
	long long count = 0;
	for (int i = 0; i < 26; i++) {
		k[i] = 0;
		b[i] = 0;
	}
	int i = 0;
	while (i < key.size()) {
		if (key[i] == ' ') {
			key.replace(i, 1, "");
			continue;
		}

		count += k[key[i] - 'a'] * i - b[key[i] - 'a'];
		k[key[i] - 'a']++;
		b[key[i] - 'a'] += i + 1;
		i++;
	}
	cout << count;
	return 0;
}
