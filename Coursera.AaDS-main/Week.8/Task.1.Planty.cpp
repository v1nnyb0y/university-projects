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

#define QMAX 11111
class HashTable
{
private:
	const int m = 11111;
	vector<long long> arrHash[QMAX];

public:
	void insert(long long);
	void remove(long long);
	bool find(long long);
};
bool HashTable::find(long long el) {
	int id = abs(el % m);
	if (arrHash[id].size() == 0) {
		return false;
	}

	for (int i = 0; i < arrHash[id].size(); i++) {
		if (arrHash[id][i] == el) {
			return true;
		}
	}
	return false;
}
void HashTable::insert(long long el) {
	int id = abs(el % m);
	if (find(el)) {
		return;
	}

	arrHash[id].push_back(el);
	return;
}
void HashTable::remove(long long el) {
	int id = abs(el % m);
	if (arrHash[id].size() == 0) {
		return;
	}

	for (int i = 0; i < arrHash[id].size(); i++) {
		if (arrHash[id][i] == el) {
			swap(arrHash[id][i], arrHash[id][arrHash[id].size() - 1]);
			arrHash[id].resize(arrHash[id].size() - 1);
			return;
		}
	}
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	scanf("%d\n", &n);
	HashTable hash_table;
	for (int id = 0; id < n; id++) {
		long long el;
		char c;
		cin >> c;
		cin >> el;

		//scanf("%c %d\n", &c, &el);
		switch (c) {
		case 'A': {
			hash_table.insert(el);
			break;
		}
		case 'D': {
			hash_table.remove(el);
			break;
		}
		case '?': {
			printf(hash_table.find(el) ? "Y\n" : "N\n");
			break;
		}
		}
	}
}