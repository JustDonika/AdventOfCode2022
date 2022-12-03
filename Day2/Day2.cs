using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            int score = 0;
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            for(int i = 0; i < lines.Length; i++){
                char opp = lines[i][0];
                char play = lines[i][2];
                if(play == 'X'){
                    score+=1;
                }
                else if(play == 'Y'){
                    score+=2;
                }
                else{
                    score+=3;
                }
                if((opp == 'A' && play == 'X') || (opp == 'B' && play == 'Y') || (opp == 'C' && play == 'Z')){
                    score+=3;
                }
                else if((opp == 'A' && play == 'Y') || (opp == 'B' && play == 'Z') || (opp == 'C' && play == 'X')){
                    score+=6;
                }
            }
            return score;
        }

        public int partTwo()
        {
            int score = 0;
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            for(int i = 0; i < lines.Length; i++){
                char opp = lines[i][0];
                char play = lines[i][2];
                //Draw, Win, or Lose?
                if(play == 'Y'){
                    score+=3;
                    score += (opp-'A'+1);
                }
                else if(play == 'Z'){
                    score+=6;
                    if(opp-'A'==2){
                        score+=1;
                    }
                    else{
                        score+=(opp-'A'+2);
                    }
                }
                else{
                    if(opp-'A' == 0){
                        score+=3;
                    }
                    else{
                        score+=opp-'A';
                    }
                }
            }
            return score;
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