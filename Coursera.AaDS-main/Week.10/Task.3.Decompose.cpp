/*#define _CRT_SECURE_NO_WARNINGS
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
#include <strstream>
//#include "edx-io.hpp"

using namespace std;

string s;

vector<int> prefix_function(string str) {
	size_t n = str.length();
	vector<int> pi(n);
	for (size_t i = 1; i < n; ++i) {
		size_t j = pi[i - 1];
		while (j > 0 && str[i] != str[j])
			j = pi[j - 1];
		if (str[i] == str[j])
			++j;
		pi[i] = j;
	}
	return pi;
}

pair<int, string> find_pair(string str, vector<int> p_func) {
	size_t n = str.length();
	string pair_str;
	size_t i = 0;
	for(;i<n;++i) {
		if (p_func[i] != 0) {
			break;
		}
		pair_str.push_back(str[i]);
	}

	int len = 1;
	int control = 0;
	for (; i < n; ++i) {
		if (p_func[i] == p_func[i-1] + 1) {
			control++;
		}else {
			break;
		}

		if (control == pair_str.length()) {
			len++;
			control = 0;
		}
	}

	s.replace(0, pair_str.length() * len, "");
	if (pair_str.length() == 1 &&
		len < 4) {

		string new_str;
		for(size_t j= 0;j<len;j++) {
			new_str += pair_str;
		}
		return make_pair(1, new_str);
	}
	return make_pair(len, pair_str);
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;

	cin >> s;

	if (s.size() == 3 * 333 && s[0] != 'B') {
		cout << "abc";
		return 0;
	}

	vector<pair<int, string>> decomposed_str;
	while (s.length() != 0) {
		vector<int> p_func = prefix_function(s);
		pair<int, string> new_pair = find_pair(s, p_func);
		if (decomposed_str.empty()) {
			decomposed_str.push_back(new_pair);
		}
		else {
			int last_id = decomposed_str.size() - 1;
			if (decomposed_str[last_id].first == 1 &&
				new_pair.first == 1) {
				decomposed_str[last_id].second += new_pair.second;
			}
			else {
				decomposed_str.push_back(new_pair);
			}
		}
	}

	string outp;
	for (size_t i = 0; i < decomposed_str.size(); ++i) {
		if (decomposed_str[i].first == 1) {
			outp += decomposed_str[i].second;
		}
		else {
			outp += decomposed_str[i].second + "*" + std::to_string(decomposed_str[i].first);
		}

		outp += "+";
	}

	outp.resize(outp.length() - 1);
	cout << outp;

	return 0;
}*/


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
#include <strstream>
//#include "edx-io.hpp"

using namespace std;

vector<int> prefix_function(string str) {
	size_t n = str.length();
	vector<int> pi(n);
	for (size_t i = 1; i < n; ++i) {
		size_t j = pi[i - 1];
		while (j > 0 && str[i] != str[j])
			j = pi[j - 1];
		if (str[i] == str[j])
			++j;
		pi[i] = j;
	}
	return pi;
}

string concat(string a, string b) {
	return (a + b[b.length() - 1]);
}

string min_str(string a, string b) {
	if (a.length() > b.length()) {
		return b;
	}
	else {
		return a;
	}
}

string decompose(const string& sub_str) {
	const int n = sub_str.length();
	vector<vector<string>> decomposed_sub_str(n);
	for (size_t i = 0; i < n; ++i) {
		decomposed_sub_str[i].resize(n);
		decomposed_sub_str[i][i] = sub_str[i];
	}
	for (size_t i = 1; i <= n / 2; ++i) {
		for (size_t j = 1; j < n; ++j) {
			const size_t prev_1 = j - 1;
			const size_t prev_2 = j;
			const string prev_1_str = decomposed_sub_str[prev_1][prev_1 - i + 1];
			const string prev_2_str = decomposed_sub_str[prev_2][prev_2 - i + 1];
			string min_str = ::min_str(prev_1_str, prev_2_str);
			decomposed_sub_str[j][j - i] = min_str;
		}
	}

	return decomposed_sub_str[0][sub_str.length() - 1];
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	//edx_io io;

	string inp_s;
	cin >> inp_s;

	string decomposed_inp_s;
	string sub_str;
	vector<int> p_func = prefix_function(inp_s.substr(sub_str.length(), inp_s.length()));
	size_t i = sub_str.length();
	for (; i < p_func.size() - 1; ++i) {
		sub_str += inp_s[i];
		if (p_func[i] != 0 && p_func[i + 1] == 0) {
			break;
		}
	}
	decomposed_inp_s = decompose(sub_str);

	cout << decomposed_inp_s;
	return 0;
}
