#define _CRT_SECURE_NO_WARNINGS
#include <fstream>
#include <iostream>
#include <vector>
#include <iterator>
#include <list>
#include <algorithm>

using namespace std;
int Size;
int vectorFirts[6000];
int vectorSecond[6000];
int vByte[256];
int vTin[6000 * 6000];
int vTmp[6000 * 6000];


int main() {

	freopen("input.txt", "r", stdin);
	freopen("output.txt", "w", stdout);
	int sizeFirst, sizeSecond;
	cin >> sizeFirst >> sizeSecond;
	Size = sizeFirst * sizeSecond;

	for (int i = 0; i < sizeFirst; i++) {
		scanf("%d", &vectorFirts[i]);
	}
	for (int i = 0; i < sizeSecond; i++) {
		scanf("%d", &vectorSecond[i]);
	}

	for (int i = 0; i < sizeFirst; i++) {
		for (int j = 0; j < sizeSecond; j++) {
			vTin[i * sizeSecond + j] = vectorFirts[i] * vectorSecond[j];
		}
	}


	for (int i = 0; i < 4; i++) {
		for (int i = 0; i < 256; i++) {
			vByte[i] = 0;
		}
		for (int j = 0; j < Size; j++) {
			vByte[(vTin[j] >> i * 8) & 255]++;
		}
		for (int j = 1; j < 256; ++j) {
			vByte[j] += vByte[j - 1];
		}
		for (int j = Size - 1; j >= 0; --j) {
			vByte[(vTin[j] >> i * 8) & 255]--;
			vTmp[vByte[(vTin[j] >> i * 8) & 255]] = vTin[j];
		}
		for (int j = 0; j < Size; j++) {
			vTin[j] = vTmp[j];
		}
	}

	long long sum = 0;
	for (int i = 0; i < Size; i += 10) {
		sum += vTin[i];
	}
	cout << sum;
	return 0;
}
