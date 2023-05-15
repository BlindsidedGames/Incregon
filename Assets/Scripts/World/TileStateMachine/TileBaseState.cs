namespace World.TileStateMachine
{
    public abstract class TileBaseState
    {
        public abstract void EnterState(TileManager tile);

        public abstract void UpdateState(TileManager tile);

        public abstract void OnExitState(TileManager tile);

        public abstract void ProcessResources(TileManager tile);
    }
}