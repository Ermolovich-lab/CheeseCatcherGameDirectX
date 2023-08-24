using GameEngineLibrary.Graphics;

namespace GameEngineLibrary.Scripts
{
    public abstract class Script
    {
        public Game3DObject GameObject { get; set; }

        public abstract void Update(float delta);

        public virtual void Init()
        {

        }
    }
}
