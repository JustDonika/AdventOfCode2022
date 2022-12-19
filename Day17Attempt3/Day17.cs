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
            int[,] currRock = rocks.ElementAt(rindex);
            int xCurrRock = 2;
            int yCurrRock = floor+4;
            int rockLength = currRock.GetLength(1);
            int rockHeight = currRock.GetLength(0);
            //Find matching sample
            int[,] matchingSample = new int[40,7];
            int matchingHeight = 0;
            while(numPlaced < numRock){
                for(int i = 0; i < line.Length; i++){
                    if(numPlaced == 100){
                        for(int y = 0; y < 40; y++){
                            for(int x = 0; x < 7; x++){
                                if(cave[y, x] == 1){
                                    matchingSample[y, x] = 1;
                                }
                            }
                        }
                        matchingHeight = floor;
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
                            floor = Math.Max(floor, yCurrRock+rockHeight);
                            //Check for a match.
                            if(floor > 200){
                                bool matching = true;
                                for(int a = -2; a < 2; a++){
                                    for(int y = 0; y < 20; y++){
                                        for(int x = 0; x < 7; x++){
                                            if(cave[floor-matchingHeight+y+a, x] != matchingSample[y, x]){
                                                matching = false;
                                            }
                                        }
                                    }
                                    if(matching){
                                        Console.WriteLine("That one");
                                        Console.WriteLine(numPlaced);
                                        Console.WriteLine(floor);
                                        Console.WriteLine((ulong)(1000000000000 % (ulong) numPlaced));
                                        //Match found; almost certainly at recurring point, in which case
                                        //(1000000000000 / numPlaced) * floor 
                                        Console.WriteLine((ulong)(1000000000000 / (ulong) numPlaced));
                                    }
                                }
                            }
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
                    Console.WriteLine(floor);
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