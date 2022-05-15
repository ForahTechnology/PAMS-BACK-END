using System;
using System.Collections.Generic;
using System.Text;

namespace PAMS.DTOs.Response
{
    public class DashboardVm
    {
        public int NumberOfClient { get; set; }

        public int NumberOfLocation { get; set; }

        public int NumberOfAnalyst { get; set; }

        public AnalysisDaysCount FieldAnalysis { get; set; } = new AnalysisDaysCount();

        public AnalysisDaysCount LabAnalysis { get; set; } = new AnalysisDaysCount();
    }

    public class AnalysisDaysCount
    {
        public int GToday { get; set; }

        public int FYesterDay { get; set; }

        public int ETwoDaysBack { get; set; }

        public int DThreeDaysBack { get; set; }

        public int CFourDaysBack { get; set; }

        public int BFiveDaysBack { get; set; }

        public int ASixDaysBack { get; set; }
    }
}
