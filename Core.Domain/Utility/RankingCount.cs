namespace LMSWebAPI.Utility
{
    public class RankingCount
    {
        public static int CountRankingOfUniversity(int Ranking,int Count )
        {
            int Stars = 0;
            if (Ranking == 1)
            {
                Stars = 5;
            }
            else if (Ranking <= Count * 0.01)
            {
                Stars = 4;
            }
            else if (Ranking <= Count * 0.25)
            {
                Stars = 3;
            }
            else if (Ranking <= Count * 0.5)
            {
                Stars = 2;
            }
            else
            {
                Stars = 1;
            }
            return Stars;
        }
    }
}
