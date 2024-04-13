#define _CRT_SECURE_NO_WARNINGS
//#define TESTING_ 1;
//#define _DEBUG 1;
#include <iostream>
#include <vector>
#include <map>
#include <set>
#include <stack>
#include <sstream>
#include <fstream>
#include <iomanip>
#include <iterator>
#include <numeric>
#include <limits>
#include <algorithm>
#include <string>
#include <cstdio>
#include <cstdlib>
#include <cmath>
#include <cassert>
#ifdef _DEBUG
#include <windows.h>
#endif

using namespace std;

void solve_task(istream& is, ostream& os) {
	int n;
	is >> n;
	vector<int> v(n);
	for (int i = 0; i < n; ++i) is >> v[i];
	vector<int> prefix_func(1 + n, 0);
	int len = 0;
	for (int i = 1; i < n; ++i) {
		while (true) {
			if (v[len] == v[i]) {
				len++;
				break;
			}
			if (len == 0) {
				break;
			}
			len = prefix_func[len];
		}
		prefix_func[i + 1] = len;
	}
	while (true) {
		int period = n - len;
		if ((n - 1) % period == 0) {
			os << period << endl;
			return;
		}
		assert(len > 1);
		len = prefix_func[len];
	}
}

#pragma region testing

const char* liner = "--------";
int problem;
int problem_filter;

void testing(int line, const char* input, const char* output) {
	problem += 1;
	if (problem_filter != -1 && problem_filter != problem) return;

	stringstream is(input);
	stringstream os;
	solve_task(is, os);
	if (os.str() != output) {
		cerr << "Case #"
			<< problem
			<< ": FAILED (line "
			<< line
			<< ")"
			<< endl;
#ifdef _DEBUG
		stringstream error;
		error << __FILE__
			<< "("
			<< line
			<< "): error: test case "
			<< problem
			<< " FAILED"
			<< endl;
		OutputDebugStringA(error.str().c_str());
#endif
		cerr << liner
			<< "EXPECTED"
			<< liner
			<< endl
			<< output;

		cerr << liner
			<< "-ACTUAL-"
			<< liner
			<< endl
			<< os.str()
			<< liner
			<< liner
			<< liner
			<< endl;
	}
	else
		cerr << "Case #"
		<< problem
		<< " OK (line "
		<< line
		<< ")"
		<< endl;
}

#pragma endregion 

int main() {
#ifdef TESTING_

	problem = -1;
	problem_filter = -1;

	testing(__LINE__,
		"13\n5 3 1 3 5 2 5 3 1 3 5 2 5\n",
		"6\n"
	);

	testing(__LINE__,
		"4\n1 1 1 1\n",
		"1\n"
	);

	testing(__LINE__,
		"4\n1 2 3 1\n",
		"3\n"
	);

#ifdef _DEBUG
	getchar();
#endif

#else

	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	//freopen("input.txt", "r", stdin);
	//freopen("output.txt", "w", stdout);
	ifstream is("input.txt");
	ofstream os("output.txt");
	// istream& is = cin;
	// ostream& os = cout;

	solve_task(is, os);

#endif

	return 0;
}
