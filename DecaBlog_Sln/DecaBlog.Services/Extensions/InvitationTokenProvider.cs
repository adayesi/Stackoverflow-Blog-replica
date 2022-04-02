using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecaBlog.Services.Extensions
{
    public class InvitationTokenProvider<User> : DataProtectorTokenProvider<User> where User : class
    {
        public InvitationTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<InvitationTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<User>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }

    public class InvitationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
