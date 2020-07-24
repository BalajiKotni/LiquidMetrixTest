using LiquidMetrixTest.Helpers;

namespace LiquidMetrixTest.Services
{
    public interface IRover
    {
        void Move(string command);
        void SetPosition(int x, int y, Direction direction);
    }
}