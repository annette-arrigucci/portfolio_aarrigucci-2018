using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace portfolio_annette_arrigucci.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string PostSlug { get; set; }
        public string AuthorId { get; set; }

        [Required(ErrorMessage = "Body cannot be empty")]
        public string Body { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        public string UpdateReason { get; set; }

        public virtual ApplicationUser Author { get; set; }
        public virtual BlogPost Post { get; set; }
    }
}