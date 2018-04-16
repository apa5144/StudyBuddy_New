namespace StudyBuddy.Core
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlert";

        private AlertStyle _alertStyle;

        public AlertStyle AlertStyle
        {
            get { return _alertStyle; }
            set
            {
                _alertStyle = value;

                switch (AlertStyle)
                {              
          
                    case AlertStyle.Danger:
                        Type = "#f8d7da";
                        Title = "Error";
                        break;
                    case AlertStyle.Information:
                        Type = "#dee9f6";
                        Title = "Information";
                        break;
                    case AlertStyle.Success:
                        Type = "#d3f2ce";
                        Title = "Success";
                        break;
                    case AlertStyle.Warning:
                        Type = "#fdebd1";
                        Title = "Warning";
                        break;
                    default:
                        Type = "#f8d7da";
                        Title = "Error";
                        break;
                }
            }
        }

        public string Message { get; set; }
        public string Type { get; private set; }
        public string Title { get; private set; }
    }
}