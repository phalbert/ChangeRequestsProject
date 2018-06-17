using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("ErrorLogs")]
    public class ApiLog : DbEntity<ApiLog>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int RecordId { get; set; }

        [Property]
        public string EventId { get; set; }

        [Property]
        public string EventType { get; set; }

        [Property]
        public string Message { get; set; }

        [Property]
        public DateTime? RecordDate { get; set; }





        public bool LogInfo(string Id, string type, string message)
        {
            try
            {
                EventId = Id;
                EventType = type;
                Message = message;
                RecordDate = DateTime.Now;
                Save();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


    }
}
