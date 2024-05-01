using UnityEngine;
public class OptionsListener 
{
    public bool CanMove => _move != null;
    public bool CanBeDeleted => _delete != null;

    public bool CanBeRotated => _rotate != null;
    public bool CanBeScaled => _scale != null;

    private IMove _move;
    private IDelete _delete;
    private IScale _scale;
    private IRotate _rotate;
     
    public void AddMove(IMove m) 
    {
        _move = m;
    }

    public void AddDelete(IDelete m) 
    {
        _delete = m;
    }

    public void AddRotate(IRotate r) 
    {
        _rotate = r;
    }

    public void AddScale(IScale s) 
    {
        _scale = s;
    }
}
