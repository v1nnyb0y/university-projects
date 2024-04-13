using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CalculatorFromDreamTeam
{
    public partial class MainWindow : Form
    {
        //создаем экземпляр класса Calculator
        Calculator calculator = new Calculator();
        //текущий заполняемый коэффициэнт
        private byte CurrentKoef = 1;
        //добавляем цифру в коэффициэент
        private void AddDigit(byte digit)
        {
            switch (CurrentKoef)
            {
                case 1:
                    if (tKoefA.Text != "0")
                        tKoefA.Text += digit;
                    else tKoefA.Text = digit.ToString();
                    break;
                case 2:
                    if (tKoefB.Text != "0")
                        tKoefB.Text += digit;
                    else tKoefB.Text = digit.ToString();
                    break;
                case 3:
                    if (tKoefC.Text != "0")
                        tKoefC.Text += digit;
                    else tKoefC.Text = digit.ToString();
                    break;
            }
        }
        //добавляем запятую в коэффициэент
        private void PutComma()
        {
            switch (CurrentKoef)
            {
                case 1:
                    if (tKoefA.Text != "")
                    {
                        if (tKoefA.Text[tKoefA.Text.Length - 1] != '√')
                        {
                            if (!tKoefA.Text.Contains(",")) tKoefA.Text += ",";
                        }
                        else tKoefA.Text += "0,";
                    }
                    else tKoefA.Text = "0,";
                    break;
                case 2:
                    if (tKoefB.Text != "")
                    {
                        if (tKoefB.Text[tKoefB.Text.Length - 1] != '√')
                        {
                            if (!tKoefB.Text.Contains(",")) tKoefB.Text += ",";
                        }
                        else tKoefB.Text += "0,";
                    }
                    else tKoefB.Text = "0,";
                    break;
                case 3:
                    if (tKoefC.Text != "")
                    {
                        if (tKoefC.Text[tKoefC.Text.Length - 1] != '√')
                        {
                            if (!tKoefC.Text.Contains(",")) tKoefC.Text += ",";
                        }
                        else tKoefC.Text += "0,";
                    }
                    else tKoefC.Text = "0,";
                    break;
            }
        }
        //добавляем квадратный корень в коэффициэнт
        private void AddRoot()
        {
            switch (CurrentKoef)
            {
                case 1:
                    if (tKoefA.Text != "")
                    {
                        if (tKoefA.Text[tKoefA.Text.Length - 1] != ',')
                        {
                            if (!tKoefA.Text.Contains("√")) tKoefA.Text += "√";
                        }
                    }
                    else tKoefA.Text = "√";
                    break;
                case 2:
                    if (tKoefB.Text != "")
                    {
                        if (tKoefB.Text[tKoefB.Text.Length - 1] != ',')
                        {
                            if (!tKoefB.Text.Contains("√")) tKoefB.Text += "√";
                        }
                    }
                    else tKoefB.Text = "√";
                    break;
                case 3:
                    if (tKoefC.Text != "")
                    {
                        if (tKoefC.Text[tKoefC.Text.Length - 1] != ',')
                        {
                            if (!tKoefC.Text.Contains("√")) tKoefC.Text += "√";
                        }
                    }
                    else tKoefC.Text = "√";
                    break;
            }
        }
        //удаление последнего символа
        private void DeleteSymbol()
        {
            switch (CurrentKoef)
            {
                case 1:
                    if (tKoefA.Text != "")
                    {
                        tKoefA.Text = tKoefA.Text.Remove(tKoefA.TextLength - 1, 1);
                    }
                    break;
                case 2:
                    if (tKoefB.Text != "")
                    {
                        tKoefB.Text = tKoefB.Text.Remove(tKoefB.TextLength - 1, 1);
                    }
                    break;
                case 3:
                    if (tKoefC.Text != "")
                    {
                        tKoefC.Text = tKoefC.Text.Remove(tKoefC.TextLength - 1, 1);
                    }
                    break;
            }
        }
        //Изменение знака
        private void ChangeSign()
        {

            switch (CurrentKoef)
            {
                case 1:
                    if (tKoefA.Text != "")
                    {
                        if (tKoefA.Text[0] != '-')
                        {
                            tKoefA.Text = '-' + tKoefA.Text;
                        }
                        else tKoefA.Text = tKoefA.Text.Remove(0, 1);
                    }
                    else tKoefA.Text = "-";
                    break;
                case 2:
                    if (tKoefB.Text != "")
                    {
                        if (tKoefB.Text[0] != '-')
                        {
                            tKoefB.Text = '-' + tKoefB.Text;
                        }
                        else tKoefB.Text = tKoefB.Text.Remove(0, 1);
                    }
                    else tKoefB.Text = "-";
                    break;
                case 3:
                    if (tKoefC.Text != "")
                    {
                        if (tKoefC.Text[0] != '-')
                        {
                            tKoefC.Text = '-' + tKoefC.Text;
                        }
                        else tKoefC.Text = tKoefC.Text.Remove(0, 1);
                    }
                    else tKoefC.Text = "-";
                    break;
            }
        }
        //Возводим коэффициэнт в степень (-1)
        private void InverseKoef()
        {
            switch (CurrentKoef)
            {
                case 1:
                    if (!lOne.Visible)
                    {
                        if (tKoefA.Text != "" && tKoefA.Text != "0")
                        {
                            lOne.Visible = true;
                            lDivideLine.Visible = true;
                        }
                    }
                    else
                    {
                        lOne.Visible = false;
                        lDivideLine.Visible = false;
                    }
                    break;
                case 2:
                    if (!lOne1.Visible)
                    {
                        if (tKoefB.Text != "" && tKoefB.Text != "0")
                        {
                            lOne1.Visible = true;
                            lDivideLine1.Visible = true;
                        }

                    }
                    else
                    {
                        lOne1.Visible = false;
                        lDivideLine1.Visible = false;
                    }
                    break;
                case 3:
                    if (!lOne2.Visible)
                    {
                        if (tKoefC.Text != "" && tKoefC.Text != "0")
                        {
                            lOne2.Visible = true;
                            lDivideLine2.Visible = true;
                        }

                    }
                    else
                    {
                        lOne2.Visible = false;
                        lDivideLine2.Visible = false;
                    }
                    break;
            }
        }
        //передвигаем стрелку
        private void MoveArrow()
        {
            switch (CurrentKoef)
            {
                case 1:
                    lArrow.Visible = true;
                    lArrow1.Visible = false;
                    lArrow2.Visible = false;
                    break;
                case 2:
                    lArrow.Visible = false;
                    lArrow1.Visible = true;
                    lArrow2.Visible = false;
                    break;
                case 3:
                    lArrow.Visible = false;
                    lArrow1.Visible = false;
                    lArrow2.Visible = true;
                    break;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        //возвращаем фокус форме
        private void ReturnFocus(object sender, EventArgs e)
        {
            lEquals.Focus();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Привязываем контекстное меню к истории вычислений
            rHistory.ContextMenuStrip = cMenu;
            //выравниваем по правому краю коэффициэнты
            tKoefA.TextAlign = HorizontalAlignment.Right;
            tKoefB.TextAlign = HorizontalAlignment.Right;
            tKoefC.TextAlign = HorizontalAlignment.Right;
            //запрет фокуса на любой кнопке
            bZero.GotFocus += new EventHandler(ReturnFocus);
            bOne.GotFocus += new EventHandler(ReturnFocus);
            bTwo.GotFocus += new EventHandler(ReturnFocus);
            bThree.GotFocus += new EventHandler(ReturnFocus);
            bFour.GotFocus += new EventHandler(ReturnFocus);
            bFive.GotFocus += new EventHandler(ReturnFocus);
            bSix.GotFocus += new EventHandler(ReturnFocus);
            bSeven.GotFocus += new EventHandler(ReturnFocus);
            bEight.GotFocus += new EventHandler(ReturnFocus);
            bNine.GotFocus += new EventHandler(ReturnFocus);
            bSqroot.GotFocus += new EventHandler(ReturnFocus);
            bSignChange.GotFocus += new EventHandler(ReturnFocus);
            bResult.GotFocus += new EventHandler(ReturnFocus);
            bClear.GotFocus += new EventHandler(ReturnFocus);
            bComma.GotFocus += new EventHandler(ReturnFocus);
            bDelete.GotFocus += new EventHandler(ReturnFocus);
            bInverse.GotFocus += new EventHandler(ReturnFocus);
            bNextKoef.GotFocus += new EventHandler(ReturnFocus);
            rHistory.GotFocus += new EventHandler(ReturnFocus);
            tKoefA.GotFocus += new EventHandler(ReturnFocus);
            tKoefB.GotFocus += new EventHandler(ReturnFocus);
            tKoefC.GotFocus += new EventHandler(ReturnFocus);
            //открываем файл истории и записываем его
            string path = Directory.GetCurrentDirectory() + "\\temp.txt";
            if (File.Exists(path))
            {
                string[] file = File.ReadAllLines(path);
                foreach (string s in file)
                {
                    rHistory.Text += s + "\n";
                }
            }
        }

        private void bZero_Click(object sender, EventArgs e)
        {
            AddDigit(0);
        }
        private void bOne_Click(object sender, EventArgs e)
        {
            AddDigit(1);
        }
        private void bTwo_Click(object sender, EventArgs e)
        {
            AddDigit(2);
        }

        private void bThree_Click(object sender, EventArgs e)
        {
            AddDigit(3);
        }

        private void bFour_Click(object sender, EventArgs e)
        {
            AddDigit(4);
        }

        private void bFive_Click(object sender, EventArgs e)
        {
            AddDigit(5);
        }

        private void bSix_Click(object sender, EventArgs e)
        {
            AddDigit(6);
        }
        private void bSeven_Click(object sender, EventArgs e)
        {
            AddDigit(7);
        }

        private void bEight_Click(object sender, EventArgs e)
        {
            AddDigit(8);
        }

        private void bNine_Click(object sender, EventArgs e)
        {
            AddDigit(9);
        }

        private void bNextKoef_Click(object sender, EventArgs e)
        {
            if (CurrentKoef < 3) CurrentKoef++;
            else CurrentKoef = 1;
            MoveArrow();
        }
        private void bComma_Click(object sender, EventArgs e)
        {
            PutComma();
        }

        private void tKoefA_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentKoef = 1;
            MoveArrow();
        }

        private void tKoefB_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentKoef = 2;
            MoveArrow();
        }

        private void tKoefC_MouseClick(object sender, MouseEventArgs e)
        {
            CurrentKoef = 3;
            MoveArrow();

        }
        private void bClear_MouseClick(object sender, MouseEventArgs e)
        {
            switch (CurrentKoef)
            {
                case 1:
                    tKoefA.Text = "";
                    lOne.Visible = false;
                    lDivideLine.Visible = false;
                    break;
                case 2:
                    lOne1.Visible = false;
                    lDivideLine1.Visible = false;
                    tKoefB.Text = "";
                    break;
                case 3:
                    lOne2.Visible = false;
                    lDivideLine2.Visible = false;
                    tKoefC.Text = "";
                    break;
            }
        }
        //событие нажатия ПКМ на кнопке С
        private void bClear_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tKoefA.Text = "";
                tKoefB.Text = "";
                tKoefC.Text = "";
                CurrentKoef = 1;
                //убираем единицы с дробями сверху
                lOne.Visible = false;
                lOne1.Visible = false;
                lOne2.Visible = false;
                lDivideLine.Visible = false;
                lDivideLine1.Visible = false;
                lDivideLine2.Visible = false;
            }
        }

        private void bSqroot_Click(object sender, EventArgs e)
        {
            AddRoot();
        }
        private void bResult_Click(object sender, EventArgs e)
        {

            string koefA = tKoefA.Text, koefB = tKoefB.Text, koefC = tKoefC.Text;
            //заполнение пустых коэффициэнтов нулями
            if (koefA == "" || koefA == null) koefA = "0";
            if (koefB == "" || koefB == null) koefB = "0";
            if (koefC == "" || koefC == null) koefC = "0";
            //удаление последней запятой
            if (koefA[koefA.Length - 1] == ',') koefA = koefA.Remove(koefA.Length - 1, 1);
            if (koefB[koefB.Length - 1] == ',') koefB = koefB.Remove(koefB.Length - 1, 1);
            if (koefC[koefC.Length - 1] == ',') koefC = koefC.Remove(koefC.Length - 1, 1);
            //изменение запятых на точки
            koefA = koefA.Replace(',', '.');
            koefB = koefB.Replace(',', '.');
            koefC = koefC.Replace(',', '.');
            //изменение знака корня на ^(0.5)
            if (koefA.Contains("√"))
            {
                if (koefA.IndexOf('√') == 0)
                    koefA = koefA.Replace('√', '(') + ")^(0.5)";
                else koefA = koefA.Replace("√", "*(") + ")^(0.5)";
            }
            if (koefB.Contains("√"))
            {
                if (koefB.IndexOf('√') == 0)
                    koefB = koefB.Replace('√', '(') + ")^(0.5)";
                else koefB = koefB.Replace("√", "*(") + ")^(0.5)";
            }
            if (koefC.Contains("√"))
            {
                if (koefC.IndexOf('√') == 0)
                    koefC = koefC.Replace('√', '(') + ")^(0.5)";
                else koefC = koefC.Replace("√", "*(") + ")^(0.5)";
            }
            //ВЫПОЛНЕНИЕ ПРОВЕРКИ ВХОДНЫХ ДАННЫХ, ВЫВОД ОШИБКИ ПРИ НЕОБХОДИМОСТИ
            if (!calculator.validExpression(koefA) || !calculator.validExpression(koefB) || !calculator.validExpression(koefC))
                MessageBox.Show("Проверьте корректность введенных данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                //возведение в (-1) степень
                if (lOne.Visible) koefA = "(" + calculator.parse(koefA).ToString().Replace(',', '.') + ")^(-1)";
                if (lOne1.Visible) koefB = "(" + calculator.parse(koefB).ToString().Replace(',', '.') + ")^(-1)";
                if (lOne2.Visible) koefC = "(" + calculator.parse(koefC).ToString().Replace(',', '.') + ")^(-1)";
                //ВЫЧИСЛЕНИЕ КОРНЕЙ УРАВНЕНИЯ
                calculator.changeCoefficients(koefA, koefB, koefC);
                string result="";
                string equation = "";
                equation = koefA + "x^2";
                if (calculator.b >= 0)
                    equation += "+" + koefB + "x";
                else equation += koefB + "x";
                if (calculator.c >= 0)
                    equation += "+" + koefC + "=0\n";
                else equation += koefC + "=0\n";
                if (calculator.firstRoot.Contains("no") || calculator.firstRoot == "null") result = equation + "Нет корней\n";
                else
                {
                    if (calculator.firstRoot.Contains("many")) result = equation + "Бескнонечное множество корней\n";
                    else
                    {
                        if (calculator.firstRoot.Contains("не"))
                            MessageBox.Show("Деление на ноль запрещено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else
                        {
                            if (calculator.firstRoot == calculator.secondRoot) result = equation + "X=" + calculator.firstRoot + "\n";
                            else result = equation + "X1=" + calculator.firstRoot + "\n" +
                  "X2=" + calculator.secondRoot + "\n";
                        }
                    }
                }
                //записываем ответ в историю операций
                rHistory.Text = result + rHistory.Text;
                //убираем единицы с дробями сверху
                lOne.Visible = false;
                lOne1.Visible = false;
                lOne2.Visible = false;
                lDivideLine.Visible = false;
                lDivideLine1.Visible = false;
                lDivideLine2.Visible = false;
                //очищаем коэффициэнты
                tKoefA.Text = "";
                tKoefB.Text = "";
                tKoefC.Text = "";
            }
        }
        private void bDelete_Click(object sender, EventArgs e)
        {
            DeleteSymbol();
        }

        private void bSignChange_Click(object sender, EventArgs e)
        {
            ChangeSign();
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //сохраняем историю вычислений в файл
            string path = Directory.GetCurrentDirectory();
            string[] file = rHistory.Text.Split('\n');
            File.WriteAllLines(path + "\\temp.txt", file);
        }

        private void bInverse_Click(object sender, EventArgs e)
        {
            InverseKoef();
        }

        private void bClearHistory_Click(object sender, EventArgs e)
        {
            rHistory.Text = "";
        }

        private void tKoefA_TextChanged(object sender, EventArgs e)
        {
            if (tKoefA.TextLength > tKoefA.MaxLength) tKoefA.Text = tKoefA.Text.Remove(tKoefA.TextLength - 1, 1);
        }

        private void tKoefB_TextChanged(object sender, EventArgs e)
        {
            if (tKoefB.TextLength > tKoefB.MaxLength) tKoefB.Text = tKoefB.Text.Remove(tKoefB.TextLength - 1, 1);
        }

        private void tKoefC_TextChanged(object sender, EventArgs e)
        {
            if (tKoefC.TextLength > tKoefC.MaxLength) tKoefC.Text = tKoefC.Text.Remove(tKoefC.TextLength - 1, 1);
        }
    }
}
