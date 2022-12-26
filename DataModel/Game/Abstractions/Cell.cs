namespace OceanBattle.DataModel.Game.Abstractions
{
    public abstract class Cell : IHittable
    {
        public bool IsPopulated { get; internal set; }
        public bool IsHit { get; private set; }

        public virtual void Hit(int damage)
        {
            IsHit = damage > 0;
        }
    }
}
