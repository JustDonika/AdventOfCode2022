using System;

namespace Csharp
{
    public class Node
    {
        public int distTravelled = 0;
        public int x = 0;
        public int y = 0;

        public Node(int xa, int ya, int dist){
            x = xa;
            y = ya;
            distTravelled = dist;
        }
    }

    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int startX = 0;
            int startY = 0;
            int endX = 0;
            int endY = 0;
            List<Node> pathTaken = new List<Node>();
            List<int[]> alreadyVisited = new List<int[]>();
            int[,] allSquares = new int[lines.Length, lines[0].Length];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o < lines[0].Length; o++){
                    if(lines[i][o] == 'S'){
                        allSquares[i, o] = 0;
                        startX = i;
                        startY = o;
                        pathTaken.Add(new Node(o, i, 0));
                    }
                    else if(lines[i][o] == 'E'){
                        allSquares[i, o] = 25;
                        endX = i;
                        endY = o;
                    }
                    else{
                        allSquares[i, o] = lines[i][o] - 'a';
                    }
                }
            }
            while(true){
                //Expand nodes
                Node n = pathTaken.ElementAt(0);
                List<int[]> adjacent = new List<int[]>();
                adjacent.Add(new int[]{n.x-1, n.y}); adjacent.Add(new int[]{n.x+1, n.y});
                adjacent.Add(new int[]{n.x, n.y-1}); adjacent.Add(new int[]{n.x, n.y+1});
                for(int i = 0; i < 4; i++){
                    if(adjacent[i][0] >= 0 && adjacent[i][0] < lines[0].Length && adjacent[i][1] >= 0 && adjacent[i][1] < lines.Length){
                        if(!(adjacent[i][0] == startX && adjacent[i][1] == startY)){
                            if(allSquares[n.y, n.x] + 1 >= allSquares[adjacent[i][1], adjacent[i][0]]){
                                bool seen = false;
                                for(int o = alreadyVisited.Count-1; o >= 0; o--){
                                    if(alreadyVisited.ElementAt(o)[0] == adjacent[i][0] && alreadyVisited.ElementAt(o)[1] == adjacent[i][1]){
                                        seen = true;
                                        o = -1;
                                    }
                                }
                                if(!seen){
                                    pathTaken.Add(new Node(adjacent[i][0], adjacent[i][1], n.distTravelled+1));
                                    alreadyVisited.Add(new int[]{adjacent[i][0], adjacent[i][1]});
                                }
                                if(adjacent[i][1] == endX && adjacent[i][0] == endY){
                                    return(n.distTravelled+1);
                                }
                            }
                        }
                    }
                }
                pathTaken.RemoveAt(0);
            }
            return 0;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int endX = 0;
            int endY = 0;
            List<Node> pathTaken = new List<Node>();
            List<int[]> alreadyVisited = new List<int[]>();
            int[,] allSquares = new int[lines.Length, lines[0].Length];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o < lines[0].Length; o++){
                    if(lines[i][o] == 'S'){
                        allSquares[i, o] = 0;
                    }
                    else if(lines[i][o] == 'E'){
                        allSquares[i, o] = 25;
                        endX = i;
                        endY = o;
                        pathTaken.Add(new Node(o, i, 0));
                    }
                    else{
                        allSquares[i, o] = lines[i][o] - 'a';
                    }
                }
            }
            while(true){
                //Expand nodes
                Node n = pathTaken.ElementAt(0);
                List<int[]> adjacent = new List<int[]>();
                adjacent.Add(new int[]{n.x-1, n.y}); adjacent.Add(new int[]{n.x+1, n.y});
                adjacent.Add(new int[]{n.x, n.y-1}); adjacent.Add(new int[]{n.x, n.y+1});
                for(int i = 0; i < 4; i++){
                    if(adjacent[i][0] >= 0 && adjacent[i][0] < lines[0].Length && adjacent[i][1] >= 0 && adjacent[i][1] < lines.Length){
                            if(allSquares[n.y, n.x] -1 <= allSquares[adjacent[i][1], adjacent[i][0]]){
                                bool seen = false;
                                for(int o = alreadyVisited.Count-1; o >= 0; o--){
                                    if(alreadyVisited.ElementAt(o)[0] == adjacent[i][0] && alreadyVisited.ElementAt(o)[1] == adjacent[i][1]){
                                        seen = true;
                                        o = -1;
                                    }
                                }
                                if(!seen){
                                    pathTaken.Add(new Node(adjacent[i][0], adjacent[i][1], n.distTravelled+1));
                                    alreadyVisited.Add(new int[]{adjacent[i][0], adjacent[i][1]});
                                }
                                if(allSquares[adjacent[i][1], adjacent[i][0]] == 0){
                                    return(n.distTravelled+1);
                                }
                            }
                    }
                }
                pathTaken.RemoveAt(0);
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