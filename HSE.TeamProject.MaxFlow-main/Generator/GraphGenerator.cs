using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public class GraphGenerator
    {
        /// <summary>
        /// Graph matrix
        /// </summary>
        private byte[,] graph;

        /// <summary>
        /// Capacity of ribs
        /// </summary>
        private int[,] rib_capacity;

        /// <summary>
        /// Size of the graph
        /// </summary>
        private int Size;

        private int Sum;

        /// <summary>
        /// Construct
        /// </summary>
        public GraphGenerator() : base() { }

        /// <summary>
        /// Construct with graph Size
        /// </summary>
        /// <param name="_Size">Graph size</param>
        /// <param name="type">Type of input</param>
        public GraphGenerator(int _Size, int type) : base()
        {
            Size = _Size;
            if (type == 0)
            {
                Generate();
            }
            else
            {
                Input();
            }
        }

        /// <summary>
        /// Return graph 
        /// </summary>
        public byte[,] GetMatrix => graph;

        /// <summary>
        /// Return ribs capacity
        /// </summary>
        public int[,] GetRibsCapacity => rib_capacity;

        public int GetSum => Sum;

        /// <summary>
        /// Generate graph
        /// </summary>
        private void Generate()
        {
                Random rnd = new Random();
                int[,] matrix = new int[Size, Size];
                byte[,] smezh = new byte[Size, Size];
                int sum = 0;
                if (Size == 0) Size = rnd.Next(3, 100);
                //Заполнение A и нулей
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (j <= i)
                        {
                            matrix[i, j] = 0;
                            smezh[i, j] = 0;
                        }
                        else
                        {
                            if (j * 2 > Size - 1 && i * 2 <= Size - 1)
                            {
                                matrix[i, j] = rnd.Next(0, 100);
                                if (matrix[i, j] == 0) smezh[i, j] = 0;
                                else smezh[i, j] = 1;
                                sum += matrix[i, j];
                            }
                        }
                    }
                }

            Sum = sum;

                //Заполнение B и C
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if ((j > i) && ((j * 2 <= Size - 1 && i * 2 <= Size - 1) || (j * 2 > Size - 1 && i * 2 > Size - 1)))
                        {
                            do
                            {
                                matrix[i, j] = rnd.Next(0, 10000);
                            } while (matrix[i, j] < sum);
                            if (matrix[i, j] == 0) smezh[i, j] = 0;
                            else smezh[i, j] = 1;
                        }
                    }
                }
                rib_capacity = matrix;
                graph = smezh;
        }
        /// <summary>
        /// Input graph from the keyboard
        /// </summary>
        private void Input() {
            StreamReader st = new StreamReader("input.txt");
            Size = Int32.Parse(st.ReadLine());
            int[,] matrix = new int[Size, Size];
            byte[,] smezh = new byte[Size, Size];
            for (int i = 0; i < Size; i++) {
                string tmp = st.ReadLine();
                string[] arr = tmp.Split(' ');
                for (int j = 0; j < Size; j++)
                {
                    matrix[i, j] = Int32.Parse(arr[j]);
                    if (matrix[i, j] == 0) smezh[i, j] = 0; else smezh[i, j] = 1;
                }
            }
            rib_capacity = matrix;
            graph = smezh;
        }
    }
}
