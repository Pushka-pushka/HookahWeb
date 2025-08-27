using RclubHook.Domain.Entities;

namespace RclubHook.Areas.Admin.ViewModel;

public class AdminPanelViewModel
{
        public IQueryable<ServiceItem> ServiceItems { get; set; }
        public IQueryable<PromoItem> PromoItems { get; set; }
        public IQueryable<EventItem> EventItems { get; set; }
        
    
}