using System;
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
        return Minimax(board, timer, 5).Item1;
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
        double[] evals = {PieceCountHeuristic(board)};
        return evals[0];
    }

    private static double PieceCountHeuristic(Board board)
    {
        double score = 0;
        foreach (PieceList pl in board.GetAllPieceLists())
        {
            foreach (Piece piece in pl)
            {
                 score += piece.PieceType switch {
                    PieceType.Pawn => 1,
                    PieceType.Knight => 3,
                    PieceType.Bishop => 3.5,
                    PieceType.Rook => 5,
                    PieceType.Queen => 9,
                    _ => 0
                } * (piece.IsWhite ? 1 : -1);
            }
        }
        return score;
    }

    private (Move, double) Black(Board board, Timer timer, int depth)
    {
        if (depth == 0) return (Move.NullMove, AnalyzeBoard(board));
        double worstScore = double.MaxValue;
        Move worstMove = Move.NullMove;
        foreach (Move legalMove in board.GetLegalMoves())
        {
            board.MakeMove(legalMove);
            double bestScore = White(board, timer, depth - 1).Item2;
            if (bestScore <= worstScore)
            {
                worstScore = bestScore;
                worstMove = legalMove; 
            } 
            board.UndoMove(legalMove);
        }
        return (worstMove, worstScore);
    }

    private (Move, double) White(Board board, Timer timer, int depth)
    {
        if (depth == 0) return (Move.NullMove, AnalyzeBoard(board));
        double bestScore = double.MinValue;
        Move bestMove = Move.NullMove;
        foreach (Move legalMove in board.GetLegalMoves())
        {
            board.MakeMove(legalMove);
            double worstScore = Black(board, timer, depth - 1).Item2;
            if (worstScore >= bestScore)
            {
                bestScore = worstScore;
                bestMove = legalMove; 
            } 
            board.UndoMove(legalMove);
        }
        return (bestMove, bestScore);
    }

    private (Move, double) Minimax(Board board, Timer timer, int depth)
    {
        if (depth == 0) return (Move.NullMove, AnalyzeBoard(board));
        double bestScore = double.MinValue;
        Move bestMove = Move.NullMove;
        foreach (Move legalMove in board.GetLegalMoves())
        {
            board.MakeMove(legalMove);
            double bestEnemyScore = Minimax(board, timer, depth - 1).Item2 * (board.IsWhiteToMove ? 1 : -1);
            if (bestEnemyScore >= bestScore)
            {
                bestScore = bestEnemyScore;
                bestMove = legalMove; 
            } 
            board.UndoMove(legalMove);
        }
        return (bestMove, bestScore);
    }
}