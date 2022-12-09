using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.User
{
    public class UserToken:BaseEntity<int>
    {
        public string HashJwtToken { get; set; }
       

        public DateTime TokenExpireDate { get; set; }
       
        public int UserId { get; set; }

        #region Relations

        [ForeignKey("UserId")]
        public User User { get; set; }

        #endregion
    }
}
