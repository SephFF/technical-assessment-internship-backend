public class Fruit
{
    public string Name { get; }
    public string Color { get; }

    public Fruit(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public override string ToString()
    {
        return $"{Name} ({Color})";
    }
}
