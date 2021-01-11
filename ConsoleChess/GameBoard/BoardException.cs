using System;

namespace GameBoard
{
    class BoardException : Exception
    {
        public BoardException(string message) : base(message) { }
    }
}
