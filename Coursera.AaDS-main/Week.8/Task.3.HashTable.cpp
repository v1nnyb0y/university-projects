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

#define Size 20004823

long long GetFirstHash(long long a) {
	return (a % 112657) * 175391;
}

long long GetSecondHash(long long a)
{
	return 1 + (a % 179);
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;
	long long *table = new long long[Size];
	for (int i = 0; i < Size; i++) {
		table[i] = 0;
	}
	long long n, x, a, b, ac, bc, ad, bd;
	cin >> n >> x >> a >> b >> ac >> bc >> ad >> bd;

	for (int i = 0; i < n; ++i) {
		long long j = 0;
		long long secH = GetSecondHash(x);
		j = (GetFirstHash(x) + secH) % Size;
		do {
			j = (j + secH) % Size;
			if (table[j] == x + 1) {
				a = (a + ac) % 1000;
				b = (b + bc) % 1000000000000000;
				break;
			}
		} while (table[j] != 0);

		if (table[j] == 0)
		{
			table[j] = x + 1;
			a = (a + ad) % 1000;
			b = (b + bd) % 1000000000000000;
		}
		x = (x * a + b) % 1000000000000000;
	}

	cout << x << " " << a << " " << b;
	return 0;
}
