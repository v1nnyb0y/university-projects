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
#include "edx-io.hpp"
using namespace std;

map<string, int> mKey;
map<int, string> mValue;
int cnt = 0;

string get(string key) {
	if (mKey.find(key) == mKey.end()) return "<none>";

	return mValue[mKey[key]];
}

void put(string key, string value) {
	if (mKey.find(key) == mKey.end()) {
		cnt++;
		mKey.insert(make_pair(key, cnt));
		mValue.insert(make_pair(cnt, value));
	}
	else {
		mValue[mKey[key]] = value;
	}
}

void remove(string key) {
	if (mKey.find(key) == mKey.end()) return;

	mValue.erase(mKey[key]);
	mKey.erase(key);
}

string next(string key) {
	if (mKey.find(key) == mKey.end()) return "<none>";

	map<int, string>::iterator it = mValue.find(mKey[key]);
	if (it == --mValue.end()) return "<none>";
	it++;
	return it->second;
}

string prev(string key) {
	if (mKey.find(key) == mKey.end()) return "<none>";

	map<int, string>::iterator it = mValue.find(mKey[key]);
	if (it == mValue.begin()) return "<none>";
	it--;
	return it->second;
}

int main()
{
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	edx_io io;
	int n;
	io >> n;
	for (int i = 0; i < n; i++) {
		string cmd, key, value;
		io >> cmd;
		io >> key;

		if (cmd == "get") {
			io << get(key);
			io << "\n";
			continue;
		}
		if (cmd == "put") {
			io >> value;
			put(key, value);
			continue;
		}
		if (cmd == "delete") {
			remove(key);
			continue;
		}
		if (cmd == "prev") {
			io << prev(key);
			io << "\n";
			continue;
		}
		if (cmd == "next") {
			io << next(key);
			io << "\n";
			continue;
		}
	}

	return 0;
}
