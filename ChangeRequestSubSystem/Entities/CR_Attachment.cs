using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("Attachments")]
    public class CR_Attachment : DbEntity<CR_Attachment>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string Name { get; set; }

        [Property(Length = 6500)]
        public string Base64StringOfContent { get; set; }

        [Property(Length = 400)]
        public string Hash { get; set; }

    }
}
