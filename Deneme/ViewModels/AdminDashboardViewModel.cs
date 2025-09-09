using Deneme.Models;

namespace Deneme.ViewModels
{
    public class AdminDashboardViewModel
    {
        public int BlogCount { get; set; }
        public int UnreadMessageCount { get; set; }
        public int UserCount { get; set; }

        public AnaSayfa? LastProject { get; set; }
        public Message? LastMessage { get; set; }

    }
}
