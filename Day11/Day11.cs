using System;

namespace Csharp
{
    public class Solution
    {
        public int numMonk = 8;
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ':', ','};
            //Defining monkeys
            int[] timesInspected = new int[numMonk];
            List<List<Int32>> itemsEach = new List<List<Int32>>();
            bool[] multiplies = new bool[numMonk];
            int[] operationChange = new int[numMonk];
            int[] divisible = new int[numMonk];
            int[] ifTrue = new int[numMonk];
            int[] ifFalse = new int[numMonk];

            for(int i = 0; i < numMonk; i++){
                string[] factors = lines[i*7+1].Split(delimiterChars);
                List<Int32> items = new List<Int32>();
                for(int o = 5; o < factors.Length; o+=2){
                    items.Add(Int32.Parse(factors[o]));
                }
                itemsEach.Add(items);

                factors = lines[i*7+2].Split(delimiterChars);
                if(factors[7] == "+"){
                    multiplies[i] = false;
                }
                else{
                    multiplies[i] = true;
                }
                double output = 0;
                //Numeric check; 9999999 holds event of operating with itself.
                if(double.TryParse(factors[8], out output)){
                    operationChange[i] = (int) output;
                }
                else{
                    operationChange[i] = 9999999;
                }

                factors = lines[i*7+3].Split(delimiterChars);
                divisible[i] = Int32.Parse(factors[6]);

                factors = lines[i*7+4].Split(delimiterChars);
                ifTrue[i] = Int32.Parse(factors[10]);
                
                factors = lines[i*7+5].Split(delimiterChars);
                ifFalse[i] = Int32.Parse(factors[10]);

            }
            //Running 20 rounds:
            for(int i = 0; i < 20; i++){
                for(int o = 0; o < numMonk; o++){
                    for(int u = 0; u < itemsEach.ElementAt(o).Count; u++){
                        timesInspected[o]+=1;
                        int worryLevel = itemsEach.ElementAt(o).ElementAt(u);
                        int factor = operationChange[o];
                        if(factor == 9999999){
                            factor = worryLevel;
                        }
                        if(multiplies[o]){
                            worryLevel = worryLevel * factor;
                        }
                        else{
                            worryLevel = worryLevel + factor;
                        }
                        //Monkey bored with
                        worryLevel = (int) (worryLevel/3);
                        if(worryLevel % divisible[o] == 0){
                            itemsEach.ElementAt(ifTrue[o]).Add(worryLevel);
                        }
                        else{
                            itemsEach.ElementAt(ifFalse[o]).Add(worryLevel);
                        }
                    }
                    itemsEach.ElementAt(o).Clear();
                }
            }
            for(int i = 0; i < numMonk; i++){
                Console.WriteLine(timesInspected[i]);
            }

            return 0;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' ', ':', ','};
            //Defining monkeys
            int[] timesInspected = new int[numMonk];
            List<List<Int32>> itemsEach = new List<List<Int32>>();
            bool[] multiplies = new bool[numMonk];
            int[] operationChange = new int[numMonk];
            int[] divisible = new int[numMonk];
            int[] ifTrue = new int[numMonk];
            int[] ifFalse = new int[numMonk];

            for(int i = 0; i < numMonk; i++){
                string[] factors = lines[i*7+1].Split(delimiterChars);
                List<Int32> items = new List<Int32>();
                for(int o = 5; o < factors.Length; o+=2){
                    items.Add(Int32.Parse(factors[o]));
                }
                itemsEach.Add(items);

                factors = lines[i*7+2].Split(delimiterChars);
                if(factors[7] == "+"){
                    multiplies[i] = false;
                }
                else{
                    multiplies[i] = true;
                }
                double output = 0;
                //Numeric check; 9999999 holds event of operating with itself.
                if(double.TryParse(factors[8], out output)){
                    operationChange[i] = (int) output;
                }
                else{
                    operationChange[i] = 9999999;
                }

                factors = lines[i*7+3].Split(delimiterChars);
                divisible[i] = Int32.Parse(factors[6]);

                factors = lines[i*7+4].Split(delimiterChars);
                ifTrue[i] = Int32.Parse(factors[10]);
                
                factors = lines[i*7+5].Split(delimiterChars);
                ifFalse[i] = Int32.Parse(factors[10]);

            }
            int totalDivisor = 1;
            for(int i = 0; i < numMonk; i++){
                totalDivisor*=divisible[i];
            }
            Console.WriteLine(totalDivisor);
            //Running 20 rounds:
            for(int i = 0; i < 10000; i++){
                for(int o = 0; o < numMonk; o++){
                    for(int u = 0; u < itemsEach.ElementAt(o).Count; u++){
                        timesInspected[o]+=1;
                        long worryLevel = (long) itemsEach.ElementAt(o).ElementAt(u);
                        int factor = operationChange[o];
                        if(factor == 9999999){
                            factor = (int) worryLevel;
                        }

                        if(multiplies[o]){
                            worryLevel = worryLevel * factor;
                        }
                        else{
                            worryLevel = worryLevel + factor;
                        }
                        //Handle extremely large worryLevels
                        worryLevel %= totalDivisor;

                        if(worryLevel % divisible[o] == 0){
                            itemsEach.ElementAt(ifTrue[o]).Add((int) worryLevel);
                        }
                        else{
                            itemsEach.ElementAt(ifFalse[o]).Add((int) worryLevel);
                        }
                    }
                    itemsEach.ElementAt(o).Clear();
                }
            }
            for(int i = 0; i < numMonk; i++){
                Console.WriteLine(timesInspected[i]);
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