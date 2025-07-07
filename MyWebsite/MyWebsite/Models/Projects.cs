namespace MyWebsite.Models
{
    public class Projects
    {
        /// <summary>
        /// Id for all the projects
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the projects
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Description of the projects
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Url of the project in the github
        /// </summary>
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
        public DateTime CreationDate { get; set; }



    }
}
