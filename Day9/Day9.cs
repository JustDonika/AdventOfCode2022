using System;

namespace Csharp
{

    public class Solution
    {
        
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<int[]> tailVisited = new List<int[]>();
            int headX = 0;
            int headY = 0;
            int tailX = 0;
            int tailY = 0;
            char[] delimiterChars = {' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                int traversed = Int32.Parse(factors[1]);
                for(int o = 0; o < traversed; o++){
                    if(factors[0] == "D"){
                        headY+=-1;
                    }
                    if(factors[0] == "U"){
                        headY+=1;
                    }
                    if(factors[0] == "L"){
                        headX+=-1;
                    }
                    if(factors[0] == "R"){
                        headX+=1;
                    }
                    if(Math.Abs(tailX-headX) == 2){
                        tailX = Math.Min(tailX, headX)+1;
                        if(Math.Abs(tailY-headY) == 1){
                            tailY = headY;
                        }
                    }
                    if(Math.Abs(tailY-headY) == 2){
                        tailY = Math.Min(tailY, headY)+1;
                        if(Math.Abs(tailX-headX) == 1){
                            tailX = headX;
                        }
                    }
                    //Record coordinate visited
                    bool alreadySeen = false;
                    for(int u = 0; u < tailVisited.Count; u++){
                        if(tailVisited[u][0] == tailX && tailVisited[u][1] == tailY){
                            alreadySeen = true;
                            u = tailVisited.Count;
                        }
                    }
                    int[] tailCoords = {tailX, tailY};
                    if(!alreadySeen){
                        tailVisited.Add(tailCoords);
                    }
                }
            }

            int sum = tailVisited.Count;
            return sum;
        }

        public int partTwo()
        {   
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            List<int[]> tailVisited = new List<int[]>();
            int[,] rope = new int[10,2];
            char[] delimiterChars = {' '};
            for(int i = 0; i < lines.Length; i++){
                string[] factors = lines[i].Split(delimiterChars);
                int traversed = Int32.Parse(factors[1]);
                for(int o = 0; o < traversed; o++){
                    for(int k = 0; k < 10; k++){
                        if(k==0){
                           if(factors[0] == "D"){
                                rope[k,1]+=-1;
                            }
                            if(factors[0] == "U"){
                                rope[k,1]+=1;
                            }
                            if(factors[0] == "L"){
                                rope[k,0]+=-1;
                            }
                            if(factors[0] == "R"){
                                rope[k,0]+=1;
                            }
                        }
                        else{
                            if(Math.Abs(rope[k,0]-rope[k-1,0]) == 2){
                                rope[k,0] = Math.Min(rope[k-1,0], rope[k,0])+1;
                                if(Math.Abs(rope[k,1]-rope[k-1,1]) == 1){
                                    rope[k,1] = rope[k-1,1];
                                }
                                else if(Math.Abs(rope[k,1]-rope[k-1,1]) == 2){
                                    rope[k,1] = Math.Min(rope[k-1,1], rope[k,1])+1;;
                                }
                            }
                            if(Math.Abs(rope[k,1]-rope[k-1,1]) == 2){
                                rope[k,1] = Math.Min(rope[k-1,1], rope[k,1])+1;
                                if(Math.Abs(rope[k,0]-rope[k-1,0]) == 1){
                                    rope[k,0] = rope[k-1,0];
                                }
                                else if(Math.Abs(rope[k,0]-rope[k-1,0]) == 2){
                                    rope[k,0] = Math.Min(rope[k-1,0], rope[k,0])+1;;
                                }
                            }
                        }
                    }

                    //Record coordinate visited
                    bool alreadySeen = false;
                    for(int u = 0; u < tailVisited.Count; u++){
                        if(tailVisited[u][0] == rope[9,0] && tailVisited[u][1] == rope[9,1]){
                            alreadySeen = true;
                            u = tailVisited.Count;
                        }
                    }
                    int[] tailCoords = {rope[9,0], rope[9,1]};
                    if(!alreadySeen){
                        tailVisited.Add(tailCoords);
                    }
                }
            }
            int sum = tailVisited.Count;
            return sum;
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