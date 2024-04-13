#include  <fstream>
#include  <iostream>

using namespace std;

int main() {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	ifstream input_file("input.txt");
	int size;
	input_file >> size;
	double* array = new double[size];
	int* index = new int[size];
	int i = 0;
	while (i < size) {
		input_file >> array[i];
		index[i] = i + 1;
		i++;
	}
	input_file.close();

	for (int j = 1; j < size; j++) {
		i = j;
		while ((i > 0) && (array[i] < array[i - 1])) {
			swap(array[i], array[i - 1]);
			swap(index[i], index[i - 1]);
			i--;
		}
	}

	ofstream output_file("output.txt");
	output_file << index[0] << " " << index[size / 2] << " " << index[size - 1];
	delete[] index;
	delete[] array;
	output_file.close();
	return 0;
}
