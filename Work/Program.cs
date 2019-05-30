using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work
{
    public class Program
    {
        static int count;
        static void Main(string[] args)
        {

            const int SIZE = 10;         
            // Последовательность символов длинной 100
            string toEncrypt = "Решётка Кардано-Инструмент Шифрования И Дешифрования,Представляющий Собой Специальную таблицу.      ";
            string toDecrypt = "";
            int ind = 0;
            for (int i = 0; i < toEncrypt.Length; i++)
            {
                if (toEncrypt.Substring(i, 1) != " " || toEncrypt.Substring(i, 1) == " ")
                    ind++;
            }
            if (ind < 100) Console.WriteLine($"В строке {ind} символов,  нужно 100. Добавьте {100 - ind} пробел/ов или букв");      
            else if(ind >100) Console.WriteLine($"В строке {ind} символов,  нужно равно 100. Сотрите {ind-100} пробел/ов или букв");
            else
            {
                Console.WriteLine("Количество символов в строке равно {0}", ind);

                // Таблица - ключ
                // "0" - прорезь по условию задачи. Прорези выбираются на основании того, чтобы после поворота таблицы-ключа вокруг центральной оси символы, записанные в ячейках не перекрывались. 
                // Другими словами, при четырех поворотах таблицы-ключа на 90 градусов ячейки, перекрыв все клетки таблицы, ни разу не должны оказаться в одном и том же месте.
                string[,] gridKey = new string[SIZE, SIZE] {
                {"1", "1", "0", "1", "1", "1", "0", "1", "1", "0"},
                {"0", "1", "1", "1", "0", "1", "1", "1", "1", "1"},
                {"1", "1", "0", "1", "1", "0", "1", "1", "1", "1"},
                {"1", "0", "1", "1", "1", "1", "1", "1", "0", "1"},
                {"0", "1", "1", "0", "1", "1", "0", "1", "1", "0"},
                {"1", "1", "0", "1", "1", "0", "1", "1", "1", "1"},
                {"1", "1", "1", "0", "1", "1", "1", "0", "1", "1"},
                {"1", "0", "1", "1", "1", "1", "0", "1", "0", "1"},
                {"0", "1", "1", "1", "0", "1", "1", "1", "0", "1"},
                {"1", "1", "0", "1", "1", "1", "0", "1", "1", "1"}
            };
                // Таблица, в которую будем записывать символы из последовательности на основании таблицы-ключа
                string[,] gridForEncrypted = new string[SIZE, SIZE] {
                {"1", "1", "0", "1", "1", "1", "0", "1", "1", "0"},
                {"0", "1", "1", "1", "0", "1", "1", "1", "1", "1"},
                {"1", "1", "0", "1", "1", "0", "1", "1", "1", "1"},
                {"1", "0", "1", "1", "1", "1", "1", "1", "0", "1"},
                {"0", "1", "1", "0", "1", "1", "0", "1", "1", "0"},
                {"1", "1", "0", "1", "1", "0", "1", "1", "1", "1"},
                {"1", "1", "1", "0", "1", "1", "1", "0", "1", "1"},
                {"1", "0", "1", "1", "1", "1", "0", "1", "0", "1"},
                {"0", "1", "1", "1", "0", "1", "1", "1", "0", "1"},
                {"1", "1", "0", "1", "1", "1", "0", "1", "1", "1"}
            };

                Console.WriteLine("Таблица-ключ:");
                Print(gridKey);

                // прямой обход таблицы
                PutValue(gridKey, gridForEncrypted, toEncrypt);

                // поворот таблицы на 90 градусов по часовой стрелке и заполнение
                var grid90 = Rotate(gridKey);
                PutValue(grid90, gridForEncrypted,toEncrypt);

                // поворот таблицы на 180 градусов по часовой стрелке и заполнение
                var grid180 = Rotate(grid90);
                PutValue(grid180, gridForEncrypted, toEncrypt);

                // поворот таблицы на 270 градусов по часовой стрелке и заполнение
                var grid270 = Rotate(grid180);
                PutValue(grid270, gridForEncrypted,toEncrypt);

                Console.WriteLine("Зашифрованная последовательность:");
                Print(gridForEncrypted);

                // Дешифровка
                Console.WriteLine("Дешифровка:");
                GetValue(gridKey, gridForEncrypted,ref toDecrypt);
                GetValue(grid90, gridForEncrypted, ref toDecrypt);
                GetValue(grid180, gridForEncrypted, ref toDecrypt);
                GetValue(grid270, gridForEncrypted, ref toDecrypt);

                Console.WriteLine(toDecrypt);
                Console.Read();
            }
            Console.Read();
        }

        // Функция поворота таблицы на 90 градусов
        public static string[,] Rotate(string[,] m)
        {
            var result = new string[m.GetLength(1), m.GetLength(0)];

            for (int i = 0; i < m.GetLength(1); i++)
                for (int j = 0; j < m.GetLength(0); j++)
                    result[i, j] = m[m.GetLength(0) - j - 1, i];

            return result;
        }
        //функция вывода матриц
        public static void Print(string[,] mas)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    Console.Write(mas[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        //функция для шифрования (заносит значение в матрицу значений) - зашифровка
        public static void PutValue(string[,] gridKey, string[,] gridForEncrypted,string toEncrypt )
        {
            for (int i = 0; i < gridKey.GetLength(0); i++)
                for (int j = 0; j < gridKey.GetLength(1); j++)
                    if (gridKey[i, j] == "0")
                    {
                        gridForEncrypted[i, j] = toEncrypt[count].ToString();
                        count++;
                    }
        }
        //функция получает значения из матрицы значений (дешифровка)
        public static void GetValue(string[,] grid, string[,] gridEncrypted,ref string Decrypt)
        {
            for (int i = 0; i < gridEncrypted.GetLength(0); i++)
                for (int j = 0; j < gridEncrypted.GetLength(1); j++)
                    if (grid[i, j] == "0")
                    {
                        Decrypt += gridEncrypted[i, j];
                    }
        }
    }

}
    
