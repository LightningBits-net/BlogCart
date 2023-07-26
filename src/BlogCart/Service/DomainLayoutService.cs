using BlogCart.Shared.MainLayouts;


namespace BlogCart.Service
{
    public class DomainLayoutService
    {
        public Type GetLayoutForDomain(string domain)
        {
            if (domain == "lightningbits.net")
            {
                return typeof(LightningBitsLayout);
            }
            else if (domain == "lightningbits.com")
            {
                return typeof(LightningBitsLayout);
            }
            else if (domain == "bluelemonz.com")
            {
                return typeof(BlueLemonZLayout);
            }
            else if (domain == "stefmancia.com")
            {
                return typeof(StefManciaLayout);
            }
            else if (domain == "thehealerisyou.com")
            {
                return typeof(TheHealerIsYouLayout);
            }
            else if (domain == "gluisi.com")
            {
                return typeof(GLuisiLayout);
            }
            else if (domain == "valleymisfit.com")
            {
                return typeof(ValleyMisfitLayout);
            }

            //development change return to set path
            else if (domain == "localhost:7099") 
            {
                return typeof(LightningBitsLayout); //change clientid to match in layout as well when in develpoment @ LayoutPage in shared folder
            }
            else
            {
                return typeof(MainLayout);
            }
        }
    }
}

