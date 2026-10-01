using System.Xml.Linq; // Імпорт простору імен (тут не використовується, але залишено за кодом)

namespace ConsolePoly;

//Дочірній клас "Студент",
//який успадковує всі властивості та методи від People
public class Student : People
{
    //Додаткова властивість, притаманна лише студенту — середній бал
    public double Rating { get; set; }

    //Порожній конструктор
    public Student() { }

    //Конструктор з параметрами:
    //ім'я та вік передає у базовий клас через : base(name, age),
    //а Rating ініціалізує сам
    public Student(string name, ushort age, double rating) : base(name, age) => Rating = rating;
    //Перевизначений метод (override) для виведення детальної інформації про студента
    public override void ViewInfo() => Console.WriteLine($"[Студент] {Name}, {Age} р. | Сер. бал: {Rating:F1}");

    //Перевизначений метод збереження у файл
    public override void Save(StreamWriter w)
    {
        //Спочатку викликаємо метод базового класу, який запише Тип, Ім'я та Вік
        base.Save(w);
        //Потім дописуємо специфічне для студента поле — рейтинг
        w.WriteLine(Rating);
    }

    //Перевизначений метод зчитання з файлу
    public override void Load(StreamReader r)
    {
        //Спочатку зчитуємо базові дані (Ім'я та Вік) через метод базового класу
        base.Load(r);
        //Потім зчитуємо та перетворюємо з тексту рейтинг
        Rating = double.Parse(r.ReadLine()!);
    }
}
