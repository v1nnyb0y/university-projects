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

node* rotateright(node* p) // правый поворот вокруг p
{
	node* q = p->left;
	p->left = q->right;
	q->right = p;
	fixheight(p);
	fixheight(q);
	return q;
}

node* rotateleft(node* q) // левый поворот вокруг q
{
	node* p = q->right;
	q->right = p->left;
	p->left = q;
	fixheight(q);
	fixheight(p);
	return p;
}

node* balance(node* p) // балансировка узла p
{
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
	return p; // балансировка не нужна
}

node* insert(node* p, int k) // вставка ключа k в дерево с корнем p
{
	if (!p) return new node(k);
	if (p->key == k) return p;
	if (k < p->key)
		p->left = insert(p->left, k);
	else
		p->right = insert(p->right, k);
	return balance(p);
}

node* findmin(node* p) // поиск узла с минимальным ключом в дереве p 
{
	return p->right->right ? findmin(p->right) : p;
}

node* removemin(node* p) // удаление узла с минимальным ключом из дерева p
{
	if (p->left) {
		p = p->left;
		return balance(p);
	}
	return nullptr;
}

node* remove(node* p, int k) // удаление ключа k из дерева p
{
	if (!p) return nullptr;
	if (k < p->key)
		p->left = remove(p->left, k);
	else if (k > p->key)
		p->right = remove(p->right, k);
	else //  k == p->key 
	{
		if (p->right == nullptr && p->left == nullptr) {
			return nullptr;
		}

		if (p->right == nullptr && p->left != nullptr) {
			p = p->left;
			return balance(p);
		}

		if (p->right != nullptr && p->left == nullptr) {
			p = p->right;
			return balance(p);
		}

		if (!p->left->right) {
			if (!p->left->left) {
				p = new node(p->left->key, p->height, nullptr, p->right);
				return balance(p);
			}
			p = new node(p->left->key, p->left->height, p->left->left, p->right);
			return balance(p);
		}
		node* max = findmin(p->left);
		int tmp = max->right->key;
		node* newLeft = new node(p->left->key, p->left->height, p->left->left, p->left->right);
		newLeft = remove(newLeft, max->right->key);
		p = new node(tmp, p->height, newLeft, p->right);
		return balance(p);
	}
	return balance(p);
}


void find(int x, node* p) {
	if (p == nullptr) {
		printf("N\n");
	}
	else {
		if (x < p->key) {
			find(x, p->left);
		}
		else {
			if (x > p->key) {
				find(x, p->right);
			}
			else {
				printf("Y\n");
			}
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
	scanf("%d\n", &n);
	ptrnode root = nullptr;
	for (int id = 0; id < n; id++) {
		char c;
		int digit;
		scanf("%c %d\n", &c, &digit);
		switch (c) {
		case 'A': {
			root = insert(root, digit);
			printf("%d\n", bfactor(root));
			break;
		}
		case 'D': {
			root = remove(root, digit);
			printf("%d\n", bfactor(root));
			break;
		}
		case 'C': {
			find(digit, root);
			break;
		}
		}
	}
	return 0;
}
