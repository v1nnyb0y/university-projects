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
#include <regex>

using namespace std;


int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	int n;
	int max_val = INT16_MIN;
	int max_id = -1;
	int min_val = INT16_MAX;
	int min_id = -1;

	int sum = 0, pow = 1;
	scanf("%d", &n);
	vector<int> arr(n);
	for (size_t i = 0; i < n; ++i) {
		scanf("%d ", &arr[i]);
		if (max_val < arr[i]) {
			max_val = arr[i];
			max_id = i;
		}

		if (min_val > arr[i]) {
			min_val = arr[i];
			min_id = i;
		}

		if (arr[i] > 0)
			sum += arr[i];
	}

	if (max_id < min_id)
		swap(min_id, max_id);
	for (size_t i = min_id + 1; i < max_id; ++i) {
		pow *= arr[i];
	}

	printf("%d %d", sum, pow);

	return 0;
}
