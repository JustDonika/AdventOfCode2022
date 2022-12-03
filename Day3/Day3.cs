using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            for(int i = 0; i < lines.Length; i++){
                string line = lines[i];
                string first = line.Substring(0, (int)(line.Length / 2));
                string last = line.Substring((int)(line.Length / 2), (int)(line.Length / 2));
                for(int o = 0; o < line.Length / 2; o++){
                    if(last.Contains(first[o])){
                        int asNumber = first[o]-'a'+1;
                        if(asNumber<0){
                            sum+=first[o]-'a'+59;
                        }
                        else{
                            sum+=first[o]-'a'+1;
                        }
                        o = 10000;
                    }
                }
            }
            return sum;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            for(int i = 0; i < lines.Length / 3; i++){
                string line1 = lines[i*3];
                string line2 = lines[i*3+1];
                string line3 = lines[i*3+2];
                for(int o = 0; o < line1.Length; o++){
                    if(line2.Contains(line1[o]) && line3.Contains(line1[o])){
                        Console.WriteLine(line1[o]);
                        int asNumber = line1[o]-'a'+1;
                        if(asNumber<0){
                            sum+=line1[o]-'a'+59;
                        }
                        else{
                            sum+=line1[o]-'a'+1;
                        }
                        o=10000;
                    }
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