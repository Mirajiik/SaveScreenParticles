using SaveScreenParticles.ViewModels;
using SaveScreenParticles.ViewModels.Base;
using System.Windows;
using System.Windows.Threading;

namespace SaveScreenParticles.Models
{
    internal class Particle : ViewModel
    {
        private int _X;
        public int X { get => _X; set => Set(ref _X, value); }

        private int _Y;
        public int Y { get => _Y; set => Set(ref _Y, value); }
        public Duration LifeTime { get; set; }
        public Duration MoveTime { get; private set; }
        private DispatcherTimer _TimerOffset;
        public DispatcherTimer TimerOffset { get => _TimerOffset;}

        public Particle(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Particle(int x, int y, TimeSpan lifeTime) : this(x, y)
        {
            LifeTime = new Duration(lifeTime);
            MoveTime = new Duration(lifeTime * 2);
            _TimerOffset = new DispatcherTimer();
            _TimerOffset.Tick += new EventHandler(OffsetPosition);
            _TimerOffset.Interval = lifeTime * 2;
        }
        private void OffsetPosition(object o, EventArgs e)
        {
            var (x, y) = MainWindowViewModel.GenerateRandomPosition();
            if (this is Particle)
            {
                var currentParticle = this;
                currentParticle.X = x;
                currentParticle.Y = y;
            }
        }
    }
}
