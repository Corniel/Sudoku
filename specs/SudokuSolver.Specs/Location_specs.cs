namespace Location_specs;

public  class Alligns_when
{
    [Test]
    public void Same_column_different_row()
        => Location.New(row: 3, col: 2).Alligns(Location.New(row: 8, col: 2)).Should().BeTrue();

    [Test]
    public void Same_row_different_column()
        => Location.New(row: 3, col: 2).Alligns(Location.New(row: 3, col: 3)).Should().BeTrue();
}

public class Alligns_not_when
{
    [Test]
    public void Same()
        => Location.New(row: 3, col: 2).Alligns(Location.New(row: 3, col: 2)).Should().BeFalse();

    [Test]
    public void Diffent_row_and_column()
        => Location.New(row: 3, col: 2).Alligns(Location.New(row: 0, col: 3)).Should().BeFalse();
}
