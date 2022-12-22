using System;

namespace Csharp
{
    public class Solution
    {
        public int partOne()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            // ' ' is wraparound, '#' is wall, '.' is free space.
            int currX = lines[0].IndexOf('.');
            int currY = 0;
            int facing = 0;
            string instructions = lines[lines.Length-1];
            char[] pivots = new char[(int)instructions.Length/2];
            int index = 0;
            for(int i = 0; i < instructions.Length; i++){
                if(instructions[i] - '0' >= 10 || instructions[i] - '0' < 0){
                    pivots[index] = instructions[i];
                    index++;
                }
            }
            char[] delimiterChars = {'R', 'L'};
            string[] nums = instructions.Split(delimiterChars);
            //Iterate through commands
            for(int i = 0; i < nums.Length; i++){
                int move = Int32.Parse(nums[i]);
                for(int u = 0; u < move; u++){
                    if(facing % 2 == 0){
                        int newX = currX + 1 - facing;
                        //Wrap around to right side of board if outside index range
                        if(newX < 0){
                            newX = lines[currY].Length-1;
                        }
                        else if(newX >= lines[currY].Length){
                            newX = 0;
                        }
                        //If space blank, find wrap around point.
                        if(lines[currY][newX] == ' '){
                            //Moving right
                            if(newX == 0 || currX < newX){
                                newX = lines[currY].IndexOf('.');
                                if(lines[currY][newX-1] == '#'){
                                    u = move;
                                    break;
                                }
                                else{
                                    currX = newX;
                                }
                            }
                            //Moving left
                            if(newX == lines[currY].Length-1 || currX > newX){
                                newX = lines[currY].LastIndexOf('.');
                                if(newX < lines[currY].Length - 1 && lines[currY][newX+1] == '#'){
                                    u = move;
                                    break;
                                }
                                else{
                                    currX = newX;
                                }
                            }
                        }
                        //If space #, stop
                        else if(lines[currY][newX] == '#'){
                            u = move; 
                            break;
                        }
                        //newX locale is .
                        else{
                            currX = newX;
                        }
                    }
                    else if(facing % 2 == 1){
                        int newY = currY - 1;
                        if(facing == 1){
                            newY+=2;
                        }
                        if(newY < 0){
                            newY = lines.Length-3;
                        }
                        if(newY >= lines.Length - 2){
                            newY = 0;
                        }
                        //Wrap around
                        if(lines[newY].Length <= currX || lines[newY][currX] == ' '){
                            //Moving down
                            if(newY == 0 || (currY < newY && newY!=lines.Length-3)){
                                while(lines[newY].Length <= currX || lines[newY][currX] == ' '){
                                    newY++;
                                    if(newY >= lines.Length - 2){
                                        newY = 0;
                                    }                        
                                }
                                if(lines[newY][currX] == '#'){
                                    u = move;
                                    break;
                                }
                                else{
                                    currY = newY;
                                }
                            }
                            //Moving up
                            if(newY == lines.Length-3 || currY > newY){
                                while(lines[newY].Length <= currX || lines[newY][currX] == ' '){
                                    newY--;
                                    if(newY<0){
                                        newY = lines.Length-3;
                                    }
                                }
                                if(lines[newY][currX] == '#'){
                                    u = move;
                                    break;
                                }
                                else{
                                    currY = newY;
                                }
                            }
                        }
                        //If space #, stop
                        else if(lines[newY][currX] == '#'){
                            u = move; 
                            break;
                        }
                        //newY locale is .
                        else{
                            currY = newY;
                        }
                    }
                }
                //Handle rotation
                if(pivots[i] == 'R'){
                    facing++;
                    facing = facing % 4;
                }
                else if (pivots[i] == 'L'){
                    facing --;
                    facing = (facing+4) % 4;
                }
            }
            Console.WriteLine(currX+1);
            Console.WriteLine(currY+1);
            Console.WriteLine(facing);
            return 1000 * (currY+1) + 4 * (currX+1) + facing;
        }


        public int partTwo()
        {
            string[] lines = System.IO.File.ReadAllLines("input.txt");
            // ' ' is wraparound, '#' is wall, '.' is free space.
            int currX = lines[0].IndexOf('.');
            int currY = 0;
            int facing = 0;
            string instructions = lines[lines.Length-1];
            char[] pivots = new char[(int)instructions.Length/2];
            int index = 0;
            for(int i = 0; i < instructions.Length; i++){
                if(instructions[i] - '0' >= 10 || instructions[i] - '0' < 0){
                    pivots[index] = instructions[i];
                    index++;
                }
            }
            char[] delimiterChars = {'R', 'L'};
            string[] nums = instructions.Split(delimiterChars);
            //Find faces
            List<Int32[]> faces = new List<int[]>();
            int faceMinY = 0;
            for(int i = 0; i < lines.Length-1; i++){
                //Find face boundaries
                int leftFace = lines[faceMinY].IndexOf('.');
                if(lines[faceMinY].IndexOf('#') != -1 && lines[faceMinY].IndexOf('#') < leftFace){
                    leftFace = lines[faceMinY].IndexOf('#');
                }
                int rightFace = Math.Max(lines[faceMinY].LastIndexOf('.'), lines[faceMinY].LastIndexOf('#'));
                //Check if next boundary changes this
                int leftCurr = lines[i].IndexOf('.');
                if(lines[i].IndexOf('#') != -1 && lines[i].IndexOf('#') < leftCurr){
                    leftCurr = lines[i].IndexOf('#');
                }
                int rightCurr = Math.Max(lines[i].LastIndexOf('.'), lines[i].LastIndexOf('#'));
                if(leftFace-rightFace != leftCurr - rightCurr){
                    //Another face to the left
                    if(i<lines.Length-2){
                        //Top left bottom right
                        faces.Add(new int[]{faceMinY, i, Math.Max(leftFace, leftCurr), Math.Min(rightFace, rightCurr)});
                        if(leftFace < leftCurr){
                            faces.Add(new int[]{faceMinY, i, leftFace, leftCurr});
                        }
                        if(rightFace > rightCurr){
                            faces.Add(new int[]{faceMinY, i, rightCurr, rightFace});
                        }
                    }
                    else{
                        leftCurr = faces.ElementAt(faces.Count-1)[2];
                        rightCurr = faces.ElementAt(faces.Count-1)[3];
                        if(rightCurr == leftFace){
                            leftCurr = faces.ElementAt(faces.Count-2)[2];
                            rightCurr = faces.ElementAt(faces.Count-2)[3];
                        }
                        //Top left bottom right
                        faces.Add(new int[]{faceMinY, i, Math.Max(leftFace, leftCurr), Math.Min(rightFace, rightCurr)});
                        if(leftFace < leftCurr){
                            faces.Add(new int[]{faceMinY, i, leftFace, leftCurr});
                        }
                        if(rightFace > rightCurr){
                            faces.Add(new int[]{faceMinY, i, rightCurr, rightFace});
                        }
                    }
                    faceMinY = i;
                }
            }
            int cubeLen = faces.ElementAt(0)[1]-faces.ElementAt(0)[0];
            //If there are too few faces, find the face that's too long.
            if(faces.Count < 8){
                for(int i = 0; i < faces.Count; i++){
                    faces.ElementAt(i)[0] = (int) Math.Round((double) faces.ElementAt(i)[0] / cubeLen) * cubeLen;
                    faces.ElementAt(i)[1] = (int) Math.Round((double) faces.ElementAt(i)[1] / cubeLen) * cubeLen;
                    faces.ElementAt(i)[2] = (int) Math.Round((double) faces.ElementAt(i)[2] / cubeLen) * cubeLen;
                    faces.ElementAt(i)[3] = (int) Math.Round((double) faces.ElementAt(i)[3] / cubeLen) * cubeLen;
                    Console.WriteLine("Coords for face "+i+": "+faces.ElementAt(i)[0]+", "+faces.ElementAt(i)[1]+" - "+faces.ElementAt(i)[2]+", "+faces.ElementAt(i)[3]);
                    if(faces.ElementAt(i)[1]-faces.ElementAt(i)[0] != cubeLen && faces.ElementAt(i)[1]-faces.ElementAt(i)[0] != 0){
                        faces.Add(new int[]{faces.ElementAt(i)[0], faces.ElementAt(i)[0]+cubeLen, faces.ElementAt(i)[2], faces.ElementAt(i)[3]});
                        faces.Add(new int[]{faces.ElementAt(i)[0]+cubeLen, faces.ElementAt(i)[1], faces.ElementAt(i)[2], faces.ElementAt(i)[3]});
                        faces.RemoveAt(i);
                        i--;
                    }
                    else if(faces.ElementAt(i)[3]-faces.ElementAt(i)[2] != cubeLen && faces.ElementAt(i)[3]-faces.ElementAt(i)[2] != 0){
                        faces.Add(new int[]{faces.ElementAt(i)[0], faces.ElementAt(i)[1], faces.ElementAt(i)[2], faces.ElementAt(i)[2]+cubeLen});
                        faces.Add(new int[]{faces.ElementAt(i)[0], faces.ElementAt(i)[1], faces.ElementAt(i)[2]+cubeLen, faces.ElementAt(i)[3]});
                        faces.RemoveAt(i);
                        i--;
                    }
                    else if(faces.ElementAt(i)[1]-faces.ElementAt(i)[0] == 0 || faces.ElementAt(i)[3]-faces.ElementAt(i)[2] == 0){
                        //Erroneously added; remove.
                        faces.RemoveAt(i);
                        i--;
                    }
                }
            }
            //Test shape hard coded
            // 0->1
            // 0->3 (left = right, bottom right 0 = top right 3)
            // 1->2
            // 1->3 (right = down)
            // 1->5
            // 2->3
            // 4->0 (up = down, top right 4 = top left 0)
            // 4->2 (down = up, bottom right 4 = bottom left 2)
            // 4->3 (left = up, top left 4 = bottom left 3)
            // 4->5 
            // 5->0 (up = right, top right 5 = bottom left 0)
            // 5->2 (down = right, bottom right 5 = top left 2)

            
            //Set this to false when running on proper input
            bool test = false;
            Dictionary<Int32, Int32> cubeDetails = new Dictionary<Int32, Int32>();
            Dictionary<Int32, Int32> whereTo = new Dictionary<Int32, Int32>();
            if(test){
                int[] entryPoint = new int[]{0, 0,  1, 1, 1, 2,  4, 4, 4, 4,  5,  5};
                int[] exitPoint  = new int[]{1, 3,  2, 3, 5, 3,  0, 2, 3, 5,  0,  2};
                int[] pivot      = new int[]{0, -2, 0, 1, 0, 0, -2, 2, 1, 0, -3, -1};
                for(int i = 0; i < 12; i++){
                    cubeDetails[entryPoint[i]*6+exitPoint[i]] = pivot[i];
                    cubeDetails[exitPoint[i]*6+entryPoint[i]] = -pivot[i];
                }
                //Right = 0, down = 1, left = 2, up = 3 
                int[] allOrigins = new int[]{0, 0, 1, 1, 1, 2, 4, 4, 4, 4, 5, 5, 1, 3, 2, 3, 5, 3, 0, 2, 3, 5, 0, 2};
                int[] direction  = new int[]{1, 0, 1, 0, 2, 0, 3, 1, 2, 0, 3, 1, 3, 0, 3, 2, 0, 2, 3, 1, 1, 2, 2, 2};
                int[] allExits   = new int[]{1, 3, 2, 3, 5, 3, 0, 2, 3, 5, 0, 2, 0, 0, 1, 1, 1, 2, 4, 4, 4, 4, 5, 5};
                //How to know which side leads to which: origin face * 5 + direction = new face
                for(int i = 0; i < allOrigins.Length; i++){
                    whereTo[allOrigins[i]*5 + direction[i]] = allExits[i];
                }
            }
            //Actual input
            // 0 -> 1
            // 0 -> 2
            // 0 -> 3 (left to right)
            // 0 -> 5 (up to right)
            // 1 -> 2 (down to left)
            // 1 -> 4 (right to left)
            // 1 -> 5 (up to up)
            // 2 -> 3 (left to down)
            // 2 -> 4 
            // 3 -> 4
            // 3 -> 5
            // 4 -> 5 (down to left)
            else{
                int[] entryPoint = new int[]{0, 0,  0,  0, 1, 1, 1,  2, 2, 3, 3, 4};
                int[] exitPoint  = new int[]{1, 2,  3,  5, 2, 4, 5,  3, 4, 4, 5, 5};
                int[] pivot      = new int[]{0, 0, -2, -3, 1, 2, 0, -1, 0, 0, 0, 1};
                for(int i = 0; i < 12; i++){
                    cubeDetails[entryPoint[i]*6+exitPoint[i]] = pivot[i];
                    cubeDetails[exitPoint[i]*6+entryPoint[i]] = -pivot[i];
                }
                //Right = 0, down = 1, left = 2, up = 3 
                int[] allOrigins = new int[]{0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 4, 1, 2, 3, 5, 2, 4, 5, 3, 4, 4, 5, 5};
                int[] direction  = new int[]{0, 1, 2, 3, 1, 0, 3, 2, 1, 0, 1, 1, 2, 3, 2, 2, 0, 0, 1, 3, 3, 2, 3, 0};
                int[] allExits   = new int[]{1, 2, 3, 5, 2, 4, 5, 3, 4, 4, 5, 5, 0, 0, 0, 0, 1, 1, 1, 2, 2, 3, 3, 4};
                //How to know which side leads to which: origin face * 5 + direction = new face
                for(int i = 0; i < allOrigins.Length; i++){
                    whereTo[allOrigins[i]*5 + direction[i]] = allExits[i];
                }
            }

            // //Determine which side is which, and what angle rotation the face is relative to the other
            // List<Int32[]> vertices = new List<int[]>();
            // for(int i = 0; i < faces.Count; i++){
            //     for(int j = i+1; j < faces.Count; j++){
            //         int[] face1 = faces[i];
            //         int[] face2 = faces[j];
            //         if((face1[1] == face2[0] && face1[2] == face2[2] && face1[3] == face2[3]) 
            //         ||(face1[0] == face2[1] && face1[2] == face2[2] && face1[3] == face2[3])
            //         ||(face1[2] == face2[3] && face1[1] == face2[1] && face1[0] == face2[0])
            //         ||(face1[3] == face2[2] && face1[1] == face2[1] && face1[0] == face2[0])){
            //             vertices.Add(new int[]{i, j});
            //         }
            //     }
            // }

            // for(int i = 0; i < vertices.Count; i++){
            //     Console.WriteLine(vertices.ElementAt(i)[0]);
            //     Console.WriteLine(vertices.ElementAt(i)[1]);
            // }

            //Iterate through commands
            int currFace = 0;
            for(int i = 0; i < nums.Length; i++){
                int move = Int32.Parse(nums[i]);
                for(int u = 0; u < move; u++){
                    for(int o = 0; o < faces.Count; o++){
                        int[] thisFace = faces[o];
                        if(thisFace[0] <= currY && thisFace[1]>currY && thisFace[2]<=currX && thisFace[3]>currX){
                            currFace = o;
                        }            
                    }
                    if(facing % 2 == 0){
                        int newX = currX + 1 - facing;
                        int specY = currY;
                        //If space blank, find wrap around point.
                        if(newX >= lines[currY].Length || newX < 0 || lines[currY][newX] == ' '){
                            int newFace = whereTo[currFace*5+facing];
                            int newFacing = (cubeDetails[currFace*6+newFace]+facing+4) % 4;
                            //Know direction to have player facing, and which face on; need to determine which square they continue from. 
                            //When moving from right to left or vice versa, y remains an equivalent distance from currface[0] BUT DOES NOT necessarily stay on the same y axis.
                            if(newFacing % 2 == 0){
                                specY = cubeLen-(currY+1-faces.ElementAt(currFace)[0]) + faces.ElementAt(newFace)[0];
                                Console.WriteLine(specY);
                                if(newFacing == 0){
                                    newX = faces.ElementAt(newFace)[2];
                                }
                                else if(newFacing == 2){
                                    newX = faces.ElementAt(newFace)[3]-1;
                                }
                            }
                            //What if moving from left/right to up/down?
                            else if(newFacing % 2 == 1){
                                //relative y to currface[1] becomes x relative to currFace2
                                newX = cubeLen-(faces.ElementAt(currFace)[1] - currY) + faces.ElementAt(newFace)[2];
                                // Console.WriteLine(currY);
                                // Console.WriteLine(faces.ElementAt(currFace)[0]);
                                // Console.WriteLine(faces.ElementAt(newFace)[2]);
                                //Adjust y
                                if(newFacing == 1){
                                    specY = faces.ElementAt(newFace)[0];
                                }
                                else if(newFacing == 3){
                                    specY =  (int) Math.Round((double) faces.ElementAt(newFace)[1] / cubeLen) * cubeLen-1;
                                }
                            }
                            Console.WriteLine("The other one :O Also, x is "+newX+", y is "+specY);

                            if(lines[specY][newX] == '#'){
                                u = move; 
                                break;
                            }
                            else{
                                currY = specY;
                                currX = newX;
                                facing = newFacing;
                            }
                        }
                        //If space #, stop
                        else if(lines[currY][newX] == '#'){
                            u = move; 
                            break;
                        }
                        //newX locale is .
                        else{
                            currX = newX;
                        }
                    }
                    else if(facing % 2 == 1){
                        int newY = currY - 1;
                        int specX = currX;
                        if(facing == 1){
                            newY+=2;
                        }
                        //Wrap around
                        if(newY >= lines.Length || newY < 0 || lines[newY].Length <= currX || lines[newY][currX] == ' '){
                            int newFace = whereTo[currFace*5+facing];
                            int newFacing = (cubeDetails[currFace*6+newFace]+facing+4) % 4;
                            //Know direction to have player facing, and which face on; need to determine which square they continue from.
                            //specX doesn't need to change if straight down or up
                            //When moving from up to down or vice versa, x remains an equivalent distance from currface[2] BUT DOES NOT necessarily stay on the same x axis.
                            specX = (currX-faces.ElementAt(currFace)[2]) + faces.ElementAt(newFace)[2];
                            if(newFacing % 2 == 1){
                                if(newFacing == 1){
                                    newY = faces.ElementAt(newFace)[0];
                                }
                                else if(newFacing == 3){
                                    newY = faces.ElementAt(newFace)[1]-1;
                                }
                            }
                            //What if moving from up/down to left/right?
                            else if(newFacing % 2 == 0){
                                //relative x to currface[2] becomes y relative to currFace[1]
                                newY = currX-faces.ElementAt(currFace)[2] + faces.ElementAt(newFace)[0];
                                //Adjust x
                                if(newFacing == 0){
                                    specX = faces.ElementAt(newFace)[2];
                                }
                                else if(newFacing == 2){
                                    specX = faces.ElementAt(newFace)[3]-1;
                                }
                            }
                            Console.WriteLine("Tis this one! Also, x is "+specX+", y is "+newY);
                            if(lines[newY][specX] == '#'){
                                u = move; 
                                break;
                            }
                            else{
                                currY = newY;
                                currX = specX;
                                facing = newFacing;
                            }
                        }
                        //If space #, stop
                        else if(lines[newY][currX] == '#'){
                            u = move; 
                            break;
                        }
                        //newY locale is .
                        else{
                            currY = newY;
                        }
                    }
                    Console.WriteLine("("+currX+", "+currY+")");
                }
                //Handle rotation
                if(pivots[i] == 'R'){
                    facing++;
                    facing = facing % 4;
                }
                else if (pivots[i] == 'L'){
                    facing --;
                    facing = (facing+4) % 4;
                }
            }
            Console.WriteLine(currX+1);
            Console.WriteLine(currY+1);
            Console.WriteLine(facing);
            return 1000 * (currY+1) + 4 * (currX+1) + facing;
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