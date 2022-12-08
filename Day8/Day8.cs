using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int length = lines[0].Length;
            int sum = 0;
            for(int x = 0; x < length; x++){
                for(int y = 0; y < lines.Length; y++){
                    //Check each direction
                    int height = lines[y][x] - '0';
                    bool visible = false;
                    bool clearPath = true;
                    //Check left
                    for(int i = 0; i < x; i++){
                        int compare = lines[y][i] - '0';
                        if(compare >= height){
                            clearPath = false;
                            i=10000;
                        }
                    }
                    if(clearPath){
                        visible = true;
                    }
                    clearPath = true;
                    //Check right
                    for(int i = x+1; i < length; i++){
                        int compare = lines[y][i] - '0';
                        if(compare >= height){
                            clearPath = false;
                            i = 10000;
                        }
                    }
                    if(clearPath){
                        visible = true;
                    }
                    clearPath = true;
                    //Check up
                    for(int i = 0; i < y; i++){
                        int compare = lines[i][x] - '0';
                        if(compare >= height){
                            clearPath = false;
                            i = 10000;
                        }
                    }
                    if(clearPath){
                        visible = true;
                    }
                    clearPath = true;
                    //Check down
                    for(int i = y+1; i < lines.Length; i++){
                        int compare = lines[i][x] - '0';
                        if(compare >= height){
                            clearPath = false;
                            i = 10000;
                        }
                    }
                    if(clearPath){
                        visible = true;
                    }
                    clearPath = true;

                    if(visible){
                        sum+=1;
                    }
                }
            }

            return sum;
        }

        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int length = lines[0].Length;
            int best = 0;
            for(int x = 0; x < length; x++){
                for(int y = 0; y < lines.Length; y++){
                    //Check each direction
                    int height = lines[y][x] - '0';
                    int left = 0;
                    int right = 0;
                    int up = 0;
                    int down = 0;
                    //Check left
                    for(int i = x-1; i >= 0; i--){
                        int compare = lines[y][i] - '0';
                        if(compare >= height){
                            left+=1;
                            i = -1;
                        }
                        else{
                            left+=1;
                        }
                    }
                    //Check right
                    for(int i = x+1; i < length; i++){
                        int compare = lines[y][i] - '0';
                        if(compare >= height){
                            right+=1;
                            i = 10000;
                        }
                        else{
                            right+=1;
                        }
                    }
                    //Check up
                    for(int i = y-1; i >= 0; i--){
                        int compare = lines[i][x] - '0';
                       if(compare >= height){
                            up+=1;
                            i = -1;
                        }
                        else{
                            up+=1;
                        }
                    }
                    //Check down
                    for(int i = y+1; i < lines.Length; i++){
                        int compare = lines[i][x] - '0';
                        if(compare >= height){
                            down+=1;
                            i = 10000;
                        }
                        else{
                            down+=1;
                        }
                    }
                    if(best < left*right*up*down){
                        best = left*right*up*down;
                    }
                }
            }
            return best;
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