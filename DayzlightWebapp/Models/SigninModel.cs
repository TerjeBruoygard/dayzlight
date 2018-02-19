using DayzlightCommon.Entities;
using System.ComponentModel.DataAnnotations;

namespace DayzlightWebapp.Models
{
    public class SigninModel : AdminEntity
    {
        public bool RememberMe { get; set; }
    }
}