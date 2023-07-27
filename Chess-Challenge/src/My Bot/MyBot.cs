using ChessChallenge.API;

public class MyBot : IChessBot
{
    /// <summary>
    /// Returns a random move.
    /// </summary>
    /// <param name="board">The <see cref="Board"/>.</param>
    /// <returns>A random legal move.</returns>
    private static Move GetRandomMove(Board board)
    {
        Move[] moves = board.GetLegalMoves();
        return moves[218372381273 % moves.Length];  
    }
    public Move Think(Board board, Timer timer)
    {
        return board.IsWhiteToMove ? White(board, timer, 5) : Black(board, timer, 5);
    }

    /// <summary>
    /// Uses heuristic(s) to analyze a board without thinking ahead.
    /// </summary>
    /// <param name="board">The current state of the game.</param>
    /// <returns>A double where:
    /// x > 0: white is estimated to be better
    /// x = 0: the game is estimated to be a draw
    /// x < 0: black is estimated to be better
    ///</returns>
    private static double AnalyzeBoard(Board board)
    {
        return 0;
    }

    private Move Black(Board board, Timer timer, int depth)
    {
        return White(board, timer, depth - 1);
    }

    private Move White(Board board, Timer timer, int depth)
    {
        return GetRandomMove(board);
    }
}