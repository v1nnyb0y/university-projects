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
#include <unordered_map>
//#include "edx-io.hpp"

using namespace std;

void tfind(string& p, string& t) {
	int t_length = t.length(), p_length = p.length();
	int i, j, inst = 0;
	int pos[10000];
	bool equiv = true;

	for (i = 0; i <= t_length - p_length; ++i) {
		for (j = 0; j < p_length; ++j) {
			if (t[i + j] != p[j]) {
				equiv = false;
				break;
			}
		}
		if (equiv == true) {
			pos[inst] = i + 1;
			inst++;
		}
		else {
			equiv = true;
		}
	}

	cout << inst << "\n";

	for (i = 0; i < inst; ++i)
		cout << pos[i] << " ";
	cout << "\n";
}


int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;

	string p, t;
	cin >> p;
	cin >> t;

	tfind(p, t);

	return 0;
}
