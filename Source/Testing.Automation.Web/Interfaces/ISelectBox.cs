﻿using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface ISelectBox : IElement
    {
        IEnumerable<IOption> GetOptions();
    }

    public interface ISelectBox<out TResult>
        where TResult : IBlock
    {
        IEnumerable<IOption<TResult>> Options { get; }
    }
}