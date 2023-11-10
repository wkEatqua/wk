using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model
{
    public class AccountSnapshot
    {
        public AccountDB Account { get; }


        public AccountSnapshot(AccountDB account) 
        {
            Account = account;

        }

    }
}
