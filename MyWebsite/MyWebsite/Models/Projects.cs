namespace MyWebsite.Models 
{ 
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
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Project Title")]
        public string Title { get; set; }

        /// <summary>
        /// Description of the projects
        /// </summary>
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        /// <summary>
        /// Url of the project in the github
        /// </summary>
        [Required(ErrorMessage = "GitHub URL is required")]
        public int UrlGithub { get; set; }

        /// <summary>
        /// Url for the online website
        /// </summary>
        public string UrlSite { get; set; }

        /// <summary>
        /// Path to the image in the project
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Creation Date of the Project
        /// </summary>
        [Required(ErrorMessage = "Creation date is required")]
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Navigation property to the technologies used in the project
        /// </summary>
        public ICollection<ProjectTecnologies> ProjectTecnologies { get; set; }

    }
}
