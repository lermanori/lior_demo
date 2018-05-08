using System;
using Checkers_LogicAndDataSection;

namespace Checkers_UI
{
    public class CheckersGame
    {
        private const int k_maxGamePlayers = 2;
        private eGameState m_gameState = eGameState.KeepGoing;
        private GameBoard m_CheckersBoard = new GameBoard();
        private Player m_currentActivePlayer;
        private bool m_isRequestedMoveLegal = false;
        private CheckersGameStep m_RequestedMove = new CheckersGameStep();

        public void RunCheckersGame()
        {
            string userMoveInput = string.Empty;

            // First Part - Get game initial values from the user
            InitialGameSetting GameSettings;
            UI.ReadGameInitialInputFromUser(out GameSettings);

            // Second Part - initialize the checkers game values according to the user's choice
            SessionData.initializeSessionData(GameSettings);
            SessionData.InitializePlayers(GameSettings);
            m_CheckersBoard.InitializeCheckersBoard();
            Ex02.ConsoleUtils.Screen.Clear();
            UI.PrintCheckersBoard(m_CheckersBoard);

            // Third Part - Game Loop
            while (m_gameState == eGameState.KeepGoing || m_gameState == eGameState.StartOver)
            {
                if (m_gameState == eGameState.StartOver)
                {
                    // In case asked to start a new game - initialize again
                    InitializeAnotherGame(GameSettings);
                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintCheckersBoard(m_CheckersBoard);
                }

                m_currentActivePlayer = SessionData.GetCurrentPlayer();
                m_currentActivePlayer.updateArmy(m_CheckersBoard); // update the possible movement for each soldier in the current player's army
                m_isRequestedMoveLegal = false;

                while (!m_isRequestedMoveLegal)
                {
                    // Read a game movement from the user
                    if (m_currentActivePlayer.Team != ePlayerOptions.ComputerPlayer)
                    {
                        m_RequestedMove = UI.ReadGameMove(ref userMoveInput);
                        if (m_RequestedMove.WantsToQuitIndicator)
                        {
                            break;
                        }
                    }
                    else
                    {
                        // choose a step to execute randomly for PC player
                        m_RequestedMove = m_currentActivePlayer.GetRandomMoveForPc();
                    }

                    // Sort the move type - EatMove or RegularMove
                    m_RequestedMove.MoveTypeInfo = m_CheckersBoard.SortMoveType(m_RequestedMove, m_currentActivePlayer);

                    if (m_RequestedMove.MoveTypeInfo.TypeIndicator != eMoveTypes.Undefined || m_RequestedMove.WantsToQuitIndicator)
                    {
                        m_isRequestedMoveLegal = true;
                    }

                    if (!m_isRequestedMoveLegal)
                    {
                        UI.PrintErrorMessage();
                    }
                }

                if (!m_RequestedMove.WantsToQuitIndicator)
                {
                    // user doesn't want to quit - execute a move!
                    m_currentActivePlayer.MakeAMove(m_RequestedMove, m_CheckersBoard); // at the end of this method - we are ready to get the next move in the game
                    Ex02.ConsoleUtils.Screen.Clear();
                    UI.PrintCheckersBoard(m_CheckersBoard);
                    UI.PrintLastMove(m_RequestedMove, m_currentActivePlayer);
                    m_gameState = SessionData.checkGameState();
                }
                else
                {
                    if (SessionData.m_CurrentActivePlayer == ePlayerOptions.Player1)
                    {
                        m_gameState = eGameState.player1Quit;
                    }
                    else
                    {
                        m_gameState = eGameState.player2Quit;
                    }
                }

                if (m_gameState != eGameState.KeepGoing)
                {
                    // calculate and print score in case of finished game
                    SessionData.CalculateScore(m_gameState);
                    UI.PrintGameResult(m_gameState);
                    m_gameState = UI.CheckIfPlayerWantsAnotherGame();
                }
            }
        }

        private void InitializeAnotherGame(InitialGameSetting o_GameDemoSettings)
        {
            SessionData.m_CurrentActivePlayer = ePlayerOptions.Player1;
            SessionData.InitializePlayers(o_GameDemoSettings);
            m_CheckersBoard.InitializeCheckersBoard();
        }

        private void setup(out InitialGameSetting o_Settings)
        {
            o_Settings = new InitialGameSetting();
            o_Settings.SetGameSettings("Ori", "Amir", eBoardSizeOptions.SmallBoard, eTypeOfGame.singlePlayer);
        }
    }
}
