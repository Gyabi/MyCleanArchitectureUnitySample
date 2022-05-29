public class PlayerState
{
    private BlockMode _blockMode;
    public PlayerState()
    {
        this._blockMode = BlockMode.Create;
    }

    public void ChangeBlockMode(BlockMode blockMode)
    {
        this._blockMode = blockMode;
    }
    public bool CanCreateBlock()
    {
        return this._blockMode == BlockMode.Create;
    }
    public bool CanDeleteBlock()
    {
        return this._blockMode == BlockMode.Delete;
    }
}