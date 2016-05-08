﻿using System;
using System.Web;

namespace Microsoft.Owin.Security.Authorization
{
    public class OwinContextAccessor : IOwinContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IOwinContext Context => _httpContextAccessor.Context.GetOwinContext();

        public OwinContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            _httpContextAccessor = httpContextAccessor;
        }
    }
}