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
        public int TecnologiesId { get; set; }
    }
}
