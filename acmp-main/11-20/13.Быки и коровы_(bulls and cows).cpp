#define _CRT_SECURE_NO_WARNINGS
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

#define TESTING_ 1;

#ifdef max
#undef max
#endif

#ifdef min
#undef min
#endif

void solve_task(istream& is, ostream& os) {
	string s;
	getline(is, s);
	int bulls = 0, cows = 0;
	for (int i = 0; i < 4; ++i)
		if (s[i + 5] == s[i]) {
			bulls += 1;
			s[i] = s[i + 5] = '.';
		}
	for (int i = 0; i < 4; ++i)
		if (s[i + 5] != '.')
			for (int j = 0; j < 4; ++j)
				if (s[j] != '.')
					if (s[i + 5] == s[j]) {
						cows += 1;
						s[i + 5] = s[j] = '.';
						break;
					}
	os << bulls << " " << cows << endl;
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
		<< line\
		<< ")"
		<< endl;
}

#pragma endregion 

int main() {
#ifdef TESTING_

	problem = -1;
	problem_filter = -1;

	testing(__LINE__,
		"5671 7251\n",
		"1 2\n"
	);

	testing(__LINE__,
		"1234 1234\n",
		"4 0\n"
	);

	testing(__LINE__,
		"2034 6234\n",
		"2 1\n"
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