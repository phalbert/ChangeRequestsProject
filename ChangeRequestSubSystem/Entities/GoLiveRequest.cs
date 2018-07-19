﻿using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("GoLiveRequest")]
    public class GoLiveRequest: DbEntity<GoLiveRequest>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }

        [Property(Length = 50)]
        public string RequesterUserId { get; set; }
        
        [Property(Length = 50, Unique = true)]
        public string ChangeRequestId { get; set; }

        [Property(Length = 50)]
        public string ApproverUserId { get; set; }

        [Property(Length = 50)]
        public string Status { get; set; }

        [Property(Length = 50)]
        public string ApproverDecision { get; set; }

        [Property(Length = 50)]
        public DateTime GoLiveStartDateTime { get; set; }

        public override bool IsValid()
        {
            string propertiesThatCanBeNull = $"{nameof(Id)}";
            string nullCheckResult = SharedCommons.SharedCommons.CheckForNulls(this, propertiesThatCanBeNull);
            if (nullCheckResult != Globals.SUCCESS_STATUS_TEXT)
            {
                StatusCode = Globals.FAILURE_STATUS_CODE;
                StatusDesc = nullCheckResult;
                return false;
            }

            return base.IsValid();
        }
    }
}