namespace ConsolePoly; //Простір імен для групування пов'язаних класів

// Базовий клас People
public class People
{
    //Властивість для збереження імені
    public string Name { get; set; } = "No name";
    //Властивість для збереження віку
    public ushort Age { get; set; }

    //Конструктор за замовчуванням,
    //потрібен для створення "порожнього" об'єкта
    public People() { }

    //Конструктор з параметрами:
    //приймає ім'я та вік і записує їх у властивості через кортеж
    public People(string name, ushort age) => (Name, Age) = (name, age);

    //Віртуальний метод, який дозволяє дочірнім класам перевизначати його поведінку
    public virtual void ViewInfo() => Console.WriteLine($"[Людина] Ім'я: {Name}, Вік: {Age}");

    //Віртуальний метод для збереження базових даних у файл через StreamWriter
    public virtual void Save(StreamWriter w)
    {
        //Записуємо у файл назву типу поточного об'єкта для майбутнього зчитування
        w.WriteLine(GetType().Name);
        //Записуємо ім'я у наступний рядок
        w.WriteLine(Name);
        //Записуємо вік у наступний рядок
        w.WriteLine(Age);
    }

    //Віртуальний метод для зчитування базових даних з файлу через StreamReader
    public virtual void Load(StreamReader r)
    {
        //Читаємо рядок з імені (! каже компілятору, що рядок точно не буде null)
        Name = r.ReadLine()!;
        //Читаємо рядок з віком і перетворюємо його з тексту на число ushort
        Age = ushort.Parse(r.ReadLine()!);
    }
}
