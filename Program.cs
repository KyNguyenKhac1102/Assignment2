using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace Assignment2
{
    class Program
    {


        public static bool isPrime(int number)
        {

            if (number == 1 || number == 0)
            {
                return false;
            };
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static async Task<List<int>> GetPrimeNumberAsync(int min, int max, int index)
        {
            List<int> StoreList = new List<int>();

            var list = await Task.Factory.StartNew(() =>
            {
                for (int i = min; i < max; i++)
                {
                    if (isPrime(i))
                    {
                        StoreList.Add(i);
                        Console.WriteLine($"List {index}: prime number: {i}");
                    }
                }
                return StoreList;
            });

            Console.WriteLine($"Total prime: {list.Count} of list {index}-DONE....");
            return list;

        }
        static async Task Main(string[] args)
        {

            int parts = 4;
            // chia làm 4 phần để tìm số nguyên tố từ 0 - 100
            Task<List<int>>[] primes = new Task<List<int>>[parts];

            // khởi tạo ban đầu chạy từ 0-25
            int min = 0;
            int max = 25;

            // lặp qua từng phần
            for (int i = 0; i < parts; i++)
            {
                // tìm số nguyên tố mỗi phần đó
                primes[i] = GetPrimeNumberAsync(min, max, i + 1);
                // mỗi phần gồm 25 phần tử
                min += 25;
                max += 25;
            }
            // chạy bất đồng bộ
            await Task.WhenAll(primes);

        }
    }
}
