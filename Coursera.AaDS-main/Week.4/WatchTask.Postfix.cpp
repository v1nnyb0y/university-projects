#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <queue>
#include <string>
#include <map>
#include <set>
#include <iomanip>
#include <deque>
#include <stack>
using namespace std;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	int n;
	cin >> n;
	stack<int> st;
	for (int i = 0; i < n; i++) {
		char c;
		cin >> c;
		if (c >= '0' && c <= '9') st.push(c - '0');
		else {
			int b = st.top();
			st.pop();
			int a = st.top();
			st.pop();
			if (c == '+') st.push(a + b);
			else if (c == '-') st.push(a - b);
			else if (c == '*') st.push(a * b);
		}
	}
	cout << st.top();
	return 0;
}
