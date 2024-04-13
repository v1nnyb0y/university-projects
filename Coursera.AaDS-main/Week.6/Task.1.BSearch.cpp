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
using namespace std;

vector<long> vMass;

void bsearch(long data) {
	int l = -1, r = vMass.size();
	while (l + 1 < r) {
		int midId = (l + r) / 2;
		if (vMass[midId] < data) {
			l = midId;
		}
		else {
			r = midId;
		}
	}

	if (r < vMass.size() && vMass[r] == data) {
		printf("%d ", r + 1);
	}
	else {
		printf("-1 -1\n");
		return;
	}

	l = r;
	r = vMass.size();
	while (l + 1 < r) {
		int midId = (l + r) / 2;
		if (vMass[midId] > data) {
			r = midId;
		}
		else {
			l = midId;
		}
	}
	printf("%d\n", r);
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	scanf("%d", &n);
	vMass.resize(n);
	for (int id = 0; id < n; id++) {
		scanf("%d ", &vMass[id]);
	}
	int m;
	scanf("%d", &m);
	for (int id = 0; id < m; id++) {
		long data;
		scanf("%d ", &data);
		bsearch(data);
	}
}
