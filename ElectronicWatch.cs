using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLibrary
{
    public class ElectronicWatch : Watch, IInit, IComparable, ICloneable
    {
        // Поле
        protected string typeOfDisplay;

        // Свойство с проверкой ограничений
        public string TypeOfDisplay
        {
            get { return typeOfDisplay; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Тип дисплея не может быть пустым.");
                typeOfDisplay = value;
            }
        }

        // Конструкторы
        public ElectronicWatch() : base()
        {
            TypeOfDisplay = "LCD";
        }

        public ElectronicWatch(string brand, int yearOfManufacture, string typeOfDisplay) : base(brand, yearOfManufacture)
        {
            TypeOfDisplay = typeOfDisplay;
        }

        public ElectronicWatch(ElectronicWatch copiedWatch) : base(copiedWatch)
        {
            TypeOfDisplay = copiedWatch.TypeOfDisplay;
        }

        // Метод Show
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Тип дисплея: {TypeOfDisplay}");
        }

        // Метод Init
        public override void Init()
        {
            base.Init();
            Console.Write("Введите тип дисплея: ");
            TypeOfDisplay = Console.ReadLine();
        }

        // Метод RandomInit
        public override void RandomInit()
        {
            base.RandomInit();
            string[] displayTypes = { "LCD", "LED", "OLED", "AMOLED", "E-Ink", "TFT-LCD", "MIP", "Transflective LCD", "Segmented Display", "Hybrid Displays" };
            Random rnd = new Random();
            TypeOfDisplay = displayTypes[rnd.Next(displayTypes.Length)];
        }

        // Метод Equals
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            ElectronicWatch other = (ElectronicWatch)obj;
            return TypeOfDisplay == other.TypeOfDisplay;
        }
        
        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Электронные часы, Бренд {Brand}, Год выпуска {YearOfManufacture}, Тип дисплея {TypeOfDisplay}";
        }
    }
}