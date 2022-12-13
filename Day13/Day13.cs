using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            int sum = 0;
            for(int i = 0; i < lines.Length/3; i++){
                string left = lines[i*3];
                string right = lines[i*3+1];
                int index = 0;
                bool correctOrder = true;
                for(int o = 0; o < left.Length; o++){
                    double leftVal = left[o] - '0';
                    double rightVal = right[index] - '0';
                    //Handle end of list
                    if(left[o] == ']' && right[index] != ']'){
                        o = left.Length;
                    }
                    else if(left[o] != ']' && right[index] == ']'){
                        correctOrder = false;
                        o = left.Length;
                    }
                    //Convert number to list if necessary
                    //Left is number, right is not.
                    else if(leftVal >= 0 && leftVal < 10 && (rightVal < 0 || rightVal >9)){
                        int numLength = 0;
                        int oadj = o;
                        while(left[oadj] - '0' >= 0 && left[oadj] - '0' < 10){
                            numLength++;
                            oadj++;
                        }
                        left = left.Substring(0, o) + "[" + left.Substring(o, numLength) + "]" + left.Substring(o+numLength);
                    }
                    //Right is number, left is not.
                    else if(rightVal >= 0 && rightVal < 10 && (leftVal < 0 || leftVal >9)){
                        int numLength = 0;
                        int indadj = index;
                        while(right[indadj] - '0' >= 0 && right[indadj] - '0' < 10){
                            numLength++;
                            indadj++;
                        }
                        right = right.Substring(0, index) + "[" + right.Substring(index, numLength) + "]" + right.Substring(index+numLength);
                    }

                    //Handle numbers
                    else if(leftVal >= 0 && leftVal < 10 && rightVal >= 0 && rightVal < 10){
                        //Handle multiple digit numbers
                        o++;
                        while(left[o] - '0' >= 0 && left[o] - '0' < 10){
                            leftVal = leftVal*10 + left[o] - '0';
                            o++;
                        }
                        //Adjust once back
                        o--;
                        index++;
                        while(right[index] - '0' >= 0 && right[index] - '0' < 10){
                            rightVal = rightVal*10 + right[index] - '0';
                            index++;
                        }
                        index--;
                        if(leftVal>rightVal){
                            correctOrder = false;
                            o = left.Length;
                        }
                        else if(rightVal>leftVal){
                            o = left.Length;
                        }

                    }
                    index++;
                }
                if(correctOrder){
                    sum+=i+1;
                }

            }
            return sum;
        }

        public int partTwo()
        {
            string[] lines = File.ReadAllLines("input.txt").Where(s => s.Trim() != string.Empty).ToArray();
            List<string> list = new List<string>(lines.ToList());
            list.Add("[[2]]");
            list.Add("[[6]]");
            lines = list.ToArray();
            for(int runs = 0; runs < lines.Length; runs++){
                for(int i = 0; i < lines.Length-1; i++){
                    string left = lines[i];
                    string right = lines[i+1];
                    int index = 0;
                    bool correctOrder = true;
                    for(int o = 0; o < left.Length; o++){
                        double leftVal = left[o] - '0';
                        double rightVal = right[index] - '0';
                        //Handle end of list
                        if(left[o] == ']' && right[index] != ']'){
                            o = left.Length;
                        }
                        else if(left[o] != ']' && right[index] == ']'){
                            correctOrder = false;
                            o = left.Length;
                        }
                        //Convert number to list if necessary
                        //Left is number, right is not.
                        else if(leftVal >= 0 && leftVal < 10 && (rightVal < 0 || rightVal >9)){
                            int numLength = 0;
                            int oadj = o;
                            while(left[oadj] - '0' >= 0 && left[oadj] - '0' < 10){
                                numLength++;
                                oadj++;
                            }
                            left = left.Substring(0, o) + "[" + left.Substring(o, numLength) + "]" + left.Substring(o+numLength);
                        }
                        //Right is number, left is not.
                        else if(rightVal >= 0 && rightVal < 10 && (leftVal < 0 || leftVal >9)){
                            int numLength = 0;
                            int indadj = index;
                            while(right[indadj] - '0' >= 0 && right[indadj] - '0' < 10){
                                numLength++;
                                indadj++;
                            }
                            right = right.Substring(0, index) + "[" + right.Substring(index, numLength) + "]" + right.Substring(index+numLength);
                        }

                        //Handle numbers
                        else if(leftVal >= 0 && leftVal < 10 && rightVal >= 0 && rightVal < 10){
                            //Handle multiple digit numbers
                            o++;
                            while(left[o] - '0' >= 0 && left[o] - '0' < 10){
                                leftVal = leftVal*10 + left[o] - '0';
                                o++;
                            }
                            //Adjust once back
                            o--;
                            index++;
                            while(right[index] - '0' >= 0 && right[index] - '0' < 10){
                                rightVal = rightVal*10 + right[index] - '0';
                                index++;
                            }
                            index--;
                            if(leftVal>rightVal){
                                correctOrder = false;
                                o = left.Length;
                            }
                            else if(rightVal>leftVal){
                                o = left.Length;
                            }

                        }
                        index++;
                    }
                    if(!correctOrder){
                        string lineStorage = lines[i];
                        lines[i] = lines[i+1];
                        lines[i+1] = lineStorage;
                    }
                }
            }
            int decoder1 = 0;
            int decoder2 = 0;
            for(int i = 0; i < lines.Length; i++){
                if(lines[i] == "[[2]]"){
                    decoder1 = i+1;
                }
                else if(lines[i] == "[[6]]"){
                    decoder2 = i+1;
                }
            }
            Console.WriteLine(decoder1);
            Console.WriteLine(decoder2);
            return decoder1*decoder2;
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