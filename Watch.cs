using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLibrary
{
    public class Watch : IInit, IComparable, ICloneable
    {
        // Поля
        protected string brand;
        protected int yearOfManufacture;
        public IdNumber Id 
        { 
            get; set; 
        }

        // Свойства с проверкой ограничений
        public string Brand
        {
            get { return brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Бренд не может быть пустым.");
                brand = value;
            }
        }

        public int YearOfManufacture
        {
            get { return yearOfManufacture; }
            set
            {
                if (value < 1806)
                    throw new ArgumentException("Некорректный год выпуска.");
                yearOfManufacture = value;
            }
        }

        public class IdNumber
        {
            public int number;

            public IdNumber(int number)
            {
                if (number < 0)
                    throw new ArgumentException("number не может быть меньше 0");
                this.number = number;
            }

            public override bool Equals(object obj)
            {
                if (obj is IdNumber other)
                    return this.number == other.number;
                return false;
            }

            public override string ToString()
            {
                return number.ToString();
            }
        }
        // Конструкторы
        public Watch()
        {
            Brand = "Unknown";
            YearOfManufacture = 2025;
            Id = new IdNumber(0);
        }

        public Watch(string brand, int yearOfManufacture)
        {
            Brand = brand;
            YearOfManufacture = yearOfManufacture;
        }

        public Watch(Watch copiedWatch)
        {
            Brand = copiedWatch.Brand;
            YearOfManufacture = copiedWatch.YearOfManufacture;
        }

        // Метод Show
        public virtual void Show()
        {
            Console.WriteLine($"Бренд: {Brand}, Год выпуска: {YearOfManufacture}");
        }

        // Метод Init
        public virtual void Init()
        {
            Console.Write("Введите бренд: ");
            Brand = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            YearOfManufacture = int.Parse(Console.ReadLine());
        }

        // Метод RandomInit
        public virtual void RandomInit()
        {
            Random rnd = new Random();
            string[] brands = { "Rolex", "Casio", "Seiko", "Cartier", "Chopard" };
            Brand = brands[rnd.Next(brands.Length)];
            YearOfManufacture = rnd.Next(1806, 2025);
        }
        // Реализация IComparable
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Watch otherWatch = obj as Watch;
            if (otherWatch != null)
                return this.YearOfManufacture.CompareTo(otherWatch.YearOfManufacture);
            else
                throw new ArgumentException("Объект не является типом Watch");
        }

        // Реализация ICloneable
        public object Clone()
        {
            return new Watch(this.Brand, this.YearOfManufacture);
        }

        // Поверхностное копирование
        public Watch ShallowCopy()
        {
            return (Watch)this.MemberwiseClone();
        }

        // Метод Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Watch other = (Watch)obj;
            return Brand == other.Brand && YearOfManufacture == other.YearOfManufacture;
        }
        
        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Часы, Бренд {Brand}, Год выпуска {YearOfManufacture}";
        }
    }
}