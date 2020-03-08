using System.Windows.Forms;

namespace Huj_Cheat.Main.Scripts
{
    class Functions
    {
        public static OpenFileDialog openfiledialog = new OpenFileDialog
        {
            Filter = "Lua Script Txt (*.txt)|*.txt|All files (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true,
            Title = "SLMCheat v1.0"
        };
    }
}
