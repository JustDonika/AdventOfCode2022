using System;

namespace Csharp
{
    public class Node{
        public List<Node> subdirectory = new List<Node>();
        public string name = " ";
        public int size = 0;
        public int calculateSize(){
            int sum = size;
            for(int i = 0; i < subdirectory.Count; i++){
                sum += subdirectory[i].calculateSize();
            }
            return sum;
        }
    }

    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            char[] delimiterChars = {' '};
            Node origin = new Node();
            Node system = new Node();
            system.name = "/";
            origin.subdirectory.Add(system);
            Node operating = origin;
            //List of subdirectories gone into
            List<String> subdirs = new List<String>();

            for(int i = 0; i < lines.Length; i++){
                string line = lines[i];
                Console.WriteLine(line);
                Console.WriteLine(operating.name);
                string[] factors = line.Split(delimiterChars);
                //cd and not ..
                if(factors[1].Equals("cd") && !factors[2].Equals("..")){
                    for(int o = 0; o < operating.subdirectory.Count; o++){
                        if(factors[2].Equals(operating.subdirectory.ElementAt(o).name)){
                            operating = operating.subdirectory.ElementAt(o);
                            subdirs.Add(factors[2]);
                            o = 10000;
                        }
                    }
                }
                //cd and ..
                else if(factors[1].Equals("cd")){ //Go back up a level.
                    subdirs.RemoveAt(subdirs.Count - 1);
                    operating = origin;
                    for(int o = 0; o < subdirs.Count; o++){
                        for(int u = 0; u < operating.subdirectory.Count; u++){
                            if(factors[2].Equals(operating.subdirectory.ElementAt(u).name)){
                                operating = operating.subdirectory.ElementAt(u);
                                u = 10000;
                        }
                    }
                    }
                }
                //Directory node
                if(factors[0].Equals("dir")){
                    Node n = new Node();
                    n.name = factors[1];
                    operating.subdirectory.Add(n);
                }
                double output = 0;
                //Numeric node
                if(double.TryParse(factors[0], out output)){
                    Node n = new Node();
                    n.size = Int32.Parse(factors[0]);
                    operating.subdirectory.Add(n); 
                }
                
            }
            return 0;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
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