using System;
using Testing.Automation.Web.Interfaces;

namespace Testing.Automation.Web.Extensions
{
    public static class BlockConvenience
    {
        public static TScope ScopeTo<TScope>(this IBlock block)
            where TScope : IBlock
        {
            return block.Session.CurrentBlock<TScope>();
        }
    }
}