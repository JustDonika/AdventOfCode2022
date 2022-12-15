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

        public long partTwo()
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
            //Point will be along the boundary of several of the beacons
            int[,] coords = new int[4, 2];
            for(int i = 0; i < numSensors; i++){
                int numGap = Math.Abs(sensors[i,0] - beacons[i,0]) + Math.Abs(sensors[i,1] - beacons[i,1])+1;
                for(int o = 0; o < numGap; o++){
                    //Left-up, up-right, right-down, down-left
                    coords[0,0]=sensors[i,0]-numGap+o;
                    coords[0,1]=sensors[i,1]+o;
                    coords[1,0]=sensors[i,0]+o;
                    coords[1,1]=sensors[i,1]+numGap-o;
                    coords[2,0]=sensors[i,0]+numGap-o;
                    coords[2,1]=sensors[i,1]-o;
                    coords[3,0]=sensors[i,0]-o;
                    coords[3,1]=sensors[i,1]-numGap+o;
                    for(int co = 0; co < 4; co++){
                        if(coords[co,0] > 0 && coords[co,0] < max && coords[co,1] > 0 && coords[co,1] < max){
                            bool foundVal = true;
                            for(int a = 0; a < numSensors; a++){
                                int compare = Math.Abs(sensors[a,0] - beacons[a,0]) + Math.Abs(sensors[a,1] - beacons[a,1]);
                                if(Math.Abs(coords[co,0]-sensors[a,0]) + Math.Abs(coords[co,1]-sensors[a,1]) <= compare){ 
                                    foundVal = false;
                                    a = numSensors;
                                }
                            }
                            if(foundVal){
                                Console.WriteLine(coords[co,0]);
                                Console.WriteLine(coords[co,1]);
                                return (long) (coords[co,0]) * 4000000 + coords[co,1];
                            }
                        }
                    }
                }
            }
            return 0;
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            int answer = s.partOne();
            Console.WriteLine("Part one: " + answer);
            long answer2 = s.partTwo();
            Console.WriteLine("Part Two: " + answer2);
        }
    }
}