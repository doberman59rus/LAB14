using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    class MyHashSet
    {
        int defaultLength = 10;
        Point[] set;
        public int Count { get => set.Length; }
        public bool IsReadOnly => false;
        public MyHashSet()
        {
            set = new Point[defaultLength];
        }
        public void Add(Watch item)
        {
            if (item == null) throw new ArgumentNullException("ссылка для добавления пустая");
            
            int index = Math.Abs(item.GetHashCode()%Count);

            if (set[index] == null)
            {
                set[index] = new Point(item);
            }
            else//идем по цепочке
            {
                Point current = set[index];
                while (current.next != null)
                {
                    if(current.data.Equals(item))//есть такой элемент
                    {
                        return;
                    }
                    current = current.next;
                }
                
                if(!(current.data.Equals(item)))
                {
                    current.next = new Point(item);//добавлен в конец
                }
            }
        }
        public void Clear()
        {
            set = new Point[defaultLength];
        }
        //поиск по ключу(названию бренда)
        public bool Contains(string brand)
        {
            for (int i = 0; i < set.Length; i++)
            {
                Point current = set[i];
                while (current != null)
                {
                    if (current.data.Brand.Equals(brand))
                    {
                        return true;
                    }
                    current = current.next;
                }
            }

            return false;//дошли до конца цепочки
        }
        public void CopyTo(Watch[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public bool Remove(string brand)
        {
            for (int i = 0; i < set.Length; i++)
            {
                Point current = set[i];
                Point previous = null;

                while (current != null)//идем по цепочке
                {
                    if (current.data.Brand.Equals(brand))//есть такой элемент
                    {
                        if (previous == null)
                        {
                            //удаление первого элемента в цепочке
                            set[i] = current.next;
                        }
                        else
                        {
                            //удаление из середины или конца цепочки
                            previous.next = current.next;
                        }
                        return true;
                    }

                    previous = current;
                    current = current.next;
                }
            }
            
            return false ;//дошли до конца цепочки
        }
        public void PrintHS()
        {
            if(set == null)
            {
                throw new Exception("Таблица не создана");
            }
            for (int i = 0; i < set.Length; i++)
            {
                Console.Write($"{i} :");
                
                if(set [i] != null)
                {
                    Point current = set [i];
                    
                    while(current!=null)
                    {
                        Console.Write(current.data);
                        current = current.next;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
