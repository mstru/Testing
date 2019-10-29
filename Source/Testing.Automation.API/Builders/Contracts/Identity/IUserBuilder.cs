﻿namespace Testing.Automation.API.Builders.Contracts.Identity
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// Used for building mocked Controller.User object.
    /// </summary>
    public interface IUserBuilder
    {
        /// <summary>
        /// Used for setting ID to the mocked user object.
        /// </summary>
        /// <param name="identifier">The user Id to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Used for setting username to the mocked user object.
        /// </summary>
        /// <param name="username">The username to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithUsername(string username);

        /// <summary>
        /// Used for setting authentication type to the mocked user object.
        /// </summary>
        /// <param name="authenticationType">The authentication type to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Used for adding claim to the mocked user object.
        /// </summary>
        /// <param name="claim">The user claim to set.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder WithClaim(Claim claim);

        /// <summary>
        /// Used for adding user role to the mocked user object.
        /// </summary>
        /// <param name="role">The user role to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRole(string role);

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Collection of roles to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Used for adding multiple user roles to the mocked user object.
        /// </summary>
        /// <param name="roles">Roles to add.</param>
        /// <returns>The same user builder.</returns>
        IAndUserBuilder InRoles(params string[] roles);
    }
}
