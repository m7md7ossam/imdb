using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMDB.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Please enter role name.")]
        [RegularExpression(@"^([A-z][A-Za-z]*\s+[A-Za-z]*)|([A-z][A-Za-z]*)$", ErrorMessage = "role name should not contain numbers")]
        [Remote("IsUserRoleNameExists", "Role", HttpMethod = "POST", ErrorMessage = "role name already exists.", AdditionalFields = "Id")]
        public string Name { get; set; }
    }
}