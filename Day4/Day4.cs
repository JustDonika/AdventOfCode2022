using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            char[] delimiterChars = { ',', '-'};
            for(int i = 0; i < lines.Length; i++){
                string line = lines[i];
                string[] nums = line.Split(delimiterChars);
                if((Int32.Parse(nums[0]) <= Int32.Parse(nums[2]) && Int32.Parse(nums[1]) >= Int32.Parse(nums[3]))
                || (Int32.Parse(nums[0]) >= Int32.Parse(nums[2]) && Int32.Parse(nums[1]) <= Int32.Parse(nums[3]))){
                    Console.WriteLine(nums[0]);
                    Console.WriteLine(nums[1]);
                    Console.WriteLine(nums[2]);
                    Console.WriteLine(nums[3]);

                    sum+=1;
                }
            }
            return sum;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            char[] delimiterChars = { ',', '-'};
            for(int i = 0; i < lines.Length; i++){
                string line = lines[i];
                string[] nums = line.Split(delimiterChars);
                if((Int32.Parse(nums[1]) >= Int32.Parse(nums[2]) && Int32.Parse(nums[1]) <= Int32.Parse(nums[3])) 
                || (Int32.Parse(nums[3]) >= Int32.Parse(nums[0]) && Int32.Parse(nums[3]) <= Int32.Parse(nums[1]))){
                    sum+=1;
                }
            }
            return sum;
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            int answer = s.partOne();
            Console.WriteLine(answer);
            int answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}