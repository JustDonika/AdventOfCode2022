using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            //Load in file
            int[] storage = new int[lines.Length];
            for(int i = 0; i < lines.Length; i++){
                storage[i] = Int32.Parse(lines[i]);
            }
            //Move in order
            int[] indices = (int[]) Enumerable.Range(0, lines.Length).ToArray();
            int[] newArr = new int[lines.Length];
            newArr = (int[]) storage.Clone();
            for(int i = 0; i < lines.Length; i++){
                int index = Array.IndexOf(indices, i);
                if(storage[index]<0){
                    for(int o = 0; o < Math.Abs(newArr[index]); o++){
                        //Find indices to swap between, index and swapIndex
                        int swapIndex = index - 1;
                        int indSwap = 0;
                        int swapArr = 0;
                        if(swapIndex < 0){
                            swapIndex = lines.Length-2;
                            swapArr = newArr[0];
                            indSwap = indices[0];
                            for(int u = 0; u < lines.Length-1; u++){
                                newArr[u] = newArr[u+1];
                                indices[u] = indices[u+1];
                            }
                            newArr[lines.Length-2] = swapArr;
                            indices[lines.Length-2] = indSwap;
                        }
                        else{
                            swapArr = newArr[swapIndex];
                            newArr[swapIndex] = newArr[index];
                            newArr[index] = swapArr;
                            //Swap indices
                            indSwap = indices[swapIndex];
                            indices[swapIndex] = indices[index];
                            indices[index] = indSwap;
                        }
                        //Reset index
                        index = swapIndex;
                    }
                }
                else{
                    for(int o = 0; o < newArr[index]; o++){
                        //Find indices to swap between, index and swapIndex
                        int swapIndex = index + 1;
                        if(swapIndex == lines.Length){
                            swapIndex = 0;
                        }
                        //Swap actual array points
                        int swapArr = newArr[swapIndex];
                        newArr[swapIndex] = newArr[index];
                        newArr[index] = swapArr;
                        //Swap indices
                        int indSwap = indices[swapIndex];
                        indices[swapIndex] = indices[index];
                        indices[index] = indSwap;
                        index = swapIndex;
                    }
                }
                storage = (int[]) newArr.Clone();
            }
            int val = Array.IndexOf(storage, 0);
            Console.WriteLine((val+1000) % lines.Length);
            Console.WriteLine((val+2000) % lines.Length);
            Console.WriteLine((val+3000) % lines.Length);
            Console.WriteLine((storage[(val+1000) % lines.Length]));
            Console.WriteLine((storage[(val+2000) % lines.Length]));
            Console.WriteLine((storage[(val+3000) % lines.Length]));

            return storage[(val+1000) % lines.Length]+storage[(val+2000) % lines.Length]+storage[(val+3000) % lines.Length];
        }

        public long partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("test.txt");
            //Load in file
            long[] storage = new long[lines.Length];
            for(int i = 0; i < lines.Length; i++){
                storage[i] = ((long) Int32.Parse(lines[i])*811589153);
            }
            //Move in order
            int[] indices = (int[]) Enumerable.Range(0, lines.Length).ToArray();
            long[] newArr = new long[lines.Length];
            newArr = (long[]) storage.Clone();
            for(int iter = 0; iter < 1; iter++){
                for(int i = 0; i < lines.Length; i++){
                    int index = Array.IndexOf(indices, i);
                    int increment = (int) (newArr[index] % (lines.Length - 1));
                    Console.WriteLine();
                    Console.WriteLine(i);
                    Console.WriteLine(index);
                    Console.WriteLine(increment);
                    Console.WriteLine();
                    Console.WriteLine("Node: "+i);
                    Console.WriteLine(storage[0]);
                    Console.WriteLine(storage[1]);
                    Console.WriteLine(storage[2]);
                    Console.WriteLine(storage[3]);
                    Console.WriteLine(storage[4]);
                    Console.WriteLine(storage[5]);
                    Console.WriteLine(storage[6]);
                    Console.WriteLine();
                    if(increment<0){
                        for(int o = 0; o < Math.Abs(increment); o++){
                            //Find indices to swap between, index and swapIndex
                            int swapIndex = index - 1;
                            int indSwap = 0;
                            long swapArr = 0;
                            if(swapIndex < 0){
                                swapIndex = lines.Length-2;
                                swapArr = newArr[0];
                                indSwap = indices[0];
                                for(int u = 0; u < lines.Length-1; u++){
                                    newArr[u] = newArr[u+1];
                                    indices[u] = indices[u+1];
                                }
                                newArr[lines.Length-2] = swapArr;
                                indices[lines.Length-2] = indSwap;
                            }
                            else{
                                swapArr = newArr[swapIndex];
                                newArr[swapIndex] = newArr[index];
                                newArr[index] = swapArr;
                                //Swap indices
                                indSwap = indices[swapIndex];
                                indices[swapIndex] = indices[index];
                                indices[index] = indSwap;
                            }
                            //Reset index
                            index = swapIndex;
                        }
                    }
                    else{
                        for(int o = 0; o < increment; o++){
                            //Find indices to swap between, index and swapIndex
                            int swapIndex = index + 1;
                            if(swapIndex == lines.Length){
                                swapIndex = 0;
                            }
                            //Swap actual array points
                            long swapArr = newArr[swapIndex];
                            newArr[swapIndex] = newArr[index];
                            newArr[index] = swapArr;
                            //Swap indices
                            int indSwap = indices[swapIndex];
                            indices[swapIndex] = indices[index];
                            indices[index] = indSwap;
                            index = swapIndex;
                        }
                    }
                    storage = (long[]) newArr.Clone();
                }
            }
            int val = Array.IndexOf(storage, 0);

            Console.WriteLine(storage[0]);
            Console.WriteLine(storage[1]);
            Console.WriteLine(storage[2]);    
            Console.WriteLine(storage[3]);
            Console.WriteLine(storage[4]);
            Console.WriteLine(storage[5]);
            Console.WriteLine(storage[6]);
            
            Console.WriteLine((val+1000) % lines.Length);
            Console.WriteLine((val+2000) % lines.Length);
            Console.WriteLine((val+3000) % lines.Length);
            Console.WriteLine((storage[(val+1000) % lines.Length]));
            Console.WriteLine((storage[(val+2000) % lines.Length]));
            Console.WriteLine((storage[(val+3000) % lines.Length]));

            return storage[(val+1000) % lines.Length]+storage[(val+2000) % lines.Length]+storage[(val+3000) % lines.Length];
        }

        static void Main(string[] args)
        {
            Solution s = new Solution();
            int answer = s.partOne();
            Console.WriteLine(answer);
            long answer2 = s.partTwo();
            Console.WriteLine(answer2);
        }
    }
}