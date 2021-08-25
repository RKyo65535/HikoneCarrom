public class StoneCounter
{
    int redStone;
    int blueStone;
    public StoneCounter(int initialStoneCount)
    {
        redStone = initialStoneCount;
        blueStone = initialStoneCount;
    }


    public void ReduceOneStone(StoneRole stone)
    {
        switch (stone)
        {
            case StoneRole.RED:
                redStone -= 1;
                break;
            case StoneRole.BLUE:
                blueStone -= 1;
                break;
            default:
                break;
        }
    }

    public bool IsThereNoStone(StoneRole stone)
    {
        switch (stone)
        {
            case StoneRole.RED:
                if (redStone == 0)
                {
                    return true;
                }
                break;
            case StoneRole.BLUE:
                if (blueStone == 0)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }

    public int GetCurrentStoneCount(StoneRole stone)
    {
        switch (stone)
        {
            case StoneRole.RED:
                return redStone;
            case StoneRole.BLUE:
                return blueStone;
            default:
                break;
        }
        return -1;
    }
}
