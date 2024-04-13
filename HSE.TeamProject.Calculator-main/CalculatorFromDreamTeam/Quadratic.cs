using System;

namespace CalculatorFromDreamTeam
{
    public delegate void Method();

    public class Quadratic
    {
        private readonly double _a; // coef
        private readonly double _b; // coef
        private readonly double _c; // coef

        private int _count; // count of solutions

        private readonly Method _solveEquation; // delegate fro solution

        private int _type = 1; // type: 0 - simple coef, 1 - x, 2- x^2 

        private string _x1, _x2; // solution

        public Quadratic(params double[] coefficiensDoubles) {
            switch (coefficiensDoubles.Length) {
                case 1: {
                    _a = 0;
                    _b = 0;
                    _c = coefficiensDoubles[0];
                    break;
                } //zero type
                case 2: {
                    _a = 0;
                    _b = coefficiensDoubles[0];
                    _c = coefficiensDoubles[1];
                    break;
                } //1 type
                case 3: {
                    _a = coefficiensDoubles[0];
                    _b = coefficiensDoubles[1];
                    _c = coefficiensDoubles[2];
                    break;
                } //2 type
            }

            _solveEquation += Type;  // type
            _solveEquation += Solve; // solution
        }

        private void Type() {
            _type = _a == 0 ? (_b == 0 ? 0 : 1) : 2;
        } // determination type

        private void Solve() {
            switch (_type) {
                case 0:
                    FreeSolution();
                    break;
                case 1:
                    LinalSolution();
                    break;
                case 2:
                    QuadSolution();
                    break;
            }
        } // solve

        private void QuadSolution() {
            var discriminant = Math.Pow(_b, 2) - 4 * _a * _c;

            if (discriminant < 0) {
                _count = 2;
                _x1 = -_b / (2 * _a) + " + " + Math.Sqrt(-discriminant) / (2 * _a) + "i";
                _x2 = -_b / (2 * _a) + " - " + Math.Sqrt(-discriminant) / (2 * _a) + "i";
            }
            else {
                if (discriminant == 0) {
                    _count = 1;
                    _x1 = (-_b / (2 * _a)).ToString();
                    _x2 = _x1;
                }
                else {
                    _count = 2;
                    _x1 = ((-_b + Math.Sqrt(discriminant)) / (2 * _a)).ToString();
                    _x2 = ((-_b - Math.Sqrt(discriminant)) / (2 * _a)).ToString();
                }
            }
        } //quad solution

        private void LinalSolution() {
            _count = 1;
            _x1 = (-_c / _b).ToString();
        } // linal solution

        private void FreeSolution() {
            _count = _c == 0 ? int.MaxValue : 0;
        } // free solution

        public void Solve(out int count, out string x1, out string x2) {
            _solveEquation();
            count = _count;
            x1 = _x1;
            x2 = _x2;
        } // outp solution
    }
}