using System.Windows;
using System.Windows.Markup;

namespace SaveScreenParticles;

[MarkupExtensionReturnType(typeof(Duration))]
public class TimeDuration : MarkupExtension
{
    private int _Hours;
    public int Hours
    {
        get => _Hours;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Значение часов не должно быть меньше 0");
            _Hours = value;
        }
    }

    private int _Minutes;
    public int Minutes
    {
        get => _Minutes;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Значение минут не должно быть меньше 0");
            _Minutes = value;
        }
    }

    private double _Seconds;

    public double Seconds
    {
        get => _Seconds;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Значение секунд не должно быть меньше 0");
            _Seconds = value;
        }
    }

    public TimeDuration() { }

    public TimeDuration(double Seconds) => this.Seconds = Seconds;
    public TimeDuration(int Minutes, double Seconds) : this(Minutes) => this.Minutes = Minutes;
    public TimeDuration(int Hours, int Minutes, double Seconds) : this(Minutes, Seconds) => this.Hours = Hours;


    public override object ProvideValue(IServiceProvider sp)
    {
        var hours   = _Hours;
        var minutes = _Minutes;
        var seconds = _Seconds;

        if (seconds >= 60)
        {
            var s = seconds % 60;
            minutes += (int)(seconds - s) / 60;
            seconds =  s;
        }

        if (minutes >= 60)
        {
            var m = minutes % 60;
            hours   += (minutes - m) / 60;
            minutes =  m;
        }

        return new Duration(new TimeSpan(hours, minutes, 0) + TimeSpan.FromSeconds(seconds));
    }
}
