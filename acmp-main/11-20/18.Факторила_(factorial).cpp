#define _CRT_SECURE_NO_WARNINGS
//#define TESTING_ 1;
#define _DEBUG 1;
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

#ifdef max
#undef max
#endif

#ifdef min
#undef min
#endif

const int MAXD = 2600, DIG = 1, BASE = 10;

const unsigned long long BOUND =
numeric_limits<unsigned long long>::max() - (unsigned long long)BASE * BASE;

struct bignum
{
	int D, digits[MAXD / DIG + 2];

	void trim() {
		while (D > 1 && digits[D - 1] == 0)
			D--;
	}

	void init(long long x) {
		memset(digits, 0, sizeof(digits));
		D = 0;

		do {
			digits[D++] = x % BASE;
			x /= BASE;
		} while (x > 0);
	}

	bignum(long long x) {
		init(x);
	}

	bignum(int x = 0) {
		init(x);
	}

	bignum(const char* s) {
		memset(digits, 0, sizeof(digits));
		int len = strlen(s), first = (len + DIG - 1) % DIG + 1;
		D = (len + DIG - 1) / DIG;

		for (int i = 0; i < first; i++)
			digits[D - 1] = digits[D - 1] * 10 + s[i] - '0';

		for (int i = first, d = D - 2; i < len; i += DIG, d--)
			for (int j = i; j < i + DIG; j++)
				digits[d] = digits[d] * 10 + s[j] - '0';

		trim();
	}

	char* str() {
		trim();
		char* buf = new char[DIG * D + 1];
		int pos = 0, d = digits[D - 1];

		do {
			buf[pos++] = d % 10 + '0';
			d /= 10;
		} while (d > 0);

		reverse(buf, buf + pos);

		for (int i = D - 2; i >= 0; i--, pos += DIG)
			for (int j = DIG - 1, t = digits[i]; j >= 0; j--) {
				buf[pos + j] = t % 10 + '0';
				t /= 10;
			}

		buf[pos] = '\0';
		return buf;
	}

	bool operator<(const bignum& o) const {
		if (D != o.D)
			return D < o.D;

		for (int i = D - 1; i >= 0; i--)
			if (digits[i] != o.digits[i])
				return digits[i] < o.digits[i];

		return false;
	}

	bool operator==(const bignum& o) const {
		if (D != o.D)
			return false;

		for (int i = 0; i < D; i++)
			if (digits[i] != o.digits[i])
				return false;

		return true;
	}

	bignum operator<<(int p) const {
		bignum temp;
		temp.D = D + p;

		for (int i = 0; i < D; i++)
			temp.digits[i + p] = digits[i];

		for (int i = 0; i < p; i++)
			temp.digits[i] = 0;

		return temp;
	}

	bignum operator>>(int p) const {
		bignum temp;
		temp.D = D - p;

		for (int i = 0; i < D - p; i++)
			temp.digits[i] = digits[i + p];

		for (int i = D - p; i < D; i++)
			temp.digits[i] = 0;

		return temp;
	}

	bignum range(int a, int b) const {
		bignum temp = 0;
		temp.D = b - a;

		for (int i = 0; i < temp.D; i++)
			temp.digits[i] = digits[i + a];

		return temp;
	}

	bignum operator+(const bignum& o) const {
		bignum sum = o;
		int carry = 0;

		for (sum.D = 0; sum.D < D || carry > 0; sum.D++) {
			sum.digits[sum.D] += (sum.D < D
				? digits[sum.D]
				: 0) + carry;

			if (sum.digits[sum.D] >= BASE) {
				sum.digits[sum.D] -= BASE;
				carry = 1;
			}
			else
				carry = 0;
		}

		sum.D = max(sum.D, o.D);
		sum.trim();
		return sum;
	}

	bignum operator-(const bignum& o) const {
		bignum diff = *this;

		for (int i = 0, carry = 0; i < o.D || carry > 0; i++) {
			diff.digits[i] -= (i < o.D
				? o.digits[i]
				: 0) + carry;

			if (diff.digits[i] < 0) {
				diff.digits[i] += BASE;
				carry = 1;
			}
			else
				carry = 0;
		}

		diff.trim();
		return diff;
	}

	bignum operator*(const bignum& o) const {
		bignum prod = 0;
		unsigned long long sum = 0, carry = 0;

		for (prod.D = 0; prod.D < D + o.D - 1 || carry > 0; prod.D++) {
			sum = carry % BASE;
			carry /= BASE;

			for (int j = max(prod.D - o.D + 1, 0); j <= min(D - 1, prod.D); j++) {
				sum += (unsigned long long)digits[j] * o.digits[prod.D - j];

				if (sum >= BOUND) {
					carry += sum / BASE;
					sum %= BASE;
				}
			}

			carry += sum / BASE;
			prod.digits[prod.D] = sum % BASE;
		}

		prod.trim();
		return prod;
	}

	double double_div(const bignum& o) const {
		double val = 0, oval = 0;
		int num = 0, onum = 0;

		for (int i = D - 1; i >= max(D - 3, 0); i--, num++)
			val = val * BASE + digits[i];

		for (int i = o.D - 1; i >= max(o.D - 3, 0); i--, onum++)
			oval = oval * BASE + o.digits[i];

		return val / oval * (D - num > o.D - onum
			? BASE
			: 1);
	}

	pair<bignum, bignum> divmod(const bignum& o) const {
		bignum quot = 0, rem = *this, temp;

		for (int i = D - o.D; i >= 0; i--) {
			temp = rem.range(i, rem.D);
			int div = (int)temp.double_div(o);
			bignum mult = o * div;

			while (div > 0 && temp < mult) {
				mult = mult - o;
				div--;
			}

			while (div + 1 < BASE && !(temp < mult + o)) {
				mult = mult + o;
				div++;
			}

			rem = rem - (o * div << i);

			if (div > 0) {
				quot.digits[i] = div;
				quot.D = max(quot.D, i + 1);
			}
		}

		quot.trim();
		rem.trim();
		return make_pair(quot, rem);
	}

	bignum operator/(const bignum& o) const {
		return divmod(o).first;
	}

	bignum operator%(const bignum& o) const {
		return divmod(o).second;
	}

	bignum power(int exp) const {
		bignum p = 1, temp = *this;

		while (exp > 0) {
			if (exp & 1)
				p = p * temp;
			if (exp > 1)
				temp = temp * temp;
			exp >>= 1;
		}

		return p;
	}
};

inline bignum gcd(bignum a, bignum b) {
	bignum t;

	while (!(b == 0)) {
		t = a % b;
		a = b;
		b = t;
	}

	return a;
}

unsigned long long f(int n) {
	unsigned long long res = 1;
	for (int i = 1; i <= n; ++i) {
		res *= i;
	}
	return res;
}

void solve_task(istream& is, ostream& os) {
	int n;
	is >> n;
	bignum r(1);
	if (n > 0) {
		for (int i = 1; i <= n; ++i)
			r = r * i;
	}
	os << r.str() << endl;
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
		"1\n",
		"1\n"
	);

	testing(__LINE__,
		"3\n",
		"6\n"
	);

	testing(__LINE__,
		"5\n",
		"120\n"
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
