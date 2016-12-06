using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class TimeRule
    {
        public TimeRule(int? id, DateTime fromDate, DateTime toDate, TimeSpan fromHour, TimeSpan toHour, string week, bool status)
        {
            this.TimeBased = new ObservableCollection<TimeBased>();

            ID = id ?? 0;
            FromDate = fromDate;
            ToDate = toDate;
            FromHour = fromHour;
            ToHour = toHour;
            Week = week;
            Status = status;
        }
        public TimeRule(TimeRule value)
        {
            ID = value.ID;
            FromDate = value.FromDate;
            ToDate = value.ToDate;
            FromHour = value.FromHour;
            ToHour = value.ToHour;
            Week = value.Week;
            Status = value.Status;
            TimeBased = new ObservableCollection<TimeBased>(value.TimeBased.Select(o => new TimeBased(o)));
        }
    }
}
