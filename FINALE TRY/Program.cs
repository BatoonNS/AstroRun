using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroRun
{
    class Program                                                    
    {                                                                   
        static int NumTargets = 25;                                     
        static Int32[] TargetX = new Int32[NumTargets];
        static Int32[] TargetY = new Int32[NumTargets];
        static Int32[] OldTargetX = new Int32[NumTargets];                                 
        static Int32[] OldTargetY = new Int32[NumTargets];
        static char MeDir = ' ';
        const string mychar = "A";
        const string Alien = "@";
        static int HypSpace = 3;
        public static void Main(string[] args)
        {
           
            bool Mainreturn = true;
            while (Mainreturn)
            {
                
                int MeX = 20;
                int MeY = 28;
                int OldMeX = 0;
                int OldMeY = 0;
                int RandY;
                int RandX;
                int Time1 = 50;
                Random R = new Random();
                int alientimestamp = DateTime.Now.Millisecond;
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleKey k;
                k = ConsoleKey.Escape;
                Console.CursorVisible = false;
                

                for (int I = 0; I < NumTargets; I++)
                {
                    RandY = R.Next(1, 20);
                    TargetY[I] = RandY;
                    RandX = R.Next(1, 89);
                    TargetX[I] = RandX;
                }
                Mainmenu();
                Console.Clear();
                Console.SetWindowSize(40, 30);
                Console.SetCursorPosition(MeX, MeY);
                Console.WriteLine(mychar);
                int elapsed = 0;
                Boolean Gameover = true;
                while ((Gameover) && (elapsed < 10000))                                                                 //game loop
                {
                    
                    OldMeX = MeX;
                    OldMeY = MeY;
                    for (int i = 0; i < NumTargets; i++)
                    {
                        OldTargetX[i] = TargetX[i];
                        OldTargetY[i] = TargetY[i];
                    }
                    k = getkeystroke(MeX, MeY);


                    MoveMe(k, ref MeX, ref MeY, ref Gameover);

                    if (wait(Time1, ref alientimestamp))
                    MoveTargets(ref TargetX, ref TargetY);

                    Mydraw(ref k, mychar, ref MeX, ref MeY, ref OldMeX, ref OldMeY, Alien, ref TargetX, ref TargetY, ref Gameover);
                }
                Console.SetWindowSize(120, 20);
            }
        }
        static void Mainmenu()
        {
            bool backTop = true;
            while (backTop)
            {
                Console.Clear();
                Console.WriteLine("          *           *           *                                    *                 *     *       *        *     ");
                Console.WriteLine("                 *              *       A  S  T  R  O     R  U  N   *             *               *        *          ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("                                          << M A I N M E N U >>");
                Console.WriteLine();
                Console.WriteLine("                                            1. >STARTGAME");
                Console.WriteLine("                                            2. >INSTRUCTIONS");
                Console.WriteLine("                                            3. >EXIT");
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                String TempChoice;
                int MenuChoice;
                TempChoice = Console.ReadLine();
                MenuChoice = Convert.ToInt32(TempChoice);
                Console.Clear();

                int menu = MenuChoice;
                switch (menu)
                {
                    case 1:
                        Console.WriteLine("          *           *           *                                    *                 *     *       *        *     ");
                        Console.WriteLine("                 *              *       A  S  T  R  O     R  U  N   *             *               *        *          ");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("                                               1. STARTGAME");
                        Console.WriteLine("                                         Press enter to start game");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        backTop = false;
                        break;
                    case 2:
                        Console.WriteLine("          *           *           *                                    *                 *     *       *        *     ");
                        Console.WriteLine("                 *              *       A  S  T  R  O     R  U  N   *             *               *        *          ");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("                                           2. INSTRUCTIONS");
                        Console.WriteLine("                        - Your goal is to cross the Astro field as Quick as possible!");
                        Console.WriteLine("                  - Make your way up to the top of the screen to win, if you get hit, you DIE");
                        Console.WriteLine("                 - The flicking dark and bright atroids are dangerous, avoid them at all costs!");
                        Console.WriteLine("                                       * Spacebar to hyper space forward a bit");
                        Console.WriteLine("                                       * ArrowLeft to move left <--");
                        Console.WriteLine("                                       * ArrowRight to move right -->");
                        Console.WriteLine("                                       * ArrowUp to move Up");
                        Console.WriteLine("                                       * ArrowDown to move Down");
                        Console.WriteLine("                                       * J to Quick end game");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("Press enter to return to Mainmenu");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine("3. EXIT");
                        Console.WriteLine("Press enter to confirm exit");
                        Console.ReadLine();
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static System.ConsoleKey getkeystroke(int MeX, int MeY)                                  // get key
        {
            ConsoleKey k;
            k = ConsoleKey.NoName;

            while (k != ConsoleKey.Escape)
            {

                if (Console.KeyAvailable)
                {
                    k = Console.ReadKey(true).Key;
                    if (k == ConsoleKey.RightArrow)
                    {
                        MeDir = 'R';
                    }
                    if (k == ConsoleKey.LeftArrow)
                    {
                        MeDir = 'L';
                    }
                    if (k == ConsoleKey.DownArrow)
                    {
                        MeDir = 'D';
                    }
                    if (k == ConsoleKey.UpArrow)
                    {
                        MeDir = 'U';
                    }
                    if (k == ConsoleKey.Spacebar)
                    {
                        MeDir = 'S';
                    }
                    return k;

                }
            }
            return k;

        }


    
        static bool wait(int Time1,ref int gettimestamp)
        {
            if (Math.Abs(DateTime.Now.Millisecond - gettimestamp) < Time1)
            {
                return false;
            }
            else
            {               
                return true;
            }
        }
        


        static void MoveMe(ConsoleKey k, ref int MeX, ref int MeY, ref bool Gameover)                                  //me move
        {
            if(MeDir == 'U')
            {
                MeY--;
            }
            if (MeDir == 'D')
            {
                MeY++;
            }
            if (MeDir == 'L')
            {
                MeX--;
            }
            if (MeDir == 'R')
            {
                MeX++;
            }
            if (MeDir == 'S')
            {
                MeY = MeY - HypSpace;
            }
                       
            if (MeX > Console.WindowWidth)
            {
                MeX = 0;
            }
            if (MeX < 0)
            {
                MeX = Console.WindowWidth;
            }
            if (MeY > Console.WindowHeight)
            {
                
                MeY = Console.WindowHeight;
            }
            if (MeY < 0)
            {
                Console.Clear();
                Console.SetWindowSize(120, 20);
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("                                                Y O U                                                                 ");
                Console.WriteLine("                                                W O N                                                                 ");
                Console.WriteLine("                                                                                                                      ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("Press enter to return to Mainmenu");
                Console.ReadLine();
                Gameover = false;
                MeY = Console.WindowHeight;
            }
            if (k == ConsoleKey.J)
            {
                Gameover = false;
            }
        }

        static void MoveTargets(ref int[] TargetX, ref int[] TargetY)                                                     // target move
        {


            for (int f = 0; f < NumTargets; f++)
            {
                if (TargetY[f] > 20)
                {
                    TargetX[f]++;
                }
                if (TargetY[f] > 13)
                {
                    TargetX[f]++;
                }
                if (TargetY[f] < 6)
                {
                    TargetX[f]--;
                }
                if (TargetY[f] <= 13)
                {
                    TargetX[f]--;
                }
                if (TargetX[f] > Console.WindowWidth)
                {
                    TargetX[f] = 0;
                }
                if (TargetX[f] < 0)
                {
                    TargetX[f] = Console.WindowWidth;
                }
                if (TargetY[f] < 0)
                {
                    TargetY[f] = Console.WindowHeight;
                }
                if (TargetY[f] > Console.WindowHeight)
                {
                    TargetY[f] = 0;
                }

            }
            


        }                                                                                                                   //mydraw
        static void Mydraw(ref ConsoleKey k, string mychar, ref int MeX, ref int MeY, ref int OldMeX, ref int OldMeY, string Alien, ref int[] TargetX, ref int[] TargetY, ref bool Gameover)
        {

            for (int i = 0; i < NumTargets; i++)
            {
                Console.SetCursorPosition(TargetX[i], TargetY[i]);
                Console.Write(Alien);
                Console.SetCursorPosition(OldTargetX[i], OldTargetY[i]);
                Console.Write(" ");
            }
            if (!(OldMeX == MeX && OldMeY == MeY))
            {

                Console.SetCursorPosition(MeX, MeY);
                Console.Write(mychar);
                Console.SetCursorPosition(OldMeX, OldMeY);
                Console.Write(" ");
            }
            for (int I = 0; I < NumTargets; I++)
            {
                if (TargetX[I] == MeX && TargetY[I] == MeY)
                {
                   
                    
                    Gameover = false;
                    Console.Clear();
                    Console.SetWindowSize(120, 20);
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("                                               G A M E                                                                ");
                    Console.WriteLine("                                               O V E R                                                                ");
                    Console.WriteLine("                                                                                                                      ");
                    Console.WriteLine("----------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("Press enter to return to Mainmenu");
                    Console.ReadLine();
                    

                }
                else
                {

                }
            }

        }


    }

}
