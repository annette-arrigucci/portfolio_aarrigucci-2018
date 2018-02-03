using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace portfolio_annette_arrigucci.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        [Required(ErrorMessage = "A title is required")]
        [StringLength(160)]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Preview { get; set; }
        [Required(ErrorMessage = "Body cannot be empty")]
        [AllowHtml]
        public string Body { get; set; }
        public string MediaURL { get; set; }
        [Display (Name ="Publish to Web")]
        public bool Published { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}