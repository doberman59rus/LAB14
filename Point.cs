using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    //определение двунаправленного списка
    class Point : ICloneable
    {
        public Watch data { get; set; }
        public Point next;//адрес следующего элемента
        public Point pred;//адрес предыдущего элемента
        
        public Point()
        {
            data = new Watch();
            next = null;
            pred = null;
        }
        
        public Point(Watch d)
        {
            data = d;
            next = null;
            pred = null;
        }
        
        public override string ToString()
        {
            return data + " ";
        }

        //ICloneable
        public object Clone()
        {
            //глубокое клонирование с учетом типа объекта
            if (data is SmartWatch smart)
                return new Point(new SmartWatch(smart));
            if (data is ElectronicWatch electronic)
                return new Point(new ElectronicWatch(electronic));
            if (data is AnalogWatch analog)
                return new Point(new AnalogWatch(analog));
            return new Point(new Watch(data));
        }

        //метод для глубокого клонирования всего списка
        public static Point DeepCloneList(Point originalBeg)
        {
            if (originalBeg == null)
            {
                return null;
            }

            //клонируем первый элемент
            Point clonedBeg = (Point)originalBeg.Clone();//копия первого элемента списка
            Point originalCurrent = originalBeg.next;//указатель для прохода по исходному списку (начинаем со второго элемента)
            Point clonedCurrent = clonedBeg;//указатель для построения нового списка (начинаем с первого склонированного элемента)

            //клонируем остальные элементы
            while (originalCurrent != null)
            {
                clonedCurrent.next = (Point)originalCurrent.Clone();
                clonedCurrent.next.pred = clonedCurrent;//установка обратной связи
                clonedCurrent = clonedCurrent.next;
                originalCurrent = originalCurrent.next;
            }

            return clonedBeg;
        }

        //создание элемента списка
        public static Point MakePoint(Watch d)
        {
            Point p = new Point(d);
            return p;
        }

        //формирование двунаправленного списка
        public static Point MakeList(int size) //добавление в начало
        {
            if(size <= 0)
            {
                return null;
            }
            
            Random rnd = new Random();
            Point beg = null;

            for (int i = 0; i < size; i++)
            {
                Watch watch;
                int type = rnd.Next(0, 4); //0-Watch, 1-AnalogWatch, 2-ElectronicWatch, 3-SmartWatch

                switch (type)
                {
                    case 0:
                        watch = new Watch();
                        watch.RandomInit();
                        break;
                    case 1:
                        var analog = new AnalogWatch();
                        analog.RandomInit();
                        watch = analog;
                        break;
                    case 2:
                        var electronic = new ElectronicWatch();
                        electronic.RandomInit();
                        watch = electronic;
                        break;
                    case 3:
                        var smart = new SmartWatch();
                        smart.RandomInit();
                        watch = smart;
                        break;
                    default:
                        watch = new Watch();
                        watch.RandomInit();
                        break;
                }

                Console.WriteLine($"Добавление часов: {watch}");
                Point p = MakePoint(watch);
                p.next = beg;
                if (beg != null)
                {
                    beg.pred = p;
                }
                beg = p;
            }
            return beg;
        }

        //вывод списка на экран
        public static void ShowList(Point beg)
        {
            //проверка наличия элементов в списке
            if (beg == null)
            {
                Console.WriteLine("Список пуст");
                return;
            }
            
            Point p = beg;
            while (p != null)
            {
                Console.WriteLine(p);
                p = p.next;//переход к следующему элементу
            }
            Console.WriteLine();
        }

        //добавление элемента на указанную позицию
        public static Point AddPoint(Point beg, int number, Watch watch)
        {
            Point NewPoint = MakePoint(watch);

            if (beg == null)
            {
                return NewPoint;
            }

            if (number == 1) //добавление в начало
            {
                NewPoint.next = beg;
                beg.pred = NewPoint;
                return NewPoint;
            }

            Point p = beg;
            for (int i = 1; i < number - 1 && p != null; i++)
            {
                p = p.next;
            }

            if (p == null)
            {
                Console.WriteLine("Ошибка! Позиция вышла за границы");
                return beg;
            }

            NewPoint.next = p.next;
            NewPoint.pred = p;
            if (p.next != null)
            {
                p.next.pred = NewPoint;
            }
            p.next = NewPoint;

            return beg;
        }

        //удаление первого элемента с заданным значением
        public static Point RemoveFirstByBrand(Point beg, string brand)
        {
            if (beg == null)
            {
                return null;
            }

            Point current = beg;
            while (current != null)
            {
                if (current.data.Brand == brand)
                {
                    if (current.pred != null)
                    {
                        current.pred.next = current.next;
                    }
                    else
                    {
                        beg = current.next;
                    }

                    if (current.next != null)
                    {
                        current.next.pred = current.pred;
                    }

                    return beg;
                }
                current = current.next;
            }

            Console.WriteLine($"Часы с брендом '{brand}' не найдены");
            return beg;
        }
        //добавление k случайных элементов в начало списка
        public static Point AddElementsToBeginning(Point beg, int k)
        {
            if (k <= 0)
            {
                return beg;
            }
            Random rnd = new Random();
            for (int i = 0; i < k; i++)
            {
                Watch watch;
                int type = rnd.Next(0, 4);
                switch (type)
                {
                    case 0:
                        watch = new Watch();
                        watch.RandomInit();
                        break;
                    case 1:
                        var analog = new AnalogWatch();
                        analog.RandomInit();
                        watch = analog;
                        break;
                    case 2:
                        var electronic = new ElectronicWatch();
                        electronic.RandomInit();
                        watch = electronic;
                        break;
                    case 3:
                        var smart = new SmartWatch();
                        smart.RandomInit();
                        watch = smart;
                        break;
                    default:
                        watch = new Watch();
                        watch.RandomInit();
                        break;
                }

                Point newPoint = MakePoint(watch);
                newPoint.next = beg;
                if (beg != null) beg.pred = newPoint;
                beg = newPoint;
            }
            return beg;
        }
        
        //удаление всего списка из памяти
        public static void ClearList(ref Point beg)
        {
            while (beg != null)
            {
                Point temp = beg;
                beg = beg.next;
                temp.next = null;
                if (beg != null)
                {
                    beg.pred = null;
                }
            }
        }
        public override int GetHashCode()
        {
            return data?.GetHashCode()??0;
        }
    }
}
