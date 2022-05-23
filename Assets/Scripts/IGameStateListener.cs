
public interface IGameStateListener
{
    void SubscribeToGameState();
    void UnSubscribeToGameState();
    void OnGameStateChanged();
}
