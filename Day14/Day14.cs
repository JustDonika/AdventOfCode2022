using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ','};
            List<int[]> rocks = new List<int[]>();
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                for(int o = factors.Length-1; o > 3; o-=3){
                    int rhs1 = Int32.Parse(factors[o-1]);
                    int rhs2 = Int32.Parse(factors[o]);
                    int lhs1 = Int32.Parse(factors[o-4]);
                    int lhs2 = Int32.Parse(factors[o-3]);
                    for(int x = Math.Min(rhs1, lhs1); x <= Math.Max(rhs1, lhs1); x++){
                        for(int y = Math.Min(rhs2, lhs2); y <= Math.Max(rhs2, lhs2); y++){
                            rocks.Add(new int[]{x, y});
                        }
                    }
                }
            }
            int sandcount = 0;
            List<int[]> sand = new List<int[]>();
            int sandx = 500;
            int sandy = 0;
            while(true){
                bool flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx, sandy})));
                if(flag){
                    flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx-1, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx-1, sandy})));
                    if(!flag){
                        sandx--;
                        sandy++;
                    }
                    else{
                        flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx+1, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx+1, sandy})));
                        if(!flag){
                            sandx++;
                            sandy++;
                        }
                        else{
                            if(sandcount % 100 == 0){
                                Console.WriteLine(sandcount);
                            }
                            sand.Add(new int[]{sandx, sandy-1});
                            sandcount++;
                            sandx = 500;
                            sandy = 0;
                        }
                    }
                }
                else{
                    sandy++;
                }
                if(sandy == 1000){
                    for(int i = 0; i < sand.Count; i++){
                        Console.WriteLine(sand.ElementAt(i)[0]);
                        Console.WriteLine(sand.ElementAt(i)[1]);
                    }
                    return sandcount;
                }
            }
        }

        public int partTwo()
        {
            //Big O go brrrrrr
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ','};
            List<int[]> rocks = new List<int[]>();
            int miny = 0;
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                for(int o = factors.Length-1; o > 3; o-=3){
                    int rhs1 = Int32.Parse(factors[o-1]);
                    int rhs2 = Int32.Parse(factors[o]);
                    int lhs1 = Int32.Parse(factors[o-4]);
                    int lhs2 = Int32.Parse(factors[o-3]);
                    if(rhs2>miny){
                        miny = rhs2;
                    }
                    if(lhs2>miny){
                        miny = lhs2;
                    }
                    for(int x = Math.Min(rhs1, lhs1); x <= Math.Max(rhs1, lhs1); x++){
                        for(int y = Math.Min(rhs2, lhs2); y <= Math.Max(rhs2, lhs2); y++){
                            rocks.Add(new int[]{x, y});
                        }
                    }
                }
            }

            //Build floor
            int floor = miny+2;
            for(int i = 300; i < 700; i++){
                rocks.Add(new int[]{i, floor});
            }

            int sandcount = 0;
            List<int[]> sand = new List<int[]>();
            int sandx = 500;
            int sandy = 0;
            while(true){
                bool flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx, sandy})));
                if(flag){
                    flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx-1, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx-1, sandy})));
                    if(!flag){
                        sandx--;
                        sandy++;
                    }
                    else{
                        flag = (rocks.Any(p => p.SequenceEqual(new int[] {sandx+1, sandy})) || sand.Any(p => p.SequenceEqual(new int[] {sandx+1, sandy})));
                        if(!flag){
                            sandx++;
                            sandy++;
                        }
                        else{
                            if(sandy == 1){
                                return sandcount+1;
                            }
                            if(sandcount % 100 == 0){
                                Console.WriteLine(sandcount);
                            }
                            sand.Add(new int[]{sandx, sandy-1});
                            sandcount++;
                            sandx = 500;
                            sandy = 0;
                        }
                    }
                }
                else{
                    sandy++;
                }
                if(sandy == floor + 10){
                    Console.WriteLine("SPILLAGE");
                    return sandcount;
                }
            }
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            //int answer = s.partOne();
            //Console.WriteLine(answer);
            int answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}