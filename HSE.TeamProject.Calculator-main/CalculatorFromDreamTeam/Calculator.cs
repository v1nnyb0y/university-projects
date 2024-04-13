using ELW.Library.Math;
using ELW.Library.Math.Tools;
using System.Collections.Generic;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;

namespace CalculatorFromDreamTeam
{
    public class Calculator
    {
        internal double _aCoefficient;  // closed field of 'a' coefficient
        internal double _bCoefficient;  // closed field of 'b' coefficient
        internal double _cCoefficient;  // closed field of 'c' coefficient
        internal string _firstRoot;     // closed field of first root of equation
        internal string _secondRoot;    // closed field of second roots of equation

        /// <summary>
        /// Gets the 'a' coefficient.
        /// </summary>
        /// <value>A coefficient.</value>
        public double a
        {
            get => _aCoefficient;
            private set => _aCoefficient = value;
        }

        /// <summary>
        /// Gets the 'b' coefficient.
        /// </summary>
        /// <value>B coefficient.</value>
        public double b
        {
            get => _bCoefficient;
            private set => _bCoefficient = value;
        }

        /// <summary>
        /// Gets the 'c' coefficient.
        /// </summary>
        /// <value>C coefficient.</value>
        public double c
        {
            get => _cCoefficient;
            private set => _cCoefficient = value;
        }

        /// <summary>
        /// Gets the first root of equation.
        /// </summary>
        /// <value>The first root of equation.</value>
        public string firstRoot
        {
            get => _firstRoot;
            private set => _firstRoot = value;
        }

        /// <summary>
        /// Gets the second root of equation.
        /// </summary>
        /// <value>The second root of equation.</value>
        public string secondRoot
        {
            get => _secondRoot;
            private set => _secondRoot = value;
        }

        /// <summary>
        /// Initializes a new default instance of the <see cref="T:CalculatorFromDreamTeam.Calculator"/> class.
        /// </summary>
        public Calculator()
        {
            a = 0;
            b = 0;
            c = 0;

            transferTo();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CalculatorFromDreamTeam.Calculator"/> class.
        /// </summary>
        /// <param name="a">The 'a' coefficient.</param>
        /// <param name="b">The 'b' coefficient.</param>
        /// <param name="c">The 'c' coefficient.</param>
        public Calculator(string a, string b, string c)
        {
            changeCoefficients(a, b, c);
        }

        /// <summary>
        /// Parse the specified expression.
        /// </summary>
        /// <returns>The parsing result.</returns>
        /// <param name="expression">The expression.</param>
        public double parse(string expression)
        {
            //converting the source string to a compiled view
            PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(expression);
            CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);

            //list of variables, not it is empty, since here are no vatiables
            List<VariableValue> variables = new List<VariableValue>();

            //returning the result of calculation
            return ToolsHelper.Calculator.Calculate(compiledExpression, variables);
        }
    
        /// <summary>
        /// Transfers the coefficients to <see cref="Quadratic"/> class and sets the roots of equation.
        /// </summary>
        void transferTo()
        {
            int count;  //the count of roots
            string x1;  //first  roots
            string x2;  //second roots

            Quadratic quadratic = new Quadratic(a, b, c);
            quadratic.Solve(out count, out x1, out x2);

            //setting the roots based on count of roots
            switch (count) {
                case 0: firstRoot = secondRoot = "null"; break;
                case 1: firstRoot = secondRoot = x1; break;
                case 2:
                    firstRoot = x1;
                    secondRoot = x2;
                    break;
                case int.MaxValue: firstRoot = secondRoot = "infinitely many solutions"; break;
                default: firstRoot = secondRoot = "no roots founf"; break;
            }
        }
    
        /// <summary>
        /// Changes the coefficients and calculate a the new roots for them.
        /// </summary>
        /// <param name="a">The 'a' coefficient.</param>
        /// <param name="b">The 'b' coefficient.</param>
        /// <param name="c">The 'c' coefficient.</param>
        public void changeCoefficients(string a, string b, string c)
        {
            this.a = parse(a);
            this.b = parse(b);
            this.c = parse(c);

            transferTo();
        }

        /// <summary>
        /// Tries to parse the expression and get the result of parsing.
        /// </summary>
        /// <returns>The result of expression and <see cref="true"/> or the default value of <see cref="double"/> and <see cref="false"/>.</returns>
        /// <param name="expression">Expression.</param>
        /// <param name="result">Result of parsing.</param>
        bool tryParse(string expression, out double result)
        {
            try {
                result = parse(expression);
                return true;
            } catch (CompilerSyntaxException) {
                result = default(double);
                return false;
            }
        }
    
        /// <summary>
        /// Checks the algebraic expression for reality.
        /// </summary>
        /// <returns><c>true</c>, if expression was valided, <c>false</c> otherwise.</returns>
        /// <param name="expression">Expression.</param>
        public bool validExpression(string expression)
        {
            return tryParse(expression, out double d);
        }
    }
}