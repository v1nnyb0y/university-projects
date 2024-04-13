using ReadLib;

namespace Lab._12
{
    internal class MainProgram
    {
        private static void Main() {
            string[] mainMenu =
                {
                    "Задание 1. Использовать коолекцию Stack.",
                    "Задание 2. Работа с обобщенной коллекцией SortedDictionary",
                    "Задание 3. Создание своей коллекции Queue", "Выход."
                };
            while (true) {
                var sw = Print.Menu(0, mainMenu);
                switch (sw) {
                    case 1:
                        var stack = new StackWork();
                        stack.Start();
                        break;
                    case 2:
                        var dictionary = new SortedDictionaryWork();
                        dictionary.Start();
                        break;
                    case 3:
                        var queue = new MyQueueWork();
                        queue.Start();
                        break;
                    case 4:
                        return;
                }
            }
        }
    }
}