using Microsoft.AspNetCore.Mvc;
using RclubHook.Domain.Entities;
using RclubHook.Domain.Repositories;
using RclubHook.Services;

namespace RclubHook.Areas.Admin.Controllers;

[Area("Admin")]
public class ServiceItemsController : Controller
{
    private readonly DataManager _dataManager;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ILogger<ServiceItemsController> _logger;

    public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostEnvironment, ILogger<ServiceItemsController> logger)
    {
        _dataManager = dataManager;
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public IActionResult Edit(Guid id)
    {
        var entity = id == default ? new ServiceItem() : _dataManager.ServiceItems.GetServiceItemById(id);
        return View(entity);
    }

    [HttpPost]
    public IActionResult Edit(ServiceItem model, IFormFile? titleImageFile)
    {
        // ✅ ОЧИСТКА ОШИБОК ВАЛИДАЦИИ ДЛЯ ФАЙЛОВ
        ModelState.Remove("titleImageFile");
        ModelState.Remove("TitleImagePath");

        if (ModelState.IsValid)
        {
            try
            {
                // ✅ ОБРАБОТКА ИЗОБРАЖЕНИЯ
                if (titleImageFile != null && titleImageFile.Length > 0)
                {
                    // ✅ ГЕНЕРАЦИЯ УНИКАЛЬНОГО ИМЕНИ ФАЙЛА
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(titleImageFile.FileName)}";
                    model.TitleImagePath = fileName;
                    
                    var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", fileName);
                    
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                    
                    _logger.LogInformation($"Изображение сохранено: {fileName}");
                }
                else if (model.Id == default && string.IsNullOrEmpty(model.TitleImagePath))
                {
                    // ✅ ЗНАЧЕНИЕ ПО УМОЛЧАНИЮ ДЛЯ НОВЫХ ЗАПИСЕЙ
                    model.TitleImagePath = "no-image.jpg";
                }

                // ✅ СОХРАНЕНИЕ В БД
                _dataManager.ServiceItems.SaveServiceItem(model);
                _logger.LogInformation($"ServiceItem сохранен: {model.Title}");

                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении ServiceItem");
                ModelState.AddModelError("", $"Ошибка при сохранении: {ex.Message}");
            }
        }
        else
        {
            // ✅ ЛОГИРОВАНИЕ ОШИБОК ВАЛИДАЦИИ
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                _logger.LogWarning($"Ошибка валидации: {error.ErrorMessage}");
            }
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Delete(Guid id)
    {
        try
        {
            _dataManager.ServiceItems.DeleteServiceItem(id);
            _logger.LogInformation($"ServiceItem удален: {id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении ServiceItem");
        }
        
        return RedirectToAction("Index", "Home", new { area = "Admin" });
    }
}

// using Microsoft.AspNetCore.Mvc;
// using RclubHook.Domain.Entities;
// using RclubHook.Domain.Repositories;
// using RclubHook.Services;
//
// namespace RclubHook.Areas.Admin.Controllers;
//
//
// [Area("Admin")]
// public class ServiceItemsController : Controller
// {
//     private readonly DataManager _dataManager;
//     private readonly IWebHostEnvironment _hostEnvironment;
//
//     public ServiceItemsController(DataManager dataManager, IWebHostEnvironment hostEnvironment)
//     {
//         _dataManager = dataManager;
//         _hostEnvironment = hostEnvironment;
//     }
//
//     public IActionResult Edit(Guid id)
//     {
//         var entity = id == default ? new ServiceItem() : _dataManager.ServiceItems.GetServiceItemById(id);
//         return View(entity);
//     }
//
//     
//     
//
//     [HttpPost]
//     public IActionResult Edit(ServiceItem model, IFormFile titleImageFile)
//     {
//         if (ModelState.IsValid)
//         {
//             if (titleImageFile != null)
//             {
//                 model.TitleImagePath = titleImageFile.FileName;
//                 using (var stream = new FileStream(Path.Combine(_hostEnvironment.WebRootPath,
//                            "images/", titleImageFile.FileName), FileMode.Create))
//                 {
//                     titleImageFile.CopyTo(stream);
//                 }
//             }
//
//             _dataManager.ServiceItems.SaveServiceItem(model);
//             return RedirectToAction("Index", "Home", new { area = "Admin" });
//            
//
//         }
//         
//         return View(model);
//     }
//
//     [HttpPost]
//     public IActionResult Delete(Guid id)
//     {
//         _dataManager.ServiceItems.DeleteServiceItem(id);
//         return RedirectToAction("Index", "Home", new { area = "Admin" });
//        
//     }
// }