#define _CRT_SECURE_NO_WARNINGS
#include <fstream>
#include <iostream>
#include <vector>
#include <iterator>
#include <list>
#include <algorithm>

using namespace std;


void anti_quicksort(vector<int>& a) {
	for (int i = 2; i < a.size(); i++) {
		swap(a[i], a[i / 2]);
	}
}

void ReadFile(vector<int>& _vector) {
	int el;
	for (int i = 0; i < _vector.size(); i++) {
		cin >> el;
		_vector.push_back(el);
	}
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	int size;
	cin >> size;
	vector<int> _vector(size);
	for (int i = 0; i < size; i++) {
		_vector[i] = i + 1;
	}
	anti_quicksort(_vector);
	for (int i = 0; i < size; i++) {
		cout << _vector[i] << " ";
	}
	return 0;
}
