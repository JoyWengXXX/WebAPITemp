using Microsoft.AspNetCore.Mvc;

namespace SignalRChatTemplete.AppInterfaceAdapters.interfaces
{
    public interface IAPIResponceAdapter
    {
        ObjectResult Fail(object data);
        ObjectResult Ok(object data);
        ObjectResult ServerFail(object data);
    }
}