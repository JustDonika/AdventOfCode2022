using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int[,] map = new int[1000,1000];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o <lines[i].Length; o++){
                    if(lines[i][o] == '#'){
                        map[500-(int)(lines[i].Length/2)+o, 500-(int)(lines.Length/2)+i] = 1;
                    }
                }
            }
            List<Int32[]> proposals = new List<Int32[]>();
            List<Int32[]> moves = new List<Int32[]>();
            //Add moves
            moves.Add(new int[]{-1, -1, 1, -1}); //North
            moves.Add(new int[]{-1, 1, 1, 1}); // South
            moves.Add(new int[]{-1, -1, -1, 1}); //West
            moves.Add(new int[]{1, -1, 1, 1}); //East
            int round = 0;
            while(round < 10){
                //For each elf, consider where to move
                for(int x = 100; x < 900; x++){
                    for(int y = 100; y < 900; y++){
                        if(map[x,y] != 0){
                            //Is there an elf in adjacent 9 squares?
                            bool noElf = true;
                            for(int i = -1; i <=1; i++){
                                for(int j = -1; j <= 1; j++){
                                    if(map[x+i,y+j] != 0 && !(i==0 && j==0)){
                                        noElf = false;
                                        i = 2;
                                        j = 2;
                                    }
                                }
                            }
                            int proposed = round;
                            if(!noElf){
                                //Proposed move dependent on round; 
                                for(int i = 0; i < 4; i++){
                                    int[] move = moves.ElementAt((proposed+i) % 4);
                                    bool moveWorks = true;
                                    for(int a = move[0]; a <= move[2]; a++){
                                        for(int b = move[1]; b <= move[3]; b++){
                                            if(map[x+a,y+b] != 0){
                                                moveWorks = false;
                                                a = 10;
                                                b = 10;
                                            }
                                        }  
                                    }
                                    if(moveWorks){
                                        proposals.Add(new int[]{x, y, x+(move[0]+move[2])/2, y+(move[1]+move[3])/2});
                                        i = 4;
                                    }
                                }
                            }
                        }
                    }
                }
                for(int i = 0; i<proposals.Count; i++){
                    bool duplicateExists = false;
                    for(int j = i+1; j<proposals.Count; j++){
                        if(proposals.ElementAt(i)[2] == proposals.ElementAt(j)[2] && proposals.ElementAt(i)[3] == proposals.ElementAt(j)[3]){
                            //Moving to same square;
                            duplicateExists = true;
                            proposals.RemoveAt(j);
                            j--;
                        }
                    }
                    //Separated in case more than two share a destination square
                    if(duplicateExists){
                        proposals.RemoveAt(i);
                        i--;
                    }
                    else{ //make move
                        map[proposals.ElementAt(i)[0], proposals.ElementAt(i)[1]] = 0;
                        map[proposals.ElementAt(i)[2], proposals.ElementAt(i)[3]] = 1;
                    }
                }
                proposals.Clear();
                Console.WriteLine(round);
                for(int i = 480; i < 520; i++){
                    for(int j = 480; j < 520; j++){
                        Console.Write(map[j,i]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                round++;
            }
            int minY = 10000;
            int maxY = 0;
            int minX = 10000;
            int maxX = 0;
            int numElves = 0;
            for(int x = 0; x < 1000; x++){
                for(int y = 0; y < 1000; y++){
                    if(map[x, y] != 0){
                        if(y < minY){
                            minY = y;
                        }
                        if(y > maxY){
                            maxY = y;
                        }
                        if(x < minX){
                            minX = x;
                        }
                        if(x > maxX){
                            maxX = x;
                        }
                        numElves++;
                    }
                }
            }
            Console.WriteLine("X is between "+minX +" and "+maxX+", Y is between "+minY +" and "+maxY+". Both are inclusive! Add one to range.");
            Console.WriteLine("There are "+numElves+" elves.");
            return (maxX-minX+1)*(maxY-minY+1)-numElves;
        }

        public int partTwo()
        {
string[] lines = System.IO.File.ReadAllLines("input.txt");
            int[,] map = new int[1000,1000];
            for(int i = 0; i < lines.Length; i++){
                for(int o = 0; o <lines[i].Length; o++){
                    if(lines[i][o] == '#'){
                        map[500-(int)(lines[i].Length/2)+o, 500-(int)(lines.Length/2)+i] = 1;
                    }
                }
            }
            List<Int32[]> proposals = new List<Int32[]>();
            List<Int32[]> moves = new List<Int32[]>();
            //Add moves
            moves.Add(new int[]{-1, -1, 1, -1}); //North
            moves.Add(new int[]{-1, 1, 1, 1}); // South
            moves.Add(new int[]{-1, -1, -1, 1}); //West
            moves.Add(new int[]{1, -1, 1, 1}); //East
            int round = 0;
            while(round < 10000){
                //For each elf, consider where to move
                for(int x = 100; x < 900; x++){
                    for(int y = 100; y < 900; y++){
                        if(map[x,y] != 0){
                            //Is there an elf in adjacent 9 squares?
                            bool noElf = true;
                            for(int i = -1; i <=1; i++){
                                for(int j = -1; j <= 1; j++){
                                    if(map[x+i,y+j] != 0 && !(i==0 && j==0)){
                                        noElf = false;
                                        i = 2;
                                        j = 2;
                                    }
                                }
                            }
                            int proposed = round;
                            if(!noElf){
                                //Proposed move dependent on round; 
                                for(int i = 0; i < 4; i++){
                                    int[] move = moves.ElementAt((proposed+i) % 4);
                                    bool moveWorks = true;
                                    for(int a = move[0]; a <= move[2]; a++){
                                        for(int b = move[1]; b <= move[3]; b++){
                                            if(map[x+a,y+b] != 0){
                                                moveWorks = false;
                                                a = 10;
                                                b = 10;
                                            }
                                        }  
                                    }
                                    if(moveWorks){
                                        proposals.Add(new int[]{x, y, x+(move[0]+move[2])/2, y+(move[1]+move[3])/2});
                                        i = 4;
                                    }
                                }
                            }
                        }
                    }
                }
                if(proposals.Count == 0){
                    return round+1;
                }
                for(int i = 0; i<proposals.Count; i++){
                    bool duplicateExists = false;
                    for(int j = i+1; j<proposals.Count; j++){
                        if(proposals.ElementAt(i)[2] == proposals.ElementAt(j)[2] && proposals.ElementAt(i)[3] == proposals.ElementAt(j)[3]){
                            //Moving to same square;
                            duplicateExists = true;
                            proposals.RemoveAt(j);
                            j--;
                        }
                    }
                    //Separated in case more than two share a destination square
                    if(duplicateExists){
                        proposals.RemoveAt(i);
                        i--;
                    }
                    else{ //make move
                        map[proposals.ElementAt(i)[0], proposals.ElementAt(i)[1]] = 0;
                        map[proposals.ElementAt(i)[2], proposals.ElementAt(i)[3]] = 1;
                    }
                }
                proposals.Clear();

                round++;
            }
            Console.WriteLine("outside range");
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