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

int compare(const void* x1, const void* x2) // функция сравнения элементов массива
{
	return (*(int*)x1 - *(int*)x2); // если результат вычитания равен 0, то числа равны, < 0: x1 < x2; > 0: x1 > x2
}

void Sort(vector<long>& _vector, int k) {
	for (int i = 0; i < k; i++) {
		vector<int> temp;
		for (int j = i; j < _vector.size(); j += k) {
			temp.push_back(_vector[j]);
		}
		sort(temp.begin(), temp.end());
		int index = 0;
		for (int j = i; j < _vector.size(); j += k) {
			_vector[j] = temp[index++];
		}
	}
}

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	long long size;
	int k;
	cin >> size >> k;
	vector<long> _vector(size);
	ReadFile(_vector);
	if (k == 1) {
		cout << "YES";
		return 0;
	}
	Sort(_vector, k);
	for (int i = 0; i < _vector.size() - 1; i++) {
		if (_vector[i] > _vector[i + 1]) {
			cout << "NO";
			return 0;
		}
	}
	cout << "YES";

	return 0;
}
