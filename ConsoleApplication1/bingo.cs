using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class bingo
    {
        
        public static bool CheckLine(bool[,] a)
        {
            int i = 0, j = 0;
            bool found = false;
            bool exitLine;
            while (i < 6 && found == false)
            {
                j = 0;
                exitLine = false;
                while (j < 6 && exitLine == false)
                {
                    if (a[i, j] == false)
                    {
                        exitLine = true;
                    }
                    // increment col index
                    j++;
                }
                if (exitLine == false) // found 6 Trues in a row
                {
                    found = true;
                }
                // increment row index
                i++;
            }
            return found;
        }

        public static bool CheckColum(bool[,] a)
        {
            int i = 0, j = 0;
            bool found = false;
            bool exitLine;
            while (j < 6 && found == false)
            {
                i = 0;
                exitLine = false;
                while (i < 6 && exitLine == false)
                {
                    if (a[i, j] == false)
                    {
                        exitLine = true;
                    }
                    // increment row index
                    i++;
                }
                if (exitLine == false) // found 6 Trues in a col
                {
                    found = true;
                }
                // increment col index
                j++;
            }
            return found;
        }

        public static bool CheckMinorCross(bool[,] a)
        {
            int i = 0;
            bool found = true;
            while (i < 6 && found == true)
            {
                if (a[i, 6 - (i + 1)] == false)
                {
                    found = false;
                }
                // increment cross index
                i++;
            }

            return found;
        }

        public static bool CheckMajorCross(bool[,] a)
        {
            int i = 0;
            bool found = true;
            while (i < 6 && found == true)
            {
                if (a[i, i] == false)
                {
                    found = false;
                }
                // increment cross index
                i++;
            }

            return found;
        }

        public static void MakeCardNumberArray(int[,] a, int seed)
        {

            int[] rangeArray = new int[100];
            Random rand = new Random(seed);

            int range = 99;
            int rangeIndex;
            int tmp;
            // make range index array
            for (int i = 0; i < 100; i++)
                rangeArray[i] = i + 1;


            // Fill card Array by Lottery
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    // Lottery
                    rangeIndex = rand.Next(0, range);
                    // Place Value
                    a[i, j] = rangeArray[rangeIndex];

                    // Sweap Array Cell
                    tmp = rangeArray[range - 1];
                    rangeArray[range - 1] = rangeArray[rangeIndex];
                    rangeArray[rangeIndex] = tmp;

                    // decrese range index 
                    range--;
                }
            }
        }

        public static void PlaceNumberInPlayerCard(int[,] numArray, bool[,] boolArray, int number)
        {
            int i = 0, j = 0;
            bool found = false;
            while (i < 6 && found == false)
            {
                j = 0;
                while (j < 6 && found == false)
                {
                    if (numArray[i, j] == number)
                    {
                        boolArray[i, j] = true;
                        found = true;
                    }
                    // increment row index
                    j++;
                }
                // increment col index
                i++;
            }
        }

        public static void printPlayerCard(int[,] numArray, bool[,] boolArray, int numberPlayer)
        {
            string line;

            Console.WriteLine();
            Console.WriteLine("Player Number - " + numberPlayer + " card:");
            Console.WriteLine();

            for (int i = 0; i < 6; i++)
            {
                line = "";
                for (int j = 0; j < 6; j++) // Craete line for printing
                {
                    if (boolArray[i, j] == true) // if number selected.
                    {
                        line = line + "**" + "\t";
                    }
                    else
                    {
                        line = line + numArray[i, j] + "\t";
                    }
                }
                Console.WriteLine(line);
            }
        }


        static void Main(string[] args)
        {
            //pLayers
            int[,] cardNumArray1 = new int[6, 6];
            bool[,] cardBoolArray1 = new bool[6, 6];
            int[,] cardNumArray2 = new int[6, 6];
            bool[,] cardBoolArray2 = new bool[6, 6];
            // Genrate random seeds
            Random randSeeds = new Random();
            //Lottery man
            int lotteryManSeed = randSeeds.Next();
            Random randLotteryMan = new Random(lotteryManSeed);
            int[] lotteryManArray = new int[100];
            int tmp;
            int range = 100;
            bool firstWon = false;
            bool secondWon = false;
            int numberChoosen;
            int indexChoosen;

            // make lottery man range index array
            for (int i = 1; i < 100; i++)
                lotteryManArray[i] = i + 1;

            // initialiize bool arrays
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    cardBoolArray1[i, j] = false;
                    cardBoolArray2[i, j] = false;
                }
            }

            // Make Lottery Cars for the 2 players
            int player1ManSeed = randSeeds.Next();
            MakeCardNumberArray(cardNumArray1, player1ManSeed);
            int player2Seed = randSeeds.Next();
            MakeCardNumberArray(cardNumArray2, player2Seed);

            Console.WriteLine("Initail Player Cards:");
            Console.WriteLine();

            printPlayerCard(cardNumArray1, cardBoolArray1, 1);
            printPlayerCard(cardNumArray2, cardBoolArray2, 2);
            Console.ReadKey();

            // Give players name 

            while (firstWon == false && secondWon == false)
            {
                // Lottery

                // Lottery
                indexChoosen = randLotteryMan.Next(1, range);
                numberChoosen = lotteryManArray[indexChoosen];

                Console.WriteLine();
                Console.WriteLine("Number Selected : " + numberChoosen);

                // Sweap Array Cell
                tmp = lotteryManArray[range - 1];
                lotteryManArray[range - 1] = lotteryManArray[indexChoosen];
                lotteryManArray[indexChoosen] = tmp;

                // decrese range index 
                range--;

                // Place Value in players cards
                PlaceNumberInPlayerCard(cardNumArray1, cardBoolArray1, numberChoosen);
                PlaceNumberInPlayerCard(cardNumArray2, cardBoolArray2, numberChoosen);
                // Check players cards for winner

                // first player
                if (CheckColum(cardBoolArray1) == true || CheckLine(cardBoolArray1) == true || CheckMajorCross(cardBoolArray1) == true || CheckMinorCross(cardBoolArray1) == true)
                {
                    firstWon = true;
                }

                // second palyer
                if (CheckColum(cardBoolArray2) == true || CheckLine(cardBoolArray2) == true || CheckMajorCross(cardBoolArray2) == true || CheckMinorCross(cardBoolArray2) == true)
                {
                    secondWon = true;
                }

                printPlayerCard(cardNumArray1, cardBoolArray1, 1);
                printPlayerCard(cardNumArray2, cardBoolArray2, 2);

                Console.ReadKey();

            }

            if (firstWon && secondWon)
            {
                Console.WriteLine("both players won!!!!!!!");
            }
            else if (firstWon)
            {
                Console.WriteLine("first player won!!!!!!!");
            }
            else
            {
                Console.WriteLine("second player won!!!!!!!");
            }





        }
    }
}

