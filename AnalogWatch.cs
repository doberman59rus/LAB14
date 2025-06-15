using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLibrary
{
    public class AnalogWatch : Watch, IInit, IComparable, ICloneable
    {
        // Поле
        protected string watchStyle;

        // Свойство с проверкой ограничений
        public string WatchStyle
        {
            get { return watchStyle; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Стиль часов не может быть пустым.");
                watchStyle = value;
            }
        }

        // Конструкторы
        public AnalogWatch() : base()
        {
            WatchStyle = "Классический";
        }

        public AnalogWatch(string brand, int yearOfManufacture, string watchStyle) : base(brand, yearOfManufacture)
        {
            WatchStyle = watchStyle;
        }

        public AnalogWatch(AnalogWatch copiedWatch) : base(copiedWatch)
        {
            WatchStyle = copiedWatch.WatchStyle;
        }

        // Метод Show
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Стиль часов: {WatchStyle}");
        }

        // Метод Init
        public override void Init()
        {
            base.Init();
            Console.Write("Введите стиль часов: ");
            WatchStyle = Console.ReadLine();
        }

        // Метод RandomInit
        public override void RandomInit()
        {
            base.RandomInit();
            string[] styles = { "Классический", "Спортивный", "Ретро", "Модерн", "Авиационный", "Военный", "Дайверский", "Минималистичный", "Скелетон", "Хронографы", "Люкс", "Ар-деко", "Футуристический", "Этнический" };
            Random rnd = new Random();
            WatchStyle = styles[rnd.Next(styles.Length)];
        }

        // Метод Equals
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            AnalogWatch other = (AnalogWatch)obj;
            return WatchStyle == other.WatchStyle;
        }
        
        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Аналоговые часы, Бренд {Brand}, Год выпуска {YearOfManufacture}, Стиль {WatchStyle}";
        }
    }
}
