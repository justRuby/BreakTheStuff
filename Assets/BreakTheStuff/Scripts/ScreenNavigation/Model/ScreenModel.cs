using BreakTheStuff.Enums;

namespace BreakTheStuff.Models
{
    public class BTSScreen
    {
        public string Name { get; }
        public ScreenType Type { get; }

        public BTSScreen(string name, ScreenType screenType = ScreenType.Single)
        {
            this.Name = name;
            this.Type = screenType;
        }
    }
}

