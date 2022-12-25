using System;

namespace Csharp
{
    public class Solution
    {
        public string partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            long total = 0;
            for(int i = 0; i < lines.Length; i++){
                string currLine = lines[i];
                long currTotal = 0;
                for(int o = currLine.Length-1; o >= 0; o--){
                    long currDig = 0;
                    if(currLine[o] == '-'){
                        currDig = -1;
                    }
                    else if(currLine[o] == '='){
                        currDig = -2;
                    }
                    else{
                        currDig = currLine[o] - '0';
                    }
                    long currExponent = (long) Math.Pow(5, currLine.Length - 1 - o);
                    currTotal+=currDig*currExponent;
                }
                total+=currTotal;
            }
            //Convert back to SNAFU
            string s = "";
            int current = 0;
            long remainder = total;
            while(remainder > 0){
                if(remainder % 5 == 0){
                    s+="0";
                    remainder /= 5;
                }
                else if(remainder % 5 == 1){
                    s+="1";
                    remainder -= 1;
                    remainder /= 5;
                }
                else if(remainder % 5 == 2){
                    s+="2";
                    remainder -= 2;
                    remainder /= 5;
                }
                else if(remainder % 5 == 3){
                    s+="=";
                    remainder += 2;
                    remainder /= 5;
                }
                else if(remainder % 5 ==4){
                    s+="-";
                    remainder += 1;
                    remainder /= 5;
                }
            }    
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            s = new string(charArray);
            return s;
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            string answer = s.partOne();
            Console.WriteLine(answer);
            Console.WriteLine("Advent of Code completed!");
        }
    }
}