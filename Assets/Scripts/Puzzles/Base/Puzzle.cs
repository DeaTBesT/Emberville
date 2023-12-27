using UnityEngine;

public abstract class Puzzle : MonoBehaviour, IPuzzle
{
    public abstract void ActivatePuzzle();

    public abstract void DiactivatePuzzle();

    public abstract void ResolvePuzzle();
}
