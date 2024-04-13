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
#include <cstdio>
#include <iomanip>
#include <deque>
#include <stack>
#include <cassert>
#include <cctype>
#include <regex>

using namespace std;

const int BASE = 1000 * 1000 * 1000;

struct UInt
{
	vector<int> digits;

	UInt(const int value)
		:digits(1, value) {
		assert(0 <= value && value < BASE);
	}

	UInt(vector<int> &&digits_)
		:digits(move(digits_)) {
		assert(digits.size() > 0);
		assert(digits.size() == 1 || digits.back() != 0);
		for (int digit : digits) {
			assert(0 <= digit && digit < BASE);
		}
	}

	void print() const {
		printf("%d", digits.back());
		for (int i = (int)digits.size() - 2; i >= 0; --i) {
			for (int pow = BASE / 10; pow >= 1; pow /= 10) {
				putchar('0' + digits[i] / pow % 10);
			}
		}
	}

	int operator [] (int index) const {
		assert(index >= 0);
		if (index < (int)digits.size()) {
			return digits[index];
		}
		else {
			return 0;
		}
	}

	int size() const {
		return (int)digits.size();
	}
};

UInt operator + (const UInt &left, const UInt &right) {
	vector<int> res;
	int carry = 0;
	for (int i = 0; i < left.size() || i < right.size() || carry > 0; ++i) {
		carry += left[i] + right[i];
		res.push_back(carry % BASE);
		carry /= BASE;
	}
	return UInt(move(res));
}

UInt operator - (const UInt &left, const UInt &right) {
	assert(left.size() >= right.size());
	vector<int> res;
	int carry = 0;
	for (int i = 0; i < left.size(); ++i) {
		carry += left[i] - right[i] + BASE;
		res.push_back(carry % BASE);
		carry /= BASE;
		carry--;
	}
	assert(carry == 0);
	while (res.size() > 1 && res.back() == 0) {
		res.pop_back();
	}
	return UInt(move(res));
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");

	int n, max_jump;
	scanf("%d %d", &max_jump, &n);
	vector<UInt> variants(2, UInt(1));
	for (int i = 2; i <= max_jump; ++i) {
		variants.push_back(variants[i - 1] + variants[i - 1]);
	}

	for (int i = max_jump + 1; i <= n; ++i) {
		variants.push_back((variants[i - 1] + variants[i - 1]) - variants[i - 1 - max_jump]);
	}

	variants[n].print();

	return 0;
}
