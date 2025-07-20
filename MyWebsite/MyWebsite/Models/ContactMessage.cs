using System.ComponentModel.DataAnnotations;

namespace MyWebsite.Models
{
    public class ContactMessage
    {

        /// <summary>
        /// Id for the contact messages
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the person who sent the message
        /// </summary>
        [StringLength(50)]
        public string Name { get; set; } = "";

        /// <summary>
        /// Email of the person who sent the message
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Title of the message sent and what it is about
        /// </summary>
        [StringLength(200)]
        public string? About { get; set; } = "";

        /// <summary>
        /// Body of the message sent
        /// </summary>
        public string Message { get; set; } = "";

        /// <summary>
        /// Date when the message was sent
        /// </summary>
        public DateTime SentDate { get; set; }

    }
}
