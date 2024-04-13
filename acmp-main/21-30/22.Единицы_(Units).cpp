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
	long n;
	is >> n;
	int number_ones = 0;
	while (n > 0) {
		if (n % 2 == 1)
			number_ones++;
		n /= 2;
	}
	os << number_ones << endl;
}

#pragma region testing

const char* liner = "--------";
int problem;
int problem_filter;

void testing(int line, const char* input, const char* output) {
	problem += 1;
	if (problem_filter != -1 && problem_filter != problem)
		return;

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
		"5\n",
		"2\n"
	);

	testing(__LINE__,
		"7\n",
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
