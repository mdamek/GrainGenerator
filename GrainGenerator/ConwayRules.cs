using System;

namespace GameOfLife
{
    public class ConwayRules : IRules 
    {
        public ToState ChangeState(int neighborhoodsNumber, bool isAlive)
        {
            if(neighborhoodsNumber > 8) throw new ArgumentException("Conway have 8 neighborhoods");
            if (isAlive == false)
            {
                if (neighborhoodsNumber == 3)
                {
                    return ToState.ToLive;
                }
            }
            else
            {
                if (neighborhoodsNumber == 2 || neighborhoodsNumber == 3)
                {
                    return ToState.DoNotChange;
                }

                if (neighborhoodsNumber < 2 || neighborhoodsNumber > 3)
                {
                    return ToState.ToDead;
                }
            }
            return ToState.DoNotChange;
        }
    }
}
