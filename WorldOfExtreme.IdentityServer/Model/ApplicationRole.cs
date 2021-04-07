using Microsoft.AspNetCore.Identity;
using System;

namespace WorldOfExtreme.IdentityServer.Model
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
