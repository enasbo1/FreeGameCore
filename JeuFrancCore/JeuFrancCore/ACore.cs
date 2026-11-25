using System.Numerics;

namespace JeuFrancCore;


public abstract class ACore<TGLogik, TInitParams, TState, TAction> 
    where TGLogik : IGameLogik<TState,TAction>, new()
{
    private TState _state;
    private TAction _action;
    private bool _terminal = false;
    private float _reward = 0f;
    private readonly TGLogik _logik = new();
    
    public abstract void SetInitParams(TInitParams initParams);
    
    public TState GetState()
    {
        return _state;
    }

    internal void SetState(TState newState)
    {
        _state = newState;
    }

    public void SetAction(TAction action)
    {
        _action = action;
    }

    internal TAction GetAction()
    {
        return _action;
    }

    internal void SetReward(float reward)
    {
        _reward = reward;
    }

    public float GetReward()
    {
        return _reward;
    }

    public bool Step(float timeStep)
    {
        (_state, _reward, _terminal) = _logik.Step(timeStep, _action, _state);
        return _terminal;
    }

    public abstract void Init();
}