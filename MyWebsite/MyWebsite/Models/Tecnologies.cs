namespace MyWebsite.Models
{
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
        public string Name { get; set; }

        /// <summary>
        /// Navigation property to the projects
        /// </summary>
        public ICollection<ProjectTecnologies> ProjectTecnologies { get; set; }
    }
}
