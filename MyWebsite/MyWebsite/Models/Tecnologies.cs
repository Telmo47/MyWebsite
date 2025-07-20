namespace MyWebsite.Models
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;

    public class Tecnologies
    {

        /// <summary>
        /// Id for the technologies
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the technology 
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; } = "";

        /// <summary>
        /// Navigation property to the projects
        /// </summary>
        [ValidateNever]
        public ICollection<ProjectTecnologies> ProjectTecnologies { get; set; } = new List<ProjectTecnologies>();
    }
}
