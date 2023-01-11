using SaveScreenParticles.Commands;
using SaveScreenParticles.Models;
using SaveScreenParticles.ViewModels.Base;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SaveScreenParticles.ViewModels
{
    internal partial class MainWindowViewModel : ViewModel
    {
        /*private IEnumerable<Particle> _Particles;
        public IEnumerable<Particle> Particles { get => _Particles; set => Set(ref _Particles, value); }*/
        #region Свойства
        /// <summary>Коллекция частиц</summary>
        public ObservableCollection<Particle> Particles { get; set; } = new ObservableCollection<Particle>();
        /// <summary>Фоновая картинка</summary>
        public BitmapImage Picture { get; set; } = FindImage();
        /// <summary>Количество частиц</summary>
        private const int n = 50;
        private const int width = 900;
        private const int height = 500;
        #endregion

        #region Команды
        #region Запуск таймеров жизни частиц
        public ICommand StartTimersCommand { get; }
        private void OnStartTimersCommandExecuted(object p)
        {
            foreach (var item in Particles)
            {
                item.TimerOffset.Start();
            }
        }
        private bool CanStartTimersCommandExecute(object p) => true;
        #endregion
        #endregion
        public MainWindowViewModel()
        {
            #region Инициализация комманд
            StartTimersCommand = new LambdaCommand(OnStartTimersCommandExecuted, CanStartTimersCommandExecute);
            #endregion
            //List<Particle> listParticle = new List<Particle>(n);
            for (int i = 0; i < n; i++)
            {
                var (x, y, lifeTime) = GenerateRandom();
                Particles.Add(new Particle(x, y, TimeSpan.FromSeconds(lifeTime)));
            }
            //Particles = listParticle;
            //listParticle.Clear();
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

        static public (int, int) GenerateRandomPosition()
        {
            Random rnd = new Random();
            return (rnd.Next(-width, width), rnd.Next(-height, height));
        }
        static public (int, int, int) GenerateRandom()
        {
            Random rnd = new Random();
            return (rnd.Next(-width, width), rnd.Next(-height, height), rnd.Next(2,10));
        }
    }
}
