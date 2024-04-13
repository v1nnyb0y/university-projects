#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <algorithm>
#include <map>
#include <stdio.h>
#include <iomanip>
#include <cassert>
#include <conio.h>

using namespace std;

struct node
{
	vector<int> items;
	int lvl;
	int sum_weight;
	int sum_value;
	double value;

	node() {
		value = 0;
		sum_value = 0;
		sum_weight = 0;
		lvl = 0;
	}
};

struct item
{
	int weight;
	int cost;
	double value;
	int index;

	item():weight(), cost(), value(), index(){}

	item(int w, int c, double v, int _i) {
		weight = w;
		cost = c;
		value = v;
		index = _i;
	}
};

int find_max_node(vector<node> kits) {
	int max_v = 0;
	node max = kits[0];
	for(int i=1;i<kits.size();++i) {
		if (kits[i].value == max.value) {
			if (kits[i].lvl > max.lvl) {
				max = kits[i];
				max_v = i;
			}
		}
		else {
			if (kits[i].value > max.value) {
				max = kits[i];
				max_v = i;
			}
		}
	}

	return max_v;
}

int main() {
	freopen("input.txt", "r", stdin);
	int n;
	int V;
	cin >> V >> n;
	vector<item> items(n);
	for(int i=0;i<n;++i) {
		int w;
		int p;
		cin >> w >> p;
		double v = p * 1.0 / w;
		item temp(w, p, v, i + 1);
		items[i] = temp;
	}

	sort(items.begin(), items.end(),
		[](const item &a, const item &b)->bool
	{
		return a.value > b.value;
	});

	vector<node> tree;
	node node_root;
	node_root.lvl = 0;
	node_root.sum_value = 0;
	node_root.sum_weight = 0;
	node_root.value = V * items[0].value;
	tree.push_back(node_root);
	int max_node = 0;
	do {
		int curr_lvl = tree[max_node].lvl;
		node left;
		node right;

		left.items = tree[max_node].items;
		left.sum_value = tree[max_node].sum_value;
		left.sum_weight = tree[max_node].sum_weight;
		left.lvl = tree[max_node].lvl + 1;
		if (left.sum_weight + items[curr_lvl].weight <=V) {
			left.items.push_back(items[curr_lvl].index);
			left.sum_weight += items[curr_lvl].weight;
			left.sum_value += items[curr_lvl].cost;
			if (curr_lvl < n-1) {
				left.value = left.sum_value + items[curr_lvl + 1].value*(V - left.sum_weight);
			}else {
				left.value = left.sum_value;
			}
			tree.push_back(left);
		}

		right.items = tree[max_node].items;
		right.sum_value = tree[max_node].sum_value;
		right.sum_weight = tree[max_node].sum_weight;
		right.lvl = tree[max_node].lvl + 1;
		if (curr_lvl < n -1) {
			right.value = right.sum_value + items[curr_lvl + 1].value*(V - right.sum_weight);
		}else {
			right.value = right.sum_value;
		}
		tree.push_back(right);
		tree.erase(tree.begin() + max_node);
		max_node = find_max_node(tree);
	} while (tree[max_node].lvl != n);
	printf("%d\n", tree[max_node].items.size());
	for(int i = 0;i<tree[max_node].items.size();++i) {
		printf("%d ", tree[max_node].items[i]);
	}

	_getch();
	return 0;
}