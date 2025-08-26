using RclubHook.Domain.Entities;

namespace RclubHook.Domain.Repositories.Abstract;

public interface IPromoItemRepository
{
    IQueryable<PromoItem> GetPromoItems();
    PromoItem GetPromoItemById(Guid id);
    void SavePromoItem(PromoItem entity);
    void DeletePromoItem(Guid id);
}