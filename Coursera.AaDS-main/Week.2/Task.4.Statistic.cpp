#define _CRT_SECURE_NO_WARNINGS
#include <fstream>
#include <iostream>
#include <vector>
#include <iterator>
#include <list>
#include <algorithm>

using namespace std;

void ReadFile(vector<long>& _vector) {
	vector<long> tmp{istream_iterator<long>(cin), istream_iterator<long>()};
	_vector = tmp;

}

void CreateVector(vector<long>& _vector, long long size, int a, int b, int c) {
	long long i = 0;
	while (_vector.size() != size) {
		_vector.push_back(a * _vector[i] + b * _vector[i + 1] + c);
		i++;
	}
}

void quickSort(vector<long>& mas, int left, int right, int k1, int k2) {
	long mid;
	int f = left, l = right;
	mid = mas[(f + l + 1) / 2]; //вычисление опорного элемента
	do {
		while (mas[f] < mid) f++;
		while (mas[l] > mid) l--;
		if (f <= l) //перестановка элементов
		{
			swap(mas[f++], mas[l--]);
		}
	}
	while (f < l);

	if (k1 <= l && l <= f && f <= k2) {
		if (left < l) quickSort(mas, left, l, k1, k2);
		if (f < right) quickSort(mas, f, right, k1, k2);
	}
	else {
		if (left < l && l >= k2) quickSort(mas, left, l, k1, k2);
		else
			if (f < right && f <= k1) quickSort(mas, f, right, k1, k2);
	}
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	long long size;
	int k1, k2, a, b, c, el_1, el_2;
	cin >> size >> k1 >> k2 >> a >> b >> c >> el_1 >> el_2;
	vector<long> _vector;
	_vector.push_back(el_1);
	_vector.push_back(el_2);
	CreateVector(_vector, size, a, b, c);
	if (k1 == k2 && k2 == 0) {
		int min = INT32_MAX;
		for (int i = 0; i < _vector.size() - 1; i++) {
			if (_vector[i] < min) {
				min = _vector[i];
			}
		}
		cout << min;
		return 0;
	}
	if (k1 == k2 && k2 == _vector.size()) {
		int max = INT32_MIN;
		for (int i = 0; i < _vector.size(); i++) {
			if (_vector[i] > max) {
				max = _vector[i];
			}
		}
		cout << max;
		return 0;
	}
	quickSort(_vector, 0, _vector.size() - 1, k1 - 1, k2 - 1);
	for (int i = k1 - 1; i <= k2 - 1; i++) {
		cout << _vector[i] << " ";
	}


	return 0;
}
