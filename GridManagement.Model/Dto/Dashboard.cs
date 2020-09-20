namespace GridManagement.Model.Dto
{
    public class DashboardSummary
    {
        public string TotalGrid {get; set;}
        public string CompletedGrid {get;set;}
        public string InProgresssGrid {get;set;}
        public string NewGrid{get;set;}
        public string TotalLayer{get;set;}
        public string CompletedLayer{get;set;}
        public string InProgressLayer{get;set;}
        public string BilledLayer{get;set;}
        public string UnBilledLayer{get;set;}
        public string NewLayer{get;set;}
        
    }

        public class LayerMonthWiseDashboard{
        public string[] Date{get;set;}
        public int[] Completed {get;set;}
        public int[]  Billed {get;set;}
    }
}