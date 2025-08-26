using Microsoft.EntityFrameworkCore;
using RclubHook.Domain.Entities;
using RclubHook.Domain.Repositories.Abstract;

namespace RclubHook.Domain.Repositories.Repositories.EF;

public class EventItemRepository: IEventItemRepository
{
    
    private readonly AppDbContext _context;

    public EventItemRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<EventItem> GetEventItems()
    {
        return _context.EventItems;
    }

    public EventItem GetEventItemById(Guid id)
    {
        return _context.EventItems.FirstOrDefault(s => s.Id == id);
    }

    public void SaveEventItem(EventItem entity)
    {
        if (entity.Id == default)
        {
            _context.Entry(entity).State = EntityState.Added;
        }
        else
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }

    public void DeleteEventItem(Guid id)
    {
        _context.EventItems.Remove(new EventItem() { Id = id });
        _context.SaveChanges();
    }
}