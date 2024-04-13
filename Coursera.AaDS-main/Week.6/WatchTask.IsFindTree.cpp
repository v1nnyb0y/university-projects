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

struct BinaryTree
{
	long Data;
	long Count;
	BinaryTree* Left;
	BinaryTree* Right;
};

BinaryTree* Create(BinaryTree* root, int index) {
	root->Data = vLines[index][0];
	root->Count = 1;
	root->Left = nullptr;
	root->Right = nullptr;
	if (vLines[index][1] == 0 && vLines[index][2] == 0) {
		return root;
	}
	if (vLines[index][1] != 0) {
		root->Left = new BinaryTree;
		root->Left = Create(root->Left, vLines[index][1] - 1);
		root->Count += root->Left->Count;
	}
	if (vLines[index][2] != 0) {
		root->Right = new BinaryTree;
		root->Right = Create(root->Right, vLines[index][2] - 1);
		root->Count += root->Right->Count;
	}
	return root;
}

bool IsFindTree(BinaryTree* root, int min, int max) {
	if (root->Data <= min || max <= root->Data) {
		return false;
	}

	if (root->Right == nullptr && root->Left == nullptr) {
		return true;
	}

	bool check = true;
	if (root->Left != nullptr) {
		check = IsFindTree(root->Left, min, root->Data);
	}

	if (root->Right != nullptr && check) {
		return IsFindTree(root->Right, root->Data, max);
	}

	return check;
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	scanf("%d", &n);
	if (n == 0) {
		printf("YES");
		return 0;
	}
	vLines.resize(n);
	for (int id = 0; id < n; id++) {
		vLines[id].resize(3);
		scanf("%d %d %d", &vLines[id][0], &vLines[id][1], &vLines[id][2]);
	}
	BinaryTree* root = new BinaryTree;
	root = Create(root, 0);
	printf(IsFindTree(root, INT32_MIN, INT32_MAX) ? "YES" : "NO");
	return 0;
}
