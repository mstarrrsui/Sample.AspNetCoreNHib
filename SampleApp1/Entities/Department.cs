#region

using System;
using FluentNHibernate.Mapping;

#endregion

namespace SampleApp1.Entities
{
    [Serializable]
    public class Department
    {
        private int? _accountingGroupTypeKey;

        #region Private Fields

        public Department()
        {
            _accountingGroupTypeKey = int.MinValue;
        }

        #endregion

        #region Public Properties

        //[KeyProperty]
        public virtual int Id { get; set; }

        public virtual string Description { get; set; }

        public virtual char? UnderwritingFlag { get; set; }

        public virtual char? ClaimsFlag { get; set; }

        public virtual char? ProgramFlag { get; set; }

        public virtual bool? DeductibleBillingFlag { get; set; }

        public virtual int? ProfitCenterKey { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual int? AccountingGroupTypeKey
        {
            get { return _accountingGroupTypeKey; }
            set { _accountingGroupTypeKey = value; }
        }

        public virtual bool? CreateNewSubmission { get; set; }

        public virtual bool SystemManagesFees { get; set; }

        #endregion
    }

    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap()
        {
            Id(x => x.Id).Column("DEPARTMENT_NUMBER").Not.Nullable();
            Map(x => x.Description).Column("DEPARTMENT_DESCRIPTION").Length(60).Nullable();
            Map(x => x.DeductibleBillingFlag).Column("DEDUCTIBLE_BILLING_FLAG").Nullable();
            Map(x => x.ProfitCenterKey).Column("PROFITCENTERKEY").Nullable();
            Map(x => x.IsActive).Not.Nullable();
            Map(x => x.AccountingGroupTypeKey).Nullable();
            Map(x => x.CreateNewSubmission).Column("CreateNewSubmissions").Nullable();
            Map(x => x.SystemManagesFees).Column("SystemManagesFees").Not.Nullable();
            Table("DEPARTMENTS");
        }
    }
}