using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            //Read in stacks
            Stack<Char>[] stacks = new Stack<Char>[9];
            for(int i = 0; i < 9; i++){
                stacks[i] = new Stack<Char>();
            }
            for(int i = 8; i >= 0; i--){
                for(int o = 0; o < 9; o++){
                    if(lines[i][1+o*4] != ' '){
                        stacks[o].Push(lines[i][1+o*4]);
                    }
                }
            }
            //Process moves
            for(int i = 10; i < lines.Length; i++){
                char[] delimiterChars = {' '};
                string line = lines[i];
                string[] features = line.Split(delimiterChars);
                for(int o = 0; o < Int32.Parse(features[1]); o++){
                    stacks[Int32.Parse(features[5])-1].Push(stacks[Int32.Parse(features[3])-1].Pop());
                }
            }
            //Print output
            for(int i = 0; i < stacks.Length; i++){
                Console.WriteLine(stacks[i].Peek());
            }

            for(int i = 0; i < stacks.Length; i++){
                Console.WriteLine(stacks[i].Count());
            }

            return 0;
        }

        public int partTwo()
        {
string[] lines = System.IO.File.ReadAllLines("input.txt");
            //Read in stacks
            Stack<Char>[] stacks = new Stack<Char>[9];
            for(int i = 0; i < 9; i++){
                stacks[i] = new Stack<Char>();
            }
            for(int i = 8; i >= 0; i--){
                for(int o = 0; o < 9; o++){
                    if(lines[i][1+o*4] != ' '){
                        stacks[o].Push(lines[i][1+o*4]);
                    }
                }
            }
            //Process moves
            for(int i = 10; i < lines.Length; i++){
                char[] delimiterChars = {' '};
                string line = lines[i];
                string[] features = line.Split(delimiterChars);
                //Send to new stack and then unstack
                Stack<Char> storage = new Stack<Char>();
                for(int o = 0; o < Int32.Parse(features[1]); o++){
                    storage.Push(stacks[Int32.Parse(features[3])-1].Pop());
                }
                for(int o = 0; o < Int32.Parse(features[1]); o++){
                    stacks[Int32.Parse(features[5])-1].Push(storage.Pop());
                }
            }
            //Print output
            for(int i = 0; i < stacks.Length; i++){
                Console.WriteLine(stacks[i].Peek());
            }

            for(int i = 0; i < stacks.Length; i++){
                Console.WriteLine(stacks[i].Count());
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