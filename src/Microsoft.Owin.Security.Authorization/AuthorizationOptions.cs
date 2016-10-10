// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Microsoft.Owin.Security.Authorization
{
    /// <summary>
    /// Provides programmatic configuration used by <see cref="IAuthorizationService"/> and <see cref="IAuthorizationPolicyProvider"/>.
    /// </summary>
    public class AuthorizationOptions
    {
        private IDictionary<string, AuthorizationPolicy> PolicyMap { get; } = new Dictionary<string, AuthorizationPolicy>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets the <see cref="IAuthorizationDependencies"/> used to resolve various dependencies for authorization.
        /// </summary>
        /// <remarks>
        /// The default dependencies is an instance of <see cref="AuthorizationDependencies"/>.
        /// </remarks>
        public IAuthorizationDependencies Dependencies { get; set; } = new AuthorizationDependencies();

        /// <summary>
        /// Gets or sets the default authorization policy.
        /// </summary>
        /// <remarks>
        /// The default policy is to require any authenticated user.
        /// </remarks>
        public AuthorizationPolicy DefaultPolicy { get; set; } = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        /// <summary>
        /// Add an authorization policy with the provided name.
        /// </summary>
        /// <param name="name">The name of the policy.</param>
        /// <param name="policy">The authorization policy.</param>
        public void AddPolicy(string name, AuthorizationPolicy policy)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (policy == null)
            {
                throw new ArgumentNullException(nameof(policy));
            }

            PolicyMap[name] = policy;
        }

        /// <summary>
        /// Add a policy that is built from a delegate with the provided name.
        /// </summary>
        /// <param name="name">The name of the policy.</param>
        /// <param name="configurePolicy">The delegate that will be used to build the policy.</param>
        public void AddPolicy(string name, Action<AuthorizationPolicyBuilder> configurePolicy)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (configurePolicy == null)
            {
                throw new ArgumentNullException(nameof(configurePolicy));
            }

            var policyBuilder = new AuthorizationPolicyBuilder();
            configurePolicy(policyBuilder);
            PolicyMap[name] = policyBuilder.Build();
        }

        /// <summary>
        /// Returns the policy for the specified name, or null if a policy with the name does not exist.
        /// </summary>
        /// <param name="name">The name of the policy to return.</param>
        /// <returns>The policy for the specified name, or null if a policy with the name does not exist.</returns>
        public AuthorizationPolicy GetPolicy(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            AuthorizationPolicy policy;
            if (PolicyMap.TryGetValue(name, out policy))
            {
                return policy;
            }

            return null;
        }
    }
}