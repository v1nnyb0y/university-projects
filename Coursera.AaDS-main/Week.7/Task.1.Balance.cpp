#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <vector>
#include <cmath>
#include <algorithm>
#include <queue>
#include <string>
#include <map>
#include <set>
#include <stdio.h>
#include <iomanip>
#include <deque>
#include <stack>
#include <cassert>
using namespace std;

vector<vector<long>> vLines;
vector<int> vBalances;

struct BinaryTree
{
	long Data;
	long High;
	BinaryTree* Left;
	BinaryTree* Right;
};

BinaryTree* Create(BinaryTree* root, int index) {
	root->Data = vLines[index][0];
	root->Left = nullptr;
	root->Right = nullptr;
	root->High = 1;
	if (vLines[index][1] == 0 && vLines[index][2] == 0) {
		vBalances[index] = 0;
		return root;
	}
	if (vLines[index][1] != 0) {
		root->Left = new BinaryTree;
		root->Left = Create(root->Left, vLines[index][1] - 1);
		root->High = root->Left->High;
	}
	if (vLines[index][2] != 0) {
		root->Right = new BinaryTree;
		root->Right = Create(root->Right, vLines[index][2] - 1);
		root->High = max(root->High, root->Right->High);
	}

	root->High++;
	if (root->Left == nullptr) {
		vBalances[index] = root->Right->High;
		return root;
	}
	if (root->Right == nullptr) {
		vBalances[index] = 0 - root->Left->High;
		return root;
	}

	vBalances[index] = root->Right->High - root->Left->High;
	return root;
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	scanf("%d", &n);
	vLines.resize(n);
	vBalances.resize(n);
	for (int id = 0; id < n; id++) {
		vLines[id].resize(3);
		scanf("%ld %d %d", &vLines[id][0], &vLines[id][1], &vLines[id][2]);
	}
	BinaryTree* root = new BinaryTree;
	root = Create(root, 0);
	for (int id = 0; id < n; id++) {
		printf("%d\n", vBalances[id]);
	}
	return 0;
}
