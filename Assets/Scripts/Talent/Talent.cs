[System.Serializable]
public class Talent
{
    public int currentLevel;
    public int maxLevel;
    public int pointsPerLevel;
    public float[] valuesAtEachLevel;

    public float CurrentValue => valuesAtEachLevel[currentLevel - 1];
}
