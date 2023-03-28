using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace Macro.Services
{
    public static class ImageService
    {
        public static List<Point> Recognize(Mat source, Mat button)
        {
            List<Point> points = new List<Point>();
            Mat templateOutput = new Mat();
            int halfHeight = button.Height / 2;
            int halfWidth = button.Width / 2;

            CvInvoke.MatchTemplate(source, button, templateOutput, TemplateMatchingType.CcoeffNormed);

            CvInvoke.Threshold(templateOutput, templateOutput, 0.85, 1, ThresholdType.ToZero);

            var matches = templateOutput.ToImage<Gray, byte>();

            for (var i = 0; i < matches.Rows; i++)
            {
                for (var j = 0; j < matches.Cols; j++)
                {
                    if (matches[i, j].Intensity > .8)
                    {
                        var loc = new Point(j + halfHeight, i + halfWidth);

                        points.Add(loc);
                    }
                }
            }

            return points.OrderByDescending(x => x.Y).ToList();
        }
    }
}
