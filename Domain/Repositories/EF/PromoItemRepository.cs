using Microsoft.EntityFrameworkCore;
using RclubHook.Domain.Entities;
using RclubHook.Domain.Repositories.Abstract;

namespace RclubHook.Domain.Repositories.Repositories.EF;

public class PromoItemRepository:IPromoItemRepository
{
    private readonly AppDbContext _context;

    public PromoItemRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<PromoItem> GetPromoItems()
    {
        return _context.PromoItems;
    }

    public PromoItem GetPromoItemById(Guid id)
    {
        return _context.PromoItems.FirstOrDefault(s => s.Id == id);
    }

  

    public void SavePromoItem(PromoItem entity)
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

    public void DeletePromoItem(Guid id)
    {
        _context.PromoItems.Remove(new PromoItem() { Id = id });
        _context.SaveChanges();
    }
}