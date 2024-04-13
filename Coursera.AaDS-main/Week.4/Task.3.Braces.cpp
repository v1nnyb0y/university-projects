#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <string>
#include <cstring>
#include <cmath>
#include <algorithm>
#include <stack>
#include <vector>
using namespace std;

class my_stack
{
	vector<char> arr;
	int top;
	int n;

public:
	my_stack(int _n) {
		top = 0;
		n = _n;
		arr.resize(n);
	}

	void pop() {
		if (top != 0) {
			top--;
		}
	}

	void push(char x) {
		if (top + 1 != n)
			arr[top++] = x;
	}

	bool empty() {
		return (top == 0);
	}

	char back() {
		return arr[top - 1];
	}
};


void Check(string str) {
	my_stack x(str.size());
	int k = str.size();
	bool ok = false;
	if (str.size() % 2 == 1) {
		cout << "NO\n";
		return;
	}
	for (int i = 0; i < str.size(); i++) {
		if (str[i] == '(' || str[i] == '[') {
			x.push(str[i]);
		}
		else {
			if (!x.empty()) {
				char top = x.back();
				if ((top == '(' && str[i] == ']') ||
					(top == '[' && str[i] == ')')) {
					cout << "NO\n";
					return;
				}
				k -= 2;
				x.pop();
			}
		}

	}
	cout << (k == 0 ? "YES\n" : "NO\n");
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	int n;
	cin >> n;
	for (int i = 0; i < n; i++) {
		string str;
		cin >> str;
		Check(str);
	}
	return 0;
}
