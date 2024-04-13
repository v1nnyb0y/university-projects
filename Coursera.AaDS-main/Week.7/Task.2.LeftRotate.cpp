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

#define pBinaryTree BinaryTree *

struct BinaryTree
{
	long Data;
	long High;
	long Balance;
	pBinaryTree Left;
	pBinaryTree Right;
	BinaryTree() : Data(), High(), Balance(), Left(), Right() {}

	BinaryTree(long _Data, long _High, long _Balance, pBinaryTree _Left, pBinaryTree _Right) {
		Data = _Data;
		High = _High;
		Balance = _Balance;
		Left = _Left;
		Right = _Right;
	}
};

pBinaryTree copy(pBinaryTree root) {
	if (root == nullptr) {
		return nullptr;
	}
	pBinaryTree newT = new BinaryTree(root->Data, root->High, root->Balance, root->Left, root->Right);
	return newT;
}

pBinaryTree Create(pBinaryTree root, int index) {
	root = new BinaryTree(vLines[index][0], 1, 0, nullptr, nullptr);
	if (vLines[index][1] == 0 && vLines[index][2] == 0) {
		return root;
	}
	if (vLines[index][1] != 0) {
		root->Left = new BinaryTree();
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
		root->Balance = root->Right->High;
		return root;
	}
	if (root->Right == nullptr) {
		root->Balance = 0 - root->Left->High;
		return root;
	}

	root->Balance = root->Right->High - root->Left->High;
	return root;
}

pBinaryTree SmallLeft(pBinaryTree root) {
	pBinaryTree newT = copy(root);
	pBinaryTree X = copy(root->Left);
	pBinaryTree Y = copy(root->Right->Left);
	pBinaryTree Z = copy(root->Right->Right);

	newT->Data = root->Right->Data;
	newT->Left = new BinaryTree(root->Data, 0, 0, X, Y);
	newT->Right = Z;

	return newT;
}

pBinaryTree BigLeft(pBinaryTree root) {
	pBinaryTree newT = copy(root);
	pBinaryTree W = copy(root->Left);
	pBinaryTree X = copy(root->Right->Left->Left);
	pBinaryTree Y = copy(root->Right->Left->Right);
	pBinaryTree Z = copy(root->Right->Right);

	newT->Data = root->Right->Left->Data;
	newT->Right = new BinaryTree(root->Right->Data, 0, 0, Y, Z);
	newT->Left = new BinaryTree(root->Data, 0, 0, W, X);

	return newT;
}

pBinaryTree LeftSwap(pBinaryTree root) {
	if (root->Right->Balance == -1) {
		return BigLeft(root);
	}
	return SmallLeft(root);
}

void Output(pBinaryTree root) {
	queue<pBinaryTree> q;

	int count = 2;
	q.push(root);
	while (q.size() != 0) {
		pBinaryTree t = q.front();
		q.pop();

		printf("%d ", t->Data);
		if (t->Left != nullptr) {
			q.push(t->Left);
			printf("%d ", count);
			count++;
		}
		else {
			printf("%d ", 0);
		}

		if (t->Right != nullptr) {
			q.push(t->Right);
			printf("%d\n", count);
			count++;
		}
		else {
			printf("%d\n", 0);
		}
	}
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
	pBinaryTree root = nullptr;
	root = Create(root, 0);
	root = LeftSwap(root);
	printf("%d\n", n);
	Output(root);
	return 0;
}
