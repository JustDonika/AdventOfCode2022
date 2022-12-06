using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            int index = 0;
            for(int i = 3; i < line.Length; i++){
                index = i+1;
                bool duplicateFound = false;
                for(int o = i-3; o <=i; o++){
                    for(int u = o + 1; u <= i; u++){
                        if(line[o] == line[u]){
                            duplicateFound = true;
                        }
                    }
                }
                if(!duplicateFound){
                    return index;
                }
            }
            return 0;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            int index = 0;
            for(int i = 13; i < line.Length; i++){
                index = i+1;
                bool duplicateFound = false;
                for(int o = i-13; o <=i; o++){
                    for(int u = o + 1; u <= i; u++){
                        if(line[o] == line[u]){
                            duplicateFound = true;
                        }
                    }
                }
                if(!duplicateFound){
                    return index;
                }
            }
            return 0;
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