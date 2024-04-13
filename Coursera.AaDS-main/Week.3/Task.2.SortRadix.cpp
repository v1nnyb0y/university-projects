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
#include "edx-io.hpp"
using namespace std;

int m;
int n;
int k;
//vector<char*> arr;
vector<int> pos1;

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	edx_io io;
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	io >> m >> n >> k;
	//cin.ignore();
	vector<string> mass(k);
	pos1.resize(m);
	for (int i = 0; i < n - k; i++) {
		string tmp;
		io >> tmp;
	}
	for (int i = 0; i < k; i++) {
		io >> mass[i];
	}
	for (int i = 0; i < m; i++) {
		pos1[i] = i;
	}
	vector<vector<int>> letters(26);
	for (int i = 1; i <= k; i++) {
		for (int t = 0; t < 26; t++) {
			letters[t].clear();
		}
		int X = k - i;
		for (int p = 0; p < m; p++) {
			int temp = pos1[p];
			letters[mass[X][temp] - 97].push_back(temp);
		}
		int curpos = 0;
		for (int t = 0; t < 26; t++) {
			for (int j = 0; j < letters[t].size(); j++, curpos++) {
				pos1[curpos] = letters[t][j];
			}
		}
	}
	for (int i = 0; i < m; i++) {
		io << (pos1[i] + 1) << " ";
	}
}
