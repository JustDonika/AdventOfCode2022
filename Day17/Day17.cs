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
            int numRock = line.Length*5;
            int numPlaced = 0;
            int[,] cave = new int[100*numRock,7];
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
            int[] trackFloor = new int[numRock*20];
            int[] perNumRock = new int[20];

            while(numPlaced < numRock*19){
                for(int i = 0; i < line.Length; i++){
                    if(numPlaced % numRock == 0){
                        perNumRock[(int)(numPlaced/numRock)] = floor;
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
                        trackFloor[numPlaced] = floor;
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
                            trackFloor[numPlaced] = floor;
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
            for(int i = 0; i < perNumRock.Length; i++){
                Console.WriteLine(perNumRock[i]);
            }
            //IT DOESN'T REPEAT??? Absolutely fucked then.

            //Now, if repeating, we can find remainder
            int remainder = (int) (1000000000000 % numRock);
            long multiple = (long) (1000000000000 / numRock);
            Console.WriteLine(remainder);
            Console.WriteLine(multiple);
            Console.WriteLine(floor);
            Console.WriteLine((long)(((long) multiple) * ((long) floor) + ((long) trackFloor[remainder])));
            return floor;
            // string[] lines = System.IO.File.ReadAllLines("input.txt");
            // string line = lines[0];
            // int numRock = 100000000;
            // int numPlaced = 0;
            // int[,] cave = new int[4*numRock,7];
            // int rindex = 0;
            // List<int[,]> rocks = new List<int[,]>();
            // //Add rocks
            // int[,] rock1 = new int[,]{{1,1,1,1}}; rocks.Add(rock1);
            // int[,] rock2 = new int[,]{{0,1,0},{1,1,1},{0,1,0}}; rocks.Add(rock2);
            // int[,] rock3 = new int[,]{{1,1,1},{0,0,1},{0,0,1}}; rocks.Add(rock3);
            // int[,] rock4 = new int[,]{{1},{1},{1},{1}}; rocks.Add(rock4);
            // int[,] rock5 = new int[,]{{1,1},{1,1}}; rocks.Add(rock5);
            // //Define rock relative to floor
            // int floor = -1;
            // int[,] currRock = rocks.ElementAt(rindex);
            // int xCurrRock = 2;
            // int yCurrRock = floor+4;
            // int rockLength = currRock.GetLength(1);
            // int rockHeight = currRock.GetLength(0);
            // int[,] baseOfCave = new int[30, 7];
            // int topRelevantFloor = 0;
            // //Advent of Code loves excessive numbers so can't get there that way; however, might be repetition?
            // while(true){
            //     for(int i = 0; i < line.Length; i++){
            //         //Find floor base a few times early on in process
            //         if(numPlaced == 10){
            //             for(int u = 0; u < 30; u++){
            //                 bool NoneExist = true;
            //                 for(int a = 0; a < 7; a++){
            //                     if(cave[u,a] == 1){
            //                         NoneExist = false;
            //                     }
            //                     baseOfCave[u,a] = cave[u,a];
            //                 }
            //                 if(NoneExist && topRelevantFloor == 0){
            //                     topRelevantFloor = u;
            //                 }
            //             }
            //         }
            //         int offset = 0;
            //         if(line[i] == '<'){
            //             offset = -1;
            //         }
            //         else{
            //             offset = 1;
            //         }
            //         //Check that new position is inbounds
            //         if(xCurrRock+offset >= 0 && xCurrRock + rockLength + offset - 1 <= 6){
            //             bool obstruction = false;
            //             for(int x = 0; x < rockLength; x++){
            //                 for(int y = 0; y < rockHeight; y++){
            //                     if(currRock[y, x] == 1 && cave[y+yCurrRock, x+xCurrRock+offset] == 1){
            //                         obstruction = true;
            //                         x = rockLength;
            //                         y = rockHeight;
            //                     }
            //                 }
            //             }
            //             if(!obstruction){
            //                 xCurrRock = xCurrRock+offset;
            //             }
            //         }
            //         //Having moved, try going down one. Can't be on the floor if lowest point above floor
            //         if(yCurrRock == 0){
            //             for(int x = 0; x < rockLength; x++){
            //                 for(int y = 0; y < rockHeight; y++){
            //                     cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
            //                 }
            //             }
            //             floor = Math.Max(floor, yCurrRock+rockHeight);
            //             //Set new rock
            //             if(rindex<4){
            //                 rindex++;
            //             }
            //             else{
            //                 rindex = 0;
            //             }
            //             currRock = rocks[rindex];
            //             xCurrRock = 2;
            //             yCurrRock = floor + 3;
            //             rockLength = currRock.GetLength(1);
            //             rockHeight = currRock.GetLength(0);
            //             numPlaced++;
            //             if(numPlaced == numRock){
            //                 i = 100000;
            //             }
            //         }
            //         else if(yCurrRock <= floor + 1){
            //             bool onFloor = false;
            //             for(int x = 0; x < rockLength; x++){
            //                 for(int y = 0; y < rockHeight; y++){
            //                     if(currRock[y, x] == 1 && cave[y+yCurrRock-1, x+xCurrRock] == 1){
            //                         onFloor = true;
            //                         x = rockLength;
            //                         y = rockHeight;
            //                     }
            //                 }
            //             }
            //             if(onFloor){
            //                 for(int x = 0; x < rockLength; x++){
            //                     for(int y = 0; y < rockHeight; y++){
            //                         if(currRock[y, x] == 1){
            //                             cave[y+yCurrRock, x+xCurrRock] = currRock[y, x]; 
            //                         }   
            //                     }
            //                 }
            //                 floor = Math.Max(floor, yCurrRock+rockHeight);

            //                 //Check if most recent levels are identical to base; repetition?
            //                 if(floor > 1000){
            //                     bool matchFound = true;
            //                     for(int u = 0; u < topRelevantFloor; u++){
            //                         for(int a = 0; a < 7; a++){
            //                             if(baseOfCave[u,a] != cave[floor-u,a]){
            //                                 u = topRelevantFloor;
            //                                 a = 7;
            //                                 matchFound = false;
            //                             }
            //                         }
            //                     }
            //                     if(matchFound){
            //                         Console.WriteLine("So you're saying there's a chance");
            //                     }
            //                 }
            //                 //Set new rock
            //                 if(rindex<4){
            //                     rindex++;
            //                 }
            //                 else{
            //                     rindex = 0;
            //                 }
            //                 currRock = rocks[rindex];
            //                 xCurrRock = 2;
            //                 yCurrRock = floor + 3;
            //                 rockLength = currRock.GetLength(1);
            //                 rockHeight = currRock.GetLength(0);
            //                 numPlaced++;
            //                 if(numPlaced == numRock){
            //                     i = 100000;
            //                 }
            //             }
            //             else{
            //                 yCurrRock--;
            //             }
            //         }
            //         else{
            //             yCurrRock--;
            //         }
            //     }
            // }

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