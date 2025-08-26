using Microsoft.EntityFrameworkCore;
using RclubHook.Domain.Entities;
using RclubHook.Domain.Repositories.Abstract;

namespace RclubHook.Domain.Repositories.Repositories.EF;

public class ServiceItemRepository: IServiceItemRepository
{
    
    private readonly AppDbContext _context;

    public ServiceItemRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<ServiceItem> GetServiceItems()
    {
        return _context.ServiceItems;
    }

    public ServiceItem GetServiceItemById(Guid id)
    {
        return _context.ServiceItems.FirstOrDefault(s => s.Id == id);
    }

    public void SaveServiceItem(ServiceItem entity)
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

    public void DeleteServiceItem(Guid id)
    {
        _context.ServiceItems.Remove(new ServiceItem() { Id = id });
        _context.SaveChanges();
    }
}