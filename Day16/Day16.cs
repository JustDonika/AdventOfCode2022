using System;

namespace Csharp
{
    public class Valve{
        public bool open = false;
        public int pressure = 0;

        public string[] connected;

        public string name;
        public Valve(bool isOpen, int valvePress, string[] valveConnect, string valveName){
            open = isOpen;
            pressure = valvePress;
            connected = valveConnect;
            name = valveName;
        }
    }

    public class Solution
    {
        Dictionary<String, Valve> allValves = new Dictionary<String, Valve>();

        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', '=', ',', ';'};
            string start = "AA";
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                string name = factors[1];
                int pressure = Int32.Parse(factors[5]);
                List<string> connections = new List<string>();
                for(int o = 11; o < factors.Length; o+=2){
                    connections.Add(factors[o]);
                }
                allValves[name] = new Valve(false, pressure, connections.ToArray(), name);
            }
            //int timeLeft = 30;
            //int intTotPressure = 0;
            //int perMinPressure = 0;
            //while(timeLeft>=0){
                //Find the path where pressure along route * (timeLeft - )
                //int optimum = -100;
                //int optimalTravel = 0;
                //string optimal = start;
                //Check each valve
                // foreach(Valve v in allValves.Values){
                //     if(!v.open){
                //         int travelled = 0;
                //         Valve search = allValves[start];
                //         HashSet<string> visited = new HashSet<string>();
                //         visited.Add(start);
                //         bool pathFound = false;
                //         while(!pathFound){
                //             HashSet<string> addToVisited = new HashSet<string>();
                //             foreach(string s in visited){
                //                 if(s.Equals(v.name)){
                //                     pathFound = true;
                //                 }
                //                 else{
                //                     Valve expand = allValves[s];
                //                     foreach(string connect in expand.connected){
                //                         if(!visited.Contains(connect)){
                //                             addToVisited.Add(connect);
                //                         }
                //                     }
                //                 }
                //             }
                //             travelled++;
                //             visited.UnionWith(addToVisited);
                //         }
                //         if(travelled <= timeLeft){
                //             if(v.pressure - travelled > optimum){
                //                 optimum = v.pressure - travelled;
                //                 optimal = v.name;
                //                 optimalTravel = travelled;
                //             }
                //         }
                //     }
                // }
                // if(!(start.Equals(optimal) && allValves[optimal].open)){
                //     allValves[optimal].open = true;
                //     start = optimal;
                //     intTotPressure += perMinPressure*optimalTravel;
                //     perMinPressure += allValves[optimal].pressure;
                //     timeLeft -= optimalTravel;
                // }
                // else{
                //     intTotPressure += timeLeft*perMinPressure;
                //     timeLeft = -1;
                // }
            //}
            //Find how far apart all valves are
            Dictionary<String, int> gap = new Dictionary<String, int>();
            foreach(Valve v in allValves.Values){
                foreach(Valve u in allValves.Values){
                    //if name already in dictionary or comparing against self, skip
                    if(!gap.ContainsKey(u.name+v.name) && !gap.ContainsKey(v.name+u.name) && !v.name.Equals(u.name)){
                        int travelled = 0;
                        Valve search = allValves[u.name];
                        HashSet<string> visited = new HashSet<string>();
                        visited.Add(u.name);
                        bool pathFound = false;
                        while(!pathFound){
                            HashSet<string> addToVisited = new HashSet<string>();
                            foreach(string s in visited){
                                if(s.Equals(v.name)){
                                    pathFound = true;
                                }
                                else{
                                    Valve expand = allValves[s];
                                    foreach(string connect in expand.connected){
                                        if(!visited.Contains(connect)){
                                            addToVisited.Add(connect);
                                        }
                                    }
                                }
                            }
                            travelled++;
                            visited.UnionWith(addToVisited);
                        }
                        gap[v.name+u.name] = travelled;
                    }
                    else if(gap.ContainsKey(u.name+v.name)){
                        gap[v.name+u.name] = gap[u.name+v.name];
                    }
                    else if(gap.ContainsKey(v.name+u.name)){
                        gap[u.name+v.name] = gap[v.name+u.name];
                    }
                }
            }
            //Knowing all distances, need to find the sequence which maximises pressure
            int time = 26;
            Dictionary<string, int> directionsPressure = new Dictionary<string, int>(); 
            Dictionary<string, int> directionsTime = new Dictionary<string, int>(); 
            Valve operating = allValves[start];
            foreach(Valve v in allValves.Values){
                if(v.pressure>0 && !v.name.Equals(start)){
                    directionsPressure.Add(operating.name+v.name, v.pressure*(time-gap[operating.name+v.name]));
                    directionsTime.Add(operating.name+v.name, time-gap[operating.name+v.name]);
                }
            }
            string bestPath = "";
            int bestPressure = 0;
            while(true){
                Dictionary<string, int> expandedPressure = new Dictionary<string, int>(); 
                Dictionary<string, int> expandedTime = new Dictionary<string, int>(); 
                foreach(string s in directionsPressure.Keys){
                    foreach(Valve v in allValves.Values){
                        if(v.pressure>0 && !s.Contains(v.name)){
                            if(directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]>0){
                                expandedPressure.Add(s+v.name, directionsPressure[s]+v.pressure*(directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]));
                                expandedTime.Add(s+v.name, directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]);
                                if(expandedPressure[s+v.name] > bestPressure){
                                    bestPressure = expandedPressure[s+v.name];
                                    Console.WriteLine("New record: " + expandedPressure[s+v.name]);
                                    Console.WriteLine("Path: " + s+v.name);
                                }
                            }
                        }
                    }
                }
                directionsPressure = expandedPressure;
                directionsTime = expandedTime;
                if(expandedPressure.Count == 0){
                    return bestPressure;
                }
            }
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', '=', ',', ';'};
            string start = "AA";
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                string name = factors[1];
                int pressure = Int32.Parse(factors[5]);
                List<string> connections = new List<string>();
                for(int o = 11; o < factors.Length; o+=2){
                    connections.Add(factors[o]);
                }
                allValves[name] = new Valve(false, pressure, connections.ToArray(), name);
            }
            //Find how far apart all valves are
            Dictionary<String, int> gap = new Dictionary<String, int>();
            foreach(Valve v in allValves.Values){
                foreach(Valve u in allValves.Values){
                    //if name already in dictionary or comparing against self, skip
                    if(!gap.ContainsKey(u.name+v.name) && !gap.ContainsKey(v.name+u.name) && !v.name.Equals(u.name) && (u.pressure>0 && v.pressure>0) || (v.name.Equals("AA") || u.name.Equals("AA"))){
                        int travelled = 0;
                        Valve search = allValves[u.name];
                        HashSet<string> visited = new HashSet<string>();
                        visited.Add(u.name);
                        bool pathFound = false;
                        while(!pathFound){
                            HashSet<string> addToVisited = new HashSet<string>();
                            foreach(string s in visited){
                                if(s.Equals(v.name)){
                                    pathFound = true;
                                }
                                else{
                                    Valve expand = allValves[s];
                                    foreach(string connect in expand.connected){
                                        if(!visited.Contains(connect)){
                                            addToVisited.Add(connect);
                                        }
                                    }
                                }
                            }
                            travelled++;
                            visited.UnionWith(addToVisited);
                        }
                        gap[v.name+u.name] = travelled;
                    }
                    else if(gap.ContainsKey(u.name+v.name)){
                        gap[v.name+u.name] = gap[u.name+v.name];
                    }
                    else if(gap.ContainsKey(v.name+u.name)){
                        gap[u.name+v.name] = gap[v.name+u.name];
                    }
                }
            }
            //Knowing all distances, need to find the sequences which maximises pressure
            //As such, find all possible paths, and see which two combinations without overlapping characters reach the highest pressure.
            int time = 26;
            Dictionary<string, int> allDirectionsPressure = new Dictionary<string, int>(); 
            Dictionary<string, int> allDirectionsTime = new Dictionary<string, int>(); 
            Dictionary<string, int> directionsPressure = new Dictionary<string, int>(); 
            Dictionary<string, int> directionsTime = new Dictionary<string, int>(); 
            Valve operating = allValves[start];
            foreach(Valve v in allValves.Values){
                if(v.pressure>0 && !v.name.Equals(start)){
                    directionsPressure.Add(operating.name+v.name, v.pressure*(time-gap[operating.name+v.name]));
                    directionsTime.Add(operating.name+v.name, time-gap[operating.name+v.name]);
                }
            }
            foreach(string s in directionsPressure.Keys){
                allDirectionsPressure.Add(s, directionsPressure[s]);
                allDirectionsTime.Add(s, directionsTime[s]);
            }
            int bestPressure = 0;
            while(true){
                Dictionary<string, int> expandedPressure = new Dictionary<string, int>(); 
                Dictionary<string, int> expandedTime = new Dictionary<string, int>(); 
                foreach(string s in directionsPressure.Keys){
                    foreach(Valve v in allValves.Values){
                        if(v.pressure>0){
                            //Can't use s.Contains because some of the values are the same God that was a hard mistake to spot.
                            bool alreadySeen = false;
                            for(int i = 0; i < s.Length; i+=2){
                                if(s[i] == v.name[0] && s[i+1] == v.name[1]){
                                    alreadySeen = true;
                                }
                            }
                            if(!alreadySeen){
                                if(directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]>0){
                                    expandedPressure.Add(s+v.name, directionsPressure[s]+v.pressure*(directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]));
                                    expandedTime.Add(s+v.name, directionsTime[s]-gap[s.Substring(s.Length-2)+v.name]);
                                }
                            }
                        }
                    }
                }
                directionsPressure = expandedPressure;
                directionsTime = expandedTime;
                foreach(string s in directionsPressure.Keys){
                    allDirectionsPressure.Add(s, directionsPressure[s]);
                    allDirectionsTime.Add(s, directionsTime[s]);
                }
                //No more paths to explore
                if(expandedPressure.Count == 0){
                    Console.WriteLine(allDirectionsPressure.Count);
                    int tally = 0;
                    List<string> allKeys = new List<string>(allDirectionsPressure.Keys);
                    for(int a = 0; a < allKeys.Count; a++){
                        for(int b = a; b < allKeys.Count; b++){
                            string s = allKeys.ElementAt(a);
                            string t = allKeys.ElementAt(b);
                            if(allDirectionsPressure[s]+allDirectionsPressure[t] > bestPressure){
                                bool validCombination = true;
                                for(int i = 2; i < s.Length; i+=2){
                                    for(int j = 2; j < t.Length; j+=2){
                                        //Overlap
                                        if(s[i] == t[j] || s[i+1] == t[j+1]){
                                            validCombination = false;
                                            i = 10000;
                                            j = 10000;
                                        }
                                    }
                                }
                                if(validCombination){
                                    int pressure = allDirectionsPressure[s]+allDirectionsPressure[t];
                                    if(pressure > bestPressure){
                                        bestPressure = pressure;
                                        Console.WriteLine("Best combined pressure; " + bestPressure);
                                        Console.WriteLine("Path one: " + t);
                                        Console.WriteLine("Path two: " + s);
                                    }
                                }
                            }
                        } 
                        tally++;
                        if(tally % 1000 == 0){
                            Console.WriteLine(tally);
                        } 
                    }
                    return bestPressure;
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