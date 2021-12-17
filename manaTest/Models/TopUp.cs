using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mana_Test.Models
{
    public class TopUp
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string CheckboxText { get; set; }
        public string ConfirmText { get; set; }
        public string CancelText { get; set; }
        public string Size { get; set; }
        public bool IsDialogResult { get; set; }
        public bool IsWaiting { get; set; }
        public string ViewModelName { get; set; }
        public string MContentServerUrl { get; set; }
        public string EndpostringId { get; set; }
        public string ThemeColor { get; set; }
        public string PageTitle { get; set; }
    }
}
