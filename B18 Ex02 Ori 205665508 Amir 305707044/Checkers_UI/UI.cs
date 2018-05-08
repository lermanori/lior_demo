using System;
using System.Text;
using Checkers_LogicAndDataSection;

namespace Checkers_UI
{
    public class UI
    {
        private const int k_PlayerXName = 0;
        private const int k_PlayerOName = 1;
        private const int k_BoardSize = 2;
        private const int k_GameType = 3;
        private const int k_NumberOfInputValues = 4;
        private const int k_MaxNameLength = 20;

        // values for the step input validity checker method
        private const int k_StepInputPartsToCheck = 3;
        private const int k_ColumnsPart = 0;
        private const int k_LinesPart = 1;
        private const int k_StartPositionColumnChar = 0;
        private const int k_StartPositionLineChar = 1;
        private const int k_ArrowPlace = 2;
        private const int k_RequestedPositionColumnChar = 3;
        private const int k_RequestedPositionLineChar = 4;

        // consts for printing reasons
        private const char k_Space = ' ';
        private const char k_LineSeperator = '=';
        private const char k_ColumnSeperator = '|';
        private const int k_NumberOfSpacesBetweenLetters = 3;
        private const char k_Team1SoldierChar = 'X';
        private const char k_Team1KingChar = 'K';
        private const char k_Team2SoldierChar = 'O';
        private const char k_Team2KingChar = 'U';
        private const char k_ArrowSign = '>';
        private const string k_QuitChar = "Q";

        // All the strings that are being used during the input-output are here:
        private static string s_StartMessage = "Hello! Welcome to our checkers game. Enjoy!";
        private static string s_UsernameMessage = "Please enter your name: ";
        private static string s_ChooseGameTypeMessge = "Please choose the game type: (1) Single-Player , (2) Double-Player";
        private static string s_ChooseBoardSizeMessage = "Please choose the board size: (6)-Small 6x6 , (8)-Medium 8x8, (10)-Large 10x10 ";
        private static string s_EnterMoveMessageFormat = "'s Turn {0}: ";
        private static string s_LastMoveMessageFormat = "'s move was {0}: ";
        private static string s_InvalidInputMessage = "Input is invalid, please try again.";
        private static string s_TheScoreMessage = "The Score is:";
        private static string s_TieMessage = "Game ended in a draw!";
        private static string s_PlayerWon = " WON! Good Job!";
        private static string s_PlayerQuit = " QUIT! You Loser!";
        private static string s_PressAnyKeyMessage = "Press any key to show results...";
        private static string s_AnotherGameMessage = "Do you wish to play another game? (1) Yes, (0) No";
        private static string s_GoodByeMessage = "Thank you for playing Checkers. Bye Bye!";
        private static string s_PressAnyKeyToExitMessage = "Press Any Key To Exit...";
        private static string s_Player1Board = "--->";
        private static string s_Player2Board = "<---";
        private static string s_ScoreMessage = "SCORE:  ";
        private static string s_PointsMessage = "POINTS: ";
        private static string s_Team1 = "(X)";
        private static string s_Team2 = "(O)";

        public static void ReadGameInitialInputFromUser(out InitialGameSetting io_GameInitialValues)
        {
            string userInputValue;
            string tempPlayer1NameHolder = string.Empty;
            string tempPlayer2NameHolder = string.Empty;
            bool[] inputValidityArray = new bool[k_NumberOfInputValues]; // holds true/false for each input value in this order: player1name, player2name, boardSize, gameType
            bool gotAllInput = false;
            eBoardSizeOptions chosenBoardSize = eBoardSizeOptions.Undefined;
            eTypeOfGame chosenGameType = eTypeOfGame.Undefined;

            Console.WriteLine(s_StartMessage);
            while (!gotAllInput)
            {
                while (!inputValidityArray[k_PlayerXName])
                {
                    Console.WriteLine(s_UsernameMessage);
                    userInputValue = Console.ReadLine();
                    if (checkUsernameValidity(userInputValue))
                    {
                        inputValidityArray[k_PlayerXName] = true;
                        tempPlayer1NameHolder = userInputValue;
                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }

                Ex02.ConsoleUtils.Screen.Clear();
                while (!inputValidityArray[k_BoardSize])
                {
                    Console.WriteLine(s_ChooseBoardSizeMessage);
                    userInputValue = Console.ReadLine();
                    if (checkBoardSizeValidity(userInputValue))
                    {
                        inputValidityArray[k_BoardSize] = true;
                        chosenBoardSize = (eBoardSizeOptions)Enum.Parse(typeof(eBoardSizeOptions), userInputValue);
                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }

                Ex02.ConsoleUtils.Screen.Clear();
                while (!inputValidityArray[k_GameType])
                {
                    Console.WriteLine(s_ChooseGameTypeMessge);
                    userInputValue = Console.ReadLine();
                    if (checkGameTypeValidity(userInputValue))
                    {
                        inputValidityArray[k_GameType] = true;
                        chosenGameType = (eTypeOfGame)Enum.Parse(typeof(eTypeOfGame), userInputValue);
                        Ex02.ConsoleUtils.Screen.Clear();
                        if (chosenGameType == eTypeOfGame.doublePlayer)
                        {
                            while (!inputValidityArray[k_PlayerOName])
                            {
                                Console.Write("{0}, ", ePlayerOptions.Player2.ToString());
                                Console.WriteLine(s_UsernameMessage);
                                userInputValue = Console.ReadLine();
                                if (checkUsernameValidity(userInputValue))
                                {
                                    inputValidityArray[k_PlayerOName] = true;
                                    tempPlayer2NameHolder = userInputValue;
                                }
                                else
                                {
                                    Console.WriteLine(s_InvalidInputMessage);
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(s_InvalidInputMessage);
                    }
                }

                Ex02.ConsoleUtils.Screen.Clear();
                gotAllInput = true;
            }

            io_GameInitialValues = new InitialGameSetting();
            io_GameInitialValues.SetGameSettings(tempPlayer1NameHolder, tempPlayer2NameHolder, chosenBoardSize, chosenGameType);
        }

        private static bool checkUsernameValidity(string i_InputValue)
        {
            return !i_InputValue.Contains(k_Space.ToString()) && i_InputValue.Length < k_MaxNameLength;
        }

        private static bool checkBoardSizeValidity(string i_InputValue)
        {
            bool returnedValue;
            eBoardSizeOptions temporaryBoardSize;
            bool inputOk = Enum.TryParse(i_InputValue, out temporaryBoardSize);
            if (!inputOk)
            {
                returnedValue = false;
            }
            else
            {
                returnedValue = temporaryBoardSize.Equals(eBoardSizeOptions.SmallBoard) || temporaryBoardSize.Equals(eBoardSizeOptions.MediumBoard) || temporaryBoardSize.Equals(eBoardSizeOptions.LargeBoard);
            }

            return returnedValue;
        }

        private static bool checkGameTypeValidity(string i_InputValue)
        {
            bool returnedValue;
            eTypeOfGame temporaryGameType;
            bool inputOk = Enum.TryParse(i_InputValue, out temporaryGameType);
            if (!inputOk)
            {
                returnedValue = false;
            }
            else
            {
                returnedValue = temporaryGameType.Equals(eTypeOfGame.singlePlayer) || temporaryGameType.Equals(eTypeOfGame.doublePlayer);
            }

            return returnedValue;
        }

        internal static eGameState CheckIfPlayerWantsAnotherGame()
        {
            eGameState newGameState = eGameState.Undefined;
            string userInput = string.Empty;
            Console.WriteLine(s_AnotherGameMessage);
            userInput = Console.ReadLine();
            if (userInput.Equals(1.ToString()))
            {
                newGameState = eGameState.StartOver;
            }
            else
            {
                Console.WriteLine(s_GoodByeMessage);
                Console.WriteLine(s_PressAnyKeyToExitMessage);
                Console.ReadKey();
                newGameState = eGameState.Quit;
            }

            return newGameState;
        }

        internal static void PrintErrorMessage()
        {
            Console.WriteLine(s_InvalidInputMessage);
        }

        internal static void PrintGameResult(eGameState io_GameState)
        {
            string player1Name = SessionData.GetPlayerName(ePlayerOptions.Player1);
            string player2Name = SessionData.GetPlayerName(ePlayerOptions.Player2);

            StringBuilder endGameMessage = new StringBuilder();
            StringBuilder resultsMessage = new StringBuilder();
            string whichMessage = string.Empty;
            switch (io_GameState)
            {
                case eGameState.Tie:
                    whichMessage = s_TieMessage;
                    break;
                case eGameState.WinPlayer1:
                    endGameMessage.Append(player1Name);
                    whichMessage = s_PlayerWon;
                    break;
                case eGameState.WinPlayer2:
                    endGameMessage.Append(player2Name);
                    whichMessage = s_PlayerWon;
                    break;
                case eGameState.player1Quit:
                    endGameMessage.Append(player1Name);
                    whichMessage = s_PlayerQuit;
                    break;
                case eGameState.player2Quit:
                    endGameMessage.Append(player2Name);

                    whichMessage = s_PlayerQuit;
                    break;
            }

            endGameMessage.AppendFormat("{0}{1}{2}", whichMessage, Environment.NewLine, s_PressAnyKeyMessage);
            Console.WriteLine(endGameMessage);
            Console.ReadKey();
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(s_TheScoreMessage);
            resultsMessage.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}", s_PointsMessage, player1Name, s_Player1Board, SessionData.m_Player1Points, k_ColumnSeperator, SessionData.m_Player2Points, s_Player2Board, player2Name);
            resultsMessage.Append(Environment.NewLine);
            resultsMessage.AppendFormat("{0}{1}{2}{3}{4}{5}{6}{7}", s_ScoreMessage, player1Name, s_Player1Board, SessionData.m_Player1OverallScore, k_ColumnSeperator, SessionData.m_Player2OverallScore, s_Player2Board, player2Name);
            Console.WriteLine(resultsMessage);
        }

        internal static void PrintLastMove(CheckersGameStep io_StepExecuted, Player io_PreviousPlayer)
        {
            System.Text.StringBuilder messageToPrint = new System.Text.StringBuilder();
            string playerName = io_PreviousPlayer.PlayerName;
            string playerTeam = string.Empty;
            StringBuilder lastMove = new StringBuilder();
            StringBuilder LastMoveMessage = new System.Text.StringBuilder();
            if (io_PreviousPlayer.Team == ePlayerOptions.Player1)
            {
                playerTeam = s_Team1;
            }
            else
            {
                playerTeam = s_Team2;
            }
            lastMove.AppendFormat(
                "{0}{1}{2}{3}{4}",
                (char)(io_StepExecuted.CurrentPosition.XCoord + 'A'),
                (char)(io_StepExecuted.CurrentPosition.YCooord + 'a'),
                 k_ArrowSign.ToString(),
                 (char)(io_StepExecuted.RequestedPosition.XCoord + 'A'),
                 (char)(io_StepExecuted.RequestedPosition.YCooord + 'a'));
            LastMoveMessage.AppendFormat(s_LastMoveMessageFormat,playerTeam);
            messageToPrint.AppendFormat("{0}{1}{2}", playerName, LastMoveMessage,lastMove);
            Console.WriteLine(messageToPrint);
        }

        public static void PrintCheckersBoard(GameBoard io_CheckersBoard)
        {
            /*The Idea:
                We create a StringBuilder object , and we call 3 diff methods to create the top, body, and bottom
                parts of the checkers board. After all these 3 function finished, the board is ready to be printed.
            */

            StringBuilder BoardWithFrames = new StringBuilder();

            CreateBoardHeader(BoardWithFrames);
            CreateBoardBody(BoardWithFrames, io_CheckersBoard);
            CreateBoardFooter(BoardWithFrames);

            Console.WriteLine(BoardWithFrames);
        }

        private static void CreateBoardHeader(StringBuilder o_BoardHeader)
        {
            string[] columnsLetters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;

            for (int i = 0; i < boardSize; i++)
            {
                o_BoardHeader.Append(k_Space, k_NumberOfSpacesBetweenLetters);
                o_BoardHeader.Append(columnsLetters[i]);
            }

            o_BoardHeader.Append(Environment.NewLine);
            o_BoardHeader.Append(k_Space);
            o_BoardHeader.Append(k_LineSeperator, numberOfLineSeperators);
            o_BoardHeader.Append(Environment.NewLine);
        }

        private static void CreateBoardBody(StringBuilder o_BoardBody, GameBoard io_CheckersBoard)
        {
            string[] columnLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;
            for (int i = 0; i < boardSize; i++)
            {
                o_BoardBody.AppendFormat("{0}{1}", columnLetters[i], k_ColumnSeperator);
                for (int j = 0; j < boardSize; j++)
                {
                    Point soldierCoordinate = new Point(j, i);
                    char soldierCharToPrint = BringCharFromMatrix(io_CheckersBoard, soldierCoordinate);
                    o_BoardBody.AppendFormat("{0}{1}{0}{2}", k_Space, soldierCharToPrint, k_ColumnSeperator);
                }

                o_BoardBody.Append(Environment.NewLine);
                if (i != boardSize - 1)
                {
                    o_BoardBody.Append(k_Space);
                    o_BoardBody.Append(k_LineSeperator, numberOfLineSeperators);
                    o_BoardBody.Append(Environment.NewLine);
                }
            }
        }

        private static char BringCharFromMatrix(GameBoard i_CheckersBoard, Point i_SoldierCoordinate)
        {
            char charToPrint = k_Space;
            GameBoard.Soldier soldierInBoard = i_CheckersBoard.GetSoldierFromMatrix(i_SoldierCoordinate);
            if (soldierInBoard != null)
            {
                switch (soldierInBoard.Team)
                {
                    case ePlayerOptions.Player1:
                        if (soldierInBoard.Rank == GameBoard.eSoldierRanks.Regular)
                        {
                            charToPrint = k_Team1SoldierChar;
                        }
                        else
                        {
                            charToPrint = k_Team1KingChar;
                        }

                        break;
                    case ePlayerOptions.Player2:
                    case ePlayerOptions.ComputerPlayer:
                        if (soldierInBoard.Rank == GameBoard.eSoldierRanks.Regular)
                        {
                            charToPrint = k_Team2SoldierChar;
                        }
                        else
                        {
                            charToPrint = k_Team2KingChar;
                        }

                        break;
                }
            }

            return charToPrint;
        }

        private static void CreateBoardFooter(StringBuilder o_BoardFooter)
        {
            int boardSize = (int)SessionData.m_BoardSize;
            int numberOfLineSeperators = (boardSize * 4) + 1;
            o_BoardFooter.Append(k_Space);
            o_BoardFooter.Append(k_LineSeperator, numberOfLineSeperators);
        }

        public static CheckersGameStep ReadGameMove(ref string io_UserInput)
        {
            bool isInputOk = false;
            Point startPosition;
            Point requestedPosition;
            CheckersGameStep requestedStep = new CheckersGameStep();
            Player currentPlayer = SessionData.GetCurrentPlayer();


            string playerNameHolder = currentPlayer.PlayerName;
            string playersTeam = string.Empty;
            System.Text.StringBuilder messageToPrint = new System.Text.StringBuilder();
            System.Text.StringBuilder enterMoveMessage = new System.Text.StringBuilder();


            if (currentPlayer.Team==ePlayerOptions.Player1)
            {
                playersTeam = s_Team1;
            }
            else
            {
                playersTeam = s_Team2;
            }
            enterMoveMessage.AppendFormat(s_EnterMoveMessageFormat, playersTeam);
            messageToPrint.AppendFormat("{0}{1}", playerNameHolder, enterMoveMessage);

            while (!isInputOk)
            {
                Console.Write(messageToPrint);
                io_UserInput = Console.ReadLine();
                isInputOk = CheckStepInputValidity(io_UserInput);
                if (!isInputOk)
                {
                    Console.WriteLine(s_InvalidInputMessage);
                }
            }

            if (io_UserInput.Equals(k_QuitChar))
            {
                if (SessionData.GetCurrentPlayer().NumberOfSoldiers < SessionData.GetOtherPlayer().NumberOfSoldiers)
                {
                    requestedStep.WantsToQuitIndicator = true;
                }
            }
            else
            {
                MakePointsFromString(io_UserInput, out startPosition, out requestedPosition);
                requestedStep.CurrentPosition = startPosition;
                requestedStep.RequestedPosition = requestedPosition;
            }

            return requestedStep;
        }

        private static bool CheckStepInputValidity(string o_StepInputFromUser)
        {
            /* In this method we check if the entered step is a legal checkers game step.
             * string length must be 5 (Ex. Ab>Cd - 5 chars)
             * string[0] and string[3] must be upper case
             * string[1] and string[4] must be lower cast
             * string[2] must be '>'
             */
            const int numOfNeededChars = 5;
            bool[] stepInputPartsValidation = new bool[k_StepInputPartsToCheck];
            bool areAllPartsValid = false;

            for (int i = 0; i < k_StepInputPartsToCheck; i++)
            {
                stepInputPartsValidation[i] = false;
            }

            if (o_StepInputFromUser.Length == numOfNeededChars)
            {
                if (char.IsUpper(o_StepInputFromUser[k_StartPositionColumnChar]) && char.IsUpper(o_StepInputFromUser[k_RequestedPositionColumnChar]))
                {
                    stepInputPartsValidation[k_ColumnsPart] = true;
                }

                if (char.IsLower(o_StepInputFromUser[k_StartPositionLineChar]) && char.IsLower(o_StepInputFromUser[k_RequestedPositionLineChar]))
                {
                    stepInputPartsValidation[k_LinesPart] = true;
                }

                if (o_StepInputFromUser[k_ArrowPlace] == k_ArrowSign)
                {
                    stepInputPartsValidation[k_ArrowPlace] = true;
                }

                areAllPartsValid = stepInputPartsValidation[k_ColumnsPart] && stepInputPartsValidation[k_LinesPart]
                                                                    && stepInputPartsValidation[k_ArrowPlace];
            }
            else if (o_StepInputFromUser.Equals(k_QuitChar) && SessionData.GetCurrentPlayer().NumberOfSoldiers < SessionData.GetOtherPlayer().NumberOfSoldiers)
            {
                // in case user entered Q and is eligible to quit --> He has less soldiers than the second player
                areAllPartsValid = true;
            }

            return areAllPartsValid;
        }

        private static void MakePointsFromString(string o_userInput, out Point o_startPosition, out Point o_requestedPosition)
        {
            o_startPosition = new Point();
            o_requestedPosition = new Point();

            o_startPosition.XCoord = (int)(o_userInput[k_StartPositionColumnChar] - 'A');
            o_startPosition.YCooord = (int)(o_userInput[k_StartPositionLineChar] - 'a');

            o_requestedPosition.XCoord = (int)(o_userInput[k_RequestedPositionColumnChar] - 'A');
            o_requestedPosition.YCooord = (int)(o_userInput[k_RequestedPositionLineChar] - 'a');
        }
    }
}
