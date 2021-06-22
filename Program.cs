using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Assignment2
{
    class Program
    {
        public static bool isPrime(int number){

            if(number == 1){
                return false;
            };
            for(int i=2 ; i< number; i++){
                if(number % i == 0){
                    return false;
                }
            }
            return true;
        }
        static void GetPrimeNumber(int min, int max){
            List<int> list = new List<int>();

            for (int i = min; i < max; i++)
            {
                if (isPrime(i))
                {
                    list.Add(i);
                }
            }
            Console.WriteLine($"Total prime number: {list.Count}");

        }
        static async Task<List<int>> GetPrimeNumberAsync(int min, int max, int index){
            List<int> StoreList = new List<int>();



           var list = await Task.Factory.StartNew(() => {
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
            Task<List<int>>[] primes = new Task<List<int>>[parts];
            int min = 0;
            int max = 100;

            for(int i=0; i< parts; i++){
                primes[i] = GetPrimeNumberAsync(min, max, i+1);
                min += 100;
                max += 100;
            }

            await Task.WhenAll(primes);
            
        }
    }
}
