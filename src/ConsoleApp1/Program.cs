using System;

namespace TicTacToe
{
    class TicTacToeProgram
    {
        static void Main(string[] args)
        {
            char[,] board = new char[3, 3]; // create a 3x3 board
            int userScore = 0;
            int computerScore = 0;


            //while loop
            while (true)
            {
                Console.WriteLine("Tic Tac Toe Game");
                Console.WriteLine($"User Score: {userScore}, Computer Score: {computerScore}\n");

                // initialize board with empty cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = ' ';
                    }
                }

                int turn = 0;
                char currentPlayer = 'X';

                while (true)
                {
                    MakeBoard(board);

                    if (CheckWinner(board, 'O'))
                    {
                        Console.WriteLine("Computer wins!");
                        computerScore++;
                        break;
                    }

                    if (CheckWinner(board, 'X'))
                    {
                        Console.WriteLine("You win!");
                        userScore++;
                        break;
                    }

                    if (IsBoardFull(board))
                    {
                        Console.WriteLine("Draw!");
                        break;
                    }

                    if (turn % 2 == 0) // user's turn
                    {
                        Console.Write("Enter row (1-3): ");
                        int row = int.Parse(Console.ReadLine()) - 1;
                        Console.Write("Enter column (1-3): ");
                        int col = int.Parse(Console.ReadLine()) - 1;

                        if (board[row, col] != ' ')
                        {
                            Console.WriteLine("That cell is already occupied. Try again.");
                            continue;
                        }

                        board[row, col] = currentPlayer;
                    }
                    else // computer's turn
                    {
                        int[] move = FindBestMove(board);
                        board[move[0], move[1]] = currentPlayer;

                        Console.WriteLine($"Computer chooses row {move[0] + 1}, column {move[1] + 1}");
                    }

                    turn++;
                    currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
                }
            }
        }

        static void MakeBoard(char[,] board)
        {
            Console.WriteLine("   1 2 3");
            Console.WriteLine("  -------");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1} |");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write($"{board[i, j]}|");
                }
                Console.WriteLine();
                Console.WriteLine("  -------");
            }
            Console.WriteLine();
        }
        
        //Check for winner method
        static bool CheckWinner(char[,] board, char player)
        {
            // check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                {
                    return true;
                }
            }

            // check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == player && board[1, j] == player && board[2, j] == player)
                {
                    return true;
                }
            }

            // check diagonals
            if (board[0, 0] == player && board[1, 1] == player && board[0, 0] == player)
                if (board[2, 0] == player && board[1, 1] == player && board[0, 2] == player)
                {
                    return true;
                }

            return false;
        }


        static bool IsBoardFull(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        static int[] FindBestMove(char[,] board)
        {
            int bestScore = -1000;
            int[] move = new int[2];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        board[i, j] = 'O';
                        int score = Minimax(board, false);
                        board[i, j] = ' ';
                        if (score > bestScore)
                        {
                            bestScore = score;
                            move[0] = i;
                            move[1] = j;
                        }
                    }
                }
            }
            return move;
        }

        static int Minimax(char[,] board, bool isMaximizing)
        {
            if (CheckWinner(board, 'O'))
            {
                return 1;
            }
            if (CheckWinner(board, 'X'))
            {
                return -1;
            }
            if (IsBoardFull(board))
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = -1000;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = 'O';
                            int score = Minimax(board, false);
                            board[i, j] = ' ';
                            if (score > bestScore)
                            {
                                bestScore = score;
                            }
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 1000;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i, j] == ' ')
                        {
                            board[i, j] = 'X';
                            int score = Minimax(board, true);
                            board[i, j] = ' ';
                            if (score < bestScore)
                            {
                                bestScore = score;
                            }
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}
