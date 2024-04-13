using System;
using System.Xml.Serialization;

namespace Lab._1.TreeIDs.TreeIDs
{
    //Класс для бинарного дерева
    [Serializable]
    public class BinaryTree<T>
    {
        public T Data;

        public BinaryTree<T> Left, //адрес левого поддерева 
            Right; //адрес правого поддерева


        public BinaryTree() {
            Data = default(T);
            Left = null;
            Right = null;
        }

        public BinaryTree(T d) {
            Data = d;
            Left = new BinaryTree<T>();
            Right = new BinaryTree<T>();
        }

        public void AddElement(T digit) {
            if (Data == null) {
                Data = digit;
                Left = new BinaryTree<T>();
                Right = new BinaryTree<T>();
                return;
            }

            if (Data.GetHashCode() < digit.GetHashCode()) {
                if (Left == null)
                    Left = new BinaryTree<T>(digit);
                else
                    Left.AddElement(digit);
            }
            else {
                if (Right == null)
                    Right = new BinaryTree<T>(digit);
                else
                    Right.AddElement(digit);
            }
        }

        public void OutputTree(int l) {
            if (Data != null) {
                Right.OutputTree(l + 3); //переход к левому поддереву
                for (var i = 0; i < l; i++)
                    Console.Write(" ");
                Console.WriteLine(Data.ToString());
                Left.OutputTree(l + 3); //переход к правому поддереву
            }
        }
    }
}