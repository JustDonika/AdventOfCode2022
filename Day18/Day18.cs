using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            int[,,] allDrops = new int[25, 25, 25];
            char[] delimiterChars = {','};
            int tally = 0;
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                allDrops[Int32.Parse(factors[0]), Int32.Parse(factors[1]), Int32.Parse(factors[2])] = 1;
            }
            for(int x = 0; x < 25; x++){
                for(int y = 0; y < 25; y++){
                    for(int z = 0; z < 25; z++){
                        if(allDrops[x, y, z] == 1){
                            tally+=6;
                            if(x-1 >= 0 && allDrops[x-1, y, z] == 1){
                                tally--;
                            }
                            if(x+1 < 25 && allDrops[x+1, y, z] == 1){
                                tally--;
                            }
                            if(y-1 >= 0 && allDrops[x, y-1, z] == 1){
                                tally--;
                            }
                            if(y+1 < 25 && allDrops[x, y+1, z] == 1){
                                tally--;
                            }
                            if(z-1 >= 0 && allDrops[x, y, z-1] == 1){
                                tally--;
                            }
                            if(z+1 < 25 && allDrops[x, y, z+1] == 1){
                                tally--;
                            }
                        }
                    }
                } 
            }
            return tally;
        }




        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            int max = 24;
            int[,,] allDrops = new int[max, max, max];
            int[,,] exposed = new int[max, max, max];
            char[] delimiterChars = {','};
            int tally = 0;
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                allDrops[Int32.Parse(factors[0])+1, Int32.Parse(factors[1])+1, Int32.Parse(factors[2])+1] = 1;
            }
            //Find all exposed squares; mark checked squares -1, keep exploring
            List<int[]> exploring = new List<int[]>();
            int initX = 23;
            int initY = 23;
            int initZ = 23;
            exploring.Add(new int[]{initX, initY, initZ});
            while(exploring.Count>0){
                int[] curr = exploring.ElementAt(0);
                exploring.RemoveAt(0);
                for(int i = 0; i < 6; i++){
                    int baseX = curr[0];
                    int baseY = curr[1];
                    int baseZ = curr[2];
                    switch(i){
                        case 0:
                            baseX++;
                            break;
                        case 1:
                            baseX--;
                            break;
                        case 2:
                            baseY++;
                            break;
                        case 3:
                            baseY--;
                            break;
                        case 4:
                            baseZ++;
                            break;
                        case 5:
                            baseZ--;
                            break;
                        default:
                            Console.WriteLine("Error");
                            return(0);
                    }
                    if(baseX >= 0 && baseX < max && baseY >= 0 && baseY < max && baseZ >= 0 && baseZ < max && allDrops[baseX, baseY, baseZ] == 0){
                        if(exposed[baseX, baseY, baseZ] != 1){
                            exposed[baseX, baseY, baseZ] = 1;
                            exploring.Add(new int[]{baseX, baseY, baseZ});
                        }
                    }
                }
            }

            for(int x = 0; x < max; x++){
                for(int y = 0; y < max; y++){
                    for(int z = 0; z < max; z++){
                        if(allDrops[x, y, z] == 1){
                            tally+=6;
                            if(x-1 >= 0 && (allDrops[x-1, y, z] == 1  || exposed[x-1, y, z] == 0)){
                                tally--;
                            }
                            if(x+1 < max && (allDrops[x+1, y, z] == 1  || exposed[x+1, y, z] == 0)){
                                tally--;
                            }
                            if(y-1 >= 0 && (allDrops[x, y-1, z] == 1  || exposed[x, y-1, z] == 0)){
                                tally--;
                            }
                            if(y+1 < max && (allDrops[x, y+1, z] == 1  || exposed[x, y+1, z] == 0)){
                                tally--;
                            }
                            if(z-1 >= 0 && (allDrops[x, y, z-1] == 1  || exposed[x, y, z-1] == 0)){
                                tally--;
                            }
                            if(z+1 < max && (allDrops[x, y, z+1] == 1  || exposed[x, y, z+1] == 0)){
                                tally--;
                            }
                        }
                    }
                } 
            }
            return tally;
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