using System;
using System.Collections.Generic;
using Testing.Automation.Web.Interfaces;

namespace Testing.Automation.Web.Component
{
    public abstract class Monkey : IMonkey
    {
        private double _probability;

        protected double Probability
        {
            get { return _probability; }
            set
            {
                if (value > 1 || value < 0)
                    throw new FormatException("Probability must be between 0 and 1");
                _probability = value;
            }
        }

        protected IBlock Block { get; set; }

        public IList<string> Logs { get; protected set; }

        public abstract void Invoke(IBlock block);

        public abstract void VerifyState();

        public virtual void SetProbability(double probability)
        {
        }

        public abstract void PerformRandomAction();
    }
}