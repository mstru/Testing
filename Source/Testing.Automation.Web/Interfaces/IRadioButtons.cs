﻿using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface IRadioButtons<out TResult> where TResult : IBlock
    {
        IEnumerable<IOption<TResult>> Options { get; }
    }
}