using System;
using System.Linq;

namespace PatternRefactorings.EncapsulateClassesFactory.Good
{
    public interface IAdvertisement
    {
        string Render();
    }

    public abstract class Advertisement : IAdvertisement
    {
        public string Render()
        {
            return this.ToString();
        }

        private class ImageAdvertisement : Advertisement
        {
        }

        private class TextAdvertisement : Advertisement
        {
        }

        public static IAdvertisement ForImage()
        {
            return new ImageAdvertisement();
        }

        public static IAdvertisement ForText()
        {
            return new TextAdvertisement();
        }
    }


    public class Client
    {
        public void Main()
        {
            // I need an ad for an image
            IAdvertisement imageAd = Advertisement.ForImage();

            // Now I need an ad for text
            IAdvertisement textAd = Advertisement.ForText();

            // Now I'm only coding to the IAdvertisement interface
        }
    }
}
