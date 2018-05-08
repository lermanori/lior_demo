using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_UI
{
    public class Output
    {
        public static void printPoint(Checkers_LogicAndDataSection.Point pt)
        {
            System.Console.SetCursorPosition(pt.XCoord, pt.YCooord);
            Console.Write("C");
        }

        public static void printSoldier(Checkers_LogicAndDataSection.GameBoard.Soldier s)
        {
            switch (s.Team)
            {

                case Checkers_LogicAndDataSection.ePlayerOptions.Player1:
                    Console.SetCursorPosition(s.Position.XCoord, s.Position.YCooord);
                    if (s.Rank == Checkers_LogicAndDataSection.GameBoard.eSoldierRanks.Regular)
                    {
                        Console.Write('X');
                    }
                    else
                    {
                        Console.Write('K');
                    }
                    break;
                case Checkers_LogicAndDataSection.ePlayerOptions.Player2:
                case Checkers_LogicAndDataSection.ePlayerOptions.ComputerPlayer:
                    Console.SetCursorPosition(s.Position.XCoord, s.Position.YCooord);
                    if (s.Rank == Checkers_LogicAndDataSection.GameBoard.eSoldierRanks.Regular)
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write('u');
                    }
                    break;

            }

        }
        public static void printMatrix(Checkers_LogicAndDataSection.GameBoard gb)
        {
            Checkers_LogicAndDataSection.GameBoard.Soldier localSoldier;
            Checkers_LogicAndDataSection.Point localPoint = new Checkers_LogicAndDataSection.Point();
            for (localPoint.YCooord = 0; localPoint.YCooord < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.YCooord++)
            {
                for (localPoint.XCoord = 0; localPoint.XCoord < (int)Checkers_LogicAndDataSection.SessionData.m_BoardSize; localPoint.XCoord++)
                {
                    localSoldier = gb.GetSoldierFromMatrix(localPoint);
                    printSoldier(localSoldier);
                }

            }


        }

        public static void InputException()
        {
            Console.WriteLine("wrong input! try again");
        }
    }
}
