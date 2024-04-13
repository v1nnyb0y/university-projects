#include  <fstream>
#include  <iostream>
#include <array>

using namespace std;

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
	output_file << "1";

	for (int j = 1; j < size; j++) {
		i = j;
		while ((i > 0) && (array[i] < array[i - 1])) {
			swap(array[i], array[i - 1]);
			i--;
		}
		output_file << " " << i + 1;
	}
	output_file << endl;
	for (int i = 0; i < size; ++i)
		output_file << array[i] << " ";
	output_file.close();
	return 0;
}
