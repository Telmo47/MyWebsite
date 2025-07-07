using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class ProjectTecnologies
    {

        /// <summary>
        /// FK to the Projects table
        /// </summary>
        [Key]
        public int ProjectId { get; set; }

        /// <summary>
        /// FK to the Tecnologies table
        /// </summary>
        [Key]
        public int TecnologyId { get; set; }

        /// <summary>
        /// Navigation property to the Tecnologies table
        /// </summary>
        public Projects Projects { get; set; }

        /// <summary>
        /// Navigation property to the Tecnologies table
        /// </summary>
        public Tecnologies Tecnologies { get; set; }

    }
}
