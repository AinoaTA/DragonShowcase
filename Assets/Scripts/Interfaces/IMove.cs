using UnityEngine;

public interface IMove 
{
    public abstract void AddListener();
    public abstract void Move(Vector3 pos);
}
