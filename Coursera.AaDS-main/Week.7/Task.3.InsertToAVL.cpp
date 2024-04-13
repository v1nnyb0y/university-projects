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

#define ptrnode node *

struct node // структура для представления узлов дерева
{
	int key;
	unsigned char height;
	node* left;
	node* right;

	node(int k) {
		key = k;
		left = right = nullptr;
		height = 1;
	}

	node() : key(), left(), right() {}

	node(int _key, unsigned char _height, node* _left, node* _right) {
		key = _key;
		height = _height;
		left = _left;
		right = _right;
	}
};

unsigned char height(node* p) {
	return p ? p->height : 0;
}

int bfactor(node* p) {
	if (!p) {
		return 0;
	}

	return height(p->right) - height(p->left);
}

void fixheight(node* p) {
	unsigned char hl = height(p->left);
	unsigned char hr = height(p->right);
	p->height = (hl > hr ? hl : hr) + 1;
}

node* rotateright(node* p) {
	node* q = p->left;
	p->left = q->right;
	q->right = p;
	fixheight(p);
	fixheight(q);
	return q;
}

node* rotateleft(node* q) {
	node* p = q->right;
	q->right = p->left;
	p->left = q;
	fixheight(q);
	fixheight(p);
	return p;
}

node* balance(node* p) {
	fixheight(p);
	if (bfactor(p) == 2) {
		if (bfactor(p->right) < 0)
			p->right = rotateright(p->right);
		return rotateleft(p);
	}
	if (bfactor(p) == -2) {
		if (bfactor(p->left) > 0)
			p->left = rotateleft(p->left);
		return rotateright(p);
	}
	return p;
}

node* insert(node* p, int k) {
	if (!p) return new node(k);
	if (k < p->key)
		p->left = insert(p->left, k);
	else
		p->right = insert(p->right, k);
	return balance(p);
}

void Output(node* root) {
	queue<node*> q;

	int count = 2;
	q.push(root);
	while (q.size() != 0) {
		node* t = q.front();
		q.pop();

		printf("%d ", t->key);
		if (t->left != nullptr) {
			q.push(t->left);
			printf("%d ", count);
			count++;
		}
		else {
			printf("%d ", 0);
		}

		if (t->right != nullptr) {
			q.push(t->right);
			printf("%d\n", count);
			count++;
		}
		else {
			printf("%d\n", 0);
		}
	}
}

ptrnode Create(ptrnode root, int index) {
	root = new node(vLines[index][0], 1, nullptr, nullptr);
	if (vLines[index][1] == 0 && vLines[index][2] == 0) {
		return root;
	}
	if (vLines[index][1] != 0) {
		root->left = new node;
		root->left = Create(root->left, vLines[index][1] - 1);
		fixheight(root->left);
	}
	if (vLines[index][2] != 0) {
		root->right = new node;
		root->right = Create(root->right, vLines[index][2] - 1);
		fixheight(root->right);
	}
	return root;
}

int main() {
	//ios::sync_with_stdio(false);
	//cin.tie(NULL);
	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	//ifstream input("input.txt");
	int n;
	scanf("%d\n", &n);
	ptrnode root = nullptr;
	vLines.resize(n);
	for (int id = 0; id < n; id++) {
		vLines[id].resize(3);
		scanf("%d %d %d", &vLines[id][0], &vLines[id][1], &vLines[id][2]);
	}
	if (n != 0)
		root = Create(root, 0);
	int digit;
	scanf("%d", &digit);
	root = insert(root, digit);
	printf("%d\n", n + 1);
	Output(root);
	return 0;
}
