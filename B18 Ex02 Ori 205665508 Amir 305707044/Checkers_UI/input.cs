using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_LogicAndDataSection;
namespace Checkers_UI
{

    public class Input
    {
        public static int i = 0;

        public static CheckersGameStep ReadAndCheckInput()
        {
            string[] inputs = { "Af>Be", "Hc>Gd", "Be>Cd", "Bc>De", "Ef>Cd", "Dc>Be", "Cf>Ad", "Gd>He", "Gf>Fe", "He>Gf", "Fg>He", "Ab>Bc", "Fe>Gd", "Gb>Hc", "Bg>Cf", "Hc>Fe", "Dg>Ef", "Fe>Dg","q" };
            bool[] validation = new bool[3];

            string i_inputFromUser = string.Empty;
            Console.Write("please enter a  legal move: ");
            //goto xy clear and that stuff


            CheckersGameStep result = new CheckersGameStep();

            Point currentPoint = new Point();
            Point NextPoint = new Point();
            bool valid = false;
            const bool quit = true;
            while (!valid)
            {
                //i_inputFromUser = Console.ReadLine();

                validation[0] = false;
                validation[1] = false;
                validation[2] = false;

                i_inputFromUser = inputs[i];



                string processedString = i_inputFromUser.Replace(" ", "");
                if (i_inputFromUser != "q")
                {

                    if (char.IsUpper(processedString[0]) && char.IsUpper(processedString[3]))
                    {
                        currentPoint.XCoord = (int)(processedString[0] - 'A');
                        NextPoint.XCoord = (int)(processedString[3] - 'A');
                        validation[0] = true;
                    }
                    if (char.IsLower(processedString[1]) && char.IsLower(processedString[4]))
                    {
                        currentPoint.YCooord = (int)(processedString[1] - 'a');
                        NextPoint.YCooord = (int)(processedString[4] - 'a');

                        validation[1] = true;
                    }
                    if (processedString[2] == '>')
                    {
                        validation[2] = true;
                    }
                    valid = (validation[0] && validation[1] && validation[2]);

                    if (!valid)
                    {
                        Output.InputException();
                    }
                    i++;
                }
                else
                {
                    result = CheckersGameStep.CreateCheckersGameStep(currentPoint, NextPoint, quit);
                    valid = true;
                }
            }

            result.CurrentPosition = currentPoint;
            result.RequestedPosition = NextPoint;
            return result;


        }
    }
}
