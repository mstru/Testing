﻿namespace Testing.Automation.API.Common
{
    /// <summary>
    /// Represents void action result in generic test builder.
    /// </summary>
    public class VoidActionResult
    {
        /// <summary>
        /// Creates new instance of <see cref="VoidActionResult"/>.
        /// </summary>
        /// <returns>Void action result.</returns>
        public static VoidActionResult Create()
        {
            return new VoidActionResult();
        }
    }
}
