namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class AppChangeEndpointEvent:Event
    {
        public string Signin { get; set; }
        public string Signout { get; set; }
    }
}
