using BlogCart.Shared.MainLayouts;

namespace BlogCart.Service
{
    public class DomainLayoutService
    {
        public Type GetLayoutForDomain(string domain)
        {
            if (domain == "localhost:7099")
            {
                return typeof(LightningBitsLayout);
            }
            else if (domain == "example1.com")
            {
                return typeof(BlueLemonZLayout);
            }
            else if (domain == "example2.com")
            {
                return typeof(TheHealerIsYouLayout);
            }
            else
            {
                return typeof(MainLayout);
            }
        }

    }

}

