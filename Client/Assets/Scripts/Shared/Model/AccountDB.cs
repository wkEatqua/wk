using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Model
{
    public class AccountDB
    {
        // 자동 AI
        public long AccountId { get; set; }
        // 최대 10자까지 가능
        public string Nickname { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateDate { get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        // 최대 50자까지 가능
        public string Comment { get; set; }

    }
}
