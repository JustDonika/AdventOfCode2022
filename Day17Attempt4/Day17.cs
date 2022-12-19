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
            int[,] cave = new int[10000000,7];
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

            //Handling large numbers
            int lastMatch = 0;
            int lowestMatch = 1000000;
            bool cycleDetected = false;
            long remainder = 1000000000000;
            long multiple = 1000000000000;
            int cycleGap = 0;
            int[,] pattern = new int[20,7];

            while(!cycleDetected){
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
                if(floor > 10000){
                    //Check for loop
                    for(int i = floor-1000; i > 0; i--){
                        bool match = true;
                        for(int a = 0; a < 20; a++){
                            for(int b = 0; b < 7; b++){
                                if(cave[floor-500+a, b] != cave[i+a, b]){
                                    match = false;
                                    a = 20;
                                    b = 7;
                                }
                            }
                        }
                        if(match){
                            Console.WriteLine("Cycle detected");
                            cycleDetected = true;
                            cycleGap = lastMatch-i;
                            lowestMatch = i;
                            for(int a = 0; a < 20; a++){
                                for(int b = 0; b < 7; b++){
                                    pattern[a, b] = cave[floor-500+a, b];
                                }
                            }
                            if(lastMatch-i != 0){
                                remainder = 1000000000000 % (long) (lastMatch-i);
                                multiple = 1000000000000 % (long) (lastMatch-i);
                            }
                            lastMatch = i;
                        }
                    }
                }
            }

            //Now know there is a cycle, how many cycles are in the number, where the lowest match is, etc.

            PartTwoCycleFound(cycleGap, multiple, remainder, lowestMatch, pattern);
            return floor;
        }

        public int PartTwoCycleFound(int totCycleGap, long totMultiple, long totRemainder, int totLowestMatch, int[,] totPattern){
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            string line = lines[0];
            long numRock = 1000000000000;
            long numPlaced = 0;
            int[,] cave = new int[1000000,7];
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

            //Handling large numbers
            int lowestMatch = totLowestMatch;
            long remainder = totRemainder;
            long multiple = totMultiple;
            int cycleGap = totCycleGap;
            int[,] pattern = totPattern;

            int upToCycle = 0;
            int perCycle = 0;
            int inRemainder = 0;
            while(true){
                for(int i = 0; i < line.Length; i++){
                    if(floor >= lowestMatch && upToCycle == 0){
                        upToCycle = (int) numPlaced;
                    }
                    if(floor >= lowestMatch+cycleGap && perCycle == 0){
                        perCycle = (int) numPlaced - upToCycle;
                        multiple = 1000000000000 / perCycle;
                    }
                    if(floor >= lowestMatch+cycleGap+remainder && inRemainder == 0){
                        inRemainder = (int) numPlaced - perCycle-upToCycle;
                        Console.WriteLine("Up to cycle: " + upToCycle);
                        Console.WriteLine("Per cycle: " + perCycle);
                        Console.WriteLine("After cycles: " + inRemainder);
                        Console.WriteLine("Multiple: " + multiple);
                        Console.WriteLine("My sanity: 0");
                        
                        Console.WriteLine((ulong)((ulong)cycleGap)*((ulong)multiple) + ((ulong)upToCycle)+((ulong)inRemainder));
                        return 0;
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
            return floor;
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