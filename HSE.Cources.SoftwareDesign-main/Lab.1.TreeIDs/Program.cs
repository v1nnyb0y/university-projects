using System;
using System.IO;
using Lab._1.TreeIDs.IDs;
using Lab._1.TreeIDs.TreeIDs;

namespace Lab._1.TreeIDs
{
    internal class Program
    {
        private static void Main() {
            var text = File.ReadAllText("input.txt");
            var tree = new BinaryTree<ID>();
            var parser = new FileParser(text);
            foreach (ID currId in parser) tree.AddElement(currId);
            tree.OutputTree(5);
            Console.ReadKey(true);
        }
    }
}