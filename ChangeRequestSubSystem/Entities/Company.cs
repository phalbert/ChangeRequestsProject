using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("Companies")]
    public class Company : DbEntity<Company>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string CompanyName { get; set; }

        [Property(Length = 50, Unique = true)]
        public string CompanyCode { get; set; }


    }
}
