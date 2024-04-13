#include  <fstream>
#include  <iostream>
#include <array>

using namespace std;

void swap(int* xp, int* yp) {
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}

void selectionSort(int arr[], int n) {
	int i, j, min_idx;
	ofstream output_file("output.txt");

	// One by one move boundary of unsorted subarray
	for (i = 0; i < n - 1; i++) {
		// Find the minimum element in unsorted array
		min_idx = i;
		for (j = i + 1; j < n; j++)
			if (arr[j] < arr[min_idx])
				min_idx = j;

		// Swap the found minimum element with the first element
		swap(&arr[min_idx], &arr[i]);
		if (i != min_idx)
			output_file << "Swap elements at indices " << i + 1 << " and " << min_idx + 1 << ".\n";
	}
	output_file << "No more swaps needed.\n";
	for (int i = 0; i < n; ++i)
		output_file << arr[i] << " ";
}


int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	ifstream input_file("input.txt");
	int size;
	input_file >> size;
	auto* array = new int[size];
	auto i = 0;
	while (i < size) {
		input_file >> array[i++];
	}
	input_file.close();
	ofstream output_file("output.txt");
	selectionSort(array, size);
	delete[] array;
	output_file.close();
	return 0;
}
