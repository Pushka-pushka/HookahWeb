using Microsoft.EntityFrameworkCore;
using RclubHook.Domain.Entities;
using RclubHook.Domain.Repositories.Abstract;

namespace RclubHook.Domain.Repositories.Repositories.EF;

public class TextFieldsRepository: ITextFieldsRepository
{
    private readonly AppDbContext _context;

    public TextFieldsRepository(AppDbContext context)
    {
        _context = context;
    }
    public IQueryable<TextField> GetTextFields()
    {
        return _context.TextFields;
    }

    public TextField GetTextFieldById(Guid id)
    {
        return _context.TextFields.FirstOrDefault(x => x.Id == id);
    }

    public TextField GetTextFieldByCodeWord(string codeWord)
    {
        return _context.TextFields.FirstOrDefault(x => x.CodeWord == codeWord);
    }

    public void SaveTextField(TextField entity)
    {
        if (entity.Id == default)
        {
            _context.Entry(entity).State = EntityState.Added;
        }
        else
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        _context.SaveChanges();
    }

    public void DeleteTextField(Guid id)
    {
        _context.TextFields.Remove(new TextField() { Id = id });
        _context.SaveChanges();
    }
}