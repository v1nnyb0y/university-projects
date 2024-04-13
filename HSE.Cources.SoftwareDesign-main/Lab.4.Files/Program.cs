using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Lab._1.TreeIDs;
using Lab._1.TreeIDs.IDs;
using Lab._1.TreeIDs.IDs.Types;
using Lab._1.TreeIDs.TreeIDs;
using Lab._4.Files.parser_zip;
using Lab._4.Files.reader_for_tree;

namespace Lab._4.Files
{
    internal class Program
    {
        private static void create_xml_ser_file() {
            var text = File.ReadAllText("input.txt");
            var tree = new BinaryTree<ID>();
            var parser = new FileParser(text);
            foreach (ID currId in parser) tree.AddElement(currId);
            tree.OutputTree(5);
            Console.ReadKey(true);

            XmlSerializer xml_serializer = new XmlSerializer(
                typeof(BinaryTree<ID>),
                new Type[]
                    {
                        typeof(ID),
                        typeof(Class),
                        typeof(Vars),
                        typeof(Method),
                        typeof(Constant),
                        typeof(Vars[])
                    });
            using (Stream f_stream = new FileStream("input_xml_ser.xml", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xml_serializer.Serialize(f_stream, tree);
            }
        }

        private static void create_b_ser_file() {
            var text = File.ReadAllText("input.txt");
            var tree = new BinaryTree<ID>();
            var parser = new FileParser(text);
            foreach (ID currId in parser) tree.AddElement(currId);
            tree.OutputTree(5);
            Console.ReadKey(true);

            BinaryFormatter bin_serializer = new BinaryFormatter();
            using (Stream f_stream = new FileStream("input_b_ser.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                bin_serializer.Serialize(f_stream, tree);
            }
        }

        private static void Main() {
            string[] items_of_menu =
                {
                    "Задание 1.1. (Де)Сериализация бинарного дерева (XML).",
                    "Задание 1.2. (Де)Сериализация бинарного дерева (Бинарная).",
                    "Задание 2. Разархивация архива.",
                    "Выход."
                };

            create_b_ser_file();
            create_xml_ser_file();

            while (true) {
                var case_number = print.Menu(0, items_of_menu);
                switch (case_number) {
                    case 1: {
                        main_reader_class reader = new main_reader_class("input_xml_ser.xml");
                        reader.start();
                            break;
                    }
                    case 2: {
                        main_reader_class reader = new main_reader_class("input_b_ser.dat");
                        reader.start();
                            break;
                    }
                    case 3: {
                        parser _parser = new parser("Current.zip");
                        _parser.start();
                        break;
                    }
                    case 4: {
                        return;
                    }
                }
            }
        }
    }
}