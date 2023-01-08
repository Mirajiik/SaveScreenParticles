using System.Windows.Markup;

namespace SaveScreenParticles;

public class RND : MarkupExtension
{
    private Random? _RND;

    public double Min { get; set; }

    public double Max { get; set; }

    private int _Seed;

    public int Seed
    {
        get => _Seed;
        set
        {
            _Seed = value;
            _RND  = new(value);
        }
    }

    public RND() { }
    public RND(double Max) => this.Max = Max;
    public RND(double Min, double Max) : this(Max) => this.Min = Min;
    public RND(double Min, double Max, int Seed) : this(Min, Max) => this.Seed = Seed;

    public override object ProvideValue(IServiceProvider sp) => (_RND ?? Random.Shared).NextDouble() * (Max - Min) + Min;
}
