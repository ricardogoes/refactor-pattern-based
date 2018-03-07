using System;
using System.Linq;

namespace PatternRefactorings.ReplaceTypeCodeWithState.Bad
{
    public class LightSwitch
    {
        public bool IsOn { get; private set; }

        public void TurnOn()
        {
            IsOn = true;
        }

        public void TurnOff()
        {
            IsOn = false;
        }
    }
}
