using System;
using System.Linq;

namespace PatternRefactorings.EncapsulateClassesFactory.Bad
{
    public interface IAdvertisement
    {
        string Render();
    }

    public class Advertisement : IAdvertisement
    {
        public string Render()
        {
            return this.ToString();
        }
    }

    public class ImageAdvertisement : Advertisement
    {
    }

    public class TextAdvertisement : Advertisement
    {
    }

    public class Client
    {
        public void Main()
        {
            // I need an ad for an image
            var imageAd = new ImageAdvertisement();

            // Now I need an ad for text
            var textAd = new TextAdvertisement();

            // Now I'm tightly coupled to these implementations
        }
    }
}
