using System.Numerics;
namespace JeuFrancCore;
using GameState = (Vector2, Vector2, Vector2 );
using GameAction = (bool, bool);

public struct InitParams
{
    public InitParams()
    {
    }
    public Vector2 FieldSize { get; set; } = Vector2.One;
}

public class ExposedCore
{
    private Vector2 _fieldSize;
    private GameAction _action;
    private GameState _state;
    private GameLogik _logik = new();

    public void SetInitParams(InitParams initParams)
    {
        _fieldSize = initParams.FieldSize;
    }
    public GameState GetState()
    {
        return _state;
    }

    internal void SetState(GameState newState)
    {
        _state = newState;
    }

    public void SetAction(bool p1Input, bool p2Input)
    {
        _action = (p1Input, p2Input);
    }

    internal GameAction GetAction()
    {
        return _action;
    }

    public void Step(double timeStep)
    {
        _state = _logik.Step(timeStep, _action, _state, _fieldSize);
    }

    public void Init()
    {
        
    }
}