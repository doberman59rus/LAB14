using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchLibrary;

namespace LAB12
{
    public class BalancedBinaryTree
    {
        public class TreeNode
        {
            public Watch Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }

            public TreeNode(Watch data)
            {
                Data = data;
                Left = null;
                Right = null;
            }
        }

        public TreeNode root;

        public void CreateBalancedTree(List<Watch> watches)
        {
            if (watches == null || watches.Count == 0)
                throw new ArgumentException("Список часов не может быть пустым");

            watches.Sort((x, y) => x.YearOfManufacture.CompareTo(y.YearOfManufacture));
            root = BuildBalancedTree(watches, 0, watches.Count - 1);
        }

        public TreeNode BuildBalancedTree(List<Watch> watches, int start, int end)
        {
            if (start > end)
                return null;

            int mid = (start + end) / 2;
            TreeNode node = new TreeNode(watches[mid]);

            node.Left = BuildBalancedTree(watches, start, mid - 1);
            node.Right = BuildBalancedTree(watches, mid + 1, end);

            return node;
        }

        public void PrintTreeByLevels()
        {
            if (root == null)
            {
                Console.WriteLine("Дерево пустое");
                return;
            }

            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);

            int level = 0;
            while (queue.Count > 0)
            {
                Console.Write($"Уровень {level}: ");
                int levelSize = queue.Count;

                for (int i = 0; i < levelSize; i++)
                {
                    TreeNode current = queue.Dequeue();
                    Console.Write($"{current.Data.YearOfManufacture} ({current.Data.Brand}) ");

                    if (current.Left != null)
                        queue.Enqueue(current.Left);
                    if (current.Right != null)
                        queue.Enqueue(current.Right);
                }

                Console.WriteLine();
                level++;
            }
        }
        public Watch FindMinYear()
        {
            if (root == null)
                throw new InvalidOperationException("Дерево пустое");

            TreeNode current = root;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Data;
        }
        //преобразование в дерево поиска
        public BalancedBinaryTree ConvertToSearchTree()
        {
            //собирание всех элементов в список
            List<Watch> elements = new List<Watch>();
            TreeTraversal(root, elements);

            //сортировка по году выпуска (для дерева поиска)
            elements.Sort((x, y) => x.YearOfManufacture.CompareTo(y.YearOfManufacture));

            //создание нового дерева (с новой памятью)
            BalancedBinaryTree searchTree = new BalancedBinaryTree();
            searchTree.root = BuildSearchTree(elements, 0, elements.Count - 1);
            return searchTree;
        }

        public void TreeTraversal(TreeNode node, List<Watch> elements)
        {
            if (node == null) return;

            TreeTraversal(node.Left, elements);
            elements.Add(node.Data);
            TreeTraversal(node.Right, elements);
        }

        public TreeNode BuildSearchTree(List<Watch> elements, int start, int end)
        {
            if (start > end) return null;

            //создание нового узла (новая память)
            int mid = (start + end) / 2;
            TreeNode newNode = new TreeNode(new Watch(elements[mid].Brand, elements[mid].YearOfManufacture));

            //рекурсивное построение поддеревьев
            newNode.Left = BuildSearchTree(elements, start, mid - 1);
            newNode.Right = BuildSearchTree(elements, mid + 1, end);

            return newNode;
        }
        //удаление узла по году выпуска
        public void DeleteNode(int year)
        {
            root = DeleteNode(root, year);
        }

        public TreeNode DeleteNode(TreeNode node, int year)
        {
            if (node == null) return null;

            //поиск узла для удаления
            if (year < node.Data.YearOfManufacture)
            {
                node.Left = DeleteNode(node.Left, year);
            }
            else if (year > node.Data.YearOfManufacture)
            {
                node.Right = DeleteNode(node.Right, year);
            }
            else //найден узел для удаления
            {
                //узел с одним подузлом или без подузлов
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }

                //узел с двумя подузлами: находим минимальный в правом поддереве
                node.Data = FindMin(node.Right).Data;

                // Удаляем минимальный узел из правого поддерева
                node.Right = DeleteNode(node.Right, node.Data.YearOfManufacture);
            }

            return node;
        }

        public TreeNode FindMin(TreeNode node)
        {
            TreeNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }
        //удаление дерева из памяти
        public void ClearTree()
        {
            root = DeleteTree(root);
        }

        public TreeNode DeleteTree(TreeNode node)
        {
            if (node == null) return null;

            //рекурсивно удаляем поддеревья
            node.Left = DeleteTree(node.Left);
            node.Right = DeleteTree(node.Right);

            //обнуляем ссылки
            node.Left = null;
            node.Right = null;

            return null;
        }
    }
}
