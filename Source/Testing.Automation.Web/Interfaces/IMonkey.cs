using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface IMonkey
    {
        IList<string> Logs { get; }

        void Invoke(IBlock block);

        void VerifyState();
    }
}