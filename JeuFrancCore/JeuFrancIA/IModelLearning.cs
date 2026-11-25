// See https://aka.ms/new-console-template for more information

namespace JeuFrancCore;

public interface IModelLearning
{
    public void Init();
    public void Load(float state);
    public void Train();
    public float Predict(float state);
}