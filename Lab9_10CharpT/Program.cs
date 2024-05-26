using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Оберіть завдання:");
            Console.WriteLine("1. Обчислення виразу в префіксній формі");
            Console.WriteLine("2. Обробка файлу");
            Console.WriteLine("3. Клас ArrayList");
            Console.WriteLine("4. Музикальний диск та пісня");
            Console.WriteLine("0. Вихід");

            int choice = int.Parse(Console.ReadLine() ?? "5");

            switch (choice)
            {
                case 1:
                    Task1();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case 2:
                    Task2();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case 3:
                    Task3();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case 4:
                    Task4();
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                case 0:
                    return;
                default:
                    Console.WriteLine("Введено некоректний варіант. Будь ласка, виберіть знову.");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
            }
        }
    }

    static void Task1()
    {
        Console.WriteLine("Введіть вираз у префіксній формі:");
        string? prefixExpression = Console.ReadLine();

        int result = EvaluatePrefixExpression(prefixExpression ?? "+ 3 4");

        Console.WriteLine($"Результат обчислення: {result}");
    }

    static int EvaluatePrefixExpression(string prefixExpression)
    {
        Stack<int> stack = new();

        for (int i = prefixExpression.Length - 1; i >= 0; i--)
        {
            char currentChar = prefixExpression[i];

            if (char.IsDigit(currentChar))
            {
                stack.Push(currentChar - '0');
            }
            else if (IsOperator(currentChar))
            {
                int operand1 = stack.Pop();
                int operand2 = stack.Pop();
                int partialResult = PerformOperation(operand1, operand2, currentChar);
                stack.Push(partialResult);
            }
        }

        return stack.Pop();
    }

    static bool IsOperator(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/';
    }

    static int PerformOperation(int operand1, int operand2, char op)
    {
        switch (op)
        {
            case '+':
                return operand1 + operand2;
            case '-':
                return operand1 - operand2;
            case '*':
                return operand1 * operand2;
            case '/':
                if (operand2 == 0)
                    throw new DivideByZeroException("Ділення на нуль неможливе");
                return operand1 / operand2;
            default:
                throw new ArgumentException("Невідомий оператор");
        }
    }


    static void Task2()
    {
        string filePath = "D:\\c#\\csharplab9-YuriiLenchuk\\Lab9_10CharpT\\input.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл з даними не знайдено.");
            return;
        }


        // Створення черги для слів, які починаються з великої букви
        Queue<string> uppercaseWords = new Queue<string>();

        // Створення черги для слів, які починаються з малої букви
        Queue<string> lowercaseWords = new Queue<string>();

        // Зчитування файлу і розділення його на слова
        string[] words = File.ReadAllText(filePath).Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        // Розділення слів на дві черги відповідно до першої літери
        foreach (string word in words)
        {
            if (char.IsUpper(word[0]))
                uppercaseWords.Enqueue(word);
            else
                lowercaseWords.Enqueue(word);
        }

        // Виведення слів у відповідному порядку
        Console.WriteLine("Слова, що починаються з великої букви:");
        while (uppercaseWords.Count > 0)
        {
            Console.WriteLine(uppercaseWords.Dequeue());
        }
        

        Console.WriteLine("\nСлова, що починаються з малої букви:");
        while (lowercaseWords.Count > 0)
        {
            Console.WriteLine(lowercaseWords.Dequeue());
        }
    }

    static void Task3()
    {
        Console.WriteLine("Введіть вираз у префіксній формі:");
        string? prefixExpression = Console.ReadLine();

        int result = EvaluatePrefixExpressionArrayList(prefixExpression ?? "+ 3 4");

        Console.WriteLine($"Результат обчислення: {result}\n");

        string filePath = "D:\\c#\\csharplab9-YuriiLenchuk\\Lab9_10CharpT\\input.txt";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл з даними не знайдено.");
            return;
        }

        CustomArrayList uppercaseWords = new();
        CustomArrayList lowercaseWords = new();

        string[] words = System.IO.File.ReadAllText(filePath).Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (string word in words)
        {
            if (char.IsUpper(word[0]))
                uppercaseWords.Add(word);
            else
                lowercaseWords.Add(word);
        }

        Console.WriteLine("Слова, що починаються з великої букви:");
        PrintArrayList(uppercaseWords);

        Console.WriteLine("\nСлова, що починаються з малої букви:");
        PrintArrayList(lowercaseWords);
    }

    static int EvaluatePrefixExpressionArrayList(string prefixExpression)
    {
        CustomArrayList customArrayList = new();

        for (int i = prefixExpression.Length - 1; i >= 0; i--)
        {
            char currentChar = prefixExpression[i];

            if (char.IsDigit(currentChar))
            {
                customArrayList.Add(currentChar - '0');
            }
            else if (IsOperator(currentChar))
            {
                int operand1 = (int) customArrayList.Pop();
                int operand2 = (int)customArrayList.Pop();
                int partialResult = PerformOperation(operand1, operand2, currentChar);
                customArrayList.Add(partialResult);
            }
        }

        return (int) customArrayList.Pop();
    }

    static void PrintArrayList(CustomArrayList list)
    {
        foreach (var item in list)
        {
            Console.WriteLine(item);
        }
    }


    class CustomArrayList : ArrayList
    {
        // Додамо інтерфейси, хоча вони не будуть використовуватися в цьому прикладі
        public object Pop()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("ArrayList is empty.");
            }

            object lastElement = this[Count - 1];
            this.RemoveAt(this.Count - 1);
            return lastElement;
        }

        // Реалізація інтерфейсу IEnumerable
        public new IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }

        // Реалізація інтерфейсу IComparer
        public new int Compare(object x, object y)
        {
            return Comparer.Default.Compare(x, y);
        }

        // Реалізація інтерфейсу ICloneable
        public new object Clone()
        {
            return base.Clone();
        }
    }


    static void Task4()
    {
        MusicCatalog catalog = new();

        catalog.AddDisc("Best of 90s");
        catalog.AddSong("Best of 90s", "Madonna", "Vogue");
        catalog.AddSong("Best of 90s", "Madonna", "Frozen");
        catalog.AddSong("Best of 90s", "Nirvana", "Smells Like Teen Spirit");

        Console.WriteLine("\n");

        catalog.AddDisc("Greatest Hits");
        catalog.AddSong("Greatest Hits", "Queen", "Bohemian Rhapsody");
        catalog.AddSong("Greatest Hits", "Queen", "We Will Rock You");


        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.RemoveSong("Best of 90s", "Madonna", "Vogue");

        Console.WriteLine("\n");

        catalog.DisplayCatalog();

        Console.WriteLine("\n");

        catalog.SearchByArtist("Queen");

        Console.ReadKey();
    }

    class MusicCatalog
    {
        private Hashtable discs = new();

        public void AddDisc(string discTitle)
        {
            if (!discs.ContainsKey(discTitle))
            {
                discs.Add(discTitle, new Hashtable());
                Console.WriteLine($"Диск {discTitle} додано.");
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} вже існує.");
            }
        }

        public void RemoveDisc(string discTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                discs.Remove(discTitle);
                Console.WriteLine($"Диск {discTitle} видалено.");
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }


        public void AddSong(string discTitle, string artist, string songTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                Hashtable disc = (Hashtable) discs[discTitle];
                if (!disc.ContainsKey(artist))
                {
                    disc.Add(artist, new ArrayList());
                }
                ArrayList songs = (ArrayList) disc[artist];
                if (!songs.Contains(songTitle))
                {
                    songs.Add(songTitle);
                    Console.WriteLine($"Пісню {songTitle} виконавця {artist} додано до диску {discTitle}.");
                }
                else
                {
                    Console.WriteLine($"Пісня {songTitle} виконавця {artist} вже існує на диску {discTitle}.");
                }
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }

        public void RemoveSong(string discTitle, string artist, string songTitle)
        {
            if (discs.ContainsKey(discTitle))
            {
                Hashtable disc = (Hashtable) discs[discTitle];
                if (disc.ContainsKey(artist))
                {
                    ArrayList songs = (ArrayList) disc[artist];
                    if (songs.Contains(songTitle))
                    {
                        songs.Remove(songTitle);
                        Console.WriteLine($"Пісню {songTitle} виконавця {artist} видалено з диску {discTitle}.");
                    }
                    else
                    {
                        Console.WriteLine($"Пісні {songTitle} виконавця {artist} не існує на диску {discTitle}.");
                    }
                }
                else
                {
                    Console.WriteLine($"Виконавець {artist} не існує на диску {discTitle}.");
                }
            }
            else
            {
                Console.WriteLine($"Диск {discTitle} не існує.");
            }
        }

        public void DisplayCatalog()
        {
            Console.WriteLine("Каталог музичних дисків:");
            foreach (DictionaryEntry discEntry in discs)
            {
                Console.WriteLine($"Диск: {discEntry.Key}");
                Hashtable disc = (Hashtable) discEntry.Value;
                foreach (DictionaryEntry artistEntry in disc)
                {
                    Console.WriteLine($"  Виконавець: {artistEntry.Key}");
                    ArrayList songs = (ArrayList) artistEntry.Value;
                    foreach (string song in songs)
                    {
                        Console.WriteLine($"    Пісня: {song}");
                    }
                }
            }
        }

        public void SearchByArtist(string artist)
        {
            Console.WriteLine($"Пошук пісень виконавця {artist}:");
            bool found = false;
            foreach (DictionaryEntry discEntry in discs)
            {
                Hashtable disc = (Hashtable) discEntry.Value;
                if (disc.ContainsKey(artist))
                {
                    ArrayList songs = (ArrayList) disc[artist];
                    foreach (string song in songs)
                    {
                        Console.WriteLine($"  Диск: {discEntry.Key}, Пісня: {song}");
                        found = true;
                    }
                }
            }
            if (!found)
            {
                Console.WriteLine($"Пісень виконавця {artist} не знайдено.");
            }
        }
    }
}