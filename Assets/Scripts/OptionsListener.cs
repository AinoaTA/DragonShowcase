using UnityEngine;
public class OptionsListener 
{
    public bool CanMove => _move != null;
    public bool CanBeDeleted => _delete != null;

    private IMove _move;
    private IDelete _delete;
     
    public void AddMove(IMove m) 
    {
        _move = m;
    }

    public void AddDelete(IDelete m) 
    {
        _delete = m;
    }
}
