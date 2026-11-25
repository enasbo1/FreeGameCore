using System.Numerics;

namespace JeuFrancCore;
using GameState = (Vector2, Vector2, Vector2);
using GameAction = (bool, bool);
public class ModelLearning : IModelLearning
{
    public void Init()
    {
    }

    public void Load(float state)
    {
        throw new NotImplementedException();
    }

    public void Train()
    {
    }

    public float Predict(float state)
    {
        throw new NotImplementedException();
    }
}