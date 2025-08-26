using RclubHook.Domain.Entities;

namespace RclubHook.Domain.Repositories.Abstract;

public interface IEventItemRepository
{
    IQueryable<EventItem> GetEventItems();
    EventItem GetEventItemById(Guid id);
    void SaveEventItem(EventItem entity);
    void DeleteEventItem(Guid id);
}