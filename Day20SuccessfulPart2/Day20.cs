using System;

namespace Csharp
{
    public class Solution
    {
        public int[] order;
        public long[] mix(long[] input){
            for(int i = 0; i < order.Length; i++){
                int index = Array.IndexOf(order, i);
                //Every multiple of numNodes-1, returns to same spot; therefore
                long traverse = (input[index] % (input.Length-1));
                int currIndex = index;
                for(int o = 0; o<Math.Abs(traverse); o++){
                    currIndex = currIndex+=(int)(traverse/Math.Abs(traverse));
                    currIndex = (currIndex + input.Length) % input.Length;
                    long storingVal = input[currIndex];
                    int storingIndex = order[currIndex];
                    input[currIndex] = input[index];
                    order[currIndex] = order[index];
                    input[index] = storingVal;
                    order[index] = storingIndex;
                    index = currIndex;
                }
            }
            return input;
        }

        public long partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            //Load in file
            long[] storage = new long[lines.Length];
            for(int i = 0; i < lines.Length; i++){
                storage[i] = (long.Parse(lines[i])*811589153);
            }
            order = (int[]) Enumerable.Range(0, lines.Length).ToArray();
            for(int i = 0; i < 10; i++){
                storage = mix(storage);
            }
            int val = Array.IndexOf(storage, 0);
            Console.WriteLine((val+100) % (lines.Length));
            Console.WriteLine((val+2000) % (lines.Length));
            Console.WriteLine((val+3000) % (lines.Length));
            Console.WriteLine((storage[(val+1000) % lines.Length]));
            Console.WriteLine((storage[(val+2000) % lines.Length]));
            Console.WriteLine((storage[(val+3000) % lines.Length]));

            return storage[(val+1000) % lines.Length]+storage[(val+2000) % lines.Length]+storage[(val+3000) % lines.Length];
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            long answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}
