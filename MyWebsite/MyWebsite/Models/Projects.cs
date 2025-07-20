namespace MyWebsite.Models 
{
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using System.ComponentModel.DataAnnotations;



    public class Projects
    {
        /// <summary>
        /// Id for all the projects
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Title of the projects
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Project Title")]
        public string Title { get; set; } = ""; // <=> string.Empty;

        /// <summary>
        /// Description of the projects
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; } = ""; // <=> string.Empty;

        /// <summary>
        /// Url of the project in the github
        /// </summary>
        [Required(ErrorMessage = "GitHub URL is required")]
        public string UrlGithub { get; set; } = ""; // <=> string.Empty;

        /// <summary>
        /// Url for the online website
        /// </summary>
        public string UrlSite { get; set; } = ""; // <=> string.Empty;

        /// <summary>
        /// Path to the image in the project
        /// </summary>
        public string? ImageUrl { get; set; } = ""; // <=> string.Empty;

        /// <summary>
        /// Creation Date of the Project
        /// </summary>
        [Required(ErrorMessage = "Creation date is required")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Navigation property to the technologies used in the project
        /// </summary>
        [ValidateNever]
        public ICollection<ProjectTecnologies> ProjectTecnologies { get; set; } = new List<ProjectTecnologies>();

    }
}
