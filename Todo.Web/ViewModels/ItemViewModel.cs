using System;
using System.ComponentModel.DataAnnotations;

namespace Todo.Web.ViewModels
{
    public class ItemViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Completed { get; set; }
    }
}
