using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebAPI.Domain
{
    public class SwordType
    {
        public int Id { get; set; }
        public string Style { get; set; }
        public int SwordId { get; set; }
        public Sword Sword { get; set; }
    }
}
