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
using namespace std;

int n, k;
vector<vector<int>> g;
vector<int> mt;
vector<char> used;

bool dfs(int v) {
	if (used[v]) return false;

	used[v] = true;
	for (int i = 0; i < g[v].size(); i++) {
		int to = g[v][i];
		if (to == -1) {
			return false;
		}

		if (mt[to] == -1 || dfs(mt[to])) {
			mt[to] = v;
			return true;
		}
	}
	return false;
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");


	printf("1 graph: ");
	scanf("%d", &n);
	printf("2 graph: ");
	scanf("%d", &k);

	if (n > k) {
		mt.resize(n, -1);
		g.resize(n);
		used.resize(n, false);
	}
	else {
		mt.resize(k, -1);
		g.resize(k);
		used.resize(k, false);
	}

	if (n > k) {
		for (int id = 0; id < n; id++) {
			printf("%d:", (id + 1));
			string inp;
			cin >> inp;
			if (inp.size() == 1) {
				if (inp[0] == ',') {
					g[id].push_back(-1);
				}
			}
			for(int i = 0; i < inp.size(); i++) {
				if (inp[i] == ' ' || inp[i] == ',') {
					continue;
				}
				g[id].push_back((inp[i] - '0') - 1);
			}
		}
	}
	else {
		for (int id = 0; id < k; id++) {
			printf("%d:", (id + 1));
			string inp;
			cin >> inp;
			if (inp.size() == 1) {
				if (inp[0] == ',') {
					g[id].push_back(-1);
				}
			}
			for (int i = 0; i < inp.size(); i++) {
				if (inp[i] == ' ' || inp[i] == ',') {
					continue;
				}

				g[id].push_back((inp[i] - '0') - 1);
			}
		}
	}

	if (n > k) {
		for (int v = 0; v < n; v++) {
			used.assign(n, false);
			dfs(v);
		}
	}else {
		for (int v = 0; v < k; v++) {
			used.assign(k, false);
			dfs(v);
		}
	}

	if (n > k) {
		for (int i = 0; i < k; i++) {
			if (mt[i] != -1) {
				printf("%d %d\n", mt[i] + 1, i + 1);
			}
		}
	}
	else {
		for (int i = 0; i < n; i++) {
			if (mt[i] != -1) {
				printf("%d %d\n", mt[i] + 1, i + 1);
			}
		}
	}

	string r;
	cin >> r;
	return 0;
}