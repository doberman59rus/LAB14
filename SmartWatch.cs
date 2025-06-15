using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchLibrary
{
    public class SmartWatch : ElectronicWatch, IInit, IComparable, ICloneable
    {
        // Поля
        protected string operatingSystem;
        protected bool heartRateSensor;

        // Свойства с проверкой ограничений
        public string OperatingSystem
        {
            get { return operatingSystem; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ОС не может быть пустой.");
                operatingSystem = value;
            }
        }

        public bool HeartRateSensor
        {
            get { return heartRateSensor; }
            set { heartRateSensor = value; }
        }

        // Конструкторы
        public SmartWatch() : base()
        {
            OperatingSystem = "Unknown";
            HeartRateSensor = false;
        }

        public SmartWatch(string brand, int yearOfManufacture, string typeOfDisplay, string operatingSystem, bool heartRateSensor) : base(brand, yearOfManufacture, typeOfDisplay)
        {
            OperatingSystem = operatingSystem;
            HeartRateSensor = heartRateSensor;
        }

        public SmartWatch(SmartWatch copiedWatch) : base(copiedWatch)
        {
            OperatingSystem = copiedWatch.OperatingSystem;
            HeartRateSensor = copiedWatch.HeartRateSensor;
        }

        // Метод Show
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Операционная система: {OperatingSystem}, Наличие датчика пульса: {HeartRateSensor}");
        }

        // Метод Init
        public override void Init()
        {
            base.Init();
            Console.Write("Введите ОС: ");
            OperatingSystem = Console.ReadLine();
            Console.Write("Наличие датчика пульса (true/false): ");
            HeartRateSensor = bool.Parse(Console.ReadLine());
        }

        // Метод RandomInit
        public override void RandomInit()
        {
            base.RandomInit();
            string[] osTypes = { "WatchOS", "Wear OS", "Tizen", "Fitbit OS", "Garmin OS", "HarmonyOS", "RTOS", "Proprietary OS", "Linux-based OS", "Other Custom OS" };
            Random rnd = new Random();
            OperatingSystem = osTypes[rnd.Next(osTypes.Length)];
            HeartRateSensor = rnd.Next(2) == 1;
        }

        // Метод Equals
        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            SmartWatch other = (SmartWatch)obj;
            return OperatingSystem == other.OperatingSystem && HeartRateSensor == other.HeartRateSensor;
        }
        
        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Умные часы, Бренд {Brand}, Год выпуска {YearOfManufacture}, Тип дисплея {TypeOfDisplay}, ОС {OperatingSystem}, Датчик пульса = {HeartRateSensor}";
        }

        // Свойство, возвращающее ссылку на объект базового класса
        public Watch BaseWatch
        {
            get 
            { 
                return new Watch(Brand, YearOfManufacture); 
            }
        }
    }
}