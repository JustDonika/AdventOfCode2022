using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ','};
            int[,] map = new int[400, 200];
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                for(int o = factors.Length-1; o > 3; o-=3){
                    int rhs1 = Int32.Parse(factors[o-1]);
                    int rhs2 = Int32.Parse(factors[o]);
                    int lhs1 = Int32.Parse(factors[o-4]);
                    int lhs2 = Int32.Parse(factors[o-3]);
                    for(int x = Math.Min(rhs1, lhs1); x <= Math.Max(rhs1, lhs1); x++){
                        for(int y = Math.Min(rhs2, lhs2); y <= Math.Max(rhs2, lhs2); y++){
                            map[x-300, y] = 2;
                        }
                    }
                }
            }

            int sandcount = 0;
            int sandx = 500;
            int sandy = 0;
            while(true){
                if(map[sandx-300, sandy] != 0){
                    if(map[sandx-301, sandy] == 0){
                        sandx--;
                        sandy++;
                    }
                    else if(map[sandx-299, sandy] == 0){
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
                        map[sandx-300, sandy-1] = 1;
                        sandcount++;
                        sandx = 500;
                        sandy = 0;
                    }
                }
                else{
                    sandy++;
                }
                if(sandy == 190){
                    return sandcount;
                }
            }
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ','};
            int[,] map = new int[400, 200];
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
                            map[x-300, y] = 2;
                        }
                    }
                }
            }

            //Build floor
            int floor = miny+2;
            for(int i = 0; i < 400; i++){
                map[i, floor] = 2;
            }

            int sandcount = 0;
            int sandx = 500;
            int sandy = 0;
            while(true){
                if(map[sandx-300, sandy] != 0){
                    if(map[sandx-301, sandy] == 0){
                        sandx--;
                        sandy++;
                    }
                    else if(map[sandx-299, sandy] == 0){
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
                        map[sandx-300, sandy-1] = 1;
                        sandcount++;
                        sandx = 500;
                        sandy = 0;
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
            int answer = s.partOne();
            Console.WriteLine(answer);
            int answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}