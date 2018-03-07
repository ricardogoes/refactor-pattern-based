using System;
using System.Linq;

namespace PatternRefactorings.ReplaceTypeCodeWithState.Good
{
    public class LightSwitch
    {
        private LightSwitchState _state;

        public bool IsOn
        {
            get
            {
                return _state.IsOn;
            }
        }

        public void TurnOn()
        {
            _state.TurnOn(this);
        }

        public void TurnOff()
        {
            _state.TurnOff(this);
        }

        public abstract class LightSwitchState
        {
            public static LightSwitchState On = new LightSwitchOnState();
            public static LightSwitchState Off = new LightSwitchOffState();

            public abstract bool IsOn { get; }
            public virtual void TurnOn(LightSwitch lightSwitch) { }
            public virtual void TurnOff(LightSwitch lightSwitch) { }
        }
        public class LightSwitchOnState : LightSwitchState
        {
            public override bool IsOn { get { return true; } }

            public override void TurnOff(LightSwitch lightSwitch)
            {
                lightSwitch._state = LightSwitchState.Off;
            }
        }

        public class LightSwitchOffState : LightSwitchState
        {
            public override bool IsOn { get { return false; } }

            public override void TurnOn(LightSwitch lightSwitch)
            {
                lightSwitch._state = LightSwitchState.On;
            }
        }

    }
}
