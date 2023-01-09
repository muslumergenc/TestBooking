using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanProject.Domain
{
    public class Token
    {
        [Key]
        public int tokenId { get; set; }
        public string token { get; set; }
        public DateTime expires { get; set; }
    }
}
