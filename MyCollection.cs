using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    public class MyCollection<T> : IList<T> where T : Watch
    {
        public List<T> items = new List<T>();

        //конструкторы
        public MyCollection() { }

        public MyCollection(int length)
        {
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                T watch = (T)GenerateRandomWatch(rnd);
                items.Add(watch);
            }
        }

        public MyCollection(MyCollection<T> c)
        {
            foreach (var item in c)
            {
                items.Add((T)item.Clone());
            }
        }

        public virtual Watch GenerateRandomWatch(Random rnd)
        {
            int type = rnd.Next(0, 4);
            Watch watch;
            switch (type)
            {
                case 0:
                    watch = new Watch();
                    watch.RandomInit();
                    return watch;
                case 1:
                    watch = new AnalogWatch();
                    watch.RandomInit();
                    return watch;
                case 2:
                    watch = new ElectronicWatch();
                    watch.RandomInit();
                    return watch;
                case 3:
                    watch = new SmartWatch();
                    watch.RandomInit();
                    return watch;
                default:
                    return new Watch();
            }
        }

        //реализация IList<T>
        public virtual T this[int index]
        {
            get => items[index];
            set => items[index] = value;
        }

        public int Count => items.Count;
        public bool IsReadOnly => false;

        public virtual void Add(T item) => items.Add(item);
        public virtual void Clear() => items.Clear();
        public bool Contains(T item) => items.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => items.CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();
        public int IndexOf(T item) => items.IndexOf(item);
        public void Insert(int index, T item) => items.Insert(index, item);
        public virtual bool Remove(T item) => items.Remove(item);
        public virtual void RemoveAt(int index) => items.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
