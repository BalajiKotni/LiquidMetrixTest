using LiquidMetrixTest.Helpers;

namespace LiquidMetrixTest.Services
{
    public class Rover : IRover
    {
        private static int maxX = 40;
        private static int maxY = 30;

        private readonly ILogger _logger;

        public Rover(ILogger logger)
        {
            this._logger = logger;
        }

        public int CurrentX { get; private set; } = 10;

        public int CurrentY { get; private set; } = 10;

        public Direction CurrentDirection { get; private set; } = Direction.N;

        public void SetPosition(int x, int y, Direction direction)
        {
            CurrentX = x;
            CurrentY = y;
            CurrentDirection = direction;
        }

        public void Move(string command)
        {
            if (!IsValidCommand(command))
            {
                _logger.Log($"Invalid command {command}");
                return;
            }

            var newDirection = GetNewDirection(command);
            var lengthToMove = int.Parse(command.Substring(1));
            
            if (newDirection == Direction.E)
            {
                var newX = CurrentX + lengthToMove;
                if (newX >= maxX)
                {
                    _logger.Log($"Invalid command {command} causing fall off plateau");
                    return;
                }
                CurrentX = newX;    
            }

            if (newDirection == Direction.W)
            {
                var newX = CurrentX - lengthToMove;
                if (newX <= 0)
                {
                    _logger.Log($"Invalid command {command} causing fall off plateau");
                    return;
                }
                CurrentX = newX;
            }

            if (newDirection == Direction.N)
            {
                var newY = CurrentY + lengthToMove;
                if (newY >= maxY)
                {
                    _logger.Log($"Invalid command {command} causing fall off plateau");
                    return;
                }
                CurrentY = newY;
            }

            if (newDirection == Direction.S)
            {
                var newY = CurrentY - lengthToMove;
                if (newY <= 0)
                {
                    _logger.Log($"Invalid command {command} causing fall off plateau");
                    return;
                }
                CurrentY = newY;
            }

            CurrentDirection = newDirection;

            _logger.Log($"Moved to x: {CurrentX} y: {CurrentY} direction: {CurrentDirection}");
        }

        private Direction GetNewDirection(string command)
        {
            if (command[0] == 'L')
            {
                if (CurrentDirection == Direction.N)
                    return Direction.W;
                if (CurrentDirection == Direction.W)
                    return Direction.S;
                if (CurrentDirection == Direction.S)
                    return Direction.E;
                return Direction.N;
            }
            else
            {
                if (CurrentDirection == Direction.N)
                    return Direction.E;
                if (CurrentDirection == Direction.E)
                    return Direction.S;
                if (CurrentDirection == Direction.S)
                    return Direction.W;
                return Direction.N;
            }
        }

        private bool IsValidCommand(string command)
        {
            if (command.Length > 3)
            {
                return false;
            }

            if (command[0] != 'L' && command[0] != 'R')
            {
                return false;
            }

            if (!int.TryParse(command.Substring(1), out _))
            {
                return false;
            }
            
            return true;
        }

    }
}
