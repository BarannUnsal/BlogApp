namespace FealtedByEsra.BAL.Helpers.Image
{
    public class ImageHelper
    {
        public string ImageResize(SixLabors.ImageSharp.Image img, int maxWidth, int maxHeigth)
        {
            if (img.Width > maxWidth || img.Height > maxHeigth)
            {
                double widthRatio = (double)img.Width / (double)maxWidth;
                double heightRatio = (double)img.Height / (double)maxHeigth;
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)(img.Width / ratio);
                int newHeight = (int)(img.Height / ratio);
                return newHeight.ToString() + "," + newWidth.ToString();
            }
            else
            {
                return img.Height.ToString() + "," + img.Width.ToString();
            }
        }
    }
}
