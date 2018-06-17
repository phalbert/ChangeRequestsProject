using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("Systems")]
    public class PegasusSystem : DbEntity<PegasusSystem>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property]
        public string SystemName { get; set; }

        [Property(Unique = true)]
        public string SystemCode { get; set; }

        [Property(Length = 2000)]
        public string ConnectionString { get; set; }
    }
}
