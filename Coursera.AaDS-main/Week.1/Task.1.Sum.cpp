#include <fstream>

using namespace std;

int main() {
	ifstream input_file("input.txt");
	int a, b;
	input_file >> a;
	input_file >> b;
	input_file.close();
	ofstream output_file("output.txt");
	output_file << a + b;
	output_file.close();
}
