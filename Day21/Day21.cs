using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            Dictionary<String, long> monkVals = new Dictionary<String, long>();
            Dictionary<String, String> monkEquation = new Dictionary<String, String>();
            char[] delimiterChars = {':', ' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                double output = 0;
                if(Double.TryParse(factors[2], out output)){
                    monkVals[factors[0]] = (int) output;
                }
                else{
                    monkEquation[factors[0]] = lines[i].Substring(6);
                }
            }
            monkVals["humn"] = 3373767893067;
            //Run through equations and solve if number already available; reiterate until monkEquations empty.
            while(monkEquation.Count>0){
                Dictionary<string, string>.KeyCollection kc = monkEquation.Keys;
                Dictionary<string, long>.KeyCollection known = monkVals.Keys;
                foreach (string key in kc)  
                {  
                    string equation = monkEquation[key];
                    string[] components = equation.Split(delimiterChars);
                    if(known.Contains(components[0]) && known.Contains(components[2])){
                        switch(components[1]){
                            case "-":
                                monkVals[key] = monkVals[components[0]] - monkVals[components[2]];
                                break;
                            case "+":
                                monkVals[key] = monkVals[components[0]] + monkVals[components[2]];
                                break;
                            case "*":
                                monkVals[key] = monkVals[components[0]] * monkVals[components[2]];
                                break;
                            case "/":
                                monkVals[key] = monkVals[components[0]] / monkVals[components[2]];
                                break;
                            default:
                                Console.WriteLine(components[1]);
                                Console.WriteLine("Unexpected symbol");
                                return 0;
                        }
                        monkEquation.Remove(key);
                    }
                }  
            }
            Console.WriteLine(monkVals["rjmz"]);
            Console.WriteLine(monkVals["nfct"]);
            return 0;
        }

        public long partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            Dictionary<String, long> monkVals = new Dictionary<String, long>();
            Dictionary<String, String> monkEquation = new Dictionary<String, String>();
            char[] delimiterChars = {':', ' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                double output = 0;
                if(Double.TryParse(factors[2], out output)){
                    monkVals[factors[0]] = (int) output;
                }
                else{
                    monkEquation[factors[0]] = lines[i].Substring(6);
                }
            }
            //Humn must be set manually.
            monkVals.Remove("humn");
            //Run through equations and solve if number already available; reiterate until monkEquations no longer shrinking
            int prevCount = monkEquation.Count+10;
            while(monkEquation.Count < prevCount){
                prevCount = monkEquation.Count;
                Dictionary<string, string>.KeyCollection kc = monkEquation.Keys;
                Dictionary<string, long>.KeyCollection known = monkVals.Keys;
                
                foreach (string key in kc)  
                {  
                    if(!key.Equals("root")){
                        string equation = monkEquation[key];
                        string[] components = equation.Split(delimiterChars);
                        if(known.Contains(components[0]) && known.Contains(components[2])){
                            switch(components[1]){
                                case "-":
                                    monkVals[key] = monkVals[components[0]] - monkVals[components[2]];
                                    break;
                                case "+":
                                    monkVals[key] = monkVals[components[0]] + monkVals[components[2]];
                                    break;
                                case "*":
                                    monkVals[key] = monkVals[components[0]] * monkVals[components[2]];
                                    break;
                                case "/":
                                    monkVals[key] = monkVals[components[0]] / monkVals[components[2]];
                                    break;
                                default:
                                    Console.WriteLine(components[1]);
                                    Console.WriteLine("Unexpected symbol");
                                    return 0;
                            }
                            monkEquation.Remove(key);
                        }
                    }  
                }
            }
            Console.WriteLine(prevCount);
            //Some trial and error is worth it to dramatically speed up process
            long humn = 3373500000000;
            long left = 0;
            long right = 10;
            long prevGap = 0;
            bool flipped = false;
            string[] subroots = monkEquation["root"].Split(delimiterChars);
            while(left!=right){
                Dictionary<String, long> speculativeVal = monkVals.ToDictionary(entry => entry.Key, entry => entry.Value);
                speculativeVal["humn"] = humn;
                while(speculativeVal.Count < lines.Length){
                    Dictionary<string, string>.KeyCollection kc = monkEquation.Keys;
                    Dictionary<string, long>.KeyCollection known = monkVals.Keys;
                    Dictionary<string, long>.KeyCollection spec = speculativeVal.Keys;
                    foreach (string key in kc)  
                    {  
                        string equation = monkEquation[key];
                        string[] components = equation.Split(delimiterChars);
                        if((known.Contains(components[0]) || spec.Contains(components[0])) 
                            && (known.Contains(components[2]) || spec.Contains(components[2]))){
                            switch(components[1]){
                                case "-":
                                    speculativeVal[key] = speculativeVal[components[0]] - speculativeVal[components[2]];
                                    break;
                                case "+":
                                    speculativeVal[key] = speculativeVal[components[0]] + speculativeVal[components[2]];
                                    break;
                                case "*":
                                    speculativeVal[key] = speculativeVal[components[0]] * speculativeVal[components[2]];
                                    break;
                                case "/":
                                    speculativeVal[key] = speculativeVal[components[0]] / speculativeVal[components[2]];
                                    break;
                                default:
                                    Console.WriteLine(components[1]);
                                    Console.WriteLine("Unexpected symbol");
                                    return 0;
                            }
                        }
                    }  
                }
                if(prevGap > 0 && speculativeVal[subroots[0]] - speculativeVal[subroots[2]] < 0
                || prevGap < 0 && speculativeVal[subroots[0]] - speculativeVal[subroots[2]] > 0){
                    flipped = true;
                }
                if(!flipped){
                    humn = (long) (humn * 1.00000001);
                }
                else{
                    humn--;
                }
                if(speculativeVal[subroots[0]] - speculativeVal[subroots[2]] == 0){
                    return humn+1;
                }
                left = speculativeVal[subroots[0]];
                right = speculativeVal[subroots[2]];
                prevGap = left - right;
            }
            return humn+1;
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            int answer = s.partOne();
            Console.WriteLine(answer);
            long answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}