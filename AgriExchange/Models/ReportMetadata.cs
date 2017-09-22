using System;
using System.ComponentModel.DataAnnotations;

namespace AgriExchange.Models
{
    [MetadataType(typeof(ReportMetadata))]
    public partial class Report
    {
    }

    public partial class ReportMetadata
    {
        [Display(Name = "ReportedBlogPost")]
        public BlogPost ReportedBlogPost { get; set; }

        [Display(Name = "ReportedComment")]
        public Comment ReportedComment { get; set; }

        [Display(Name = "ReportingUser")]
        public ApplicationUser ReportingUser { get; set; }

        [Required(ErrorMessage = "Please enter : ID")]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Display(Name = "Reason")]
        public string Reason { get; set; }

    }
}
