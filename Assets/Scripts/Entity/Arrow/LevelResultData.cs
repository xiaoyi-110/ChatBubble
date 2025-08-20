public struct LevelResultData
{
    public int Level;
    public int CorrectCount;
    public int WrongCount;
    public float Accuracy => (CorrectCount + WrongCount) > 0
        ? (float)CorrectCount / (CorrectCount + WrongCount)
        : 0f;

    //数据结构中计算等级
    public string Rank => Accuracy >= 1f ? "S" :
                          Accuracy >= 0.8f ? "A" :
                          Accuracy >= 0.6f ? "B" : "C";

    //数据结构中计算等级索引
    public int RankIndex
    {
        get
        {
            if (Accuracy >= 1f) return 0; // S
            if (Accuracy >= 0.8f) return 1; // A
            if (Accuracy >= 0.6f) return 2; // B
            return 3; // C
        }
    }

    public LevelResultData(int level, int correct, int wrong)
    {
        Level = level;
        CorrectCount = correct;
        WrongCount = wrong;
    }
}