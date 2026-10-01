using System.Text; //Для кодування тексту у консолі
using System.IO; //Для роботи з файлами (StreamWriter, StreamReader)
using System.Collections.Generic; //Для роботи зі списками List<T>
using Bogus; //Бібліотека для генерації тестових даних
using ConsolePoly; //Підключаємо наші класи

//Налаштовуємо консоль на роботу з кодуванням UTF-8, щоб коректно відображалися українські літери
Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

string file = "data.txt"; //Назва файлу, який виступає нашою розстроченою базою даних

//Налаштовуємо фабрику-генератор тестових даних для студентів українською мовою
var studentFaker = new Faker<Student>("uk")
    .CustomInstantiator(f => new Student(
        f.Name.FullName(), //Генеруємо випадкове ПІБ
        (ushort)f.Random.Number(17, 25), //Генеруємо випадковий вік від 17 до 25
        Math.Round(f.Random.Double(60, 100), 1) //Випадковий бал від 60 до 100, заокруглений до 1 знака
    ));

//Налаштовуємо фабрику-генератор тестових даних для викладачів
var teacherFaker = new Faker<Teacher>("uk")
    .CustomInstantiator(f => new Teacher(
        f.Name.FullName(), //Генеруємо випадкове ПІБ
        (ushort)f.Random.Number(30, 65), //Випадковий вік від 30 до 65
        f.Random.Decimal(18000, 45000) //Заплата від 18 000 до 45 000 грн
    ));

//Створюємо список,
//де будуть зберігатися об'єкти базового типу People
List<People> original = new();
//Об'єкт для отримання випадкових чисел
Random rnd = new();

//Заповнюємо список 10-ма випадковими об'єктами
for (int i = 0; i < 10; i++)
{
    //Якщо випадкове число 0 — створюємо студента, інакше — викладача
    People p = rnd.Next(2) == 0 ? studentFaker.Generate() : teacherFaker.Generate();
    //Додаємо згенерований об'єкт до списку
    original.Add(p);
}

//Запис даних у файл.
//Блок using гарантує закриття файлу та звільнення ресурсів після завершення
using (StreamWriter w = new(file))
{
    //У перший рядок файлу записуємо загальну кількість об'єктів
    w.WriteLine(original.Count);

    //Завдяки поліморфізму викликаємо p.Save(w)
    //для кожного об'єкта виконується відповідний його типу метод Save
    foreach (var p in original) p.Save(w);
} //Файл автоматично зберігається і закривається тут

//Створюємо новий порожній список для зчитаних об'єктів
List<People> loaded = new();

//Відкриваємо файл для читання
using (StreamReader r = new(file))
{
    //Зчитуємо перший рядок, де вказана кількість записів
    int count = int.Parse(r.ReadLine()!);

    //Проходимо по файлу стільки разів, скільки об'єктів збережено
    for (int i = 0; i < count; i++)
    {
        //Читаємо тип об'єкта
        string type = r.ReadLine()!;

        //Фабричний підхід:
        //якщо тип "Student" — створюємо екземпляр Student, інакше Teacher
        People p = type == nameof(Student) ? new Student() : new Teacher();

        //Заповнюємо створений об'єкт даними з файлу (кожен клас прочитає свої відповідні поля)
        p.Load(r);
        //Додаємо відновлений об'єкт у список
        loaded.Add(p);
    }
}

//Виводимо заголовок у консоль
Console.WriteLine("==== Зчитані дані з файлу ====");
//Проходимо по зчитаному списку та виводимо інформацію про кожен об'єкт
foreach (People p in loaded)
{
    //Поліморфний виклик: якщо p це Student
    //виконається Student.ViewInfo(), якщо Teacher — Teacher.ViewInfo()
    p.ViewInfo();
}
