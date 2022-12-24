using System;

namespace Csharp
{
    public class Solution
    {
        public static int IndexOfMin(List<int> s)
        {
            if (s == null) {
                throw new ArgumentNullException("self");
            }

            if (s.Count == 0) {
                throw new ArgumentException("List is empty.", "self");
            }

            int min = s[0];
            int minIndex = 0;

            for (int i = 1; i < s.Count; ++i) {
                if (s[i] < min) {
                    min = s[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int startX = lines[0].IndexOf('.');
            int startY = 0;
            int targetX = lines[lines.Length-1].IndexOf('.');
            int targetY = lines.Length-1;
            //Create map where each square consists of
            // <^
            // >v
            //So blizzards can pass each other.
            int[,] map = new int[lines[0].Length*2, lines.Length*2];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o < lines[i].Length; o++){
                    if(lines[i][o] == '#'){
                        for(int a = 0; a < 2; a++){
                            for(int b = 0; b < 2; b++){
                                map[o*2+a,i*2+b] = 8;
                            }
                        }
                    }
                    else if(lines[i][o] == 'v'){
                        map[o*2+1,i*2+1] = 1;
                    }
                    else if(lines[i][o] == '<'){
                        map[o*2,i*2] = 1;
                    }             
                    else if(lines[i][o] == '>'){
                        map[o*2,i*2+1] = 1;
                    }
                    else if(lines[i][o] == '^'){
                        map[o*2+1,i*2] = 1;
                    }
                    //If ., do nothing
                }
            }
            Dictionary<Int32, int[,]> allMaps = new Dictionary<Int32, int[,]>();
            //Calculate map progression for 5000 turns
            for(int i = 0; i < 1000; i++){
                allMaps[i] = map;
                int[,] storageMap = new int[lines[0].Length*2, lines.Length*2];
                for(int x = 0; x < lines[0].Length; x++){
                    for(int y = 0; y < lines.Length; y++){
                        //Left
                        if(map[x*2, y*2] == 1){
                            if(x == 1){
                                storageMap[(lines[0].Length-2)*2, y*2] = 1;
                            }
                            else{
                                storageMap[(x-1)*2, y*2] = 1;
                            }
                        }
                        //Right
                        if(map[x*2, y*2+1] == 1){
                            if(x == lines[0].Length-2){
                                storageMap[1*2, y*2+1] = 1;
                            }
                            else{
                                storageMap[(x+1)*2, y*2+1] = 1;
                            }
                        }
                        //Up
                        if(map[x*2+1, y*2] == 1){
                            if(y == 1){
                                storageMap[x*2+1, (lines.Length-2)*2] = 1;
                            }
                            else{
                                storageMap[x*2+1, (y-1)*2] = 1;
                            }
                        }
                        //Down
                        if(map[x*2+1, y*2+1] == 1){
                            if(y == lines.Length-2){
                                storageMap[x*2+1, 1*2+1] = 1;
                            }
                            else{
                                storageMap[x*2+1, (y+1)*2+1] = 1;
                            }
                        }
                        //Draw in walls again
                        if(map[x*2, y*2] == 8){
                            for(int a = 0; a < 2; a++){
                                for(int b = 0; b < 2; b++){
                                    storageMap[x*2+a,y*2+b] = 8;
                                }
                            }
                        }
                    }
                }
                map = storageMap;
            }
            Console.WriteLine("Done");
            //Find best path
            Dictionary<Int32, Int32> allPaths = new Dictionary<Int32, Int32>();
            List<Int32> allOptions = new List<Int32>();
            List<Int32> heuristic = new List<Int32>();
            int bestFound = 1000000;
            allPaths[10000*(startY*(lines[0].Length)+startX)] = 0;
            allOptions.Add(10000*(startY*(lines[0].Length)+startX));
            heuristic.Add(Math.Abs(targetX-startX)+Math.Abs(targetY-startY));
            int upTo = 0;
            while(allOptions.Count > 0){
                int indexOfNote = IndexOfMin(heuristic);
                int bestHeuristic = heuristic.ElementAt(indexOfNote);
                int currInstance = allOptions.ElementAt(indexOfNote);
                int currRound = allPaths[currInstance];
                allOptions.RemoveAt(indexOfNote);
                heuristic.RemoveAt(indexOfNote);
                if(currRound > upTo){
                    Console.WriteLine(currRound);
                    upTo=currRound;
                }
                //If holding on to too many datapoints, try reducing size
                if(allOptions.Count > 10000){
                    Console.WriteLine(allOptions.Count);
                    for(int i = 0; i < allOptions.Count; i++){
                        //Find duplicates
                        for(int j = i + 1; j < allOptions.Count; j++){
                            if(allOptions.ElementAt(i) == allOptions.ElementAt(j)){
                                allOptions.RemoveAt(j);
                                heuristic.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                    Console.WriteLine(allOptions.Count);
                }
                if(currRound < bestFound){
                    int[,] currMap = allMaps[currRound+1];
                    int currY = (int) (currInstance / (lines[0].Length*10000));
                    int currX = (currInstance/10000) % lines[0].Length;
                    // Console.WriteLine(currX + ", "+currY);
                    //Find valid spots to move to;
                    //Up
                    if(currY > 0 && currMap[currX*2,(currY-1)*2] == 0 && currMap[currX*2+1,(currY-1)*2] == 0 && currMap[currX*2,(currY-1)*2+1] == 0 && currMap[currX*2+1,(currY-1)*2+1] == 0){
                        allPaths[10000*((currY-1)*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*((currY-1)*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-(currY-1))+currRound+1);
                    }
                    //Left
                    if(currMap[(currX-1)*2,currY*2] == 0 && currMap[(currX-1)*2+1,currY*2] == 0 && currMap[(currX-1)*2,currY*2+1] == 0 && currMap[(currX-1)*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+(currX-1))+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+(currX-1))+currRound+1);
                        heuristic.Add(Math.Abs(targetX-(currX-1))+Math.Abs(targetY-currY)+currRound+1);
                    }
                    //Wait
                    if(currMap[currX*2,currY*2] == 0 && currMap[currX*2+1,currY*2] == 0 && currMap[currX*2,currY*2+1] == 0 && currMap[currX*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-currY)+currRound+1);
                    }
                    //Down; Win condition is down
                    if(currY < lines.Length && currMap[currX*2,(currY+1)*2] == 0 && currMap[currX*2+1,(currY+1)*2] == 0 && currMap[currX*2,(currY+1)*2+1] == 0 && currMap[currX*2+1,(currY+1)*2+1] == 0){
                        allPaths[10000*((currY+1)*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*((currY+1)*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-(currY+1))+currRound+1);
                        if(currY + 1 == lines.Length-1 && bestFound > currRound+1){
                            bestFound = currRound+1;
                            Console.WriteLine("New best");
                            Console.WriteLine(bestFound);
                            allOptions.Clear();
                        }
                    }
                    //Right
                    if(currMap[(currX+1)*2,currY*2] == 0 && currMap[(currX+1)*2+1,currY*2] == 0 && currMap[(currX+1)*2,currY*2+1] == 0 && currMap[(currX+1)*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+(currX+1))+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+(currX+1))+currRound+1);
                        heuristic.Add(Math.Abs(targetX-(currX+1))+Math.Abs(targetY-currY)+currRound+1);
                    }
                }
            }
            return bestFound;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int startX = lines[0].IndexOf('.');
            int startY = 0;
            int targetX = lines[lines.Length-1].IndexOf('.');
            int targetY = lines.Length-1;
            //Create map where each square consists of
            // <^
            // >v
            //So blizzards can pass each other.
            int[,] map = new int[lines[0].Length*2, lines.Length*2];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o < lines[i].Length; o++){
                    if(lines[i][o] == '#'){
                        for(int a = 0; a < 2; a++){
                            for(int b = 0; b < 2; b++){
                                map[o*2+a,i*2+b] = 8;
                            }
                        }
                    }
                    else if(lines[i][o] == 'v'){
                        map[o*2+1,i*2+1] = 1;
                    }
                    else if(lines[i][o] == '<'){
                        map[o*2,i*2] = 1;
                    }             
                    else if(lines[i][o] == '>'){
                        map[o*2,i*2+1] = 1;
                    }
                    else if(lines[i][o] == '^'){
                        map[o*2+1,i*2] = 1;
                    }
                    //If ., do nothing
                }
            }
            Dictionary<Int32, int[,]> allMaps = new Dictionary<Int32, int[,]>();
            //Calculate map progression for 3000 turns
            for(int i = 0; i < 3000; i++){
                allMaps[i] = map;
                int[,] storageMap = new int[lines[0].Length*2, lines.Length*2];
                for(int x = 0; x < lines[0].Length; x++){
                    for(int y = 0; y < lines.Length; y++){
                        //Left
                        if(map[x*2, y*2] == 1){
                            if(x == 1){
                                storageMap[(lines[0].Length-2)*2, y*2] = 1;
                            }
                            else{
                                storageMap[(x-1)*2, y*2] = 1;
                            }
                        }
                        //Right
                        if(map[x*2, y*2+1] == 1){
                            if(x == lines[0].Length-2){
                                storageMap[1*2, y*2+1] = 1;
                            }
                            else{
                                storageMap[(x+1)*2, y*2+1] = 1;
                            }
                        }
                        //Up
                        if(map[x*2+1, y*2] == 1){
                            if(y == 1){
                                storageMap[x*2+1, (lines.Length-2)*2] = 1;
                            }
                            else{
                                storageMap[x*2+1, (y-1)*2] = 1;
                            }
                        }
                        //Down
                        if(map[x*2+1, y*2+1] == 1){
                            if(y == lines.Length-2){
                                storageMap[x*2+1, 1*2+1] = 1;
                            }
                            else{
                                storageMap[x*2+1, (y+1)*2+1] = 1;
                            }
                        }
                        //Draw in walls again
                        if(map[x*2, y*2] == 8){
                            for(int a = 0; a < 2; a++){
                                for(int b = 0; b < 2; b++){
                                    storageMap[x*2+a,y*2+b] = 8;
                                }
                            }
                        }
                    }
                }
                map = storageMap;
            }
            Console.WriteLine("Done");
            //Find best path
            Dictionary<Int32, Int32> allPaths = new Dictionary<Int32, Int32>();
            List<Int32> allOptions = new List<Int32>();
            List<Int32> heuristic = new List<Int32>();
            int bestFound = 1000000;
            allPaths[10000*(startY*(lines[0].Length)+startX)] = 0;
            allOptions.Add(10000*(startY*(lines[0].Length)+startX));
            heuristic.Add(Math.Abs(targetX-startX)+Math.Abs(targetY-startY));
            int upTo = 0;
            bool firstArrival = false;
            bool snackFound = false;
            while(allOptions.Count > 0){
                int indexOfNote = IndexOfMin(heuristic);
                int bestHeuristic = heuristic.ElementAt(indexOfNote);
                int currInstance = allOptions.ElementAt(indexOfNote);
                int currRound = allPaths[currInstance];
                allOptions.RemoveAt(indexOfNote);
                heuristic.RemoveAt(indexOfNote);
                if(currRound > upTo){
                    Console.WriteLine(currRound);
                    upTo=currRound;
                }
                //If holding on to too many datapoints, try reducing size
                if(allOptions.Count > 10000){
                    Console.WriteLine(allOptions.Count);
                    for(int i = 0; i < allOptions.Count; i++){
                        //Find duplicates
                        for(int j = i + 1; j < allOptions.Count; j++){
                            if(allOptions.ElementAt(i) == allOptions.ElementAt(j)){
                                allOptions.RemoveAt(j);
                                heuristic.RemoveAt(j);
                                j--;
                            }
                        }
                    }
                    Console.WriteLine(allOptions.Count);
                }
                if(currRound < bestFound){
                    int[,] currMap = allMaps[currRound+1];
                    int currY = (int) (currInstance / (lines[0].Length*10000));
                    int currX = (currInstance/10000) % lines[0].Length;
                    // Console.WriteLine(currX + ", "+currY);
                    //Find valid spots to move to;
                    //Up; might have reached snack point
                    if(currY > 0 && currMap[currX*2,(currY-1)*2] == 0 && currMap[currX*2+1,(currY-1)*2] == 0 && currMap[currX*2,(currY-1)*2+1] == 0 && currMap[currX*2+1,(currY-1)*2+1] == 0){
                        allPaths[10000*((currY-1)*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*((currY-1)*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-(currY-1))+currRound+1);
                        if(!snackFound && currY - 1 == targetY && bestFound > currRound+1){
                                Console.WriteLine("The deeply essential snacks have been acquired");
                                snackFound = true;
                                Console.WriteLine(currRound+1);
                                allPaths.Clear();
                                allOptions.Clear();
                                heuristic.Clear();
                                startX = lines[0].IndexOf('.');
                                startY = 0;
                                targetX = lines[lines.Length-1].IndexOf('.');
                                targetY = lines.Length-1;
                                allPaths[10000*(startY*(lines[0].Length)+startX)+currRound+1] = currRound+1;
                                allOptions.Add((10000*(startY*(lines[0].Length)+startX)+currRound+1));
                                heuristic.Add(Math.Abs(targetX-startX)+Math.Abs(targetY-startY)+currRound+1);
                            }
                    }
                    //Left
                    if(currMap[(currX-1)*2,currY*2] == 0 && currMap[(currX-1)*2+1,currY*2] == 0 && currMap[(currX-1)*2,currY*2+1] == 0 && currMap[(currX-1)*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+(currX-1))+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+(currX-1))+currRound+1);
                        heuristic.Add(Math.Abs(targetX-(currX-1))+Math.Abs(targetY-currY)+currRound+1);
                    }
                    //Wait
                    if(currMap[currX*2,currY*2] == 0 && currMap[currX*2+1,currY*2] == 0 && currMap[currX*2,currY*2+1] == 0 && currMap[currX*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-currY)+currRound+1);
                    }
                    //Down; Win condition is down
                    if(currY < lines.Length-1 && currMap[currX*2,(currY+1)*2] == 0 && currMap[currX*2+1,(currY+1)*2] == 0 && currMap[currX*2,(currY+1)*2+1] == 0 && currMap[currX*2+1,(currY+1)*2+1] == 0){
                        allPaths[10000*((currY+1)*(lines[0].Length)+currX)+currRound+1] = currRound+1;
                        allOptions.Add(10000*((currY+1)*(lines[0].Length)+currX)+currRound+1);
                        heuristic.Add(Math.Abs(targetX-currX)+Math.Abs(targetY-(currY+1))+currRound+1);
                        if(currY + 1 == targetY && bestFound > currRound+1){
                            if(!firstArrival){
                                Console.WriteLine("Not the snacks! Oh the horror!");
                                firstArrival = true;
                                Console.WriteLine(currRound+1);
                                allPaths.Clear();
                                allOptions.Clear();
                                heuristic.Clear();
                                targetX = lines[0].IndexOf('.');
                                targetY = 0;
                                startX = lines[lines.Length-1].IndexOf('.');
                                startY = lines.Length-1;
                                allPaths[10000*(startY*(lines[0].Length)+startX)+currRound+1] = currRound+1;
                                allOptions.Add(10000*(startY*(lines[0].Length)+startX)+currRound+1);
                                heuristic.Add(Math.Abs(targetX-startX)+Math.Abs(targetY-startY)+currRound+1);
                            }
                            else{
                                bestFound = currRound+1;
                                Console.WriteLine("New best");
                                Console.WriteLine(bestFound);
                                allOptions.Clear();
                            }
                        }
                    }
                    //Right
                    if(currMap[(currX+1)*2,currY*2] == 0 && currMap[(currX+1)*2+1,currY*2] == 0 && currMap[(currX+1)*2,currY*2+1] == 0 && currMap[(currX+1)*2+1,currY*2+1] == 0){
                        allPaths[10000*(currY*(lines[0].Length)+(currX+1))+currRound+1] = currRound+1;
                        allOptions.Add(10000*(currY*(lines[0].Length)+(currX+1))+currRound+1);
                        heuristic.Add(Math.Abs(targetX-(currX+1))+Math.Abs(targetY-currY)+currRound+1);
                    }
                }
            }
            return bestFound;
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