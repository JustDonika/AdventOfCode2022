using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            int numSensors = 38;
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', '=', ',', ':'};
            int[,] sensors = new int[numSensors, 2];
            int[,] beacons = new int[numSensors, 2];
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                sensors[i,0] = Int32.Parse(factors[3]);
                sensors[i,1] = Int32.Parse(factors[6]);
                beacons[i,0] = Int32.Parse(factors[13]);
                beacons[i,1] = Int32.Parse(factors[16]);
            }
            //Find clear non-beacons at 2000000
            int rowCheck = 2000000;
            List<Int32> clear = new List<Int32>();
            int[,] range =  new int[numSensors, 2];
            for(int i = 0; i < numSensors; i++){
                if(Math.Abs(sensors[i, 1] - beacons[i, 1]) + Math.Abs(sensors[i, 0] - beacons[i, 0]) > Math.Abs(sensors[i, 1]-rowCheck)){
                    Console.WriteLine(i);
                    int numCleared = Math.Abs(sensors[i, 1] - beacons[i, 1]) + Math.Abs(sensors[i, 0] - beacons[i, 0]) - Math.Abs(sensors[i, 1]-rowCheck);
                    range[i,0] = sensors[i, 0] - numCleared;
                    range[i,1] = sensors[i, 0] + numCleared;
                }
            }
            //Remove unnecessary ranges
            for(int i = 0; i < numSensors; i++){
                for(int j = i + 1; j < numSensors; j++){
                    if(!(range[i, 0] == 0 && range[i, 1] == 0) && !(range[j, 0] == 0 && range[j, 1] == 0)){
                        //Range i starts inside range j
                        if(range[i, 0] >= range[j,0] && range[i, 0] <= range[j,1]){
                            range[i, 0] = range[j, 1]+1;
                        }
                        //Range i ends inside range j
                        if(range[i, 1] >= range[j,0] && range[i, 1] <= range[j,1]){
                            range[i, 1] = range[j, 0]-1;
                        }
                        //Range j starts inside range i
                        if(range[j, 0] >= range[i,0] && range[j, 0] <= range[i,1]){
                            range[j, 0] = range[i, 1]+1;
                        }
                        //Range j ends inside range i
                        if(range[j, 1] >= range[i,0] && range[j, 1] <= range[i,1]){
                            range[j, 1] = range[i, 0]-1;
                        }
                        //Range i subsumes range j
                        if(range[i, 0] <= range[j, 0] && range[i, 1] >= range[j, 1]){
                            range[j,0] = 0;
                            range[j,1] = 0;
                        }
                        //Range j subsumes range i
                        else if(range[j, 0] <= range[i, 0] && range[j, 1] >= range[i, 1]){
                            range[i,0] = 0;
                            range[i,1] = 0;
                        }
                    }
                }
            }
            for(int i = 0; i < numSensors; i++){
                if(!(range[i, 0] == 0 && range[i, 1] == 0)){
                    for(int o = range[i, 0]; o <= range[i,1]; o++){
                        clear.Add(o);
                        if(clear.Count % 50000 == 0){
                            Console.WriteLine(clear.Count);
                        }
                    }
                }
            }
            //Find number of beacons on the row
            List<Int32> beaconXs = new List<Int32>();
            for(int i = 0; i < numSensors; i++){
                if(beacons[i,1] == rowCheck && !beaconXs.Contains(beacons[i,0])){
                    beaconXs.Add(beacons[i,0]);
                }
            }
            return clear.Count - beaconXs.Count;
        }

        public int partTwo()
        {
            int numSensors = 38;
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', '=', ',', ':'};
            int[,] sensors = new int[numSensors, 2];
            int[,] beacons = new int[numSensors, 2];
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                sensors[i,0] = Int32.Parse(factors[3]);
                sensors[i,1] = Int32.Parse(factors[6]);
                beacons[i,0] = Int32.Parse(factors[13]);
                beacons[i,1] = Int32.Parse(factors[16]);
            }
            int max = 4000000;
            //Remove unnecessary ranges
            for(int y = 0; y < max; y++){
                int rowCheck = y;
                int[,] range =  new int[numSensors, 2];
                for(int i = 0; i < numSensors; i++){
                    if(Math.Abs(sensors[i, 1] - beacons[i, 1]) + Math.Abs(sensors[i, 0] - beacons[i, 0]) > Math.Abs(sensors[i, 1]-rowCheck)){
                        int numCleared = Math.Abs(sensors[i, 1] - beacons[i, 1]) + Math.Abs(sensors[i, 0] - beacons[i, 0]) - Math.Abs(sensors[i, 1]-rowCheck);
                        range[i,0] = sensors[i, 0] - numCleared;
                        range[i,1] = sensors[i, 0] + numCleared;
                    }
                }
                for(int i = 0; i < numSensors; i++){
                    for(int j = i + 1; j < numSensors; j++){
                        //Remove over max and under 0
                        if(range[i, 0] > max || range[i, 1] < 0){
                            range[i, 0] = 0;
                            range[i, 1] = 0;
                        }
                        //Remove over max
                        if(range[i, 1] > max){
                            range[i, 1] = max;
                        }
                        //Remove under min
                        if(range[i, 0] < 0){
                            range[i, 0] = 0;
                        }
                        if(!(range[i, 0] == 0 && range[i, 1] == 0) && !(range[j, 0] == 0 && range[j, 1] == 0)){
                            //Range i starts inside range j
                            if(range[i, 0] >= range[j,0] && range[i, 0] <= range[j,1]){
                                range[i, 0] = range[j, 1]+1;
                            }
                            //Range i ends inside range j
                            if(range[i, 1] >= range[j,0] && range[i, 1] <= range[j,1]){
                                range[i, 1] = range[j, 0]-1;
                            }
                            //Range j starts inside range i
                            if(range[j, 0] >= range[i,0] && range[j, 0] <= range[i,1]){
                                range[j, 0] = range[i, 1]+1;
                            }
                            //Range j ends inside range i
                            if(range[j, 1] >= range[i,0] && range[j, 1] <= range[i,1]){
                                range[j, 1] = range[i, 0]-1;
                            }
                            //Range i subsumes range j
                            if(range[i, 0] <= range[j, 0] && range[i, 1] >= range[j, 1]){
                                range[j,0] = 0;
                                range[j,1] = 0;
                            }
                            //Range j subsumes range i
                            else if(range[j, 0] <= range[i, 0] && range[j, 1] >= range[i, 1]){
                                range[i,0] = 0;
                                range[i,1] = 0;
                            }
                        }
                    }
                }
                //See if there's any area missing
                List<Int32> clear = new List<Int32>();
                for(int i = 0; i < numSensors; i++){
                    if(!(range[i, 0] == 0 && range[i, 1] == 0)){
                        for(int o = range[i, 0]; o <= range[i,1]; o++){
                            clear.Add(o);
                            if(clear.Count % 4000000 == 0){
                                Console.WriteLine(y);
                            }
                        }
                    }
                }
                int x = 0;
                if(clear.Count != max+1){
                    Console.WriteLine(y);
                    for(int i = 0; i <= max; i++){
                        if(!clear.Contains(i)){
                            x = i;
                            Console.WriteLine(x);
                            return(x*4000000+y);
                        }
                    }
                }
            }
            return 0;
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