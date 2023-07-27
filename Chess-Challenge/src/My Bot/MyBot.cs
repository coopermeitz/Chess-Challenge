using ChessChallenge.API;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class MyBot : IChessBot
{
    public static Move GetRandomMove(Board board)
    {
        Move[] moves = board.GetLegalMoves();
        return moves[218372381273 % moves.Length];  
    }
    public Move Think(Board board, Timer timer)
    {
        return GetRandomMove(board);
    }
}