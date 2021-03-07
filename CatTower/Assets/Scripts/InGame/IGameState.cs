namespace CatTower
{
    public interface IGameState
    {
        void InStart();
        void InFinish(IngameStatus response);
    }
}
