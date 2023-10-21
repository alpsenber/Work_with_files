using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HelloApp

{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }

        public
        Person(string firstName, string lastName, string group)
        {
            FirstName = firstName;
            LastName = lastName;
            Group = group;
        }
        Person() 
        {
            FirstName = "";
            LastName = "";
            Group = "";
        }
    }

    public class Hero
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Fraction { get; set; }

        [DataMember]
        public string Health { get; set; }

        public
            Hero(string? name, string? fraction, string? health)
        {
            Name = Fraction = Health = "";
            if (name != null) Name = name;
            if (fraction != null) Fraction = fraction;
            if (health != null) Health = health;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Вывести информацию о логических дисках");
                Console.WriteLine("2. Работа с файлами");
                Console.WriteLine("3. Работа с форматом JSON");
                Console.WriteLine("4. Работа с форматом XML");
                Console.WriteLine("5. Создание zip архива");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        DisplayDriveInformation();
                        break;
                    case 2:
                        FileOperationsMenu();
                        break;
                    case 3:
                        JsonOperationsMenu();
                        break;
                    case 4:
                        XmlOperationsMenu();
                        break;
                    case 5:
                        ZipOperationsMenu();
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("Желаете выполнить еще одно действие? (y/n)");
                string response = Console.ReadLine();
                if (response.ToLower() != "y")
                {
                    exit = true;
                }
            }
        }

        static void DisplayDriveInformation()
        {
            Console.WriteLine("Информация о логических дисках:");
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                Console.WriteLine($"Имя: {drive.Name}");
                Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
                Console.WriteLine($"Размер: {drive.TotalSize}");
                Console.WriteLine($"Тип файловой системы: {drive.DriveFormat}");
                Console.WriteLine();
            }
        }

        static void FileOperationsMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать файл");
            Console.WriteLine("2. Записать в файл");
            Console.WriteLine("3. Прочитать файл");
            Console.WriteLine("4. Удалить файл");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                return;
            }

            switch (choice)
            {
                case 1:
                    CreateFile();
                    break;
                case 2:
                    WriteToFile();
                    break;
                case 3:
                    ReadFromFile();
                    break;
                case 4:
                    DeleteFile();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

        static void CreateFile()
        {
            Console.WriteLine("Введите имя файла:");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем уже существует.");
                return;
            }

            File.Create(fileName).Close();
            Console.WriteLine($"Файл '{fileName}' успешно создан.");
        }

        static void WriteToFile()
        {
            Console.WriteLine("Введите имя файла:");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            Console.WriteLine("Введите строку для записи в файл:");
            string userInput = Console.ReadLine();
            File.WriteAllText(fileName, userInput);
            Console.WriteLine("Строка успешно записана в файл.");
        }

        static void ReadFromFile()
        {
            Console.WriteLine("Введите имя файла для чтения:");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            string fileContent = File.ReadAllText(fileName);
            Console.WriteLine($"Содержимое файла '{fileName}': {fileContent}");
        }

        static void DeleteFile()
        {
            Console.WriteLine("Введите имя файла для удаления:");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            File.Delete(fileName);
            Console.WriteLine($"Файл '{fileName}' успешно удален.");
        }

        static void JsonOperationsMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать файл JSON");
            Console.WriteLine("2. Сериализовать объект и записать в файл JSON");
            Console.WriteLine("3. Прочитать файл JSON");
            Console.WriteLine("4. Удалить файл JSON");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                return;
            }

            switch (choice)
            {
                case 1:
                    CreateJsonFile();
                    break;
                case 2:
                    SerializeToJson();
                    break;
                case 3:
                    ReadJsonFile();
                    break;
                case 4:
                    DeleteJsonFile();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

        static void CreateJsonFile()
        {
            Console.WriteLine("Введите имя файла JSON:");
            string fileName = Console.ReadLine() + ".JSON";
            if (File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем уже существует.");
                return;
            }

            File.Create(fileName).Close();
            Console.WriteLine($"Файл JSON '{fileName}' успешно создан.");
        }

        static void SerializeToJson()
        {
            Console.WriteLine("Введите имя файла JSON:");
            string fileName = Console.ReadLine() + ".JSON";
            string? ans = "+";
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            List<Hero>? heroes = new List<Hero>();
            while (ans == "+")
            {
                // Ввод данных для создания объекта
                Console.WriteLine("Введите имя:");
                string? name = Console.ReadLine();

                Console.WriteLine("Введите фракцию:");
                string? fraction = Console.ReadLine();

                Console.WriteLine("Введите здоровье:");
                string? health = Console.ReadLine();

                // Создание объекта
                Hero hero = new Hero(name, fraction, health);
                heroes.Add(hero);
                Console.WriteLine("Eсли хотите добавить объект, введите '+',");
                Console.Write("другой ввод - запись введенных объектов в файл: > ");
                ans = Console.ReadLine();
            }
            // Сериализация объекта в JSON
            var options = new JsonSerializerOptions { WriteIndented = true };
            var fstream = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
            JsonSerializer.Serialize(fstream, heroes, options);
            //var serializer = new DataContractJsonSerializer(typeof(Hero));
            fstream.Close();
            Console.WriteLine("Данные успешно сериализованы и записаны в файл JSON.");
        }

        static void ReadJsonFile()
        {
            Console.WriteLine("Введите имя файла JSON для чтения:");
            string fileName = Console.ReadLine() + ".JSON";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }
            // Десериализация объекта из JSON
            //var serializer = new DataContractJsonSerializer(typeof(List<Hero>));
            FileStream fstream = new FileStream(fileName, FileMode.Open);
            List<Hero>? heroes = JsonSerializer.Deserialize<List<Hero>>(fstream);
            Console.WriteLine("Данные из файла " + fileName + " :");
            int i = 0;
            foreach (Hero h in heroes)
            {
                //var heroes = (List<Hero>)serializer.ReadObject(stream);
                //? hero = new Hero();
                Console.WriteLine("Герой #" + i.ToString());
                Console.WriteLine($"Имя: {h.Name}");
                Console.WriteLine($"Фракция: {h.Fraction}");
                Console.WriteLine($"Здоровье: {h.Health}");
                Console.WriteLine(); i++;
            }
            Console.WriteLine("Вывод закончен.");
            Console.WriteLine();
        }

        static void DeleteJsonFile()
        {
            Console.WriteLine("Введите имя файла для удаления:");
            string fileName = Console.ReadLine() + ".JSON";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            File.Delete(fileName);
            Console.WriteLine($"Файл '{fileName}' успешно удален.");
        }

        static void XmlOperationsMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать файл XML");
            Console.WriteLine("2. Записать данные в файл XML");
            Console.WriteLine("3. Прочитать файл XML");
            Console.WriteLine("4. Удалить файл XML");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                return;
            }

            switch (choice)
            {
                case 1:
                    CreateXmlFile();
                    break;
                case 2:
                    WriteToXmlFile();
                    break;
                case 3:
                    ReadXmlFile();
                    break;
                case 4:
                    DeleteXmlFile();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

        static void CreateXmlFile()
        {
            Console.WriteLine("Введите имя файла XML:");
            string fileName = Console.ReadLine()+".xml";

            if (File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем уже существует.");
                return;
            }

            File.Create(fileName).Close();
            Console.WriteLine($"Файл XML '{fileName}' успешно создан.");
        }

        static void WriteToXmlFile()
        {
            Console.WriteLine("Введите имя файла XML:");
            string fileName = Console.ReadLine() + ".xml";
            string? ans = "+";
            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }
            List<Person>? persons = new List<Person>();

            while (ans == "+")
            {
                // Ввод данных для создания объекта
                Console.WriteLine("Введите имя:");
                string firstName = Console.ReadLine();

                Console.WriteLine("Введите фамилию:");
                string lastName = Console.ReadLine();

                Console.WriteLine("Введите группу:");
                string group = Console.ReadLine();

                // Создание объекта
                Person person = new Person(firstName, lastName, group);
                persons.Add(person);
                Console.WriteLine("Eсли хотите добавить объект, введите '+',");
                Console.Write("другой ввод - запись введенных объектов в файл: > ");
                ans = Console.ReadLine();
            }
            // Сериализация объекта в XML
            var serializer = new XmlSerializer(typeof(List<Person>));
            FileStream stream = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
            serializer.Serialize(stream, persons);
            Console.WriteLine("Данные успешно записаны в файл XML.");
        }

        static void ReadXmlFile()
        {
            Console.WriteLine("Введите имя файла XML для чтения:");
            string fileName = Console.ReadLine() + ".xml";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            // Десериализация объекта из XML
            XmlSerializer serializer = new XmlSerializer(typeof(List<Person>));
            FileStream fstream = new FileStream(fileName, FileMode.Open);
            List<Person>? persons = (List<Person>)serializer.Deserialize(fstream);
            Console.WriteLine("Данные из файла " + fileName + " :");
            int i = 0;
            foreach (Person p in persons)
            {
                Console.WriteLine("Персона #" + i.ToString());
                Console.WriteLine($"Имя: {p.FirstName}");
                Console.WriteLine($"Фамилия: {p.LastName}");
                Console.WriteLine($"Группа: {p.Group}");
                Console.WriteLine(); i++;
            }
            Console.WriteLine("Вывод закончен.");
            Console.WriteLine();
        }

        static void DeleteXmlFile()
        {
            Console.WriteLine("Введите имя файла для удаления:");
            string fileName = Console.ReadLine() + ".xml";

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            File.Delete(fileName);
            Console.WriteLine($"Файл '{fileName}' успешно удален.");
        }

        static void ZipOperationsMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать zip архив");
            Console.WriteLine("2. Добавить файл в архив");
            Console.WriteLine("3. Разархивировать файл");
            Console.WriteLine("4. Удалить файл и архив");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                return;
            }

            switch (choice)
            {
                case 1:
                    CreateZipArchive();
                    break;
                case 2:
                    AddFileToArchive();
                    break;
                case 3:
                    ExtractFileFromArchive();
                    break;
                case 4:
                    DeleteFileAndArchive();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
            }
        }

        static void CreateZipArchive()
        {
            Console.WriteLine("Введите имя zip архива:");
            string zipFileName = Console.ReadLine();

            if (File.Exists(zipFileName))
            {
                Console.WriteLine("Файл с таким именем уже существует.");
                return;
            }

            using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Create))
            {
                Console.WriteLine($"Архив '{zipFileName}' успешно создан.");
            }
        }

        static void AddFileToArchive()
        {
            Console.WriteLine("Введите имя zip архива:");
            string zipFileName = Console.ReadLine();

            if (!File.Exists(zipFileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            Console.WriteLine("Введите имя файла для добавления в архив:");
            string fileName = Console.ReadLine();

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Update))
            {
                archive.CreateEntryFromFile(fileName, Path.GetFileName(fileName));
                Console.WriteLine("Файл успешно добавлен в архив.");
            }
        }

        static void ExtractFileFromArchive()
        {
            Console.WriteLine("Введите имя zip архива:");
            string zipFileName = Console.ReadLine();

            if (!File.Exists(zipFileName))
            {
                Console.WriteLine("Файл с таким именем не существует.");
                return;
            }

            using (ZipArchive archive = ZipFile.Open(zipFileName, ZipArchiveMode.Read))
            {
                Console.WriteLine("Введите имя файла для разархивирования:");
                string fileName = Console.ReadLine();

                ZipArchiveEntry entry = archive.GetEntry(fileName);
                if (entry == null)
                {
                    Console.WriteLine("Файл с таким именем не найден в архиве.");
                    return;
                }

                string extractedFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                entry.ExtractToFile(extractedFilePath, true);
                Console.WriteLine($"Файл успешно разархивирован: {extractedFilePath}");

                FileInfo fileInfo = new FileInfo(extractedFilePath);
                Console.WriteLine($"Размер файла: {fileInfo.Length} байт");
            }
        }

        static void DeleteFileAndArchive()
        {
            Console.WriteLine("Введите имя файла для удаления:");
            string fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
                Console.WriteLine($"Файл '{fileName}' успешно удален.");
            }

            Console.WriteLine("Введите имя zip архива для удаления:");
            string zipFileName = Console.ReadLine();

            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
                Console.WriteLine($"Архив '{zipFileName}' успешно удален.");
            }
        }
    }
}