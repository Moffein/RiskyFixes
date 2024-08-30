using BepInEx.Configuration;
using System;

namespace RiskyFixes.Fixes
{
    //Based off of TILER code
    public abstract class FixBase<T> : FixBase where T : FixBase<T>
    {
        //This, which you will see on all the -base classes, will allow both you and other modders to enter through any class with this to access internal fields/properties/etc as if they were a member inheriting this -Base too from this class.
        public static T Instance { get; private set; }

        public FixBase()
        {
            if (Instance != null) throw new InvalidOperationException("Singleton class \"" + typeof(T).Name + "\" inheriting FixBase was instantiated twice");
            Instance = this as T;
        }
    }

    public abstract class FixBase
    {
        public abstract string ConfigCategoryString { get; }
        public abstract string ConfigOptionName { get; }

        public abstract string ConfigDescriptionString { get;  }

        public ConfigEntry<bool> Enabled { get; private set; }

        public virtual bool StopLoadOnConfigDisable => true; //Intended for RiskOfOptions compat if it becomes a thing

        protected virtual void ReadConfig(ConfigFile config)
        {
            Enabled = config.Bind<bool>(ConfigCategoryString, ConfigOptionName, true, ConfigDescriptionString);
        }

        internal void Init(ConfigFile config)
        {
            ReadConfig(config);
            if (StopLoadOnConfigDisable && !Enabled.Value) return;
            ApplyChanges();
        }

        protected virtual void ApplyChanges()
        {
            
        }
    }
}
