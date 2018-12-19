namespace SAE.ShoppingMall.Identity.Domain.Event
{
    public class ChangeAppEndpointEvent:Event
    {
        public string Signin { get; set; }
        public string Signout { get; set; }
    }
}
