using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("PostChangeTests")]
    public class PostChangeTest:DbEntity<PostChangeTest>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 6500)]
        public string TestDesc { get; set; }

        [Property(Length = 50)]
        public string ModifiedBy { get; set; }

        [Property(Length = 50)]
        public string CreatedBy { get; set; }

        [Property(Length = 50)]
        public DateTime ModifiedOn { get; set; }

        [Property(Length = 50)]
        public DateTime CreatedOn { get; set; }
    }
}
