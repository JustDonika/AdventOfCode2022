using System;

namespace Csharp
{
    public class Node{
        public List<String> subdirectory = new List<String>();
        public int size = 0;

    }

    public class Solution
    {

        Dictionary<String, Node> allDirectories = new Dictionary<String, Node>();

        public int calculateSize(string s){
            Node n = allDirectories[s];
            int sum = n.size;
            for(int i = 0; i < n.subdirectory.Count; i++){
                sum+=calculateSize(s+n.subdirectory.ElementAt(i));
            }
            return sum;
        }

        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' '};
            string currentDir = "";
            allDirectories["/"] = new Node();
            List<String> subdirs = new List<String>();

            for(int i = 0; i < lines.Length; i++){
                string line = lines[i];
                string[] factors = line.Split(delimiterChars);
                //cd and not ..
                if(factors[1].Equals("cd") && !factors[2].Equals("..")){
                    subdirs.Add(factors[2]);
                    currentDir = "";
                    for (int a = 0; a < subdirs.Count; a++){
                        currentDir = currentDir + subdirs.ElementAt(a);
                    }
                }
                //cd and ..
                else if(factors[1].Equals("cd")){ //Go back up a level.
                    subdirs.RemoveAt(subdirs.Count - 1);
                    currentDir = "";
                    for (int a = 0; a < subdirs.Count; a++){
                        currentDir = currentDir + subdirs.ElementAt(a);
                    }
                }
                //Directory node
                else if(factors[0].Equals("dir")){
                    allDirectories[currentDir].subdirectory.Add(factors[1]);
                    allDirectories[currentDir+factors[1]] = new Node();
                }
                double output = 0;
                //Numeric node
                if(double.TryParse(factors[0], out output)){
                    allDirectories[currentDir].size+=(int)output;
                }
            }
            int sum = 0;
            foreach(KeyValuePair<String, Node> entry in allDirectories)
            {
                int value = calculateSize(entry.Key);
                if(value <= 100000){
                    sum+=value;
                }
            }
            return sum;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int minimumRequired = calculateSize("/")-40000000;
            int nearestMinimum = 1000000000;
            foreach(KeyValuePair<String, Node> entry in allDirectories)
            {
                int value = calculateSize(entry.Key);
                if(value > minimumRequired && value < nearestMinimum){
                    nearestMinimum = value;
                }
            }
            Console.WriteLine(minimumRequired);
            return nearestMinimum;
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