using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ':'};
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
            //Define parameters
            int minutes = 24;
            int blueprintID = 0;
            int oreRobotCost = 0;
            int clayRobotCost = 0;
            int obRobotOreCost = 0;
            int obRobotClayCost = 0;
            int geodeRobotOreCost = 0;
            int geodeRobotObCost = 0;
            //Store quality levels
            int[] qualityLevel = new int[lines.Length];
            //Iterate through blueprints
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                blueprintID = Int32.Parse(factors[1]);
                oreRobotCost = Int32.Parse(factors[7]);
                clayRobotCost = Int32.Parse(factors[13]);
                obRobotOreCost = Int32.Parse(factors[19]);
                obRobotClayCost = Int32.Parse(factors[22]);
                geodeRobotOreCost = Int32.Parse(factors[28]);
                geodeRobotObCost = Int32.Parse(factors[31]);
                int bestGeodes = 0;
                int oreBots = 1;
                int clayBots = 0;
                int obBots = 0;
                int geodeBots = 0;
                //Iterate through every possible set of operations
                //Scenario defined as minute, numOre, numClay, numOb, numGeode, numOreBot, numClayBot, numObBot, numGeodeBot
                int[] scenario = new int[]{0, 0, 0, 0, 0, oreBots, clayBots, obBots, geodeBots};
                List<int[]> allOptions = new List<int[]>();
                allOptions.Add(scenario);
                while(allOptions.Count > 0){
                    int[] currScenario = allOptions.ElementAt(allOptions.Count-1);
                    allOptions.RemoveAt(allOptions.Count-1);
                    if(currScenario[4] + (currScenario[8] + 0.5*(minutes-currScenario[0]))*(minutes-currScenario[0]) > bestGeodes){
                        //Add min and resources from bots; set whether bots are craftable BEFORE adding resources.
                        bool oreBotAddable = currScenario[1]>=oreRobotCost;
                        bool clayBotAddable = currScenario[1]>=clayRobotCost;
                        bool obBotAddable = currScenario[1]>=obRobotOreCost && currScenario[2]>=obRobotClayCost;
                        bool geodeBotAddable = currScenario[1]>=geodeRobotOreCost && currScenario[3]>=geodeRobotObCost;
                        currScenario[1]+=currScenario[5];
                        currScenario[2]+=currScenario[6];
                        currScenario[3]+=currScenario[7];
                        currScenario[4]+=currScenario[8];
                        currScenario[0]++;
                        //Check if final minute reached.
                        if(currScenario[0]==minutes){
                            if(currScenario[4]>bestGeodes){
                                bestGeodes = currScenario[4];
                                Console.WriteLine(bestGeodes);
                            }
                        }
                        //Else, add scenario back (and add variants with more bots if can be afforded)
                        else{
                            allOptions.Add(currScenario);
                            int[] variant = (int[])currScenario.Clone();
                            //Can afford ore robot?
                            if(oreBotAddable){
                                variant[1]-=oreRobotCost;
                                variant[5]++;
                                allOptions.Add(variant);
                                variant = (int[])currScenario.Clone();
                            }
                            //Can afford clay robot?
                            if(clayBotAddable){
                                variant[1]-=clayRobotCost;
                                variant[6]++;
                                allOptions.Add(variant);
                                variant = (int[])currScenario.Clone();
                            }
                            //Can afford obsidian robot?
                            if(obBotAddable){
                                variant[1]-=obRobotOreCost;
                                variant[2]-=obRobotClayCost;
                                variant[7]++;
                                allOptions.Add(variant);
                                variant = (int[])currScenario.Clone();
                            }
                            //Can afford geode robot?
                            if(geodeBotAddable){
                                variant[1]-=geodeRobotOreCost;
                                variant[3]-=geodeRobotObCost;
                                variant[8]++;
                                allOptions.Add(variant);
                                variant = (int[])currScenario.Clone();
                            }
                        }
                    }
                }
                qualityLevel[i] = bestGeodes;
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                Console.WriteLine("New");
            }
            int sum = 0;
            for(int i = 0; i < qualityLevel.Length; i++){
                Console.WriteLine(qualityLevel[i]);
                sum+=qualityLevel[i]*(i+1);
            }
            return sum;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ':'};
            Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
            //Define parameters
            int minutes = 32;
            int blueprintID = 0;
            int oreRobotCost = 0;
            int clayRobotCost = 0;
            int obRobotOreCost = 0;
            int obRobotClayCost = 0;
            int geodeRobotOreCost = 0;
            int geodeRobotObCost = 0;
            //Store quality levels
            int[] qualityLevel = new int[Math.Min(lines.Length, 3)];
            //Iterate through blueprints
            for(int i = 0; i < Math.Min(lines.Length, 3); i++){
                string[] factors = lines[i].Split(delimiterChars);
                blueprintID = Int32.Parse(factors[1]);
                oreRobotCost = Int32.Parse(factors[7]);
                clayRobotCost = Int32.Parse(factors[13]);
                obRobotOreCost = Int32.Parse(factors[19]);
                obRobotClayCost = Int32.Parse(factors[22]);
                geodeRobotOreCost = Int32.Parse(factors[28]);
                geodeRobotObCost = Int32.Parse(factors[31]);
                int bestGeodes = 0;
                int oreBots = 1;
                int clayBots = 0;
                int obBots = 0;
                int geodeBots = 0;
                //Iterate through every possible set of operations
                //Scenario defined as minute, numOre, numClay, numOb, numGeode, numOreBot, numClayBot, numObBot, numGeodeBot
                int iteration = 0;
                int[] scenario = new int[]{0, 0, 0, 0, 0, oreBots, clayBots, obBots, geodeBots};
                List<int[]> allOptions = new List<int[]>();
                allOptions.Add(scenario);
                while(allOptions.Count > 0){
                    int[] currScenario = allOptions.ElementAt(allOptions.Count-1);
                    allOptions.RemoveAt(allOptions.Count-1);
                    iteration++;
                    if(currScenario[4] + (currScenario[8] + 0.5*(minutes-currScenario[0]))*(minutes-currScenario[0]) > bestGeodes){
                        //Add min and resources from bots; set whether bots are craftable BEFORE adding resources.
                        bool oreBotAddable = currScenario[1]>=oreRobotCost;
                        bool clayBotAddable = currScenario[1]>=clayRobotCost;
                        bool obBotAddable = currScenario[1]>=obRobotOreCost && currScenario[2]>=obRobotClayCost;
                        bool geodeBotAddable = currScenario[1]>=geodeRobotOreCost && currScenario[3]>=geodeRobotObCost;
                        
                        currScenario[1]+=currScenario[5];
                        currScenario[2]+=currScenario[6];
                        currScenario[3]+=currScenario[7];
                        currScenario[4]+=currScenario[8];
                        currScenario[0]++;
                        //Check if final minute reached.
                        if(currScenario[0]==minutes){
                            if(currScenario[4]>bestGeodes){
                                bestGeodes = currScenario[4];
                                Console.WriteLine(bestGeodes);
                            }
                        }
                        //Else, add scenario back (and add variants with more bots if can be afforded)
                        else{
                            allOptions.Add(currScenario);
                            int[] variant = new int[]{currScenario[0], currScenario[1], currScenario[2], currScenario[3], currScenario[4], currScenario[5], currScenario[6],currScenario[7],currScenario[8]};
                            //Can afford ore robot?
                            if(oreBotAddable){
                                variant[1]-=oreRobotCost;
                                variant[5]++;
                                allOptions.Add(variant);
                                variant = new int[]{currScenario[0], currScenario[1], currScenario[2], currScenario[3], currScenario[4], currScenario[5], currScenario[6],currScenario[7],currScenario[8]};
                            }
                            //Can afford clay robot?
                            if(clayBotAddable){
                                variant[1]-=clayRobotCost;
                                variant[6]++;
                                allOptions.Add(variant);
                                variant = new int[]{currScenario[0], currScenario[1], currScenario[2], currScenario[3], currScenario[4], currScenario[5], currScenario[6],currScenario[7],currScenario[8]};
                            }
                            //Can afford obsidian robot?
                            if(obBotAddable){
                                variant[1]-=obRobotOreCost;
                                variant[2]-=obRobotClayCost;
                                variant[7]++;
                                allOptions.Add(variant);
                                variant = new int[]{currScenario[0], currScenario[1], currScenario[2], currScenario[3], currScenario[4], currScenario[5], currScenario[6],currScenario[7],currScenario[8]};
                            }
                            //Can afford geode robot?
                            if(geodeBotAddable){
                                variant[1]-=geodeRobotOreCost;
                                variant[3]-=geodeRobotObCost;
                                variant[8]++;
                                allOptions.Add(variant);
                                variant = new int[]{currScenario[0], currScenario[1], currScenario[2], currScenario[3], currScenario[4], currScenario[5], currScenario[6],currScenario[7],currScenario[8]};

                            }
                        }
                    }
                    //who needs updates more than once every billion operations
                    if(iteration % 500000000 == 0){
                        Console.WriteLine("Still running");
                        Console.WriteLine(iteration);
                        Console.WriteLine(allOptions.Count);
                        Console.WriteLine();
                    }
                }
                qualityLevel[i] = bestGeodes;
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt"));
                Console.WriteLine("New");
            }
            int sum = 0;
            for(int i = 0; i < qualityLevel.Length; i++){
                Console.WriteLine(qualityLevel[i]);
                sum*=qualityLevel[i];
            }
            return sum;
        }

        static void Main(string[] args)
        {
            //Who needs remotely efficient code when you can leave it to run overnight instead
            Solution s = new Solution();
            //int answer = s.partOne();
            //Console.WriteLine(answer);
            int answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}