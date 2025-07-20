using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class ProjectTecnologies
    {

        /// <summary>
        /// FK to the Projects table
        /// </summary>
        
        public int ProjectId { get; set; }

        /// <summary>
        /// FK to the Tecnologies table
        /// </summary>
        
        public int TecnologyId { get; set; }

        /// <summary>
        /// Navigation property to the Tecnologies table
        /// </summary>
        [ValidateNever]
        public Projects Projects { get; set; } = null!;

        /// <summary>
        /// Navigation property to the Tecnologies table
        /// </summary>
        [ValidateNever]
        public Tecnologies Tecnologies { get; set; } = null!;

    }
}
