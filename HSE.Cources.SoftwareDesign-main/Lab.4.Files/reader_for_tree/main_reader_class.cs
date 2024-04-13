using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Lab._1.TreeIDs.IDs;
using Lab._1.TreeIDs.IDs.Enum;
using Lab._1.TreeIDs.IDs.Types;
using Lab._1.TreeIDs.TreeIDs;

namespace Lab._4.Files.reader_for_tree
{
    internal class main_reader_class
    {
        private int escape;
        private readonly string path;
        private BinaryTree<ID> tree;
        private readonly bool type;

        public main_reader_class(string path_to_reader_file) {
            path = path_to_reader_file;
            escape = 1;
            type = ".xml" == path_to_reader_file.Substring(path_to_reader_file.Length - 4,4);
        }

        private void de_ser_b_file() {
            escape = 0;
            var bin_formatter = new BinaryFormatter();

            using (Stream f_stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None)) {
                tree = bin_formatter.Deserialize(f_stream) as BinaryTree<ID>;
            }

            tree.OutputTree(5);
            Console.ReadKey(true);
        }

        private void ser_b_file() {
            var bin_formatter = new BinaryFormatter();

            using (Stream f_stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None)) {
                bin_formatter.Serialize(f_stream, tree);
            }

            Console.WriteLine("Файл успешно сохранен под названием: " + path);
            Console.ReadKey(true);
        }

        private void ser_xml_file() {
            XmlSerializer xml_serializer = new XmlSerializer(
                typeof(BinaryTree<ID>),
                new Type[]
                    {
                        typeof(ID),
                        typeof(Class),
                        typeof(Vars),
                        typeof(Method),
                        typeof(Constant)
                    });
            using (Stream f_stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                xml_serializer.Serialize(f_stream, tree);
            }

            Console.WriteLine("Файл успешно сохранен под названием: " + path);
            Console.ReadKey(true);
        }

        private void de_ser_xml_file() {
            escape = 0;
            XmlSerializer xml_serializer = new XmlSerializer(
                typeof(BinaryTree<ID>), 
                new Type[]
                    {
                        typeof(ID),
                        typeof(Class),
                        typeof(Vars),
                        typeof(Method),
                        typeof(Constant)
                    });

            using (Stream f_stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                tree = xml_serializer.Deserialize(f_stream) as BinaryTree<ID>;
            }

            tree.OutputTree(5);
            Console.ReadKey(true);
        }

        public void start() {
            string[] items_of_menu =
                {
                    "1. Десериализация.",
                    "2. Сериализация.",
                    "Назад."
                };

            while (true) {
                var case_number = print.Menu(escape, items_of_menu);
                switch (case_number) {
                    case 1:
                    {
                        if (type)
                            de_ser_xml_file();
                        else
                            de_ser_b_file();
                        break;
                    }
                    case 2: {
                        if (type)
                            ser_xml_file();
                        else
                            ser_b_file();
                        break;
                    }
                    case 3: {
                        return;
                    }
                }
            }
        }
    }
}