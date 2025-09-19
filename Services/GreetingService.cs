namespace ProductCatalog.Services
{
    public class GreetingService : IGreetingService
    {
        public string GetMessage()
        {
            return "Привет! Это сообщение из GreetingService.";
        }
    }
}
