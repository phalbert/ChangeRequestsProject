using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeRequestSubSystem.Entities
{
    [ActiveRecord("RiskAnalsisResults")]
    public class RiskAnalysis:DbEntity<RiskAnalysis>
    {
        [PrimaryKey(PrimaryKeyType.Identity, "RecordId")]
        public int Id { get; set; }


        //Bussiness Impact and Communication
        [Property]
        public bool ChangeAffectsMultipleServices { get; set; } //Yes (2 or more) or No (1 service)

        [Property]
        public bool Are3rdPartyClientsAffected { get; set; } //e.g Wave, Terrapay Stanbic

        [Property]
        public bool Will3rdPartiesBeInformedInCaseOfDownTime { get; set; } //Yes or No

        [Property]
        public string HowWere3rdPartiesInformed { get; set; } //email or whatsup

        [Property]
        public bool IsChangeDependantOnOtherChange { get; set; } //Yes or No

        [Property]
        public bool AreMultiplePartiesInvolvedInChange { get; set; } //Does it Involve Pegasus And Someone Else

        [Property]
        public bool NumberOfUsersImpactedInCaseOfFailure { get; set; } //<10,<100,<1000, <10000,>10000

        [Property]
        public bool WillChangeBeVisibleToUsers { get; set; } //Yes or No

        //Time and Enviromnent
        [Property]
        public bool IsChangeInMaintainanceWindow { get; set; } //Yes or No

        [Property(Length =50)]
        public string WhenWillChangeHappen { get; set; } //After Hours, High Peak - Bussiness Hours,Medium Peak - Bussiness Hours, Low Peak - Bussiness Hours

        [Property ]
        public bool IsChangeHappeningOnProduction { get; set; } //yes or No

        [Property (Length = 50)]
        public string EnvironmentChangeIsHappening { get; set; } //Test, Live

        [Property]
        public bool WillChangeBeImplementedInPhases { get; set; } //Yes or No

        [Property(Length = 50)]
        public string SeverityOfImpactOfChange { get; set; } //Not Available, Performance Degraded, Fully Available


        //Testing
        [Property]
        public bool WereAnyTestsDone { get; set; } //Yes or No

        [Property(Length = 50)]
        public string NumberOfTestsDone { get; set; } //A few, Many Tests, Tests done on Production

        [Property(Length = 50)]
        public string NumberOfTestsThatPassed { get; set; } //A few, All, None

        [Property]
        public bool AreTherePostChangeTests { get; set; } //Yes or No

        [Property(Length = 50)]
        public string IsTheChangeEasyToValidate{ get; set; } //easily validated, complex to validate,validation by User Only,Validation under Peak Time

        [Property]
        public bool IsThereARollbackPlan { get; set; } //Yes or No

        [Property (Length = 50)]
        public string TimeTakenToDoRollback { get; set; } //<30 minutes, <1 hour, <3 hours, <6 hours, >6 hours

        [Property]
        public bool WillServicesBeUpDuringRollback { get; set; } //Yes or No

        
        //Implementation questions
        [Property]
        public bool HasImplementerEverDoneThisChange { get; set; } //Yes or No

        [Property]
        public bool DoesChangeAffectArchitecturalStyleOrFlow { get; set; } //Yes or No

        [Property]
        public bool DoesChangeHaveImpactOnApplicationSecurity { get; set; } //Yes or No

        [Property]
        public bool DoeschangeAffectDR { get; set; } //Yes or No

        [Property]
        public bool DoesChangeRequireSpecialAccess { get; set; } //Yes or No

        [Property]
        public int TotalRiskScore { get; set; }

        [Property]
        public bool IsRecommended { get; set; }

        [Property(Length = 500)]
        public string Reason { get; set; }

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
