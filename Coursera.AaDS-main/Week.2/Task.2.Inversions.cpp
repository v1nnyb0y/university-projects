#define _CRT_SECURE_NO_WARNINGS
#include <fstream>
#include <iostream>
#include <vector>
#include <iterator>
#include <list>
#include <algorithm>

using namespace std;

vector<int> merge(vector<int> left, vector<int> right, long long& counter) {

	vector<int> result;

	vector<int>::iterator it_l = left.begin();
	vector<int>::iterator it_r = right.begin();

	int index_left = 0;

	while (it_l != left.end() || it_r != right.end()) {

		// the following is true if we are finished with the left vector 
		// OR if the value in the right vector is the smaller one.

		if (it_l == left.end() || (it_r != right.end() && *it_r < *it_l)) {
			result.push_back(*it_r);
			++it_r;

			// increase inversion counter
			counter += left.size() - index_left;
		}
		else {
			result.push_back(*it_l);
			++it_l;
			index_left++;

		}
	}

	return result;
}

vector<int> merge_sort_and_count(vector<int> A, long long& counter) {

	int N = A.size();
	if (N == 1)return A;

	vector<int> left(A.begin(), A.begin() + N / 2);
	vector<int> right(A.begin() + N / 2, A.end());

	left = merge_sort_and_count(left, counter);
	right = merge_sort_and_count(right, counter);


	return merge(left, right, counter);

}

void ReadFile(int& size, vector<int>& _vector) {
	int el;
	cin >> size;
	for (int i = 0; i < size; i++) {
		cin >> el;
		_vector.push_back(el);
	}
}

void OutpurArray(vector<int> _vector) {
	for (int element : _vector) {
		cout << element << " ";
	}
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	int size;
	vector<int> vector;
	ReadFile(size, vector);
	long long res = 0;
	merge_sort_and_count(vector, res);
	cout << res;
	return 0;
}
