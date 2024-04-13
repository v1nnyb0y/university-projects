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

int removeRoot(BinaryTree* root, long data) {
	if (root->Data == data) {
		return root->Count;
	}
	if (data < root->Data) {
		if (root->Left != nullptr) {
			int temp = root->Count;
			root->Count -= removeRoot(root->Left, data);
			if (root->Left->Data == data) {
				root->Left = nullptr;
			}
			return (temp - root->Count);
		}
	}
	else {
		if (root->Right != nullptr) {
			int temp = root->Count;
			root->Count -= removeRoot(root->Right, data);
			if (root->Right->Data == data) {
				root->Right = nullptr;
			}
			return (temp - root->Count);
		}
	}
	return 0;
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
	for (int id = 0; id < n; id++) {
		vLines[id].resize(3);
		scanf("%d %d %d", &vLines[id][0], &vLines[id][1], &vLines[id][2]);
	}
	BinaryTree* root = new BinaryTree;
	root = Create(root, 0);
	int m;
	scanf("%d", &m);
	for (int id = 0; id < m; id++) {
		long data;
		scanf("%d", &data);
		removeRoot(root, data);
		printf("%d\n", root->Count);
	}
	return 0;
}
