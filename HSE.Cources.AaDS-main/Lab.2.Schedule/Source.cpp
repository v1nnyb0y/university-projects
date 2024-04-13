#include <iostream>
#include <fstream>
#include  <vector>
#include <iterator>
#include <algorithm>

/*Программа решает задачу на расписание.
 *На вход программе подается файл:
 *1. Первая строка: кол-во работников. Тип int
 *2. Вторая строка: вектор продолжительности работ
 */

using namespace std;

//Считаем сумму работ и самую продолжительную работу
void Sum_a_Max(vector<double> vec, double& sum, double& maximum) {
	sum = 0;
	maximum = INT32_MIN;
	for (auto el : vec) {
		sum += el;
		maximum = max(el, maximum);
	}
}

//Читаем файл
vector<double> ReadFile(int& number_workers) {
	ios::sync_with_stdio(false);
	cin.tie(nullptr);
	ifstream input_file("input.txt");
	input_file >> number_workers; //Читаем кол-во работников
	vector<double> vec{ istream_iterator<double>(input_file), istream_iterator<double>() };
	//Читаем время выполнения каждой работы
	return vec;
}

//Выводим стобчатую диаграмму
void OutputTable(vector<double> vec, int workers, double max_time) {
	char letter = 'A';
	bool ok = false;
	double element = 0;
	for (int i = 0; i < workers; i++) {
		cout << i + 1 << ": ";
		double start = 0;
		while (true) {
			if (!vec.empty()) {
				if (element == 0) {
					element = vec[0];
					vec.erase(vec.cbegin());
					if (ok)
						letter++;
					ok = true;
				}
			}
			else {
				if (start == 0 && element <= 0) {
					cout << "Slacker" << endl;
					break;
				}
				if (!element > 0)
					break;
			}
			if (start + element < max_time) {
				cout << letter << "(" << start << " - " << start + element << ")" << " ";
				start += element;
				element = 0;

			}
			else {
				cout << letter << "(" << start << " - " << max_time << ")" << endl;
				element -= max_time - start;
				break;
			}
		}
	}
}

int main() {
	double sum, maximum;
	int number_workers;
	const vector<double> vec = ReadFile(number_workers); //Считываем файл
	Sum_a_Max(vec, sum, maximum); //Считаем сумму работ и самую продолжительную работу
	double mid = sum / number_workers; //Считаем среднее значение по работам
	double max_time = max(maximum, mid); //Находим длину рабочего дня
	OutputTable(vec, number_workers, max_time); //Выводим столбчатую диаграмму
	return 0;
}
