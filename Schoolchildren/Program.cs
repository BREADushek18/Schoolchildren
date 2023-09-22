using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schoolchildren
{
    // Класс для хранения данных об ученике
    class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int SchoolNumber { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }

        public Student(string lastName, string firstName, int schoolNumber, int grade1, int grade2)
        {
            LastName = lastName;
            FirstName = firstName;
            SchoolNumber = schoolNumber;
            Grade1 = grade1;
            Grade2 = grade2;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем список учеников
            List<Student> students = new List<Student>();

            // Заполняем список данными
            for (int schoolNumber = 1; schoolNumber <= 3; ++schoolNumber)
            {
                for (int index = 1; index <= 3; ++index)
                {
                    Console.WriteLine($"Введите данные для {index}-ого ученика  школы №{schoolNumber}:");
                    Console.Write("Фамилия: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Имя: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Оценка по первому предмету: ");
                    int grade1 = int.Parse(Console.ReadLine());
                    Console.Write("Оценка по второму предмету: ");
                    int grade2 = int.Parse(Console.ReadLine());
                    Console.Write("\n\n");

                    // Создаем новый объект студента и добавляем его в список
                    students.Add(new Student(lastName, firstName, schoolNumber, grade1, grade2));
                }
            }

            // Записываем данные в файл CSV
            using (StreamWriter writer = new StreamWriter("students.csv"))
            {
                writer.WriteLine("Фамилия,Имя,Школа,Оценка1,Оценка2");

                foreach (Student student in students)
                {
                    writer.WriteLine($"{student.LastName},{student.FirstName},{student.SchoolNumber},{student.Grade1},{student.Grade2}");
                }
            }

            // Читаем данные из файла CSV и выводим лучшего ученика для каждой школы
            for (int schoolNumber = 1; schoolNumber <= 3; ++schoolNumber)
            {
                List<Student> bestStudents = new List<Student>();
                int bestSum = 0;

                using (StreamReader reader = new StreamReader("students.csv"))
                {
                    reader.ReadLine(); // пропускаем первую строку (заголовки)

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        string lastName = values[0];
                        string firstName = values[1];
                        int studentSchoolNumber = int.Parse(values[2]);
                        int grade1 = int.Parse(values[3]);
                        int grade2 = int.Parse(values[4]);

                        if (studentSchoolNumber == schoolNumber)
                        {
                            int sum = grade1 + grade2;

                            if (sum > bestSum)
                            {
                                bestSum = sum;
                                bestStudents.Clear(); // очищаем список лучших учеников
                                bestStudents.Add(new Student(lastName, firstName, studentSchoolNumber, grade1, grade2));
                            }
                            else if (sum == bestSum)
                            {
                                bestStudents.Add(new Student(lastName, firstName, studentSchoolNumber, grade1, grade2));
                            }
                        }
                    }
                }

                // Выводим лучших учеников для текущей школы
                Console.Write($"Лучшие ученики школы №{schoolNumber}: ");
                for (int index = 0; index < bestStudents.Count; ++index)
                {
                    Console.Write($"{bestStudents[index].LastName} {bestStudents[index].FirstName}");
                    if (index < bestStudents.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine($", сумма оценок: {bestSum}");
            }
            Console.ReadKey();
        }
    }
}
