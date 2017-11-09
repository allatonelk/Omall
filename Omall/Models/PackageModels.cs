using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PackageVM
    {
        [Display(Name ="Items")]
        public string PackageItemName { get; set; }
        public string PkgItemId { get; set; }
        [Display(Name = "Type")]
        public string PackageTypeName { get; set; }
        [Display(Name = "Value")]
        public string PkgDefValue { get; set; }

        [Display(Name = "Price")]
        public decimal? PackageDefPrice { get; set; }
    }
}
