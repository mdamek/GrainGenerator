namespace GameOfLife
{
    public interface IRules
    {
        ToState ChangeState(int neighborhoodsNumber, bool isAlive);
    }
}
