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
	long High;
	BinaryTree* Left;
	BinaryTree* Right;

public:
	BinaryTree(long data, BinaryTree* left, BinaryTree* right) {
		Data = data;
		Left = left;
		Right = right;
		High = _Max_value(Left->High, Right->High);
	}

	BinaryTree(long data) {
		High = 1;
		Data = data;
	}

	void addLeft(BinaryTree* root) {
		Left = root;
		High = _Max_value(High, Left->High);
	}

	void addRigth(BinaryTree* root) {
		Right = root;
		High = _Max_value(High, Right->High);
	}
};

BinaryTree Create(int index) {
	BinaryTree bt(vLines[index][0]);
	if (vLines[index][1] == 0 && vLines[index][2] == 0) {
		return bt;
	}
	if (vLines[index][1] != 0) {
		BinaryTree* left = &Create(vLines[index][1] - 1);
		bt.addLeft(left);
	}
	if (vLines[index][2] != 0) {
		BinaryTree* right = &Create(vLines[index][2] - 1);
		bt.addRigth(right);
	}
	bt.High += 1;
	return bt;
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
		printf("0");
		return 0;
	}
	vLines.resize(n);
	for (int id = 0; id < n; id++) {
		vLines[id].resize(3);
		scanf("%d %d %d\n", &vLines[id][0], &vLines[id][1], &vLines[id][2]);
	}
	BinaryTree root = Create(0);
	printf("%d", root.High);
	return 0;
}
