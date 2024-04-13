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

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	double h1;
	scanf("%d %lf", &n, &h1);
	double left = 0;
	double right = h1;
	double last = -1;
	while ((right - left) > 0.0000001 / (n - 1)) {
		double midId = (left + right) / 2;
		double prev = h1;
		double cur = midId;
		bool aboveGround = cur > 0;
		for (int id = 3; id <= n; id++) {
			double next = 2 * cur - prev + 2;
			aboveGround &= next > 0;
			prev = cur;
			cur = next;
		}
		if (aboveGround) {
			right = midId;
			last = cur;
		}
		else {
			left = midId;
		}
	}
	assert(last > -1);
	printf("%0.6f", last);
	return 0;
}
