using System.ComponentModel.DataAnnotations;

namespace ECMS.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}