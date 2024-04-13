#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <queue>
#include <string>
#include <map>
#include <set>
#include <iomanip>
#include <deque>
#include <stack>
#include <unordered_map>
#include <cassert>
using namespace std;

class my_priority_queue
{
	vector<pair<int, long>> vBunch;
	vector<int> index;

public:
	my_priority_queue(int n) {
		index.resize(n, INT32_MAX);
	}

	void Heap_Insert(long key, int id) {
		vBunch.push_back(make_pair(id, key));
		index[id] = vBunch.size() - 1;
		Heap_Decrese_Key(id, key, false);
	}

	void Heap_Decrese_Key(int i, long key, bool ok) {
		if (ok) {
			//int id = find(vBunch.begin(), vBunch.end(), temp) - vBunch.begin();
			int id = index[i];
			vBunch[id].second = key;
			while (id >= 1 && vBunch[Parent(id)].second > vBunch[id].second) {
				swap(vBunch[id], vBunch[Parent(id)]);
				swap(index[vBunch[id].first], index[vBunch[Parent(id)].first]);
				id = Parent(id);
			}

		}
		else {
			i = vBunch.size() - 1;
			while (i >= 1 && vBunch[Parent(i)].second > vBunch[i].second) {
				swap(vBunch[i], vBunch[Parent(i)]);
				swap(index[vBunch[i].first], index[vBunch[Parent(i)].first]);
				i = Parent(i);
			}
		}
	}

private:
	int Parent(int id) {
		return (id - 1) / 2;
	}

public:
	void Heap_Extract_Min() {
		if (vBunch.size() == 0) {
			printf("*\n");
			return;
		}
		long min = vBunch[0].second;
		vBunch[0] = vBunch[vBunch.size() - 1];
		index[vBunch[0].first] = 0;
		vBunch.resize(vBunch.size() - 1);
		Min_Heapify(0);
		printf("%d\n", min);
	}


	void Min_Heapify(int i) {
		int l = 2 * i + 1;
		int r = 2 * i + 2;
		int min_id = i;

		if (l < vBunch.size()) {
			if (vBunch[l].second < vBunch[i].second) {
				min_id = l;
			}
		}

		if (r < vBunch.size()) {
			if (vBunch[r].second < vBunch[min_id].second) {
				min_id = r;
			}
		}

		if (min_id != i) {
			swap(vBunch[i], vBunch[min_id]);
			swap(index[vBunch[i].first], index[vBunch[min_id].first]);
			Min_Heapify(min_id);
		}
	}
};

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	setlocale(LC_ALL, "Russian");
	int n;
	scanf("%d\n", &n);
	my_priority_queue heap(n);
	for (int i = 0; i < n; i++) {
		char c;
		scanf("%c", &c);
		switch (c) {
		case 'A': {
			long temp;
			scanf(" %d\n", &temp);
			heap.Heap_Insert(temp, i);
			break;
		}
		case 'X': {
			scanf("\n");
			heap.Heap_Extract_Min();
			break;
		}
		case 'D': {
			long key;
			int id;
			scanf(" %d %d\n", &id, &key);
			heap.Heap_Decrese_Key(id - 1, key, true);
			break;
		}
		}
	}
	return 0;
}
