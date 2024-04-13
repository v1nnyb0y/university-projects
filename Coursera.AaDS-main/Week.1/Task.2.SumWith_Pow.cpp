#include <iostream>
#include <fstream>

using namespace std;

int main() {
	ifstream input_file("input.txt");
	long long a, b, c;
	input_file >> a;
	input_file >> b;
	input_file.close();
	ofstream output_file("output.txt");
	c = a + b * b;
	printf("%u", c);
	output_file << c;
	output_file.close();
	return 0;
}
