using RclubHook.Domain.Entities;

namespace RclubHook.Domain.Repositories.Abstract;

public interface IServiceItemRepository
{
    IQueryable<ServiceItem> GetServiceItems();
    ServiceItem GetServiceItemById(Guid id);
    void SaveServiceItem(ServiceItem entity);
    void DeleteServiceItem(Guid id);
}