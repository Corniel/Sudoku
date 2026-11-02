using Qowaiv.Text;

namespace Sudoku.App;

internal static class Style
{
    public static readonly Ansi H1 = Ansi.White | Ansi.BG.Rgb(200, 3, 2);
    public static readonly Ansi H1_Pipe = Ansi.Rgb(200, 3, 2) | Ansi.BG.Rgb(200, 3, 2);
    public static readonly Ansi Gray = Ansi.Gray(127);
    public static readonly Ansi Yellow = Ansi.Bright.Yellow;
    public static readonly Ansi Url = Gray | Ansi.Underline;
    public static readonly Ansi UrlEnd = Ansi.Default | Ansi.Not.Underline;
    public static readonly Ansi Error = Ansi.Rgb(200, 3, 2);

}
