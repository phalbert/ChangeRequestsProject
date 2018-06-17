using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("SystemSettings")]
    public class SystemSetting : DbEntity<SystemSetting>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Unique = true)]
        public string SettingKey { get; set; }

        [Property(Length = 6500)]
        public string SettingValue { get; set; }
    }
}
