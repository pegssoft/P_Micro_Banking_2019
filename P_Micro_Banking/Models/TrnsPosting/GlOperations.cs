using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P_Micro_Banking.Models.TrnsPosting
{
    public class GlOperations
    {
        public string DrawerCode { get; set; }
        public string AccountName { get; set; }
        public string CurrentDate { get; set; }
        public string Balance { get; set; }
        public string Status { get; set; }

    }

    public class GLTransPosting
    {
        [Required]
        public string Teller { get; set; }
        [Required]
        public string ToAccount { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string Naration { get; set; }
       
    }

}
