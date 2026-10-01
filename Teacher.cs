using System.Xml.Linq; // Допоміжна бібліотека (не використовується в даному блоці)

namespace ConsolePoly;

//Дочірній клас "Викладач",
//який успадковує класичну людину
public class Teacher : People
{
    //Унікальна властивість викладача — заробітна плата
    public decimal Salary { get; set; }

    //Порожній конструктор
    public Teacher() { }

    //Конструктор: передає name і age батьківському класу People,
    //а salary записує собі
    public Teacher(string name, ushort age, decimal salary) : base(name, age) => Salary = salary;

    //Перевизначення виведення даних:
    //формат :C0 відображає грошове значення як валюту без копійок
    public override void ViewInfo() => Console.WriteLine($"[Викладач] {Name}, {Age} р. | ЗП: {Salary:C0}");

    //Перевизначення збереження у файл
    public override void Save(StreamWriter w)
    {
        //Базове збереження
        base.Save(w);
        //Додатково записуємо зарплату
        w.WriteLine(Salary);
    }

    //Перевизначення зчитування з файлу
    public override void Load(StreamReader r)
    {
        //Базове зчитування
        base.Load(r);
        //Зчитуємо та перетворюємо зарплату у decimal
        Salary = decimal.Parse(r.ReadLine()!);
    }
}
