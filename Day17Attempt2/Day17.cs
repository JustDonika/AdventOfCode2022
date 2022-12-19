using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            int numRock = 2022;
            int numPlaced = 0;
            int[,] cave = new int[4*numRock,7];
            int rindex = 0;
            List<int[,]> rocks = new List<int[,]>();
            //Add rocks
            int[,] rock1 = new int[,]{{1,1,1,1}}; rocks.Add(rock1);
            int[,] rock2 = new int[,]{{0,1,0},{1,1,1},{0,1,0}}; rocks.Add(rock2);
            int[,] rock3 = new int[,]{{1,1,1},{0,0,1},{0,0,1}}; rocks.Add(rock3);
            int[,] rock4 = new int[,]{{1},{1},{1},{1}}; rocks.Add(rock4);
            int[,] rock5 = new int[,]{{1,1},{1,1}}; rocks.Add(rock5);
            //Define rock relative to floor
            int floor = -1;
            int[,] currRock = rocks.ElementAt(rindex);
            int xCurrRock = 2;
            int yCurrRock = floor+4;
            int rockLength = currRock.GetLength(1);
            int rockHeight = currRock.GetLength(0);

            while(numPlaced < numRock){
                for(int i = 0; i < line.Length; i++){
                    int offset = 0;
                    if(line[i] == '<'){
                        offset = -1;
                    }
                    else{
                        offset = 1;
                    }
                    //Check that new position is inbounds
                    if(xCurrRock+offset >= 0 && xCurrRock + rockLength + offset - 1 <= 6){
                        bool obstruction = false;
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                if(currRock[y, x] == 1 && cave[y+yCurrRock, x+xCurrRock+offset] == 1){
                                    obstruction = true;
                                    x = rockLength;
                                    y = rockHeight;
                                }
                            }
                        }
                        if(!obstruction){
                            xCurrRock = xCurrRock+offset;
                        }
                    }
                    //Having moved, try going down one. Can't be on the floor if lowest point above floor
                    if(yCurrRock == 0){
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
                            }
                        }
                        floor = Math.Max(floor, yCurrRock+rockHeight);
                        //Set new rock
                        if(rindex<4){
                            rindex++;
                        }
                        else{
                            rindex = 0;
                        }
                        currRock = rocks[rindex];
                        xCurrRock = 2;
                        yCurrRock = floor + 3;
                        rockLength = currRock.GetLength(1);
                        rockHeight = currRock.GetLength(0);
                        numPlaced++;
                        if(numPlaced == numRock){
                            i = 100000;
                        }
                    }
                    else if(yCurrRock <= floor + 1){
                        bool onFloor = false;
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                if(currRock[y, x] == 1 && cave[y+yCurrRock-1, x+xCurrRock] == 1){
                                    onFloor = true;
                                    x = rockLength;
                                    y = rockHeight;
                                }
                            }
                        }
                        if(onFloor){
                            for(int x = 0; x < rockLength; x++){
                                for(int y = 0; y < rockHeight; y++){
                                    if(currRock[y, x] == 1){
                                        cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
                                    }   
                                }
                            }
                            floor = Math.Max(floor, yCurrRock+rockHeight);
                            //Set new rock
                            if(rindex<4){
                                rindex++;
                            }
                            else{
                                rindex = 0;
                            }
                            currRock = rocks[rindex];
                            xCurrRock = 2;
                            yCurrRock = floor + 3;
                            rockLength = currRock.GetLength(1);
                            rockHeight = currRock.GetLength(0);
                            numPlaced++;
                            if(numPlaced == numRock){
                                i = 100000;
                            }
                        }
                        else{
                            yCurrRock--;
                        }
                    }
                    else{
                        yCurrRock--;
                    }
                }
            }
            return floor;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            //Trying to find repetition; should reach the same jet order and rock every multiple of 5 and number of jet streams.
            long numRock = 1000000000000;
            long numPlaced = 0;
            int[,] cave = new int[100000000,7];
            int rindex = 0;
            List<int[,]> rocks = new List<int[,]>();
            //Add rocks
            int[,] rock1 = new int[,]{{1,1,1,1}}; rocks.Add(rock1);
            int[,] rock2 = new int[,]{{0,1,0},{1,1,1},{0,1,0}}; rocks.Add(rock2);
            int[,] rock3 = new int[,]{{1,1,1},{0,0,1},{0,0,1}}; rocks.Add(rock3);
            int[,] rock4 = new int[,]{{1},{1},{1},{1}}; rocks.Add(rock4);
            int[,] rock5 = new int[,]{{1,1},{1,1}}; rocks.Add(rock5);
            //Define rock relative to floor
            int floor = -1;
            int batches = 10000000;
            int[] prevFloors = new int[1000000000000/batches];
            int[,] currRock = rocks.ElementAt(rindex);
            int xCurrRock = 2;
            int yCurrRock = floor+4;
            int rockLength = currRock.GetLength(1);
            int rockHeight = currRock.GetLength(0);

            while(numPlaced < numRock){
                for(int i = 0; i < line.Length; i++){
                    //Copy top thousand floor to new cave
                    if(numPlaced != 0 && numPlaced % batches == 0 && prevFloors[(int) (numPlaced / batches) - 1] == 0){
                        int[,] newCave = new int[100000000,7];
                        for(int u = 0; u < 1000; u++){
                            for(int n = 0; n < 7; n++){
                                newCave[u,n] = cave[floor-1000+u,n];
                            }
                        }
                        if((int) (numPlaced / batches) - 1 == 0){
                            prevFloors[(int) (numPlaced / batches) - 1] = floor;
                            Console.WriteLine(floor);
                        }
                        else{
                            prevFloors[(int) (numPlaced / batches) - 1] = floor-1000;
                            Console.WriteLine(floor-1000);
                        }
                        floor = 1000;
                        yCurrRock = yCurrRock % 1000;
                        if(yCurrRock > 100){
                            yCurrRock = (yCurrRock + 1000) % 1000;
                        }
                        cave = newCave;
                    }
                    int offset = 0;
                    if(line[i] == '<'){
                        offset = -1;
                    }
                    else{
                        offset = 1;
                    }
                    //Check that new position is inbounds
                    if(xCurrRock+offset >= 0 && xCurrRock + rockLength + offset - 1 <= 6){
                        bool obstruction = false;
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                if(currRock[y, x] == 1 && cave[y+yCurrRock, x+xCurrRock+offset] == 1){
                                    obstruction = true;
                                    x = rockLength;
                                    y = rockHeight;
                                }
                            }
                        }
                        if(!obstruction){
                            xCurrRock = xCurrRock+offset;
                        }
                    }
                    //Having moved, try going down one. Can't be on the floor if lowest point above floor
                    if(yCurrRock == 0){
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
                            }
                        }
                        floor = Math.Max(floor, yCurrRock+rockHeight);
                        //Set new rock
                        if(rindex<4){
                            rindex++;
                        }
                        else{
                            rindex = 0;
                        }
                        currRock = rocks[rindex];
                        xCurrRock = 2;
                        yCurrRock = floor + 3;
                        rockLength = currRock.GetLength(1);
                        rockHeight = currRock.GetLength(0);
                        numPlaced++;
                        if(numPlaced == numRock){
                            i = 100000;
                        }
                    }
                    else if(yCurrRock <= floor + 1){
                        bool onFloor = false;
                        for(int x = 0; x < rockLength; x++){
                            for(int y = 0; y < rockHeight; y++){
                                if(currRock[y, x] == 1 && cave[y+yCurrRock-1, x+xCurrRock] == 1){
                                    onFloor = true;
                                    x = rockLength;
                                    y = rockHeight;
                                }
                            }
                        }
                        if(onFloor){
                            for(int x = 0; x < rockLength; x++){
                                for(int y = 0; y < rockHeight; y++){
                                    if(currRock[y, x] == 1){
                                        cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
                                    }   
                                }
                            }
                            floor = Math.Max(floor, yCurrRock+rockHeight);
                            //Set new rock
                            if(rindex<4){
                                rindex++;
                            }
                            else{
                                rindex = 0;
                            }
                            currRock = rocks[rindex];
                            xCurrRock = 2;
                            yCurrRock = floor + 3;
                            rockLength = currRock.GetLength(1);
                            rockHeight = currRock.GetLength(0);
                            numPlaced++;
                            if(numPlaced == numRock){
                                i = 100000;
                            }
                        }
                        else{
                            yCurrRock--;
                        }
                    }
                    else{
                        yCurrRock--;
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