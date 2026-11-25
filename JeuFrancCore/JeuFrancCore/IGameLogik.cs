namespace JeuFrancCore;

public interface IGameLogik<TState, TAction>
{
    internal (TState, float, bool) Step(float timeStep, TAction action, TState state);
    
}