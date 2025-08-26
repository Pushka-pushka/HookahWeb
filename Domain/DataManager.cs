using RclubHook.Domain.Repositories.Abstract;

namespace RclubHook.Domain.Repositories;

public class DataManager
{
    public ITextFieldsRepository TextFields { get; set; }
    public IEventItemRepository EventItems { get; set; }
    public IPromoItemRepository PromoItems { get; set; }
    public IServiceItemRepository ServiceItems { get; set; }

    public DataManager(ITextFieldsRepository textFieldsRepository,
                        IEventItemRepository eventItemRepository,
                        IPromoItemRepository promoItemRepository,
                        IServiceItemRepository serviceItemRepository)
    {
        TextFields = textFieldsRepository;
        EventItems = eventItemRepository;
        PromoItems = promoItemRepository;
        ServiceItems = serviceItemRepository;

    }
}