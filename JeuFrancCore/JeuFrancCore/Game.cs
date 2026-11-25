using System.Numerics;
namespace JeuFrancCore;

using GameState = (Vector2, Vector2, Vector2);
using GameAction = (bool, bool);

public class Game : Game<ModelLearning, ExposedCore, GameLogik, InitParams, GameState, GameAction>;

public abstract class Game<TModel, TCore, TGLogik, TInitParams, TState, TAction> 
    where TModel : IModelLearning
    where TCore : ACore<TGLogik, TInitParams, TState, TAction>, new()
    where TGLogik : IGameLogik<TState, TAction>, new()
    where TInitParams : new()

{
    private readonly TCore _core = new();
    private readonly TGLogik _logik = new();
    private int _playerNb;

    public void Init(int playerNb = 2)
    {
        _playerNb = playerNb;
        _core.SetInitParams(new TInitParams());
        _core.Init();
    }

    public TState Step(TAction action = default)
    {
        TAction = 
        if (_playerNb>1)
        _core.GetState();

    }
    private TState IAStep()
    {
        _core.GetState();
        //_core.Step();
        return _core.GetState();
    }

}