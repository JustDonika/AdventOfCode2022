using System;

namespace Csharp{
public class Solution {
    public int largestCalories() {
        string[] lines = System.IO.File.ReadAllLines("input.txt");
        int largestIndividual = 0;
        int currentIndividual = 0;
        for(int i = 0; i < lines.Length; i++){
            if(String.IsNullOrEmpty(lines[i])){
                currentIndividual = 0;
            }
            else{
                currentIndividual += Int32.Parse(lines[i]);
            }
            if(currentIndividual > largestIndividual){
                largestIndividual = currentIndividual;
            }
        }
        return largestIndividual;
    }

    public int topThree(){
        string[] lines = System.IO.File.ReadAllLines("input.txt");
        int[] allIndividuals = new int[3];
        int currentIndividual = 0;
        for(int i = 0; i < lines.Length; i++){
            if(String.IsNullOrEmpty(lines[i])){
                if(currentIndividual > allIndividuals[0]){
                    allIndividuals[2] = allIndividuals[1];
                    allIndividuals[1] = allIndividuals [0];
                    allIndividuals[0] = currentIndividual;
                }
                else if(currentIndividual > allIndividuals[1]){
                    allIndividuals[2] = allIndividuals[1];
                    allIndividuals[1] = currentIndividual;
                }
                else if(currentIndividual > allIndividuals[2]){
                    allIndividuals[2] = currentIndividual;
                }
                currentIndividual = 0;
            }
            else{
                currentIndividual += Int32.Parse(lines[i]);
            }
        }
        return allIndividuals[0]+allIndividuals[1]+allIndividuals[2];
    }

    static void Main(string[] args)
    {
        Solution s = new Solution();
        int calories = s.largestCalories();
        Console.WriteLine(calories);
        int threeCalories = s.topThree();
        Console.WriteLine(threeCalories);
    }
}
}