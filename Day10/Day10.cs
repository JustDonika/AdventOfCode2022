using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int register = 1;
            int cycle = 0;
            char[] delimiterChars = {' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                if(factors[0] == "addx"){
                    cycle+=1;
                    if((cycle-20) % 40 == 0){
                        Console.WriteLine(cycle);
                        Console.WriteLine(register*cycle);
                    }
                    cycle+=1;
                    if((cycle-20) % 40 == 0){
                        Console.WriteLine(cycle);
                        Console.WriteLine(register*cycle);
                    }
                    register+=Int32.Parse(factors[1]);
                }
                else{
                    cycle+=1;
                    if((cycle-20) % 40 == 0){
                        Console.WriteLine(cycle);
                        Console.WriteLine(register*cycle);
                    }
                }
            }
            return 0;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int register = 1;
            int cycle = 0;
            string line = "";
            char[] delimiterChars = {' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                if(factors[0] == "addx"){
                    cycle+=1;
                    if(cycle % 40 >= register && cycle % 40 < register + 3){
                        line += '#';
                    }
                    else{
                        line+='.';
                    }
                    if(cycle % 40 == 0){
                        Console.WriteLine(line);
                        line = "";
                    }

                    cycle+=1;
                    if(cycle % 40 >= register && cycle % 40 < register + 3){
                        line += '#';
                    }
                    else{
                        line+='.';
                    }
                    if(cycle % 40 == 0){
                        Console.WriteLine(line);
                        line = "";
                    }
                    register+=Int32.Parse(factors[1]);
                }
                else{
                    cycle+=1;
                    if(cycle % 40 >= register && cycle % 40 < register + 3){
                        line += '#';
                    }
                    else{
                        line+='.';
                    }
                    if(cycle % 40 == 0){
                        Console.WriteLine(line);
                        line = "";
                    }
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