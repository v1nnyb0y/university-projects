#define _CRT_SECURE_NO_WARNINGS
#include <fstream>
#include <iostream>
#include <vector>
#include <iterator>

using namespace std;

vector<int> Merge(vector<int> a_vector, vector<int> b_vector, int left, int right) {
	int i = 0, j = 0;
	int n = a_vector.size(), m = b_vector.size();
	int l = 0, k = 0;
	vector<int> c_vector;
	while ((i < n) || (j < m)) {
		if ((j == m) || ((i < n) && (a_vector[i] <= b_vector[j]))) {
			c_vector.push_back(a_vector[i++]);
		}
		else {
			c_vector.push_back(b_vector[j++]);
		}
	}
	cout << left + 1 << " " << right + 1 << " " << c_vector[0] << " " << c_vector[c_vector.size() - 1] << endl;
	return c_vector;
}

vector<int> Merge_Sort(vector<int> _vector, int left, int right) {
	int n = _vector.size();
	if (n == 1) return _vector;
	int i = 0;
	vector<int> a_vector, b_vector;
	for (auto element : _vector) {
		if (i < n / 2) {
			a_vector.push_back(element);
		}
		else {
			b_vector.push_back(element);
		}
		i++;
	}
	a_vector = Merge_Sort(a_vector, left, left + a_vector.size() - 1);
	b_vector = Merge_Sort(b_vector, left + a_vector.size(), right);
	return Merge(a_vector, b_vector, left, right);
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
	vector = Merge_Sort(vector, 0, vector.size() - 1);
	OutpurArray(vector);
	return 0;
}
