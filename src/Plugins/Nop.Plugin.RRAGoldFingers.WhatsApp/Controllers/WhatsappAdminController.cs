using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.RRAGoldFingers.WhatsApp.Models;
using Nop.Services.Configuration;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.RRAGoldFingers.WhatsApp.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.Admin)]
public class WhatsappAdminController : BasePluginController
{
    protected readonly IPermissionService _permissionService;
    protected readonly ISettingService _settingService;

    public WhatsappAdminController(
        IPermissionService permissionService,
        ISettingService settingService)
    {
        _permissionService = permissionService;
        _settingService = settingService;
    }

    public virtual async Task<IActionResult> Configure()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManagePlugins))
            return AccessDeniedView();

        var settings = await _settingService.LoadSettingAsync<WhatsappSettings>();

        return View("~/Plugins/RRAGoldFingers.WhatsApp/Views/Configure.cshtml", settings);
    }

    [HttpPost]
    public async Task<IActionResult> SaveWhatsappSettings(WhatsappSettings model)
    {
        await _settingService.SaveSettingAsync(model);

        return await Configure();
    }
}
