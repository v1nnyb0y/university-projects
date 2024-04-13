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

int n;

class stack_min
{
private:
	class elem
	{
	public:
		int a, m;
		elem(int _a = 0, int _m = 0) : a(_a), m(_m) {};
	};

	stack<elem> s;

public:

	void pop() {
		s.pop();
	}

	int minimum() {
		return s.size() <= 0 ? 1e9 : s.top().m;
	}

	void push(int x) {
		s.push(elem(x, min(x, minimum())));
	}

	bool size() {
		return (s.size() != 0);
	}

	int top() {
		return s.top().a;
	}
};

class queue_min
{
private:
	stack_min s1, s2;
public:
	void pop() {
		if (s1.size())
			s1.pop();
		else {
			while (s2.size()) {
				s1.push(s2.top());
				s2.pop();
			}
			if (s1.size())
				s1.pop();
		}
	}

	void push(int x) {
		s2.push(x);
	}

	void minimum() {
		printf("%d\n", min(s1.minimum(), s2.minimum()));
	}
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	scanf("%d", &n);
	queue_min qInt;
	for (int i = 0; i < n; i++) {
		char str;
		scanf("\n%c", &str);
		if (str == '-') {
			qInt.pop();
		}
		else {
			if (str == '+') {
				int temp;
				scanf("%d", &temp);
				qInt.push(temp);
			}
			else {
				qInt.minimum();
			}
		}
	}
}
