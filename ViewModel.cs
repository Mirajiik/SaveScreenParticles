using SaveScreenParticles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace SaveScreenParticles
{
    public class ViewModel : INotifyPropertyChanged
    {
        Random rnd = new Random();

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<Point> pointsList { get; set; } = new List<Point>() { new Point(100, 100) };
        public ObservableCollection<Point> points { get; set; } = new ObservableCollection<Point>();
        public ViewModel()
        {            
            
            for (int i = 0; i < 25; i++)
            {
                points.Add(new Point(rnd.Next(-960, 960), rnd.Next(-540, 540)));
            }
        }
    }
}
