using SaveScreenParticles;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SaveScreenParticles
{
    public class ViewModel
    {
        Random rnd = new Random();

        public ObservableCollection<Particle> points { get; set; } = new ObservableCollection<Particle>();
        public BitmapImage Picture { get; set; } = FindImage();
        public ViewModel()
        {            
            
            for (int i = 0; i < 25; i++)
            {
                points.Add(new Particle(rnd.Next(-960, 960), rnd.Next(-540, 540), TimeSpan.FromSeconds(5)));
            }
        }


        public class Particle
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Duration LifeTime { get; set; }
            public Duration MoveTime { get; private set; }

            public Particle(int x, int y)
            {
                X = x;
                Y = y;
            }
            public Particle(int x, int y, TimeSpan lifeTime) : this(x, y)
            {
                LifeTime = new Duration(lifeTime);
                MoveTime = new Duration(lifeTime*2);
            }
        }

        static private BitmapImage FindImage()
        {
            var fileNames = Directory.EnumerateFiles("../../../img", "*.*")
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) 
                || f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) 
                || f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) 
                || f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));
            if (fileNames.Count() == 0)
            {
                MessageBox.Show("Ошибка! Не найдено изображение в папке img!");
                Application.Current.Shutdown();
                return null;
            }

            return new BitmapImage(new Uri(fileNames.First(), UriKind.Relative));
        }
    }
}
